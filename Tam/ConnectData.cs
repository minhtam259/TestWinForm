using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tam
{
    class ConnectData
    {
        public static SqlConnection DataConnection()
        {
            string connectionString = null;
            connectionString = @"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=SinhVienDB;User ID=sa;Password=123";
            SqlConnection cnn = new SqlConnection(connectionString);
            return cnn;
        }
    }
}
