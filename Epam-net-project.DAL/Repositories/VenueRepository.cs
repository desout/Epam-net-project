using System.Collections.Generic;
using System.Data.SqlClient;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;

namespace EpamNetProject.DAL.Repositories
{
    public class VenueRepository : IVenueRepository
    {
        private const string GetAllQuery = @"
                SELECT [Id]
                      ,[Name]
                      ,[Description]
                      ,[Address]
                      ,[Phone]
                  FROM [dbo].[Venue]";

        private const string GetQuery = @"
                SELECT [Id]
                      ,[Name]
                      ,[Description]
                      ,[Address]
                      ,[Phone]
                  FROM [dbo].[Venue]
                WHERE Id= @Id";

        private const string AddQuery = @"
                INSERT INTO [dbo].[Venue]
                           ([Description]
                           ,[Address]
                           ,[Phone]
                           ,[Name])
                     OUTPUT inserted.Id
                     VALUES
                           (@Descr
                           ,@Address
                           ,@Phone
                           ,@Name)";

        private const string RemoveQuery = @"
            DELETE FROM [dbo].[Venue]
            WHERE Id = @Id";

        private readonly string _sqlConnectionString;

        public VenueRepository(string connectionString)
        {
            _sqlConnectionString = connectionString;
        }

        public int Add(Venue entity)
        {
            using (var conn = new SqlConnection(_sqlConnectionString))
            using (var command = new SqlCommand(AddQuery, conn))
            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("@Descr", entity.Description));
                command.Parameters.Add(new SqlParameter("@Address", entity.Address));
                command.Parameters.Add(new SqlParameter("@Phone", entity.Phone));
                command.Parameters.Add(new SqlParameter("@Name", entity.Name));
                return (int) command.ExecuteScalar();
            }
        }

        public Venue Get(int id)
        {
            using (var conn = new SqlConnection(_sqlConnectionString))
            using (var command = new SqlCommand(GetQuery, conn))
            {
                command.Parameters.Add(new SqlParameter("@Id", id));
                conn.Open();
                var tempEvent = (Venue) command.ExecuteScalar();
                return tempEvent;
            }
        }

        public IEnumerable<Venue> GetAll()
        {
            var list = new List<Venue>();
            using (var conn = new SqlConnection(_sqlConnectionString))
            using (var command = new SqlCommand(GetAllQuery, conn))
            {
                conn.Open();
                using (var insertedOutput = command.ExecuteReader())
                {
                    while (insertedOutput.Read())
                        list.Add(new Venue
                        {
                            Id = insertedOutput.GetInt32(0),
                            Name = insertedOutput.GetString(1),
                            Description = insertedOutput.GetString(2),
                            Address = insertedOutput.GetString(3),
                            Phone = insertedOutput.GetString(4)
                        });
                }
            }

            return list;
        }

        public int Remove(int id)
        {
            using (var conn = new SqlConnection(_sqlConnectionString))
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
