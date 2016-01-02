using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;  

namespace BE
{
    [DataContract]
    public class Renting
    {
        private int idNumber;
        private DateTime rentalStartDate;
        private DateTime rentalEndDate;         
        private Drivers driversAllowed;
        private int carId;
        private int driversNumber;
        private int kilometersAtRentalStart;
        private int kilometersAtRentalEnd = 0;
        private bool returnedWithFault = false;
        private double rentalPriceDaily; 
        public static int autoIncrement = 0;

        [DataMember]
        public int IdNumber
        {
            get { return idNumber; }
            set { idNumber = value; }
        }
        [DataMember]        
        public DateTime RentalStartDate
        {
            get { return rentalStartDate; }
            set { rentalStartDate = value; }
        }
        [DataMember]
        public DateTime RentalEndDate
        {
            get { return rentalEndDate; }
            set { rentalEndDate = value; }
        }
        [DataMember]
        public Drivers DriversAllowed
        {
            get { return driversAllowed; }
            set { driversAllowed = value; }
        }
        [DataMember]
        public int CarId
        {
            get { return carId; }
            set { carId = value; }
        }
        [DataMember]
        public int DriversNumber
        {
            get { return driversNumber; }
            set { driversNumber = value; }
        }
        [DataMember]
        public double RentalPriceDaily
        {
            get { return Math.Round(rentalPriceDaily, 0); }
            set { rentalPriceDaily = value; }
        }
        [DataMember]
        public int KilometersAtRentalStart
        {
            get { return kilometersAtRentalStart; }
            set { kilometersAtRentalStart = value; }
        }
        [DataMember]
        public int KilometersAtRentalEnd
        {
            get { return kilometersAtRentalEnd; }
            set { kilometersAtRentalEnd = value; }
        }
        [DataMember]
        public bool ReturnedWithFault
        {
            get { return returnedWithFault; }
            set { returnedWithFault = value; }
        }

        /// <summary>
        /// Constructor        
        /// </summary>
        public Renting(DateTime s, int c, int ks, int d1, int d2 = 0, int id=0, DateTime? s2 = null, int ke = 0, bool r = false, double p = 0.0)
        {
            idNumber = (id == 0) ? ++autoIncrement : id;
            rentalStartDate = s;
            carId = c;
            driversNumber = (d2 == 0) ? 1 : 2;
            driversAllowed = new Drivers() { idClient1 = d1, idClient2 = d2 };
            kilometersAtRentalStart = ks; // ks must be retrieved from kilometers of the car

            //optionals
            rentalEndDate = (s2 == null) ? DateTime.MaxValue : (DateTime)s2;
            KilometersAtRentalEnd = ke;
            ReturnedWithFault = r;
            RentalPriceDaily = p;
        }

        public Renting()
        {
            // TODO: Complete member initialization
        }


        public override string ToString()
        {
            bool rentEnded = (rentalPriceDaily != 0);
            string str = "";
            str += "Renting ID: " + idNumber + "\n";
            str += "Rental Start: " + rentalStartDate.ToShortDateString() + "\n";
            if (rentEnded) str += "Rental End: " + rentalEndDate.ToShortDateString() + "\n";
            str += "Drivers allowed: Client " + DriversAllowed.idClient1 + (driversNumber == 2 ? ", " +  "Client " + DriversAllowed.idClient2 : "") + "\n";
            str += "Reserved Car ID: " + carId + "\n";
            str += "Drivers number: " + driversNumber + "\n";
            str += "Km at rental start: " + KilometersAtRentalStart + "km" + "\n";
            if (rentEnded) str += "Km at rental end: " + kilometersAtRentalEnd + "km" + "\n";
            if (rentEnded) str += "Returned with fault: " + returnedWithFault + "\n";
            if (rentEnded) str += "Rental Price: " + rentalPriceDaily + "\n";
            return str;
        }

        /// <summary>
        /// returns an array of the the drivers' client ID
        /// </summary>
        /// <returns></returns>
        public int[] DriversId()
        {
            int[] DriversId = new int[2] { DriversAllowed.idClient1, DriversAllowed.idClient2 };
            return DriversId;
        }
    }  
    
    [DataContract]
    public struct Drivers 
    {
        [DataMember]
        public int idClient1;

        [DataMember]
        public int idClient2;

        public Drivers (int n1, int n2)
        {
            idClient1 = n1;
            idClient2 = n2;
        }

        public override string ToString()
        {
            string str = "";
            str += "Client #" + idClient1;
            str += (idClient2 == 0)? "" : " and client #" + idClient2;
            return str;
        }
    }
}
