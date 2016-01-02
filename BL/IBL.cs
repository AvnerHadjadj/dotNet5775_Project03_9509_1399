using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{    
    public interface IBL
    {
        #region DAL Func
        bool InsertClient(Client c);
        bool InsertCar(Car c);
        bool InsertRenting(Renting r);
        bool InsertFault(Fault f);
        bool InsertCar_Fault(Car_Fault cf);

        bool DeleteClient(int id);
        bool DeleteCar(int id);
        bool DeleteRenting(int id);
        bool DeleteFault(int id);
        bool DeleteCar_Fault(int id);

        bool UpdateClient(Client c);
        bool UpdateCar(Car c);
        bool UpdateRenting(Renting r);
        bool UpdateFault(Fault f);
        bool UpdateCar_Fault(Car_Fault cf);

        List<Client> SelectAllClients();
        List<Car> SelectAllCars();
        List<Renting> SelectAllRentings();
        List<Fault> SelectAllFaults();
        List<Car_Fault> SelectAllCar_Faults();

        Client SelectClient(int clientId);
        Car SelectCar(int carId);
        Renting SelectRenting(int rentingId);
        Fault SelectFault(int faultId);
        Car_Fault SelectCar_Fault(int car_faultId);
        #endregion


        // BL Func

        /// <summary>
        /// return the list of the renting associated to the client 
        /// </summary>
        /// <param name="clientId">id of the client</param>
        /// <returns></returns>
        List<Renting> GetRentingForClient(int clientId);

        /// <summary>
        /// returns the sum of the expenses of the client between two dates
        /// </summary>
        /// <param name="clientId">id of the client</param>
        /// <param name="dateBegin">date from</param>
        /// <param name="dateEnd">date end</param>
        /// <returns></returns>
        double ExpensesForClient(int clientId, DateTime dateBegin, DateTime dateEnd);

        /// <summary>
        /// returns the sum of the money earned with the car
        /// </summary>
        /// <param name="carId">id of the car</param>
        /// <returns></returns>
        double EarningsForCar(int carId);

        /// <summary>
        /// returns lists of the faults' name order by frequency
        /// </summary>
        /// <returns></returns>
        List<string> FaultsNamesOrderByFrequency();

        /// <summary>
        /// returns the list of client who are associated with a renting matching the given predicate
        /// </summary>
        /// <param name="match">predicate to apply to the renting</param>
        /// <returns></returns>
        List<Client> FindRenting(Predicate<Renting> match);

        /// <summary>
        /// returns if there was a fault on the car at the end of the renting
        /// </summary>
        /// <param name="rentingId">id of the renting</param>
        /// <returns></returns>
        bool ThereWasFault(int rentingId);

        /// <summary>
        /// returns the final price of the renting including taxes and fault repair costs
        /// </summary>
        /// <param name="rentingId">id of the renting</param>
        /// <returns></returns>
        double FinalRentingPrice(int rentingId);

        /// <summary>
        /// ends a renting
        /// </summary>
        /// <param name="rentId">id of the renting</param>
        /// <param name="dateEnd">rental date end</param>
        /// <param name="ke">kilometers drivens</param>
        /// <returns></returns>
        bool RentingEnding(int rentId, DateTime dateEnd, int ke);

        /// <summary>
        /// checks if a driver is a young driver (<25 years old)
        /// </summary>
        /// <param name="clientId">id of the driver = id of the client</param>
        /// <returns></returns>
        bool IsYoungDriver(int clientId);

        /// <summary>
        /// checks if a driver is a new driver (license obtained in the last 2 years)
        /// </summary>
        /// <param name="clientId">id of the driver = id of the client</param>
        /// <returns></returns>
        bool IsNewDriver(int clientId);
    }
}
