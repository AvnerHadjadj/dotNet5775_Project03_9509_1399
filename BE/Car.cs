using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;  

namespace BE
{
    [DataContract]
    public class Car
    {
        private int idNumber;
        private string registrationNumber;
        private DateTime manufactureDate;
        private CarType name;
        private Transmission transmissionType;
        private int passengersNumber;
        private int doorsNumber;
        private int kilometers;
        private string branchAddress;
        private Category cat;
        public static int autoIncrement = 0;
        
        [DataMember]
        public int IdNumber
        {
            get { return idNumber; }
            set { idNumber = value; }
        }
        [DataMember]
        public DateTime ManufactureDate
        {
            get { return manufactureDate; }
            set { manufactureDate = value; }
        }
        [DataMember]
        public CarType Name
        {
            get { return name; }
            set { name = value; }
        }
        [DataMember]
        public Transmission TransmissionType
        {
            get { return transmissionType; }
            set { transmissionType = value; }
        }
        [DataMember]
        public int PassengersNumber
        {
            get { return passengersNumber; }
            set { passengersNumber = value; }
        }
        [DataMember]
        public int DoorsNumber
        {
            get { return doorsNumber; }
            set { doorsNumber = value; }
        }
        [DataMember]
        public string BranchAddress
        {
            get { return branchAddress; }
            set { branchAddress = value; }
        }
        [DataMember]
        public string RegistrationNumber
        {
            get { return registrationNumber; }
            set { registrationNumber = value; }
        }
        [DataMember]
        public int Kilometers
        {
            get { return kilometers; }
            set { kilometers = value; }
        }
        [DataMember]
        public Category Cat
        {
            get { return cat; }
            set { cat = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Car(string r, DateTime m, CarType n, Transmission t, int p, int d, int k, string b, Category c, int id = 0)
        {
            idNumber = (id == 0) ? ++autoIncrement : id;
            registrationNumber = r;
            manufactureDate = m;
            name = n;
            transmissionType = t;
            passengersNumber = p;
            doorsNumber = d;
            kilometers = k;
            branchAddress = b;
            cat = c;
        }

        public Car()
        {
            // TODO: Complete member initialization
        }

        public override string ToString()
        {
            string str = "";
            str += "Car ID: " + idNumber + "\n";
            str += "Registration: " + registrationNumber + "\n";
            str += "Manufacture date: " + ManufactureDate.ToShortDateString() + "\n";
            str += "Car type: " + Name.mark + " " + Name.model + " " + Name.volumecc + "hp " + Name.color + "\n";
            str += "Transmission: " + TransmissionType + "\n";
            str += "Places: " + PassengersNumber + "\n";
            str += "Doors: " + DoorsNumber + "\n";
            str += "Kilometers: " + Kilometers + "km" + "\n";
            str += "Branch address: " + BranchAddress + "\n";
            str += "Category: " + Cat + "\n";
            return str;
        }
    }

    [DataContract] 
    public enum Transmission 
    {
        [EnumMember]
        Manual,

        [EnumMember]
        Automatic 
    }
    
    [DataContract]   
    public enum Category
    {
        [EnumMember]
        A = 100,

        [EnumMember]
        B = 60,

        [EnumMember]
        C = 30,

        [EnumMember]
        D = 20
    }

    [DataContract]
    public struct CarType
    {
        [DataMember]
        public string mark;

        [DataMember]
        public string model;

        [DataMember]
        public string volumecc;

        [DataMember]
        public string color;

        public CarType(string Mark, string Model, string Volumecc, string Color) 
        {
            mark = Mark;
            model = Model;
            volumecc = Volumecc;
            color = Color;
        }

        public override string ToString()
        {
 	         return this.mark + ' ' + this.model + ' ' + this.color + ' ' + this.volumecc + "hp";
        }
    }
}
