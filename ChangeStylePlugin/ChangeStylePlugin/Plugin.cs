using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PluginStructure;
using System.Windows.Forms;
using System.Drawing;

namespace ChangeStylePlugin
{
    //inherit PluginInterface
    public class Plugin: PluginInterface
    {
        //implement the method of the interface
        public void ChangeStyle(Form PluginHost)
        {
            //change the backcolor of the form
            PluginHost.BackColor = Color.SteelBlue;

            //loop on all controls and change the font
            foreach (Control control in PluginHost.Controls)
                control.Font = new Font("Cambria", 9, FontStyle.Bold);
        }
        //lets build and test
    }
}
