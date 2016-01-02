using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDAL
    {
        // INSERT METHODS : Client, Car, Renting, Fault, Car_Fault
        // insert a element in the datasource
        bool InsertClient(Client c);
        bool InsertCar(Car c);
        bool InsertRenting(Renting r);
        bool InsertFault(Fault f);
        bool InsertCar_Fault(Car_Fault cf);

        // DELETE METHODS : Client, Car, Renting, Fault, Car_Fault
        // delete an element from the datasource
        bool DeleteClient(int id);
        bool DeleteCar(int id);
        bool DeleteRenting(int id);
        bool DeleteFault(int id);
        bool DeleteCar_Fault(int id);

        // UPDATE METHODS : Client, Car, Renting, Fault, Car_Fault
        // update an element in the datasource
        bool UpdateClient(Client c);
        bool UpdateCar(Car c);
        bool UpdateRenting(Renting r);
        bool UpdateFault(Fault f);
        bool UpdateCar_Fault(Car_Fault cf);

        // SELECTALL METHODS : Client, Car, Renting, Fault, Car_Fault
        // returns all the elements of en entity datasource
        List<Client> SelectAllClients();
        List<Car> SelectAllCars();
        List<Renting> SelectAllRentings();
        List<Fault> SelectAllFaults();
        List<Car_Fault> SelectAllCar_Faults();

        // SELECT METHODS; Client, Car, Renting, Fault, Car_Fault
        // select a specific element in the datasource
        Client SelectClient(int clientId);
        Car SelectCar(int carId);
        Renting SelectRenting(int rentingId);
        Fault SelectFault(int faultId);
        Car_Fault SelectCar_Fault(int car_faultId);
    }
}
