using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.ServiceModel;

namespace BL_WcfService
{
    [ServiceContract (Name = "BlServiceContract")]
    public interface IBL
    {
        #region DAL Func
        [OperationContract]
        bool InsertClient(Client c);
        [OperationContract]
        bool InsertCar(Car c);
        [OperationContract]
        bool InsertRenting(Renting r);
        [OperationContract]
        bool InsertFault(Fault f);
        [OperationContract]
        bool InsertCar_Fault(Car_Fault cf);

        [OperationContract]
        bool DeleteClient(int id);
        [OperationContract]
        bool DeleteCar(int id);
        [OperationContract]
        bool DeleteRenting(int id);
        [OperationContract]
        bool DeleteFault(int id);
        [OperationContract]
        bool DeleteCar_Fault(int id);

        [OperationContract]
        bool UpdateClient(Client c);
        [OperationContract]
        bool UpdateCar(Car c);
        [OperationContract]
        bool UpdateRenting(Renting r);
        [OperationContract]
        bool UpdateFault(Fault f);
        [OperationContract]
        bool UpdateCar_Fault(Car_Fault cf);

        [OperationContract]
        List<Client> SelectAllClients();
        [OperationContract]
        List<Car> SelectAllCars();
        [OperationContract]
        List<Renting> SelectAllRentings();
        [OperationContract]
        List<Fault> SelectAllFaults();
        [OperationContract]
        List<Car_Fault> SelectAllCar_Faults();

        [OperationContract]
        Client SelectClient(int clientId);
        [OperationContract]
        Car SelectCar(int carId);
        [OperationContract]
        Renting SelectRenting(int rentingId);
        [OperationContract]
        Fault SelectFault(int faultId);
        [OperationContract]
        Car_Fault SelectCar_Fault(int car_faultId);
        #endregion


        // BL Func
        [OperationContract]
        List<Renting> GetRentingForClient(int clientId);
        [OperationContract]
        double ExpensesForClient(int clientId, DateTime dateBegin, DateTime dateEnd);
        [OperationContract]
        double EarningsForCar(int carId);
        [OperationContract]
        List<string> FaultsNamesOrderByFrequency();
        [OperationContract]
        List<Client> FindRenting(Predicate<Renting> match);
        [OperationContract]
        bool ThereWasFault(int rentingId);
        [OperationContract]
        double FinalRentingPrice(int rentingId);
        [OperationContract]
        bool RentingEnding(int rentId, DateTime dateEnd, int ke);
        [OperationContract]
        bool IsYoungDriver(int clientId);
        [OperationContract]
        bool IsNewDriver(int clientId);
    }
}
