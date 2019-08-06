using System.Collections.Generic;
using System.Data.SqlClient;
using DAL.Interfaces;
using DAL.models;

namespace DAL.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private const string GetAllQuery = @"
            SELECT [Id]
            ,[LayoutId]
            ,[Description]
            ,[CoordX]
            ,[CoordY]
            FROM [dbo].[Area]";

        private const string GetQuery = @"
            DECLARE @Id int;
            SELECT [Id]
            ,[LayoutId]
            ,[Description]
            ,[CoordX]
            ,[CoordY]
            FROM [dbo].[Area]
            WHERE Id= @Id";

        private const string AddQuery = @"
            DECLARE @LayoutId int, @Descr varchar(50), @CoordX int, @CoordY int
            INSERT INTO [dbo].[Area]
               ([LayoutId]
               ,[Description]
               ,[CoordX]
               ,[CoordY])
            OUTPUT inserted.Id
            VALUES
               (@LayoutId
               ,@Descr
               ,@CoordX
               ,@CoordY)";

        private const string RemoveQuery = @"
            DECLARE @Id int;
            DELETE FROM [dbo].[Area]
            WHERE Id = @Id";

        private readonly string SqlConnectionString;

        public AreaRepository(string connectionString)
        {
            SqlConnectionString = connectionString;
        }

        public int Add(Area entity)
        {
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand(AddQuery, conn))
            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("@LayoutId", entity.LayoutId));
                command.Parameters.Add(new SqlParameter("@Descr", entity.Description));
                command.Parameters.Add(new SqlParameter("@CoordX", entity.CoordX));
                command.Parameters.Add(new SqlParameter("@CoordY", entity.CoordY));
                return (int) command.ExecuteScalar();
            }
        }

        public Area Get(int id)
        {
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand(GetQuery, conn))
            {
                command.Parameters.Add(new SqlParameter("@Id", id));
                conn.Open();
                var tempEvent = (Area) command.ExecuteScalar();
                return tempEvent;
            }
        }

        public IEnumerable<Area> GetAll()
        {
            var list = new List<Area>();
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand(GetAllQuery, conn))
            {
                conn.Open();
                using (var insertedOutput = command.ExecuteReader())
                {
                    while (insertedOutput.Read())
                        list.Add(new Area
                        {
                            Id = insertedOutput.GetInt32(0),
                            LayoutId = insertedOutput.GetInt32(1),
                            Description = insertedOutput.GetString(2),
                            CoordX = insertedOutput.GetInt32(3),
                            CoordY = insertedOutput.GetInt32(4)
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
