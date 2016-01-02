using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using BE;
using System.Reflection; 

namespace DAL
{
    public class DAL_XML_imp : IDAL
    {
        XElement configsRoot;
        string configsPath;

        XElement rentingRoot;
        string rentingPath;
        
        XElement clientRoot;
        string clientPath;

        XElement carRoot;
        string carPath;
        
        XElement faultRoot;
        string faultPath;

        XElement carfaultRoot;
        string carfaultPath;


        // CONSTRUCTOR                
        public DAL_XML_imp()
        {
            string str = Assembly.GetExecutingAssembly().Location;
            string localPath = Path.GetDirectoryName(str);
            
            // We keep the root directory of the project, 
            // in order to make PLFORMS and BE_WcfService working on the same XML files
            for (int i = 0; i < 3; i++)
                localPath = Directory.GetParent(localPath).FullName;

            rentingPath = localPath + @"\\XML\\RentingXML.xml";
            clientPath = localPath + @"\\XML\\ClientXML.xml";
            carPath = localPath + @"\\XML\\CarXML.xml";
            faultPath = localPath + @"\\XML\\FaultXML.xml";
            carfaultPath = localPath + @"\\XML\\CarFaultXML.xml";
            configsPath = localPath + @"\\XML\\ConfigsXML.xml";

            // ---------------------------------------------------
            // Generating XML Files
            // ---------------------------------------------------

            // Configuration File
            if (!File.Exists(configsPath))
            {
                // We config the auto-increment indexes
                configsRoot = new XElement("autoincrements");
                XElement numberRenting = new XElement("numberRenting", 0);
                XElement numberClient = new XElement("numberClient", 0);
                XElement numberCar = new XElement("numberCar", 0);
                XElement numberFault = new XElement("numberFault", 0);
                XElement numberCarFault = new XElement("numberCarFault", 0);

                configsRoot.Add(new XElement("autoincrement", numberRenting, numberClient, numberCar, numberFault, numberCarFault));
                configsRoot.Save(configsPath);
            }
            try { configsRoot = XElement.Load(configsPath); } 
            catch { throw new Exception("Failed to load " + configsPath); }
            
            // updating class static auto-increments
            Renting.autoIncrement = getNumberOf("Renting");
            Client.autoIncrement = getNumberOf("Client");
            Car.autoIncrement = getNumberOf("Car");
            Fault.autoIncrement = getNumberOf("Fault");
            Car_Fault.autoIncrement = getNumberOf("CarFault");

            // Rentings File
            if (!File.Exists(rentingPath))           
                MakeFile("rentings", rentingPath, rentingRoot);
            try { rentingRoot = XElement.Load(rentingPath); }
            catch { throw new Exception("Failed to load " + rentingPath); }
            

            // Clients File
            if (!File.Exists(clientPath))
                MakeFile("clients", clientPath, clientRoot);
            try { clientRoot = XElement.Load(clientPath); }
            catch { throw new Exception("Failed to load " + clientPath); }
            

            // Car File
            if (!File.Exists(carPath))
                MakeFile("cars", carPath, carRoot);
            try { carRoot = XElement.Load(carPath); }
            catch { throw new Exception("Failed to load " + carPath); }
            

            // Faults File
            if (!File.Exists(faultPath))
                MakeFile("faults", faultPath, faultRoot);
            try { faultRoot = XElement.Load(faultPath); }
            catch { throw new Exception("Failed to load " + faultPath); }
            

            // Car_Faults File
            if (!File.Exists(carfaultPath))
                MakeFile("carfaults", carfaultPath, carfaultRoot);            
            try { carfaultRoot = XElement.Load(carfaultPath); }
            catch { throw new Exception("Failed to load " + carfaultPath); }
            
        }


        /// <summary>
        /// Creates a XML file for storing datas
        /// </summary>
        /// <param name="name">string name of the data type</param>
        /// <param name="path">string path to the file</param>
        /// <param name="root">XElement handling the xml file</param>
        private void MakeFile(string name, string path, XElement root)
        {
            root = new XElement(name);
            root.Save(path);
        }


        #region Insert methods

