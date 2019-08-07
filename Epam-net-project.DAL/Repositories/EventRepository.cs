using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;

namespace EpamNetProject.DAL.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly string SqlConnectionString;

        public EventRepository(string connectionString)
        {
            SqlConnectionString = connectionString;
        }

        public int Add(Event entity)
        {
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand("EventInsert", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("@Name", entity.Name));
                command.Parameters.Add(new SqlParameter("@Descr", entity.Description));
                command.Parameters.Add(new SqlParameter("@EventDate", entity.EventDate));
                command.Parameters.Add(new SqlParameter("@LayoutId", entity.LayoutId));
                return (int) command.ExecuteScalar();
            }
        }

        public Event Get(int id)
        {
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand("EventSelectById", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                command.Parameters.Add(new SqlParameter("@Id", id));
                conn.Open();
                var tempEvent = (Event) command.ExecuteScalar();
                return tempEvent;
            }
        }

        public IEnumerable<Event> GetAll()
        {
            var list = new List<Event>();
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand("EventSelectAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                using (var insertedOutput = command.ExecuteReader())
                {
                    while (insertedOutput.Read())
                        list.Add(new Event
                        {
                            Id = insertedOutput.GetInt32(0),
                            Name = insertedOutput.GetString(1),
                            Description = insertedOutput.GetString(2),
                            EventDate = insertedOutput.GetDateTime(3),
                            LayoutId = insertedOutput.GetInt32(4)
                        });
                }
            }

            return list;
        }

        public int Remove(int id)
        {
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand("EventDeleteById", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();
            }

            return id;
        }

        public int Update(Event entity)
        {
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand("EventUpdate", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("@Name", entity.Name));
                command.Parameters.Add(new SqlParameter("@Descr", entity.Description));
                command.Parameters.Add(new SqlParameter("@EventDate", entity.EventDate));
                command.Parameters.Add(new SqlParameter("@LayoutId", entity.LayoutId));
                command.Parameters.Add(new SqlParameter("@Id", entity.Id));
                command.ExecuteNonQuery();
            }

            return entity.Id;
        }
    }
}