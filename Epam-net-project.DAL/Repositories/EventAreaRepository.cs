using System.Collections.Generic;
using System.Data.SqlClient;
using DAL.Interfaces;
using DAL.models;

namespace DAL.Repositories
{
    public class EventAreaRepository : IEventAreaRepository
    {
        private const string GetAllQuery = @"
                SELECT [Id]
                  ,[EventId]
                  ,[Description]
                  ,[CoordX]
                  ,[CoordY]
                  ,[Price]
                FROM [dbo].[EventArea]";

        private const string GetQuery = @"
                DECLARE @Id int;
                SELECT [Id]
                  ,[EventId]
                  ,[Description]
                  ,[CoordX]
                  ,[CoordY]
                  ,[Price]
                FROM [dbo].[EventArea]
                WHERE Id= @Id";

        private const string AddQuery = @"
            DECLARE @EventId int, @Descr varchar(50), @CoordX int, @CoordY int, @Price decimal(18,0)
            INSERT INTO [dbo].[EventArea]
               ([EventId]
               ,[Description]
               ,[CoordX]
               ,[CoordY]
               ,[Price])
            OUTPUT inserted.Id
            VALUES
               (@EventId
               ,@Descr
               ,@CoordX
               ,@CoordY
               ,@Price)";

        private const string RemoveQuery = @"
            DECLARE @Id int;
            DELETE FROM [dbo].[EventArea]
            WHERE Id = @Id";

        private readonly string SqlConnectionString;

        public EventAreaRepository(string connectionString)
        {
            SqlConnectionString = connectionString;
        }

        public int Add(EventArea entity)
        {
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand(AddQuery, conn))
            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("@EventId", entity.EventId));
                command.Parameters.Add(new SqlParameter("@Descr", entity.Description));
                command.Parameters.Add(new SqlParameter("@CoordX", entity.CoordX));
                command.Parameters.Add(new SqlParameter("@CoordY", entity.CoordY));
                command.Parameters.Add(new SqlParameter("@Price", entity.Price));
                return (int) command.ExecuteScalar();
            }
        }

        public EventArea Get(int id)
        {
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand(GetQuery, conn))
            {
                command.Parameters.Add(new SqlParameter("@Id", id));
                conn.Open();
                var tempEvent = (EventArea) command.ExecuteScalar();
                return tempEvent;
            }
        }

        public IEnumerable<EventArea> GetAll()
        {
            var list = new List<EventArea>();
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand(GetAllQuery, conn))
            {
                conn.Open();
                using (var insertedOutput = command.ExecuteReader())
                {
                    while (insertedOutput.Read())
                        list.Add(new EventArea
                        {
                            Id = insertedOutput.GetInt32(0),
                            EventId = insertedOutput.GetInt32(1),
                            Description = insertedOutput.GetString(2),
                            CoordX = insertedOutput.GetInt32(3),
                            CoordY = insertedOutput.GetInt32(4),
                            Price = insertedOutput.GetDecimal(5)
                        });
                }
            }

            return list;
        }

        public int Remove(int id)
        {
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand(RemoveQuery, conn))
            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();
            }

            return id;
        }
    }
}