        public bool InsertRenting(Renting r)
        {
            XElement idNumber = new XElement("idNumber", r.IdNumber);
            XElement rentalStartDate = new XElement("rentalStartDate", r.RentalStartDate);
            XElement rentalEndDate = new XElement("rentalEndDate", r.RentalEndDate);
            XElement idClient1 = new XElement("idClient1", r.DriversAllowed.idClient1);
            XElement idClient2 = new XElement("idClient2", r.DriversAllowed.idClient2);
            XElement driversAllowed = new XElement("driversAllowed", idClient1, idClient2);
            XElement carId = new XElement("carId", r.CarId);
            XElement driversNumber = new XElement("driversNumber", r.DriversNumber);
            XElement kilometersAtRentalStart = new XElement("kilometersAtRentalStart", r.KilometersAtRentalStart);
            XElement kilometersAtRentalEnd = new XElement("kilometersAtRentalEnd", r.KilometersAtRentalEnd);
            XElement returnedWithFault = new XElement("returnedWithFault", r.ReturnedWithFault);
            XElement rentalPriceDaily = new XElement("rentalPriceDaily", r.RentalPriceDaily);
            rentingRoot.Add(new XElement("renting", idNumber, rentalStartDate, rentalEndDate, driversAllowed, carId, driversNumber, kilometersAtRentalStart, kilometersAtRentalEnd, returnedWithFault, rentalPriceDaily));
            rentingRoot.Save(rentingPath);

            // updating the autoincrement of clients
            configsRoot.Element("autoincrement").Element("numberRenting").Value = r.IdNumber.ToString();
            configsRoot.Save(configsPath);
            return true;
        }

        public bool InsertClient(Client c)
        {
            XElement idNumber = new XElement("idNumber", c.IdNumber);
            XElement idTeoudaZeout = new XElement("idTeoudaZeout", c.IdTeoudaZeout);
            XElement address = new XElement("address", c.Address);
            XElement name = new XElement("name", c.Name);
            XElement birthDate = new XElement("birthDate", c.BirthDate);
            XElement licenseDrivingDate = new XElement("licenseDrivingDate", c.LicenseDrivingDate);
            XElement creditCartNumber = new XElement("creditCartNumber", c.CreditCartNumber);
            XElement faultResponsible = new XElement("faultResponsible", c.FaultResponsible);
            clientRoot.Add(new XElement("client", idNumber, idTeoudaZeout, address, name, birthDate, licenseDrivingDate, creditCartNumber, faultResponsible));
            clientRoot.Save(clientPath);

            // updating the autoincrement of clients
            configsRoot.Element("autoincrement").Element("numberClient").Value = c.IdNumber.ToString();
            configsRoot.Save(configsPath);            
            return true;
        }

        public bool InsertCar(Car c)
        {
            XElement idNumber = new XElement("idNumber", c.IdNumber);
            XElement registrationNumber = new XElement("registrationNumber", c.RegistrationNumber);
            XElement manufactureDate = new XElement("manufactureDate", c.ManufactureDate);
            XElement mark = new XElement("mark", c.Name.mark);
            XElement model = new XElement("model", c.Name.model);
            XElement volumecc = new XElement("volumecc", c.Name.volumecc);
            XElement color = new XElement("color", c.Name.color);
            XElement name = new XElement("name", mark, model, volumecc, color);
            XElement transmission = new XElement("transmissionType", c.TransmissionType);
            XElement passengersNumber = new XElement("passengersNumber", c.PassengersNumber);
            XElement doorsNumber = new XElement("doorsNumber", c.DoorsNumber);
            XElement kilometers = new XElement("kilometers", c.Kilometers);
            XElement branchAddress = new XElement("branchAddress", c.BranchAddress);
            XElement category = new XElement("category", c.Cat);
            carRoot.Add(new XElement("car", idNumber, registrationNumber, manufactureDate, name, transmission, passengersNumber, doorsNumber, kilometers, branchAddress, category));
            carRoot.Save(carPath);
            
            // updating the autoincrement of clients
            configsRoot.Element("autoincrement").Element("numberCar").Value = c.IdNumber.ToString();
            configsRoot.Save(configsPath);
            return true;
        }

        public bool InsertFault(Fault f)
        {
            XElement idNumber = new XElement("idNumber", f.IdNumber);
            XElement description = new XElement("description", f.Description);
            XElement responsible = new XElement("responsible", f.Responsible);
            XElement repairCost = new XElement("repairCost", f.RepairCost);
            XElement preferredGarage = new XElement("preferredGarage", f.PreferredGarage);
            faultRoot.Add(new XElement("fault", idNumber, description, responsible, repairCost, preferredGarage));
            faultRoot.Save(faultPath);
            
            // updating the autoincrement of clients
            configsRoot.Element("autoincrement").Element("numberFault").Value = f.IdNumber.ToString();
            configsRoot.Save(configsPath);
            return true;
        }

