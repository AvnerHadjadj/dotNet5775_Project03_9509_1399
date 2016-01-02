using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;


namespace BL
{
    public class BLFactory
    {
        public static BL_imp bl = new BL_imp();
        public static BL_imp getBL() { return bl; }
    }
}
