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
            catch
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
                WorkPlaceName = entityEmployee.WorkPlace.Name
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
                WorkPlace = null,
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
            FitnessClub.Models.Event modelEvent = new FitnessClub.Models.Event();

            modelEvent.Id = entityEvent.Id;
            modelEvent.ClientName = entityEvent.Card.FirstName + " " + entityEvent.Card.LastName;
            modelEvent.TicketId = entityEvent.Ticket.Id;
            modelEvent.Date = entityEvent.Date;
            modelEvent.Type = entityEvent.Type;
            modelEvent.RoomName = entityEvent.Room.Name;
            modelEvent.EmployeeName = entityEvent.Inserter.Name;

            return modelEvent;
        }
        public static List<FitnessClub.Models.Event> EntityEventToModelEventList(List<DataAccessLayer.Entities.Event> entityTypeList)
        {
            List<FitnessClub.Models.Event> modelList = new List<FitnessClub.Models.Event>();
            foreach (var entityType in entityTypeList)
            {
                modelList.Add(EntityEventToModelEvent(entityType));
            }

            return modelList;
        }



        public static FitnessClub.Models.Room EntityRoomToModelRoom(DataAccessLayer.Entities.Room entityRoom)
        {
            FitnessClub.Models.Room modelRoom = new FitnessClub.Models.Room();

            modelRoom.Id = entityRoom.Id;
            modelRoom.Name = entityRoom.Name;
            modelRoom.IsDeleted = entityRoom.IsDeleted;

            return modelRoom;
        }


        public static List<FitnessClub.Models.Room> EntityRoomToModelRoomList(List<DataAccessLayer.Entities.Room> entityTypeList)
        {
            List<FitnessClub.Models.Room> modelList = new List<FitnessClub.Models.Room>();
            foreach (var entityType in entityTypeList)
            {
                modelList.Add(EntityRoomToModelRoom(entityType));
            }

            return modelList;
        }



        public static FitnessClub.Models.Ticket EntityTicketToModelTicket(DataAccessLayer.Entities.Ticket entityTicket)
        {
            FitnessClub.Models.Ticket modelTicket = new FitnessClub.Models.Ticket();

            modelTicket.Id = entityTicket.Id;
            modelTicket.ClientId = entityTicket.Card.Id;
            modelTicket.ClientName = entityTicket.Card.FirstName + " " + entityTicket.Card.LastName;
            modelTicket.BuyingDate = entityTicket.BuyingDate;
            modelTicket.StartDate = entityTicket.StartDate;
            modelTicket.LastLoginDate = entityTicket.LastLoginDate;
            modelTicket.LoginsNum = entityTicket.LoginsNum;
            modelTicket.Price = entityTicket.Price;
            modelTicket.EmployeeName = entityTicket.Inserter.Name;
            modelTicket.IsDeleted = entityTicket.IsDeleted;
            modelTicket.TicketName = entityTicket.Type.Name;
            modelTicket.EndDate = entityTicket.Type.DayNum != 0 ? modelTicket.StartDate.AddDays(entityTicket.Type.DayNum) : entityTicket.BuyingDate;//mivel muszaj idopontot megadjak ezert berakom a vasarlas pillanatat
            modelTicket.RemaningLoginNum = entityTicket.Type.OccasionNum != 0 ? entityTicket.Type.OccasionNum - modelTicket.LoginsNum : -1;

            return modelTicket;
        }
        public static List<FitnessClub.Models.Ticket> EntityTicketLIstInToModelTicketList(List<DataAccessLayer.Entities.Ticket> entityTicketList)
        {
            List<FitnessClub.Models.Ticket> modelTicketList = new List<FitnessClub.Models.Ticket>();
            foreach (var entityTicket in entityTicketList)
            {
                modelTicketList.Add(EntityTicketToModelTicket(entityTicket));
            }

            return modelTicketList;
        }


        public static FitnessClub.Models.TicketType EntityTicketTypeToModelTicketType(DataAccessLayer.Entities.TicketType entityTicketType)
        {
            FitnessClub.Models.TicketType modelTicketType = new FitnessClub.Models.TicketType();

            modelTicketType.Id = entityTicketType.Id;
            modelTicketType.Name = entityTicketType.Name;
            modelTicketType.DayNum = entityTicketType.DayNum;
            modelTicketType.OccasionNum = entityTicketType.OccasionNum;
            modelTicketType.Status = entityTicketType.Status;
            modelTicketType.Price = entityTicketType.Price;
            modelTicketType.Description = entityTicketType.Description;

            return modelTicketType;
        }
        public static List<FitnessClub.Models.TicketType> EntityTicketLIstInToModelTicketTypeList(List<DataAccessLayer.Entities.TicketType> entityTicketTypeList)
        {
            List<FitnessClub.Models.TicketType> modelTicketTypeList = new List<FitnessClub.Models.TicketType>();
            foreach (var entityTicketType in entityTicketTypeList)
            {
                modelTicketTypeList.Add(EntityTicketTypeToModelTicketType(entityTicketType));
            }
            return modelTicketTypeList;
        }


    }
}