        public bool InsertCar_Fault(Car_Fault c)
        {
            XElement idNumber = new XElement("idNumber", c.IdNumber);
            XElement carId = new XElement("carId", c.CarId);
            XElement faultId = new XElement("faultId", c.FaultId);
            XElement faultDate = new XElement("faultDate", c.FaultDate);
            carfaultRoot.Add(new XElement("carfault", idNumber, carId, faultId, faultDate));
            carfaultRoot.Save(carfaultPath);

            // updating the autoincrement of clients
            configsRoot.Element("autoincrement").Element("numberCarFault").Value = c.IdNumber.ToString();
            configsRoot.Save(configsPath);            
            return true;
        }

        #endregion

        #region Delete methods

        public bool DeleteClient(int id)
        {
            XElement clientElement;
            
            try
            {
                clientElement = (from p in clientRoot.Elements()
                                 where Convert.ToInt32(p.Element("idNumber").Value) == id
                                 select p).FirstOrDefault();
                clientElement.Remove();

                clientRoot.Save(clientPath);                               
            }
            catch
            {
                throw new Exception("Client not found");
            }
            return true;
        }

        public bool DeleteCar(int id)
        {
            XElement carElement;

            try
            {
                carElement = (from p in carRoot.Elements()
                              where Convert.ToInt32(p.Element("idNumber").Value) == id
                                 select p).FirstOrDefault();
                carElement.Remove();

                carRoot.Save(carPath);

                return true;
            }

            catch
            {
                throw new Exception("Car not found");
            }
        }

        public bool DeleteRenting(int id)
        {
            XElement rentingElement;

            try
            {
                rentingElement = (from p in rentingRoot.Elements()
                              where Convert.ToInt32(p.Element("idNumber").Value) == id
                              select p).FirstOrDefault();
                rentingElement.Remove();

                rentingRoot.Save(rentingPath);

                return true;
            }

            catch
            {
                throw new Exception("Renting not found");
            }
        }

        public bool DeleteFault(int id)
        {
            XElement faultElement;

            try
            {
                faultElement = (from p in faultRoot.Elements()
                              where Convert.ToInt32(p.Element("idNumber").Value) == id
                              select p).FirstOrDefault();
                faultElement.Remove();

                faultRoot.Save(faultPath);

                return true;
            }

            catch
            {
                throw new Exception("Fault not found");
            }
        }

        public bool DeleteCar_Fault(int id)
        {
            XElement carfaultElement;

            try
            {
                carfaultElement = (from p in carfaultRoot.Elements()
                                where Convert.ToInt32(p.Element("idNumber").Value) == id
                                select p).FirstOrDefault();
                carfaultElement.Remove();

                faultRoot.Save(faultPath);

                return true;
            }

            catch
            {
                throw new Exception("Car fault not found");
            }
        }

        #endregion

        #region Update methods

        public bool UpdateClient(Client id)
        {
            try
            {
                XElement clientElement = (from p in clientRoot.Elements()
                                          where Convert.ToInt32(p.Element("idNumber").Value) == id.IdNumber
                                          select p).FirstOrDefault();

                clientElement.Element("idTeoudaZeout").Value = id.IdTeoudaZeout.ToString();
                clientElement.Element("address").Value = id.Address.ToString();
                clientElement.Element("name").Value = id.Name.ToString();
                clientElement.Element("birthDate").Value = id.BirthDate.ToString();
                clientElement.Element("licenseDrivingDate").Value = id.LicenseDrivingDate.ToString();
                clientElement.Element("creditCartNumber").Value = id.CreditCartNumber.ToString();
                clientElement.Element("faultResponsible").Value = id.FaultResponsible.ToString();
                
                clientRoot.Save(clientPath);

                return true;
            }

            catch
            {
                return false;
            }

        }

        public bool UpdateCar(Car id)
        {
            try
            {
                XElement carElement = (from p in carRoot.Elements()
                                          where Convert.ToInt32(p.Element("idNumber").Value) == id.IdNumber
                                          select p).FirstOrDefault();

                carElement.Element("registrationNumber").Value = id.RegistrationNumber.ToString();
                carElement.Element("manufactureDate").Value = id.ManufactureDate.ToString();
                carElement.Element("name").Element("mark").Value = id.Name.mark.ToString();
                carElement.Element("name").Element("model").Value = id.Name.model.ToString();
                carElement.Element("name").Element("volumecc").Value = id.Name.volumecc.ToString();
                carElement.Element("name").Element("color").Value = id.Name.color.ToString();
                carElement.Element("transmission").Value = id.TransmissionType.ToString();
                carElement.Element("passengersNumber").Value = id.PassengersNumber.ToString();
                carElement.Element("doorsNumber").Value = id.DoorsNumber.ToString();
                carElement.Element("kilometers").Value = id.Kilometers.ToString();
                carElement.Element("branchAddress").Value = id.BranchAddress.ToString();
                carElement.Element("category").Value = id.Cat.ToString();

                carRoot.Save(carPath);

                return true;
            }

            catch
            {
                return false;
            }
        }

