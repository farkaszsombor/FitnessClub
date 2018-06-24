using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessClub.Mappings
{
    public class MappingDtos
    {
        //---------------------------CLIENT----------------------------------
        public static Models.Client EntityClientToModelClient(DataAccessLayer.Entities.Client entityClient)
        {
            try
            {
                Models.Client modelClient = new Models.Client
                {

                    Id = entityClient.Id,
                    FirstName = entityClient.FirstName,
                    LastName = entityClient.LastName,
                    Phone = entityClient.Phone,
                    Email = entityClient.Email,
                    ImagePath = String.IsNullOrEmpty(entityClient.ImagePath) ? "-" : entityClient.ImagePath,
                    IsDeleted = entityClient.IsDeleted,
                    BirthYear = entityClient.BirthYear,
                    InsertedDate = entityClient.InsertedDate,
                    IdentityNumber = entityClient.IdentityNumber,
                    Sex = entityClient.Sex ? "Female" : "Male",
                    InserterName = entityClient.Inserter.Name,
                };
                return modelClient;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<Models.Client> EntityClientToModelClientAsList(List<DataAccessLayer.Entities.Client> entityClientList)
        {
            List<Models.Client> clientList = new List<Models.Client>();
            foreach (var element in entityClientList)
            {
                clientList.Add(EntityClientToModelClient(element));
            }
            return clientList;
        }

        public static DataAccessLayer.Entities.Client ModelClientToEntityClient(Models.Client clientModel)
        {
            DataAccessLayer.Entities.Client entityClient = new DataAccessLayer.Entities.Client
            {
                Id = clientModel.Id,
                FirstName = clientModel.FirstName,
                LastName = clientModel.LastName,
                Phone = clientModel.Phone,
                Email = clientModel.Email,
                ImagePath = clientModel.ImagePath,
                IsDeleted = clientModel.IsDeleted,
                BirthYear = clientModel.BirthYear,
                InsertedDate = clientModel.InsertedDate,
                IdentityNumber = clientModel.IdentityNumber,
                Sex = clientModel.Sex == "Male" ? false : true,
                Inserter = DataAccessLayer.Utils.EmployeeUtils.GetEmployeeByName(clientModel.InserterName),
            };
            return entityClient;
        }

        public static List<DataAccessLayer.Entities.Client> ModelClientToEntityClientAsList(List<Models.Client> clientModelList)
        {
            List<DataAccessLayer.Entities.Client> entityClientList = new List<DataAccessLayer.Entities.Client>();
            foreach(var element in clientModelList)
            {
                entityClientList.Add(ModelClientToEntityClient(element));
            }
            return entityClientList;
        }

        //-------------------------------EMPLOYEE-------------------------------------------
        public static Models.Employee EntityEmployeeToModelEmployee(DataAccessLayer.Entities.Employee entityEmployee)
        {
            Models.Employee modelEmployee = new Models.Employee
            {
                Id = entityEmployee.Id,
                Name = entityEmployee.Name,
                Password = entityEmployee.Password,
                IsDeleted = entityEmployee.IsDeleted,
                Department = entityEmployee.Department,
                WorkPlaceName = entityEmployee.WorkPlace.Name,
                Days = entityEmployee.Days,
                EndHour = entityEmployee.EndHour,
                StartHour = entityEmployee.StartHour
            };
            return modelEmployee;
        }

        public static List<Models.Employee> EntityEmployeeToModelEmployeeAsList(List<DataAccessLayer.Entities.Employee> entityEmployeeList)
        {
            List<Models.Employee> modelList = new List<Models.Employee>();
            foreach (var entityType in entityEmployeeList)
            {
                modelList.Add(EntityEmployeeToModelEmployee(entityType));
            }
            return modelList;
        }

        public static DataAccessLayer.Entities.Employee ModelEmployeeToEntityEmployee(Models.Employee modelEmployee)
        {
            DataAccessLayer.Entities.Employee entityEmployee = new DataAccessLayer.Entities.Employee
            {
                Id = modelEmployee.Id,
                Name = modelEmployee.Name,
                IsDeleted = modelEmployee.IsDeleted,
                Password = modelEmployee.Password,
                Department = modelEmployee.Department,
                WorkPlace = DataAccessLayer.Utils.RoomUtils.GetRoomByName(modelEmployee.WorkPlaceName),
                Days = modelEmployee.Days,
                EndHour = modelEmployee.EndHour,
                StartHour = modelEmployee.StartHour,
            };
            return entityEmployee;
        }

        public static List<DataAccessLayer.Entities.Employee> ModelEmployeeToEntityEmployeeAsList(List<Models.Employee> modelEmployeeList)
        {
            List<DataAccessLayer.Entities.Employee> entityEmployeeList = new List<DataAccessLayer.Entities.Employee>();
            foreach(var element in modelEmployeeList)
            {
                entityEmployeeList.Add(ModelEmployeeToEntityEmployee(element));
            }
            return entityEmployeeList;
        }

        //---------------------------------------EVENT-----------------------------------------
        public static Models.Event EntityEventToModelEvent(DataAccessLayer.Entities.Event entityEvent)
        {
            Models.Event modelEvent = new Models.Event
            {
                Id = entityEvent.Id,
                ClientName = entityEvent.Card.FirstName + " " + entityEvent.Card.LastName,
                TicketId = entityEvent.Ticket.Id,
                Date = entityEvent.Date,
                Type = entityEvent.Type,
                RoomName = entityEvent.Room.Name,
                EmployeeName = entityEvent.Inserter.Name
            };

            return modelEvent;
        }

        public static List<Models.Event> EntityEventToModelEventList(List<DataAccessLayer.Entities.Event> entityEventList)
        {
            List<Models.Event> modelList = new List<Models.Event>();
            foreach (var element in entityEventList)
            {
                modelList.Add(EntityEventToModelEvent(element));
            }

            return modelList;
        }

        public static DataAccessLayer.Entities.Event ModelEventToEntityEvent(Models.Event modelEvent)
        {
            DataAccessLayer.Entities.Event entityEvent = new DataAccessLayer.Entities.Event()
            {
                Card = null,
                Date = modelEvent.Date,
                Id = modelEvent.Id,
                Inserter = null,
                Room = null,
                Ticket = null,
                Type = modelEvent.Type
            };

            return entityEvent;
        }

        public static List<DataAccessLayer.Entities.Event> ModelEventToEntityEventAsList(List<Models.Event> modelEventList)
        {
            List<DataAccessLayer.Entities.Event> entityEventList = new List<DataAccessLayer.Entities.Event>();
            foreach(var element in modelEventList)
            {
                entityEventList.Add(ModelEventToEntityEvent(element));
            }

            return entityEventList;
        }

        //----------------------------------------ROOM-------------------------------------------------------
        public static Models.Room EntityRoomToModelRoom(DataAccessLayer.Entities.Room entityRoom)
        {
            Models.Room modelRoom = new Models.Room
            {
                Id = entityRoom.Id,
                Name = entityRoom.Name,
                IsDeleted = entityRoom.IsDeleted
            };
 
            return modelRoom;
        }


        public static List<Models.Room> EntityRoomToModelRoomAsList(List<DataAccessLayer.Entities.Room> entityRoomList)
        {
            List<Models.Room> modelList = new List<Models.Room>();
            foreach (var element in entityRoomList)
            {
                modelList.Add(EntityRoomToModelRoom(element));
            }

            return modelList;
        }

        public static DataAccessLayer.Entities.Room ModelRoomToEntityRoom(Models.Room modelRoom)
        {
            DataAccessLayer.Entities.Room entityRoom = new DataAccessLayer.Entities.Room()
            {
                Id = modelRoom.Id,
                Name = modelRoom.Name,
                IsDeleted = modelRoom.IsDeleted,
            };

            return entityRoom;
        }

        public static List<DataAccessLayer.Entities.Room> ModelRoomToEntityRoomAsList(List<Models.Room> modelRoomList)
        {
            List<DataAccessLayer.Entities.Room> entityRoomList = new List<DataAccessLayer.Entities.Room>();
            foreach(var element in modelRoomList)
            {
                entityRoomList.Add(ModelRoomToEntityRoom(element));
            }

            return entityRoomList;
        }

        //-----------------------------------TICKET--------------------------------------------------------
        public static Models.Ticket EntityTicketToModelTicket(DataAccessLayer.Entities.Ticket entityTicket)
        {
            Models.Ticket modelTicket = new Models.Ticket
            {
                Id = entityTicket.Id,
                ClientId = entityTicket.Card.Id,
                ClientName = entityTicket.Card.FirstName + " " + entityTicket.Card.LastName,
                BuyingDate = entityTicket.BuyingDate,
                StartDate = entityTicket.StartDate,
                LastLoginDate = entityTicket.LastLoginDate,
                LoginsNum = entityTicket.LoginsNum,
                Price = entityTicket.Price,
                EmployeeName = entityTicket.Inserter.Name,
                IsDeleted = entityTicket.IsDeleted,
                TicketName = entityTicket.Type.Name
            };
            modelTicket.EndDate = entityTicket.Type.DayNum != 0 ? modelTicket.StartDate.AddDays(entityTicket.Type.DayNum) : entityTicket.BuyingDate;//mivel muszaj idopontot megadjak ezert berakom a vasarlas pillanatat
            modelTicket.RemaningLoginNum = entityTicket.Type.OccasionNum != 0 ? entityTicket.Type.OccasionNum - modelTicket.LoginsNum : -1;
            return modelTicket;
        }

        public static List<Models.Ticket> EntityTicketLIstToModelTicketAsList(List<DataAccessLayer.Entities.Ticket> entityTicketList)
        {
            List<Models.Ticket> modelTicketList = new List<Models.Ticket>();
            foreach (var entityTicket in entityTicketList)
            {
                modelTicketList.Add(EntityTicketToModelTicket(entityTicket));
            }

            return modelTicketList;
        }

        public static DataAccessLayer.Entities.Ticket ModelTicketToEntityTicket(Models.Ticket modelTicket)
        {
            DataAccessLayer.Entities.Ticket entityTicket = new DataAccessLayer.Entities.Ticket()
            {
                Id = modelTicket.Id,
                Card = null,
                BuyingDate = modelTicket.BuyingDate,
                Inserter = null,
                IsDeleted = modelTicket.IsDeleted,
                LastLoginDate = modelTicket.LastLoginDate,
                LoginsNum = modelTicket.LoginsNum,
                Price = modelTicket.Price,
                StartDate = modelTicket.StartDate,
                Type = null
            };

            return entityTicket;
        }

        public static List<DataAccessLayer.Entities.Ticket> ModelTicketToEntityTicketAsList(List<Models.Ticket> modelTicketList)
        {
            List<DataAccessLayer.Entities.Ticket> entityTicketList = new List<DataAccessLayer.Entities.Ticket>();
            foreach(var element in modelTicketList)
            {
                entityTicketList.Add(ModelTicketToEntityTicket(element));
            }

            return entityTicketList;
        }


        //--------------------------------------TICKET TYPE--------------------------------------------------------------
        public static Models.TicketType EntityTicketTypeToModelTicketType(DataAccessLayer.Entities.TicketType entityTicketType)
        {
            Models.TicketType modelTicketType = new Models.TicketType
            {
                Id = entityTicketType.Id,
                Name = entityTicketType.Name,
                DayNum = entityTicketType.DayNum,
                OccasionNum = entityTicketType.OccasionNum,
                Status = entityTicketType.Status,
                Price = entityTicketType.Price,
                Description = entityTicketType.Description,
                StartHour = entityTicketType.StartHour,
                EndHour = entityTicketType.EndHour
            };

            return modelTicketType;
        }
        public static List<Models.TicketType> EntityTicketLIstToModelTicketTypeAsList(List<DataAccessLayer.Entities.TicketType> entityTicketTypeList)
        {
            List<Models.TicketType> modelTicketTypeList = new List<Models.TicketType>();
            foreach (var element in entityTicketTypeList)
            {
                modelTicketTypeList.Add(EntityTicketTypeToModelTicketType(element));
            }
            return modelTicketTypeList;
        }

        public static DataAccessLayer.Entities.TicketType ModelTicketTypeToEntityTicketType(Models.TicketType modelTicketType)
        {
            DataAccessLayer.Entities.TicketType entityTicketType = new DataAccessLayer.Entities.TicketType()
            {
                Id = modelTicketType.Id,
                DayNum = modelTicketType.DayNum,
                Description = modelTicketType.Description,
                IsDeleted = modelTicketType.IsDeleted,
                Name = modelTicketType.Name,
                OccasionNum = modelTicketType.OccasionNum,
                Price = modelTicketType.Price,
                Status = modelTicketType.Status,
                StartHour = modelTicketType.StartHour,
                EndHour = modelTicketType.EndHour
            };

            return entityTicketType;
        }

        public static List<DataAccessLayer.Entities.TicketType> ModelTicketTypeToEntityTicketTypeAsList(List<Models.TicketType> modelTicketTypeList)
        {
            List<DataAccessLayer.Entities.TicketType> entityTicketTypeList = new List<DataAccessLayer.Entities.TicketType>();
            foreach(var element in modelTicketTypeList)
            {
                entityTicketTypeList.Add(ModelTicketTypeToEntityTicketType(element));
            }

            return entityTicketTypeList;
        }
    }
}