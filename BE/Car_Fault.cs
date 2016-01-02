using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;  

namespace BE
{
    [DataContract]
    public class Car_Fault
    {
        private int idNumber;
        private int carId { get; set; }
        private int faultId;
        private DateTime faultDate { get; set; } 
        public static int autoIncrement = 0;

        [DataMember]
        public int CarId
        {
            get { return carId; }
            set { carId = value; }
        }
        [DataMember]
        public int FaultId
        {
            get { return faultId; }
            set { faultId = value; }
        }
        [DataMember]
        public DateTime FaultDate
        {
            get { return faultDate; }
            set { faultDate = value; }
        }
        [DataMember]
        public int IdNumber
        {
            get { return idNumber; }
            set { idNumber = value; }
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="c">carId</param>
        /// <param name="f">faultId</param>
        /// <param name="d">faultDate</param>
        /// <param name="id">ID of the car_fault (optional)</param>
        public Car_Fault(int c, int f, DateTime d, int id=0)
        {
            idNumber = (id==0) ? ++autoIncrement : id;
            carId = c;
            faultId = f;
            faultDate = d;
        }

        public Car_Fault()
        {
            // TODO: Complete member initialization
        }
    }
}
