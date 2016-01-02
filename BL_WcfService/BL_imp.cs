using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BE;

namespace BL_WcfService
{
    public class BL_imp : IBL
    {
        public IDAL MyDAL;

        public BL_imp() 
        {
            MyDAL = DALFactory.getDAL();
        }

        public IDAL GetDALInstance()
        {
            return MyDAL;
        }
        


        /// <summary>
        /// Returns a list of renting for a specific client
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<Renting> GetRentingForClient(int clientId)
        {
            var listOfRentingMatching = from r in MyDAL.SelectAllRentings()
                        where r.DriversId().Contains(clientId)
                        select r;
            return listOfRentingMatching.ToList();
        }

        /// <summary>
        /// Returns the sum of expenses for a specific client int the dates range
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="dateBegin"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public double ExpensesForClient(int clientId, DateTime dateBegin, DateTime dateEnd)
        {
            var listOfRentingMatching = from r in MyDAL.SelectAllRentings()
                                        where r.DriversId().Contains(clientId)
                                        && r.RentalStartDate >= dateBegin
                                        && r.RentalEndDate <= dateEnd
                                        select FinalRentingPrice(r.IdNumber);
            return listOfRentingMatching.Sum();
        }

        /// <summary>
        /// Returns total earning made with a specific car
        /// </summary>
        /// <param name="carId"></param>
        /// <returns></returns>
        public double EarningsForCar(int carId)
        {
            var listOfRentingMatching = from r in MyDAL.SelectAllRentings()
                                        where r.CarId == carId
                                        select r.RentalPriceDaily;
            return listOfRentingMatching.Sum();
        }

        /// <summary>
        /// Returns list of faults names ordered by frequency
        /// </summary>
        /// <returns></returns>
        public List<string> FaultsNamesOrderByFrequency()
        {
            var NamesOfFault = MyDAL.SelectAllCar_Faults().GroupBy(cf => cf.FaultId)
                               .OrderByDescending(gp => gp.Count()).Select(f => MyDAL.SelectFault(f.Key).Description);
            
            return NamesOfFault.ToList();
        }

        /// <summary>
        /// Returns list of clients of rentings matching the predicate 
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public List<Client> FindRenting(Predicate<Renting> match)
        {
            var listClientsOfRenting = from rents in MyDAL.SelectAllRentings().Where(r => match(r))
                                       from client in MyDAL.SelectAllClients()
                                       where rents.DriversId()[0] == client.IdNumber
                                       select client;
            return listClientsOfRenting.ToList();
        }

        /// <summary>
        /// Returns bool wether the given renting ended with a new fault
        /// </summary>
        /// <param name="rentingId"></param>
        /// <returns></returns>
        public bool ThereWasFault(int rentingId)
        {
            var thereWasNewFault = from rent in MyDAL.SelectAllRentings().Where(r => r.IdNumber == rentingId)
                                  join carfault in MyDAL.SelectAllCar_Faults() on rent.CarId equals carfault.CarId into faults
                                  from fault in MyDAL.SelectAllCar_Faults()
                                  where faults.Contains(fault)
                                  select faults.Where(f => f.FaultDate > rent.RentalStartDate && f.FaultDate < rent.RentalEndDate);
            return thereWasNewFault.Count() > 0;
        }