        public bool UpdateRenting(Renting id)
        {
            try
            {
                XElement rentingElement = (from p in rentingRoot.Elements()
                                          where Convert.ToInt32(p.Element("idNumber").Value) == id.IdNumber
                                          select p).FirstOrDefault();

                rentingElement.Element("rentalStartDate").Value = id.RentalStartDate.ToString();
                rentingElement.Element("rentalEndDate").Value = id.RentalEndDate.ToString();
                rentingElement.Element("driversAllowed").Element("idClient1").Value = id.DriversAllowed.idClient1.ToString();                
                rentingElement.Element("driversAllowed").Element("idClient2").Value = id.DriversAllowed.idClient2.ToString();
                rentingElement.Element("carId").Value = id.CarId.ToString();
                rentingElement.Element("driversNumber").Value = id.DriversNumber.ToString();
                rentingElement.Element("kilometersAtRentalStart").Value = id.KilometersAtRentalStart.ToString();
                rentingElement.Element("kilometersAtRentalEnd").Value = id.KilometersAtRentalEnd.ToString();
                rentingElement.Element("returnedWithFault").Value = id.ReturnedWithFault.ToString();
                rentingElement.Element("rentalPriceDaily").Value = id.RentalPriceDaily.ToString();

                rentingRoot.Save(rentingPath);

                return true;
            }

            catch
            {
                return false;
            }
        }

        public bool UpdateFault(Fault id)
        {
            try
            {
                XElement faultElement = (from p in faultRoot.Elements()
                                          where Convert.ToInt32(p.Element("idNumber").Value) == id.IdNumber
                                          select p).FirstOrDefault();

                faultElement.Element("description").Value = id.Description.ToString();
                faultElement.Element("responsible").Value = id.Responsible.ToString();
                faultElement.Element("repairCost").Value = id.RepairCost.ToString();
                faultElement.Element("preferredGarage").Value = id.PreferredGarage.ToString();
              
                faultRoot.Save(faultPath);

                return true;
            }

            catch
            {
                return false;
            }
        }

        public bool UpdateCar_Fault(Car_Fault id)
        {
            try
            {
                XElement carfaultElement = (from p in carfaultRoot.Elements()
                                         where Convert.ToInt32(p.Element("idNumber").Value) == id.IdNumber
                                         select p).FirstOrDefault();

                carfaultElement.Element("carId").Value = id.CarId.ToString();
                carfaultElement.Element("faultId").Value = id.FaultId.ToString();
                carfaultElement.Element("faultDate").Value = id.FaultDate.ToString();                

                carfaultRoot.Save(carfaultPath);

                return true;
            }

            catch
            {
                return false;
            }
        }

        #endregion

        #region SelectAll methods

        public List<Client> SelectAllClients()
        {
            List<Client> clients;

            try
            {
                clients = (from c in clientRoot.Elements("client")
                           select new Client() {                           
                               IdNumber = Convert.ToInt32(c.Element("idNumber").Value),
                               IdTeoudaZeout = Convert.ToInt32(c.Element("idTeoudaZeout").Value),
                               Address = Convert.ToString(c.Element("address").Value),
                               Name = Convert.ToString(c.Element("name").Value),
                               BirthDate = Convert.ToDateTime(c.Element("birthDate").Value),
                               LicenseDrivingDate = Convert.ToDateTime(c.Element("licenseDrivingDate").Value),
                               CreditCartNumber = Convert.ToString(c.Element("creditCartNumber").Value),
                               FaultResponsible = Convert.ToInt32(c.Element("faultResponsible").Value)
                           }).ToList();
            }
            catch
            {
                return new List<Client>();
            }

            return clients; 
        }

        public List<Car> SelectAllCars()
        {
            List<Car> cars;

            try
            {
                cars = (from c in carRoot.Elements("car")
                        select new Car()
                        {
                            IdNumber = Convert.ToInt32(c.Element("idNumber").Value),
                            RegistrationNumber = Convert.ToString(c.Element("registrationNumber").Value),
                            ManufactureDate = Convert.ToDateTime(c.Element("manufactureDate").Value),
                            Name = new CarType(
                                Mark: c.Element("name").Element("mark").Value,
                                Model: c.Element("name").Element("model").Value,
                                Color: c.Element("name").Element("color").Value,
                                Volumecc: c.Element("name").Element("volumecc").Value),
                            TransmissionType = (Transmission)Enum.Parse(typeof(Transmission), c.Element("transmissionType").Value, true),
                            PassengersNumber = Convert.ToInt32(c.Element("passengersNumber").Value),
                            DoorsNumber = Convert.ToInt32(c.Element("doorsNumber").Value),
                            Kilometers = Convert.ToInt32(c.Element("kilometers").Value),
                            BranchAddress = Convert.ToString(c.Element("branchAddress").Value),
                            Cat = (Category)Enum.Parse(typeof(Category), c.Element("category").Value, true)
                        }).ToList();                              
            }
            catch
            {
                return new List<Car>();
            }

            return cars; 
        }

