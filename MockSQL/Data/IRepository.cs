using MockSQL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockSQL.Data
{
     public interface IRepository
    {
        void LogIn(string server, string username, string password);
        IEnumerable<Database> GetDatabases();
        IEnumerable<DBEntity> GetDBEntities(Database database, DBEntityType dBEntityType);
        IEnumerable<Parameter> GetParameters(Procedure procedure);
        DataSet CreateDataSet(Database database, string query);

    }
}
