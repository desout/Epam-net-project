using System.Collections.Generic;
using System.Data.SqlClient;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;

namespace EpamNetProject.DAL.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private const string GetAllQuery = @"
                SELECT [Id]
                      ,[AreaId]
                      ,[Row]
                      ,[Number]
                FROM [dbo].[Seat]";

        private const string GetQuery = @"
                SELECT [Id]
                      ,[AreaId]
                      ,[Row]
                      ,[Number]
                FROM [dbo].[Seat]
                WHERE Id= @Id";

        private const string AddQuery = @"
                INSERT INTO [dbo].[Seat]
                       ([AreaId]
                       ,[Row]
                       ,[Number])
                 OUTPUT inserted.Id
                 VALUES
                       (@AreaId
                       ,@Row
                       ,@Number)";

        private const string UpdateQuery = @"
                UPDATE [dbo].[Seat]
                   SET [AreaId] = @AreaId
                      ,[Row] = @Row
                      ,[Number] = @Number
                WHERE Id= @Id";

        private const string RemoveQuery = @"
            DELETE FROM [dbo].[Seat]
            WHERE Id = @Id";

        private readonly string SqlConnectionString;

        public SeatRepository(string connectionString)
        {
            SqlConnectionString = connectionString;
        }

        public int Add(Seat entity)
        {
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand(AddQuery, conn))
            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("@AreaId", entity.AreaId));
                command.Parameters.Add(new SqlParameter("@Row", entity.Row));
                command.Parameters.Add(new SqlParameter("@Number", entity.Number));
                return (int) command.ExecuteScalar();
            }
        }

        public Seat Get(int id)
        {
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand(GetQuery, conn))
            {
                command.Parameters.Add(new SqlParameter("@Id", id));
                conn.Open();
                using (var insertedOutput = command.ExecuteReader())
                {
                    insertedOutput.Read();
                    return new Seat
                    {
                        Id = insertedOutput.GetInt32(0),
                        AreaId = insertedOutput.GetInt32(1),
                        Row = insertedOutput.GetInt32(2),
                        Number = insertedOutput.GetInt32(3)
                    };
                }
            }
        }

        public IEnumerable<Seat> GetAll()
        {
            var list = new List<Seat>();
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand(GetAllQuery, conn))
            {
                conn.Open();
                using (var insertedOutput = command.ExecuteReader())
                {
                    while (insertedOutput.Read())
                        list.Add(new Seat
                        {
                            Id = insertedOutput.GetInt32(0),
                            AreaId = insertedOutput.GetInt32(1),
                            Row = insertedOutput.GetInt32(2),
                            Number = insertedOutput.GetInt32(3)
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

        public int Update(Seat entity)
        {
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand(UpdateQuery, conn))
            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("@AreaId", entity.AreaId));
                command.Parameters.Add(new SqlParameter("@Row", entity.Row));
                command.Parameters.Add(new SqlParameter("@Number", entity.Number));
                command.Parameters.Add(new SqlParameter("@Id", entity.Id));
                command.ExecuteNonQuery();
            }

            return entity.Id;
        }
    }
}
