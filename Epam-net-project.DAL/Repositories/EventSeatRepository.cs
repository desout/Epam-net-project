﻿using System.Collections.Generic;
using System.Data.SqlClient;
using EpamNetProject.DAL.Interfaces;
using EpamNetProject.DAL.models;

namespace EpamNetProject.DAL.Repositories
{
    public class EventSeatRepository : IEventSeatRepository
    {
        private const string GetAllQuery = @"
                SELECT [Id]
                    ,[Row]
                    ,[EventAreaId]
                    ,[Number]
                    ,[State]
                FROM [dbo].[EventSeat]";

        private const string GetQuery = @"
                SELECT [Id]
                    ,[Row]
                    ,[EventAreaId]
                    ,[Number]
                    ,[State]
                FROM [dbo].[EventSeat]
                WHERE Id= @Id";

        private const string AddQuery = @"
                INSERT INTO [dbo].[EventSeat]
                       ([Row]
                       ,[EventAreaId]
                       ,[Number]
                       ,[State])
                OUTPUT inserted.Id
                VALUES
                       (@Row
                       ,@EventAreaId
                       ,@Number
                       ,@State)";

        private const string UpdateQuery = @"
                UPDATE [dbo].[EventSeat]
                   SET [Row] = @Row
                      ,[EventAreaId] = @EventAreaId
                      ,[Number] = @Number
                      ,[State] = @State
                 WHERE Id= @Id";

        private const string RemoveQuery = @"
            DELETE FROM [dbo].[EventSeat]
            WHERE Id = @Id";

        private readonly string _sqlConnectionString;

        public EventSeatRepository(string connectionString)
        {
            _sqlConnectionString = connectionString;
        }

        public int Add(EventSeat entity)
        {
            using (var conn = new SqlConnection(_sqlConnectionString))
            using (var command = new SqlCommand(AddQuery, conn))
            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("@Row", entity.Row));
                command.Parameters.Add(new SqlParameter("@Number", entity.Number));
                command.Parameters.Add(new SqlParameter("@EventAreaId", entity.EventAreaId));
                command.Parameters.Add(new SqlParameter("@State", entity.State));
                return (int) command.ExecuteScalar();
            }
        }

        public EventSeat Get(int id)
        {
            using (var conn = new SqlConnection(_sqlConnectionString))
            using (var command = new SqlCommand(GetQuery, conn))
            {
                command.Parameters.Add(new SqlParameter("@Id", id));
                conn.Open();
                using (var insertedOutput = command.ExecuteReader())
                {
                    insertedOutput.Read();
                    return new EventSeat
                    {
                        Id = insertedOutput.GetInt32(0),
                        Row = insertedOutput.GetInt32(1),
                        Number = insertedOutput.GetInt32(2),
                        EventAreaId = insertedOutput.GetInt32(3),
                        State = insertedOutput.GetInt32(4)
                    };
                }
            }
        }

        public IEnumerable<EventSeat> GetAll()
        {
            var list = new List<EventSeat>();
            using (var conn = new SqlConnection(_sqlConnectionString))
            using (var command = new SqlCommand(GetAllQuery, conn))
            {
                conn.Open();
                using (var insertedOutput = command.ExecuteReader())
                {
                    while (insertedOutput.Read())
                        list.Add(new EventSeat
                        {
                            Id = insertedOutput.GetInt32(0),
                            Row = insertedOutput.GetInt32(1),
                            Number = insertedOutput.GetInt32(2),
                            EventAreaId = insertedOutput.GetInt32(3),
                            State = insertedOutput.GetInt32(4)
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

        public int Update(EventSeat entity)
        {
            using (var conn = new SqlConnection(_sqlConnectionString))
            using (var command = new SqlCommand(UpdateQuery, conn))
            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("@Row", entity.Row));
                command.Parameters.Add(new SqlParameter("@Number", entity.Number));
                command.Parameters.Add(new SqlParameter("@EventAreaId", entity.EventAreaId));
                command.Parameters.Add(new SqlParameter("@State", entity.State));
                command.Parameters.Add(new SqlParameter("@Id", entity.Id));
                command.ExecuteNonQuery();
            }

            return entity.Id;
        }
    }
}
