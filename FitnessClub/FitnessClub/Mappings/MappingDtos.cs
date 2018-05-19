using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FitnessClub.Mappings
{
    public class MappingDtos
    {
        public static FitnessClub.Models.Client EntityClientToModelClient(DataAccessLayer.Entities.Client entityClient)
        {

            FitnessClub.Models.Client modelClient = new FitnessClub.Models.Client ();

            modelClient.Id = entityClient.Id;
            modelClient.FirstName = entityClient.FirstName;
            modelClient.LastName = entityClient.LastName;
            modelClient.Phone = entityClient.Phone;
            modelClient.Email = entityClient.Email;
            modelClient.ImagePath = entityClient.ImagePath;
            modelClient.IsDeleted = entityClient.IsDeleted;
            modelClient.BirthYear = entityClient.BirthYear;
            modelClient.InsertedDate = entityClient.InsertedDate;
            modelClient.IdentityNumber = entityClient.IdentityNumber;
            modelClient.Sex = entityClient.Sex;
            modelClient.InserterName = entityClient.Inserter.Name;

            return modelClient;
        }
        
        public static FitnessClub.Models.Employee EntityEmployeeToModelEmployee(DataAccessLayer.Entities.Employee entityEmployee)
        {
            FitnessClub.Models.Employee modelEmployee = new FitnessClub.Models.Employee();

            modelEmployee.Id = entityEmployee.Id;
            modelEmployee.Name = entityEmployee.Name;
            modelEmployee.Password = entityEmployee.Password;
            modelEmployee.IsDeleted = entityEmployee.IsDeleted;
            modelEmployee.Department = entityEmployee.Department;
            modelEmployee.WorkPlaceName = entityEmployee.WorkPlace.Name;

            return modelEmployee;
        }

        public static FitnessClub.Models.Event EntityEventToModelEvent(DataAccessLayer.Entities.Event entityEvent)
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

        public static FitnessClub.Models.Room EntityRoomToModelRoom(DataAccessLayer.Entities.Room entityRoom)
        {
            FitnessClub.Models.Room modelRoom = new FitnessClub.Models.Room();

            modelRoom.Id = entityRoom.Id;
            modelRoom.Name = entityRoom.Name;
            modelRoom.IsDeleted = entityRoom.IsDeleted;

            return modelRoom;
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

            return modelTicket;
        }

        public static FitnessClub.Models.TicketType EntityTicketTypeToModelTicketType (DataAccessLayer.Entities.TicketType entityTicketType)
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

    }
}