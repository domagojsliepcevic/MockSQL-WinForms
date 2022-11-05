using MockSQL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockSQL.Data
{
    internal class SqlRepository : IRepository
    {

        private const string ConnectionString = "Server={0};Uid={1};Pwd={2}";
        private const string SelectDatabases = "SELECT name As Name FROM sys.databases";
        private const string SelectEntities = "SELECT TABLE_SCHEMA AS [Schema], TABLE_NAME AS Name FROM {0}.INFORMATION_SCHEMA.{1}S";
        private const string SelectProcedures = "SELECT SPECIFIC_NAME as Name, ROUTINE_DEFINITION as Definition FROM {0}.INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE'";
        private const string SelectColumns = "SELECT COLUMN_NAME as Name, DATA_TYPE as DataType FROM {0}.INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{1}'";
        private const string SelectProcedureParameters = "SELECT PARAMETER_NAME as Name, PARAMETER_MODE as Mode, DATA_TYPE as DataType FROM {0}.INFORMATION_SCHEMA.PARAMETERS WHERE SPECIFIC_NAME='{1}'";
        private const string SelectQuery = "SELECT * FROM {0}.{1}.{2}";

        private string cs;

        public void LogIn(string server, string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(string.Format(ConnectionString, server, username, password)))
            {

                cs = connection.ConnectionString;
                connection.Open();
            }
        }

        public IEnumerable<Database> GetDatabases()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = SelectDatabases;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new Database
                            {
                                Name = dr[nameof(Database.Name)].ToString()
                            };
                        }
                    }
                }
            }
        }


        public IEnumerable<DBEntity> GetDBEntities(Database database,DBEntityType dBEntityType)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = String.Format(SelectEntities,database.Name,dBEntityType.ToString());
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new DBEntity
                            {
                                Name = dr[nameof(DBEntity.Name)].ToString(),
                                Schema = dr[nameof(DBEntity.Name)].ToString(),
                                Database=database
                            };
                        }
                    }
                }
            }
        }
        public IEnumerable<Parameter> GetParameters(Procedure procedure)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = String.Format(SelectProcedureParameters, procedure.Database.Name, procedure.Name);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            yield return new Parameter
                            {
                                Name = dr[nameof(Parameter.Name)].ToString(),
                                Mode = dr[nameof(Parameter.Mode)].ToString(),
                                DataType = dr[nameof(Parameter.DataType)].ToString()
                            };
                        }
                    }
                }
            }
        }

        public DataSet CreateDataSet(Database database, string query)
        {
            string csdata = cs + $";Database={database.Name}";
            
            using (SqlConnection con = new SqlConnection(csdata))
            {
             

                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, csdata);
                DataSet ds = new DataSet();
                sqlDataAdapter.Fill(ds);

                return ds;
            }
        }

    }
}
