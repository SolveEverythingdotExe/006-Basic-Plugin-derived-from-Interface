using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PluginStructure
{
    //make it public
    public interface PluginInterface
    {
        //change style draft
        void ChangeStyle(Form PluginHost);
    }
    //lets build and its done
}
