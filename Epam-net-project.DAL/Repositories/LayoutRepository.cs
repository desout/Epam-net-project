﻿using System.Collections.Generic;
using System.Data.SqlClient;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;

namespace EpamNetProject.DAL.Repositories
{
    public class LayoutRepository : ILayoutRepository
    {
        private const string GetAllQuery = @"
                SELECT [Id]
                  ,[LayoutName]
                  ,[VenueId]
                  ,[Description]
                FROM [dbo].[Layout]";

        private const string GetQuery = @"
                SELECT [Id]
                  ,[LayoutName]
                  ,[VenueId]
                  ,[Description]
                FROM [dbo].[Layout]
                WHERE Id= @Id";

        private const string AddQuery = @"
                INSERT INTO [dbo].[Layout]
                       ([VenueId]
                       ,[Description]
                       ,[LayoutName])
                OUTPUT inserted.Id
                VALUES
                       (@VenueId
                       ,@Descr
                       ,@LayoutName)";

        private const string UpdateQuery = @"
            UPDATE [dbo].[Layout]
               SET [VenueId] = @VenueId
                  ,[LayoutName] = @LayoutName
                  ,[Description] = @Descr
             WHERE Id= @Id";

        private const string RemoveQuery = @"
            DELETE FROM [dbo].[Layout]
            WHERE Id = @Id";

        private readonly string _sqlConnectionString;

        public LayoutRepository(string connectionString)
        {
            _sqlConnectionString = connectionString;
        }

        public int Add(Layout entity)
        {
            using (var conn = new SqlConnection(_sqlConnectionString))
            using (var command = new SqlCommand(AddQuery, conn))
            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("@VenueId", entity.VenueId));
                command.Parameters.Add(new SqlParameter("@Descr", entity.Description));
                command.Parameters.Add(new SqlParameter("@LayoutName", entity.LayoutName));
                return (int) command.ExecuteScalar();
            }
        }

        public Layout Get(int id)
        {
            using (var conn = new SqlConnection(_sqlConnectionString))
            using (var command = new SqlCommand(GetQuery, conn))
            {
                command.Parameters.Add(new SqlParameter("@Id", id));
                conn.Open();
                using (var insertedOutput = command.ExecuteReader())
                {
                    insertedOutput.Read();
                    return new Layout
                    {
                        Id = insertedOutput.GetInt32(0),
                        LayoutName = insertedOutput.GetString(1),
                        VenueId = insertedOutput.GetInt32(2),
                        Description = insertedOutput.GetString(3)
                    };
                }
            }
        }

        public IEnumerable<Layout> GetAll()
        {
            var list = new List<Layout>();
            using (var conn = new SqlConnection(_sqlConnectionString))
            using (var command = new SqlCommand(GetAllQuery, conn))
            {
                conn.Open();
                using (var insertedOutput = command.ExecuteReader())
                {
                    while (insertedOutput.Read())
                        list.Add(new Layout
                        {
                            Id = insertedOutput.GetInt32(0),
                            LayoutName = insertedOutput.GetString(1),
                            VenueId = insertedOutput.GetInt32(2),
                            Description = insertedOutput.GetString(3)
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

        public int Update(Layout entity)
        {
            using (var conn = new SqlConnection(_sqlConnectionString))
            using (var command = new SqlCommand(UpdateQuery, conn))
            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("@VenueId", entity.VenueId));
                command.Parameters.Add(new SqlParameter("@Descr", entity.Description));
                command.Parameters.Add(new SqlParameter("@LayoutName", entity.LayoutName));
                command.Parameters.Add(new SqlParameter("@Id", entity.Id));
                command.ExecuteNonQuery();
            }

            return entity.Id;
        }
    }
}
