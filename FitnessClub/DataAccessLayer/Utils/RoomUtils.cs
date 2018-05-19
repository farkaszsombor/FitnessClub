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