        /// <summary>
        /// Returns the final price for the given renting       
        /// </summary>
        /// <param name="rentingId"></param>
        /// <returns></returns>
        public double FinalRentingPrice(int rentingId)
        {            
            // we get the renting price based on the number of days, and other criterias
            Renting rent = MyDAL.SelectRenting(rentingId);
            if (rent.RentalPriceDaily == 0)
                throw new ArgumentNullException("The renting must be started before calculating the final price.");

            // getting the properties of the rent
            double price = rent.RentalPriceDaily;
            int driversNb = rent.DriversNumber;
            DateTime today = Convert.ToDateTime(DateTime.Now.ToString());

            // we take fee of 50 NIS for a second driver
            int taxSecondDriver = driversNb == 2 ? 50 : 0;

            /* * * * * * * * * * *
             * Additional fees for 
             * recently obtained 
             * driver license
             * * * * * * * * * * */

            int taxRecentDriver = 0;
            for (int i = 0; i < driversNb; i++)
            {
               if (IsNewDriver(rent.DriversId()[i]))
                    taxRecentDriver += 100;
            }

            /* * * * * * * * * * *
             * Additional fees for 
             * young driver
             * under 25 years old
             * * * * * * * * * * */

            int taxYoungDriver = 0;
            for (int i = 0; i < driversNb; i++)
            {
                if (IsYoungDriver(rent.DriversId()[i]))
                    taxYoungDriver += 100;
            }

            // If he is responsible of a takala so he has to pay for
            double taxFaults = 0;
            if (ThereWasFault(rentingId))
            {
                // select all faults that happened during the renting
                var thereWasNewFault = MyDAL.SelectAllCar_Faults().
                                       Where(cf => cf.CarId == rent.CarId).
                                       Where(cf => cf.FaultDate > rent.RentalStartDate && cf.FaultDate < rent.RentalEndDate).                                       
                                       ToList();

                foreach (Car_Fault item in thereWasNewFault)
                {
                    Fault currentFault = MyDAL.SelectFault(item.FaultId);
                    if (currentFault.Responsible)
                        taxFaults += currentFault.RepairCost;
                }
            }
            
            // Calculating final price
            double finalPrice = price + taxSecondDriver + taxRecentDriver + taxYoungDriver + taxFaults;

            return finalPrice;
        }

        /// <summary>
        /// End the renting with an end date, and a number of km driven, and 
        /// </summary>
        /// <param name="rentId">ID of the renting to end</param>
        /// <param name="dateEnd">Date of renting end</param>
        /// <param name="ke">Kilometers driven between start and end of the renting</param>
        /// <returns>returns if it was ended correctly or not</returns>
        public bool RentingEnding(int rentId, DateTime dateEnd, int ke)
        {
                          
            Renting rentactual = MyDAL.SelectRenting(rentId);
            Car caractual = MyDAL.SelectCar(rentactual.CarId);
            int catPrice = (int)caractual.Cat;

            // calculate the during
            TimeSpan during = dateEnd - rentactual.RentalStartDate;

            // calculate the price: depending on the category, and the number of kilometers done
            double price = during.TotalDays * catPrice + (double)ke/100.0;

            // update the km of the car
            caractual.Kilometers += ke;
            MyDAL.UpdateCar(caractual);

            // checking faults
            var query = from takala in MyDAL.SelectAllCar_Faults().Where(t => t.CarId == rentactual.CarId)
                        where takala.FaultDate > rentactual.RentalStartDate && takala.FaultDate < dateEnd
                        select takala;
            int nbFault = query.ToList().Count();

            // filter faults for those the client responsible
            int nbTakRes = query.ToList().Where(t => MyDAL.SelectFault(t.FaultId).Responsible == true).Count();

            // maj du champs returnedwithfault
            rentactual.ReturnedWithFault = (nbFault > 0) ? true : false;

            // maj du/des client
            for (int i = 0; i < rentactual.DriversNumber; i++)
            {
                Client clientupdated = MyDAL.SelectClient(rentactual.DriversNumber);
                clientupdated.FaultResponsible = (nbTakRes > 0) ? nbTakRes : 0;
                MyDAL.UpdateClient(clientupdated);
            }

            // preparation du renting a mettre a jour
            rentactual.RentalPriceDaily = price;
            rentactual.RentalEndDate = dateEnd;
            rentactual.KilometersAtRentalEnd = caractual.Kilometers;

            // mis a jour finale
            MyDAL.UpdateRenting(rentactual);
           
            // Renting ended and updated successfully
            return true;
        }

        /// <summary>
        /// Checks whether a client is a young driver (under 25 y.o)
        /// </summary>
        /// <param name="clientId">ID of the client</param>
        /// <returns>TRUE if the client is a young driver, FALSE else</returns>
        public bool IsYoungDriver(int clientId)
        {
            DateTime today = Convert.ToDateTime(DateTime.Now.ToString());

            DateTime driverBirth = MyDAL.SelectClient(clientId).BirthDate;
            int age = today.Year - driverBirth.Year;
            if (driverBirth > today.AddYears(-age)) age--; // adjust the age of the driver concernig actual year

            if (age < 25)
                return true;
            return false;
        }

