using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using EpamNetProject.DAL.Interfaces;

namespace EpamNetProject.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly string SqlConnectionString;
        protected readonly string TableName;

        protected Repository(string sqlConnectionString)
        {
            SqlConnectionString = sqlConnectionString;
            TableName = typeof(TEntity).ToString();
        }

        public int Add(TEntity entity)
        {
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand("InsertProc", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("@Table_Name", TableName));
                command.Parameters.Add(new SqlParameter("@Values", entity.ToString()));
                command.ExecuteNonQuery();
            }

            return -1;
        }

        public TEntity Get(int id)
        {
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand("SelectById", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("@Table_Name", TableName));
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();
            }

            return null;
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand("SelectAll", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("@Table_Name", TableName));
                command.ExecuteNonQuery();
            }

            return null;
        }

        public int Remove(int id)
        {
            using (var conn = new SqlConnection(SqlConnectionString))
            using (var command = new SqlCommand("RemoveById", conn)
            {
                CommandType = CommandType.StoredProcedure
            })
            {
                conn.Open();
                command.Parameters.Add(new SqlParameter("@Table_Name", TableName));
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();
            }

            return -1;
        }

        public int Update(TEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}