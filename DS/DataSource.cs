using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    /// <summary>
    /// Data : Lists of cars, clients, faults, rentings...
    /// </summary>
    public sealed class DataSource 
    {
        // Singleton use
        private static readonly DataSource instance = new DataSource();
        
        // Unique DataSource to use
        public static DataSource Instance
        {
            get
            {
                return instance;
            }
        }
        
        // Constructors
        static DataSource() { }
        private DataSource() { }

        // Properties
        public static List<Client> clientList = new List<Client>();

        public static List<Car> carList = new List<Car>();

        public static List<Renting> rentingList = new List<Renting>();

        public static List<Fault> faultList = new List<Fault>();

        public static List<Car_Fault> car_faultList = new List<Car_Fault>();
    }
}
