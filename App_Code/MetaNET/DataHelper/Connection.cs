namespace MetaNET.DataHelper
{
   
    using System;
    using System.Data;
    using System.Data.Odbc;
    using System.Data.OleDb;
    using System.Data.SqlClient;

    public class Connection
    {
        //public static IDbConnection GET_CONN(string ConfigKey)
        //{
        //    return new SqlConnection(global::System.Configuration.ConfigurationManager.ConnectionStrings[ConfigKey].ConnectionString);
        //}
        //public static IDbConnection GET_CONN(string ConfigKey, ConnectionType Type)
        //{
        //    switch (Type)
        //    {
        //        case ConnectionType.OLEDB:
        //            return new OleDbConnection(global::System.Configuration.ConfigurationManager.ConnectionStrings[ConfigKey].ConnectionString);

        //        case ConnectionType.ODBC:
        //            return new OdbcConnection(global::System.Configuration.ConfigurationManager.ConnectionStrings[ConfigKey].ConnectionString);
        //    }
        //    return new SqlConnection(global::System.Configuration.ConfigurationManager.ConnectionStrings[ConfigKey].ConnectionString);
        //}
        //public enum ConnectionType
        //{
        //    SQLCLIENT,
        //    OLEDB,
        //    ODBC
        //}
    }
}