        public List<Renting> SelectAllRentings()
        {
            List<Renting> rentings;

            try
            {
                rentings = (from c in rentingRoot.Elements("renting")
                            select new Renting()
                            {
                                IdNumber =Convert.ToInt32(c.Element("idNumber").Value),
                                RentalStartDate = Convert.ToDateTime(c.Element("rentalStartDate").Value),
                                RentalEndDate = Convert.ToDateTime(c.Element("rentalEndDate").Value),
                                CarId = Convert.ToInt32(c.Element("carId").Value),
                                KilometersAtRentalStart = Convert.ToInt32(c.Element("kilometersAtRentalStart").Value),
                                KilometersAtRentalEnd = Convert.ToInt32(c.Element("kilometersAtRentalEnd").Value),
                                ReturnedWithFault = Convert.ToBoolean(c.Element("returnedWithFault").Value),
                                DriversAllowed = new Drivers(
                                    Convert.ToInt32(c.Element("driversAllowed").Element("idClient1").Value),
                                    Convert.ToInt32(c.Element("driversAllowed").Element("idClient2").Value)),
                                RentalPriceDaily = Convert.ToDouble(c.Element("rentalPriceDaily").Value)
                            }
                            ).ToList();
            }
            catch
            {
                return new List<Renting>();
            }

            return rentings; 
        }

        public List<Fault> SelectAllFaults()
        {
            List<Fault> faults;

            try
            {
                faults = (from c in faultRoot.Elements("fault")
                          select new Fault()
                          {
                              IdNumber = Convert.ToInt32(c.Element("idNumber").Value),
                              Description = Convert.ToString(c.Element("description").Value),
                              Responsible = Convert.ToBoolean(c.Element("responsible").Value),
                              RepairCost = Convert.ToInt32(c.Element("repairCost").Value),
                              PreferredGarage = Convert.ToString(c.Element("preferredGarage").Value)
                          }
                        ).ToList();
            }
            catch
            {
                return new List<Fault>();
            }

            return faults; 
        }

        public List<Car_Fault> SelectAllCar_Faults()
        {
            List<Car_Fault> carfaults;

            try
            {
                carfaults = (from c in carfaultRoot.Elements("carfault")
                             select new Car_Fault()
                             {
                                 IdNumber = Convert.ToInt32(c.Element("idNumber").Value),
                                 CarId = Convert.ToInt32(c.Element("carId").Value),
                                 FaultId = Convert.ToInt32(c.Element("faultId").Value),
                                 FaultDate = Convert.ToDateTime(c.Element("faultDate").Value)
                             }
                            ).ToList();
            }
            catch
            {
                return new List<Car_Fault>();
            }

            return carfaults; 
        }

        #endregion

        #region Select methods

        public Client SelectClient(int clientId)
        {
            try
            {
                return SelectAllClients().Where(c => c.IdNumber == clientId).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public Car SelectCar(int carId)
        {
            try
            {
                return SelectAllCars().Where(c => c.IdNumber == carId).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public Renting SelectRenting(int rentingId)
        {
            try
            {
                return SelectAllRentings().Where(c => c.IdNumber == rentingId).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public Fault SelectFault(int faultId)
        {
            try
            {
                return SelectAllFaults().Where(c => c.IdNumber == faultId).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        public Car_Fault SelectCar_Fault(int car_faultId)
        {
            try
            {
                return SelectAllCar_Faults().Where(c => c.IdNumber == car_faultId).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region others methods

        //public int GetCarKm(int carId)
        //{
        //    return SelectCar(carId).;
        //}

        //public int GetNbFaultResponsible(int clientId)
        //{
        //    return 1;
        //}

        #endregion

        #region Get Auto increments method

        /// <summary>
        /// Returns the current value of the auto-increment requested
        /// </summary>
        /// <returns></returns>
        public int getNumberOf(string numberOf)
        {
            return Convert.ToInt32(configsRoot.Element("autoincrement").Element("number" + numberOf).Value);
        }

        #endregion
    }
}