        /// <summary>
        /// Checks whether a client is a new driver (obtained driving license for less than 2 years)
        /// </summary>
        /// <param name="clientId">ID of the client</param>
        /// <returns>TRUE if the client is a new driver, FALSE else</returns>
        public bool IsNewDriver(int clientId)
        {
            DateTime today = Convert.ToDateTime(DateTime.Now.ToString());

            DateTime licenseDate = MyDAL.SelectClient(clientId).LicenseDrivingDate;
            TimeSpan difference = today - licenseDate;

            if (difference.TotalDays < 730)
                return true;
            return false;
        }


        // IDAL functions implementation

        // Insert Methods
        public bool InsertClient(Client c)
        {
            List<Client> listclient= MyDAL.SelectAllClients();
            bool clientAlreadyExist = listclient.Where(cl => cl.IdTeoudaZeout == c.IdTeoudaZeout).Count() > 0;
            bool creditCardExist = MyDAL.SelectAllClients().Where(cl => cl.CreditCartNumber == c.CreditCartNumber).Count() > 0;             
            
            if (c.Name == string.Empty)
                throw new ArgumentNullException("The client's name isn't a valid string.");
            if (clientAlreadyExist)
                throw new ArgumentException("This client's Teoudat Zeout has already been registered by another client.");
            if (creditCardExist)
                throw new ArgumentException("This client's credit card has already been registered by another client.");
            
            try
            {
                return MyDAL.InsertClient(c);
            }
            catch(Exception)
            {
                throw;
            }
        }

        public bool InsertCar(Car c)
        {
            bool carAlreadyExist = MyDAL.SelectAllCars().Where(car => car.RegistrationNumber == c.RegistrationNumber).Count() > 0;
            
            if (c.RegistrationNumber == string.Empty)
                throw new ArgumentException("The car's registration number isn't a valid string.");
            if (carAlreadyExist)
                throw new ArgumentException("The car's registration number has already been registered for another car.");
            try
            {
                return MyDAL.InsertCar(c);
            }
            catch (Exception)
            {
                throw;
            } 
        }

