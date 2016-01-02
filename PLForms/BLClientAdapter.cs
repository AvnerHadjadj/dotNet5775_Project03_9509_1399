using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BE;

namespace PLForms
{
    class BLClientAdapter : IBL
    {
        BLServiceReference.BlServiceContractClient bl;

        public BLClientAdapter()
        {
            bl = new BLServiceReference.BlServiceContractClient();
        }

        public bool InsertClient(Client c)
        {
            return bl.InsertClient(c);
        }

        public bool InsertCar(Car c)
        {
            return bl.InsertCar(c);
        }

        public bool InsertRenting(Renting r)
        {
            return bl.InsertRenting(r);
        }

        public bool InsertFault(Fault f)
        {
            return bl.InsertFault(f);
        }

        public bool InsertCar_Fault(Car_Fault cf)
        {
            return bl.InsertCar_Fault(cf);
        }

        public bool DeleteClient(int id)
        {
            return bl.DeleteClient(id);
        }

        public bool DeleteCar(int id)
        {
            return bl.DeleteCar(id);
        }

        public bool DeleteRenting(int id)
        {
            return bl.DeleteRenting(id);
        }

        public bool DeleteFault(int id)
        {
            return bl.DeleteFault(id);
        }

        public bool DeleteCar_Fault(int id)
        {
            return bl.DeleteCar_Fault(id);
        }

        public bool UpdateClient(Client c)
        {
            return bl.UpdateClient(c);
        }

        public bool UpdateCar(Car c)
        {
            return bl.UpdateCar(c);
        }

        public bool UpdateRenting(Renting r)
        {
            return bl.UpdateRenting(r);
        }

        public bool UpdateFault(Fault f)
        {
            return bl.UpdateFault(f);
        }

        public bool UpdateCar_Fault(Car_Fault cf)
        {
            return bl.UpdateCar_Fault(cf);
        }

        public List<Client> SelectAllClients()
        {
            return bl.SelectAllClients();
        }

        public List<Car> SelectAllCars()
        {
            return bl.SelectAllCars();
        }

        public List<Renting> SelectAllRentings()
        {
            return bl.SelectAllRentings();
        }

        public List<Fault> SelectAllFaults()
        {
            return bl.SelectAllFaults();
        }

        public List<Car_Fault> SelectAllCar_Faults()
        {
            return bl.SelectAllCar_Faults();
        }

        public Client SelectClient(int clientId)
        {
            return bl.SelectClient(clientId);
        }

        public Car SelectCar(int carId)
        {
            return bl.SelectCar(carId);
        }

        public Renting SelectRenting(int rentingId)
        {
            return bl.SelectRenting(rentingId);
        }

        public Fault SelectFault(int faultId)
        {
            return bl.SelectFault(faultId);
        }

        public Car_Fault SelectCar_Fault(int car_faultId)
        {
            return bl.SelectCar_Fault(car_faultId);
        }

        public List<Renting> GetRentingForClient(int clientId)
        {
            return bl.GetRentingForClient(clientId);
        }

        public double ExpensesForClient(int clientId, DateTime dateBegin, DateTime dateEnd)
        {
            return bl.ExpensesForClient(clientId, dateBegin, dateEnd);
        }

        public double EarningsForCar(int carId)
        {
            return bl.EarningsForCar(carId);
        }

        public List<string> FaultsNamesOrderByFrequency()
        {
            return bl.FaultsNamesOrderByFrequency();
        }

        public List<Client> FindRenting(Predicate<Renting> match)
        {
            return bl.FindRenting(match);
        }

        public bool ThereWasFault(int rentingId)
        {
            return bl.ThereWasFault(rentingId);
        }

        public double FinalRentingPrice(int rentingId)
        {
            return bl.FinalRentingPrice(rentingId);
        }

        public bool RentingEnding(int rentId, DateTime dateEnd, int ke)
        {
            return bl.RentingEnding(rentId, dateEnd, ke);
        }

        public bool IsYoungDriver(int clientId)
        {
            return bl.IsYoungDriver(clientId);
        }

        public bool IsNewDriver(int clientId)
        {
            return bl.IsNewDriver(clientId);
        }
    }
}

