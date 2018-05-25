using DataAccessLayer.Contexts;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DataAccessLayer.Utils
{
    public class RoomUtils
    {
        public static bool InsertRoom(string name)
        {
            bool result = false;

            using (var ctx = new NorthwindContext())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        ctx.Rooms.Add(new Room { Name = name });
                        ctx.SaveChanges();
                        dbContextTransaction.Commit();
                        result = true;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        result = false;
                    }
                }
            }
            return result; 
        }

        public static bool DeleteRoomFromDatabase(string name)
        {
            bool temp = false;
            using (var ctx = new NorthwindContext())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
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
                            ctx.SaveChanges();
                            temp = true;
                        }
                        else
                        {   //room isnt in the database
                            temp = false;
                        }
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        temp = false;
                    }
                }
            }
            return temp;
        }

        public static bool DeleteRoomFromDatabase(int roomId)
        {
            bool temp = false;

            using (var ctx = new NorthwindContext())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        Room delRoom = (from e in ctx.Rooms
                                        where e.Id == roomId
                                        select e).FirstOrDefault();
                        if (delRoom != null)
                        {
                            ctx.Rooms.Remove(delRoom);
                            ctx.SaveChanges();
                            temp = true;
                        }
                        else
                        {
                            temp = false;
                        }
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        temp = false;
                    }
                }
            }

            return temp;
        }

        public static bool DeleteRoom(int Id)
        {
            bool result = false;

            using (var ctx = new NorthwindContext())
            {
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var query = (from r in ctx.Rooms where r.Id == Id select r).FirstOrDefault();
                        if (!query.IsDeleted)
                        {
                            query.IsDeleted = true;
                            ctx.SaveChanges();
                            result = true;
                        }
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        result = false;
                    }
                }
            }

            return result;
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

        public static Room GetRoomByName(string name)
        {
            Room room;
            using(var ctx = new NorthwindContext())
            {
                room = (from r in ctx.Rooms where r.Name == name select r).FirstOrDefault();
            } 
            return room;
        }

        public static bool UpdateRoom(Room Room)
        {
            bool result = false;
            using(var ctx = new NorthwindContext())
            {
                using(var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        var Query = (from r in ctx.Rooms where r.Id == Room.Id select r).FirstOrDefault();
                        Query.Name = Room.Name;
                        ctx.SaveChanges();
                        dbContextTransaction.Commit();
                        result = true;
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                        result = false;
                    }
                }
            }
            return result;
        }

    }
}
