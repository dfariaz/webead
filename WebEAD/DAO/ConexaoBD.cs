using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace WebEAD.DAO
{
    public class ConexaoBD
    {
        public string connectionString = string.Empty;
        MySqlConnection connection;

        private static string GetMySQLDbConnectionString()
        {
            return ConfigurationManager.AppSettings.Get("MYSQL_CONNECTION_STRING") ??
                   ConfigurationManager.ConnectionStrings["BDConnection"].ConnectionString;
        }

        public void CriaStringConexao()
        {
            connectionString = GetMySQLDbConnectionString();
        }

        public MySqlConnection AbreConexao()
        {
            CriaStringConexao();
            connection = new MySqlConnection(connectionString);
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
                return connection;
            }
            else { return connection; }
        }

        public MySqlConnection FechaConexao()
        {
            CriaStringConexao();
            connection = new MySqlConnection(connectionString);
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                return connection;
            }
            else
            {
                return connection;
            }
        }

    }
}