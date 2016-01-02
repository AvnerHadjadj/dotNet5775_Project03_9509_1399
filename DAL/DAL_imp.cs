using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using BE;

namespace DAL
{
    public class DAL_imp : IDAL
    {
        // Insert Methods
        /// <summary>
        /// Returns true if the given client has been successfully added
        /// </summary>
        /// <param name="newClient"></param>
        /// <returns></returns>
        public bool InsertClient(Client newClient) 
        {
            DataSource.clientList.Add(newClient);           
            return true;
        }
        public bool InsertCar(Car newCar)
        {
            DataSource.carList.Add(newCar);
            return true;
        }
        public bool InsertRenting(Renting newRenting)
        {
            DataSource.rentingList.Add(newRenting);
            return true;
        }
        public bool InsertFault(Fault newFault)
        {
            DataSource.faultList.Add(newFault);
            return true;
        }
        public bool InsertCar_Fault(Car_Fault newCar_Fault)
        {
            DataSource.car_faultList.Add(newCar_Fault);
            return true;
        }

        // Delete Methods
        public bool DeleteClient(int id)
        {
            DataSource.clientList.RemoveAt(id - 1);
            return true;
        }
        public bool DeleteCar(int id)
        {
            DataSource.carList.RemoveAt(id - 1); 
            return true;
        }
        public bool DeleteRenting(int id)
        {
            DataSource.rentingList.RemoveAt(id - 1);
            return true;
        }
        public bool DeleteFault(int id)
        {
            DataSource.faultList.RemoveAt(id - 1); 
            return true;
        }
        public bool DeleteCar_Fault(int id)
        {
            DataSource.car_faultList.RemoveAt(id - 1);
            return true;
        }

        // Update Methods
        public bool UpdateClient(Client clientUpdated)
        {
            DataSource.clientList[clientUpdated.IdNumber - 1] = clientUpdated;
            return true;
        }
        public bool UpdateCar(Car carUpdated)
        {
            DataSource.carList[carUpdated.IdNumber - 1] = carUpdated;
            return true;
        }
        public bool UpdateRenting(Renting rentingUpdated)
        {
            DataSource.rentingList[rentingUpdated.IdNumber - 1] = rentingUpdated;
            return true;
        }
        public bool UpdateFault(Fault faultUpdated)
        {
            DataSource.faultList[faultUpdated.IdNumber - 1] = faultUpdated;
            return true;
        }
        public bool UpdateCar_Fault(Car_Fault car_faultUpdated)
        {
            DataSource.car_faultList[car_faultUpdated.IdNumber - 1] = car_faultUpdated;
            return true;
        }

        // SelectAll Methods
        public List<Client> SelectAllClients()
        {
            return DataSource.clientList.OrderBy(c => c.IdNumber).ToList();
        }
        public List<Car> SelectAllCars()
        {
            return DataSource.carList.OrderBy(c => c.IdNumber).ToList();
        }
        public List<Renting> SelectAllRentings()
        {
            return DataSource.rentingList.OrderBy(r => r.IdNumber).ToList();
        }
        public List<Fault> SelectAllFaults()
        {
            return DataSource.faultList.OrderBy(f => f.IdNumber).ToList();
        }
        public List<Car_Fault> SelectAllCar_Faults()
        {
            return DataSource.car_faultList.OrderBy(f => f.IdNumber).ToList();
        }

        // Select specific element Methods matching its ID
        public Client SelectClient(int clientId)
        {
            return (Client)DataSource.clientList.Where(c => c.IdNumber == clientId).FirstOrDefault();
        }
        public Car SelectCar(int carId)
        {
            return (Car)DataSource.carList.Where(r => r.IdNumber == carId).FirstOrDefault();
        }
        public Renting SelectRenting(int rentingId)
        {
            return (Renting)DataSource.rentingList.Where(r => r.IdNumber == rentingId).FirstOrDefault();
        }
        public Fault SelectFault(int faultId)
        {
            return (Fault)DataSource.faultList.Where(f => f.IdNumber == faultId).FirstOrDefault();
        }
        public Car_Fault SelectCar_Fault(int car_faultId)
        {
            return (Car_Fault)DataSource.car_faultList.Where(r => r.IdNumber == car_faultId).FirstOrDefault();
        }

        // Other methods

        public int GetCarKm(int carId)
        {
            Car carSearched = DataSource.carList.Where(c => c.IdNumber == carId).FirstOrDefault();
            return carSearched.Kilometers;
        }
        public int GetNbFaultResponsible(int clientId)
        {
            Client clientSearched = DataSource.clientList.Where(c => c.IdNumber == clientId).FirstOrDefault();
            return clientSearched.FaultResponsible;
        }

        
    }
}