using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;  

namespace BE
{
    [DataContract]
    public class Fault
    {
        private int idNumber;
        private string description;
        private bool responsible;
        private double repairCost;
        private string preferredGarage;
        public static int autoIncrement = 1;

        [DataMember]
        public int IdNumber
        {
            get { return idNumber; }
            set { idNumber = value; }
        }
        [DataMember]
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        [DataMember]
        public bool Responsible
        {
            get { return responsible; }
            set { responsible = value; }
        }
        [DataMember]
        public double RepairCost
        {
            get { return repairCost; }
            set { repairCost = value; }
        }
        [DataMember]
        public string PreferredGarage
        {
            get { return preferredGarage; }
            set { preferredGarage = value; }
        }


        public Fault(string d, bool r, int rc, string pg, int id=0)
        {
            idNumber = (id==0) ? autoIncrement++ : id;
            description = d;
            responsible = r;
            repairCost = rc;
            preferredGarage = pg;
        }

        public Fault()
        {
            // TODO: Complete member initialization
        }
        
        public override string ToString()
        {
            string str = "";
            str += "Fault ID: " + idNumber + "\n";
            str += "Description: " + description + "\n";
            str += "Responsible: " + (responsible ? "Yes" : "No") + "\n";
            str += "Repair cost: " + RepairCost + "\n";
            str += "Preferred Garage: " + PreferredGarage + "\n";
            return str;
        }
    }
}
