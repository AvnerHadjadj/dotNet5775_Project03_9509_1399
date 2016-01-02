using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALFactory
    {
        public static DAL_XML_imp dal = new DAL_XML_imp();
        public static DAL_XML_imp getDAL() { return dal; }
    }
}
