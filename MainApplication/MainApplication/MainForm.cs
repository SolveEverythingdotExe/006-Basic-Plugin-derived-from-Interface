using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using PluginStructure;

namespace MainApplication
{
    public partial class MainForm : Form
    {
        PluginInterface plugin;

        public MainForm()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            //open the form based on menu strip's text
            if (e.ClickedItem.Text != "Plugin")
            {
                String formName = e.ClickedItem.Text + "Form";

                //check if already opened
                bool Opened = Application.OpenForms[formName] != null;

                //open a new instance if not opened
                if (!Opened)
                {
                    Form form = Activator.CreateInstance(Type.GetType("MainApplication." + formName)) as Form;

                    form.MdiParent = this;

                    if (plugin != null)
                        plugin.ChangeStyle(form);

                    form.Show();
                    form.Location = new Point(0, 0);
                }
            }
        }

        //load the dll
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = String.Empty;
            openFileDialog1.Filter = "DLL files (*.dll)|";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //load the dll
                Assembly pluginFile = Assembly.LoadFile(openFileDialog1.FileName);

                //filter out the dll
                foreach (Type type in pluginFile.GetExportedTypes())
                { 
                    //make sure that the right plugin will be loaded
                    if (type.IsClass && type.GetInterface("PluginInterface") != null)
                    {
                        //create an instance of the plugin
                        plugin = Activator.CreateInstance(type) as PluginInterface;

                        //apply the style on our host
                        plugin.ChangeStyle(this);

                        //lastly apply also the style on the opened forms
                        foreach (Form form in Application.OpenForms)
                            plugin.ChangeStyle(form);
                    }
                    else
                        MessageBox.Show("Wrong plugin!");
                }
            }
            //lets apply the style on all form
        }

        //lets reset the forms if plugin where remove
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            plugin = null;
            this.BackColor = SystemColors.Control;
            this.Font = new Font("MS Sans Serif", 8, FontStyle.Regular);

            //loop on all forms and reset the style
            foreach (Form form in Application.OpenForms)
            {
                form.BackColor = SystemColors.Control;
                form.Font = new Font("MS Sans Serif", 8, FontStyle.Regular);
            }

            //we forgot to loop on all controls
            foreach(Control control in this.Controls)
                control.Font = new Font("MS Sans Serif", 8, FontStyle.Regular);
        }

        //lets build and test
    }
}