        public bool InsertRenting(Renting r)
        {
            // Checking existing client and car
            int clientMatching = MyDAL.SelectAllClients().Where(c => (r.DriversId()[0] == c.IdNumber) || (r.DriversId()[1] == c.IdNumber)).Count();
            int carMatching = MyDAL.SelectAllCars().Where(c => r.CarId == c.IdNumber).Count();

            // Checking faults responsibility of the driver/drivers
            bool faultsNbTooBig = false;
            for (int i = 0; i < r.DriversNumber; i++)
            {
                if (MyDAL.SelectClient(r.DriversId()[i]).FaultResponsible > 2)
                    faultsNbTooBig = true;
            }

            // Handling exceptions
            if (clientMatching < 1) // there must be the client1 and the default client or the client2
                throw new Exception("The renting's clients haven't been registered yet.");
            if (carMatching == 0)
                throw new Exception("The car selected hasn't been registered yet.");
            if (faultsNbTooBig)
                throw new Exception("One of the renting's driver has been responsible for 2 faults or more. The renting can't be done.");
            
            try
            {
                return MyDAL.InsertRenting(r);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool InsertFault(Fault f)
        {
            if (f.Description == string.Empty)
                throw new ArgumentNullException("The fault's description can't be empty.");
            try
            {
                return MyDAL.InsertFault(f);
            }
            catch (Exception)
            { 
                throw;
            } 
        }

        public bool InsertCar_Fault(Car_Fault c)
        {
            try
            {
                return MyDAL.InsertCar_Fault(c);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Delete Methods
        public bool DeleteClient(int id)
        {
            // verify if client had a renting in the last 30 day
            int rentingOfClient = MyDAL.SelectAllRentings().Where(r => r.DriversId().Contains(id)).Where(r => ((DateTime.Now - r.RentalEndDate).TotalDays < 30)).Count();

            if (rentingOfClient > 0)
                throw new Exception("This client #" + id + " made a renting in the last 30 days. Deleting unsuccessful.");
            
            try
            {
                return MyDAL.DeleteClient(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteCar(int id)
        {
            try
            {
                return MyDAL.DeleteCar(id);
            }
            catch (Exception)
            {
                throw;
            }

        }

        public bool DeleteRenting(int id)
        {
            try
            {
                return MyDAL.DeleteRenting(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteFault(int id)
        {
            try
            {
                return MyDAL.DeleteFault(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteCar_Fault(int id)
        {
            try
            {
                return MyDAL.DeleteCar_Fault(id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Update Methods
        public bool UpdateClient(Client clientUpdated)
        {
            try
            {
                return MyDAL.UpdateClient(clientUpdated);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateCar(Car carUpdated)
        {
            try
            {
                return MyDAL.UpdateCar(carUpdated);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateRenting(Renting rentingUpdated)
        {
            try
            {
                return MyDAL.UpdateRenting(rentingUpdated);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateFault(Fault faultUpdated)
        {
            try
            {
                return MyDAL.UpdateFault(faultUpdated);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateCar_Fault(Car_Fault car_faultUpdated)
        {
            try
            {
                return MyDAL.UpdateCar_Fault(car_faultUpdated);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // SelectAll methods
        public List<Client> SelectAllClients()
        {
            try
            {
                return MyDAL.SelectAllClients();
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("The list of clients is null.");
            }
        }

        public List<Car> SelectAllCars()
        {
            try
            {
                return MyDAL.SelectAllCars();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        public List<Renting> SelectAllRentings()
        {
            try
            {
                return MyDAL.SelectAllRentings();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        public List<Fault> SelectAllFaults()
        {
            try
            {
                return MyDAL.SelectAllFaults();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
            
        }

        public List<Car_Fault> SelectAllCar_Faults()
        {
            try
            {
                return MyDAL.SelectAllCar_Faults();
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        // Select specific element methods matching its ID
        public Client SelectClient(int clientId)
        {
            bool clientExist = MyDAL.SelectAllClients().Where(c => c.IdNumber == clientId).Count() == 1;
            
            if (!clientExist)
                throw new Exception("The client #"+clientId+" doesn't exist.");

            try
            {
                return MyDAL.SelectClient(clientId);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        public Car SelectCar(int carId)
        {
            bool carExist = MyDAL.SelectAllCars().Where(c => c.IdNumber == carId).Count() == 1;

            if (!carExist)
                throw new Exception("The car #" + carId + " doesn't exist.");

            try
            {
                return MyDAL.SelectCar(carId);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        public Renting SelectRenting(int rentingId)
        {
            bool rentingExist = MyDAL.SelectAllRentings().Where(c => c.IdNumber == rentingId).Count() == 1;

            if (!rentingExist)
                throw new Exception("The renting #" + rentingId + " doesn't exist.");

            try
            {
                return MyDAL.SelectRenting(rentingId);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        public Fault SelectFault(int faultId)
        {
            bool faultExist = MyDAL.SelectAllFaults().Where(c => c.IdNumber == faultId).Count() == 1;

            if (!faultExist)
                throw new Exception("The fault #" + faultId + " doesn't exist.");

            try
            {
                return MyDAL.SelectFault(faultId);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }

        public Car_Fault SelectCar_Fault(int car_faultId)
        {
            try
            {
                return MyDAL.SelectCar_Fault(car_faultId);
            }
            catch (ArgumentNullException)
            {
                throw;
            }
        }
        
        //// Others methods
        //public int GetCarKm(int carId)
        //{
        //    // we check wether the carId belongs to an existing car
        //    int currentCarId = MyDAL.SelectCar(carId).IdNumber;

        //    try
        //    {
        //        return MyDAL.GetCarKm(currentCarId);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public int GetNbFaultResponsible(int clientId)
        //{
        //    // we check wether the clientId belongs to an existing car
        //    int currentCarId = MyDAL.SelectCar(clientId).IdNumber;

        //    try
        //    {
        //        return MyDAL.GetNbFaultResponsible(currentCarId);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
