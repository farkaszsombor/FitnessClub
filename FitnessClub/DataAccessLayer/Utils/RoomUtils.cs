using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Utils
{
    public class RoomUtils
    {
        public static bool InsertRoom(string name)
        {
            using (var ctx = new NorthwindContext())
            {
                ctx.Rooms.Add(new Room {Name = name});
                return true;
            }
        }

        public static bool DeleteRoomFromDatabase(string name)
        {
            bool temp = false;
            using (var ctx = new NorthwindContext())
            {
                //testing if room already exist
                Room result;
                var query = from r in ctx.Rooms
                                  where r.Name == name
                                  select r;
                result = query.FirstOrDefault();
                if (result != null)
                {   
                    ctx.Rooms.Remove(result);
                    temp = true;
                }
                else
                {   //room isnt in the database
                    temp = false;
                }
            }
            return temp;
        }

        public static bool DeleteRoomFromDatabase(int roomId)
        {
            bool temp = false;

            using (var ctx = new NorthwindContext())
            {
                Room delRoom = (from e in ctx.Rooms
                                 where e.Id == roomId
                                 select e).FirstOrDefault();
                if (delRoom != null)
                {
                    ctx.Rooms.Remove(delRoom);
                    temp = true;
                }
                else
                {
                    temp = false;
                }
            }

            return temp;
        }

        public static bool DeleteRoom(int roomId)
        {
            bool temp = false;

            using (var ctx = new NorthwindContext())
            {
                Room delRoom = (from e in ctx.Rooms
                                where e.Id == roomId
                                select e).FirstOrDefault();
                if (delRoom != null)
                {
                    delRoom.IsDeleted = true;
                    temp = true;
                }
                else
                {
                    temp = false;
                }
            }

            return temp;
        }


        public static List<Room> GetAllRooms()
        {
            List<Room> roomContextList = new List<Room>();
            using(var ctx = new NorthwindContext())
            {
                var query = ctx.Rooms.Select(x => x);
                roomContextList.AddRange(query);
            }
            return roomContextList;
        }
    }
}
