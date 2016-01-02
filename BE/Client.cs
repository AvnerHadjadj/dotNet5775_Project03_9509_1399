using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;  

namespace BE
{
    [DataContract]
    public class Client
    {
        private int idNumber;
        private int idTeoudaZeout;
        private string address;
        private string name;
        private DateTime birthDate;
        private DateTime licenseDrivingDate;
        private string creditCartNumber;
        private int faultResponsible = 0;
        public static int autoIncrement = 0;

        [DataMember]
        public int IdNumber
        {
            get { return idNumber; }
            set { idNumber = value; }
        }
        [DataMember]
        public int IdTeoudaZeout
        {
            get { return idTeoudaZeout; }
            set { idTeoudaZeout = value; }
        }
        [DataMember]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [DataMember]
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        [DataMember]
        public string CreditCartNumber
        {
            get { return creditCartNumber; }
            set { creditCartNumber = value; }
        }
        [DataMember]
        public DateTime LicenseDrivingDate
        {
            get { return licenseDrivingDate; }
            set { licenseDrivingDate = value; }
        }
        [DataMember]
        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }
        [DataMember]
        public int FaultResponsible
        {
            get { return faultResponsible; }
            set { faultResponsible = value; }
        }

        public Client(int tz, string a, string n, DateTime b, DateTime l, string cc, int id = 0, int f=0)
        {
            idNumber = (id==0) ? ++autoIncrement : id;
            idTeoudaZeout = tz;
            address = a;
            name = n;
            birthDate = b;
            licenseDrivingDate = l;
            creditCartNumber = cc;

            //optional
            FaultResponsible = f;
        }

        public Client()
        {
            // TODO: Complete member initialization

        }

        public override string ToString()
        {
            string str = "";
            str += "Client ID: " + idNumber + "\n";
            str += "Teoudat Zeout: " + idTeoudaZeout + "\n";
            str += "Address: " + Address + "\n";
            str += "Name: " + Name + "\n";
            str += "Birthdate: " + birthDate.ToShortDateString() + "\n";
            str += "License Obtained on: " + licenseDrivingDate.ToShortDateString() + "\n";
            str += "Credit card: **********" + creditCartNumber.Substring(creditCartNumber.Length - 4, 4) + "\n";
            str += "Responsible for: " + faultResponsible + " car faults"+ "\n";
            return str;
        }       
    }
}
