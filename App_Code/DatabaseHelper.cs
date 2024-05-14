using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using log4net;
using System.Collections;
public class DatabaseHelper : IDisposable
{
	private string strConnectionString;
	private DbConnection objConnection;
	private DbCommand objCommand;
	private DbProviderFactory objFactory = null;
	private bool boolHandleErrors;
	private string strLastError;
	private bool boolLogError;
	private string strLogFile;
	public string sql_query;

	public int TotalRow;

	protected ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
	public DatabaseHelper()
	{
		string strConnect = "";
        strConnect = global::System.Configuration.ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
		this.connectDB(strConnect, Providers.SqlServer);
	}


	public DatabaseHelper(string connectionstring, Providers provider = Providers.SqlServer)
	{
		this.connectDB(connectionstring, provider);
	}
	public DataRow GetDataRow(string pCommandText)
	{
		try
		{
			DataTable dt = new DataTable();
			DataRow mRow;
			dt = ExecuteDataTable(pCommandText);
			//dt = this.
			if (dt.Rows.Count != 0)
			{
				mRow = dt.Rows[0];
			}
			else
			{
				mRow = null;
			}
			dt.Dispose();
			return mRow;
		}
		catch
		{
			return null;
		}
	}
	public DataRow GetDataRowAuto(string table, bool f = false)
	{
		DataRow dr = null;
		if (!f)
			return dr;
		dr = Global.db.GetDataRow(Global.db.SQL_Query(table, "*", Global.db.sql_query));
		sql_query = "";
		return dr;
	}
	/// <summary>
	/// tra ve gia tri string cua fieldname duoc dua vao, neu khong co se tra ve chuoi rong
	/// </summary>
	/// <param name="pSQLCommand">Command string</param>
	/// <param name="pFieldReturn">Ten cot lay gia tri traa ve</param>
	/// <returns>tra ve gia tri string cua fieldname duoc dua vao, neu khong co se tra ve chuoi rong</returns>
	public string LookUpTable(string pSQLCommand, string pFieldReturn)
	{
		try
		{
			DataRow dtRow;
			dtRow = GetDataRow(pSQLCommand);
			if (dtRow != null)
			{
				return Convert.ToString(dtRow[pFieldReturn]).Trim();
			}
			return "";
		}
		catch (SqlException Ex)
		{
			return "";
		}
	}
	private void connectDB(string connectionstring, Providers provider)
	{
		try
		{
			strConnectionString = connectionstring;
			switch (provider)
			{
				case Providers.SqlServer:
					objFactory = SqlClientFactory.Instance;
					break;
				case Providers.OleDb:
					objFactory = OleDbFactory.Instance;
					break;
				case Providers.ODBC:
					objFactory = OdbcFactory.Instance;
					break;
				case Providers.ConfigDefined:
					string providername = ConfigurationManager.ConnectionStrings["connectionstring"].ProviderName;
					switch (providername)
					{
						case "System.Data.SqlClient":
							objFactory = SqlClientFactory.Instance;
							break;
						case "System.Data.OleDb":
							objFactory = OleDbFactory.Instance;
							break;
						case "System.Data.Odbc":
							objFactory = OdbcFactory.Instance;
							break;
					}
					break;
			}
			objConnection = objFactory.CreateConnection();
			objCommand = objFactory.CreateCommand();
			objCommand.CommandTimeout = 200;
			objConnection.ConnectionString = strConnectionString;
			objCommand.Connection = objConnection;
			objCommand.Parameters.Clear();
		}
		catch (Exception ex)
		{
			log.Fatal(ex.ToString());
		}
	}
	public DatabaseHelper(Providers provider)
		: this(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString, provider)
	{
	}
	public DatabaseHelper(string connectionstring)
		: this(connectionstring, Providers.SqlServer)
	{
	}
	/*
	public DatabaseHelper()
	: this(ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString, Providers.ConfigDefined)
	{
	}
	*/
	public bool HandleErrors
	{
		get
		{
			return boolHandleErrors;
		}
		set
		{
			boolHandleErrors = value;
		}
	}
	public string LastError
	{
		get
		{
			return strLastError;
		}
	}
	public bool LogErrors
	{
		get
		{
			return boolLogError;
		}
		set
		{
			boolLogError = value;
		}
	}
	public string LogFile
	{
		get
		{
			return strLogFile;
		}
		set
		{
			strLogFile = value;
		}
	}
	public int AddParameter(string name, object value)
	{
		DbParameter p = objFactory.CreateParameter();
		p.ParameterName = name;
		p.Value = value;
		return objCommand.Parameters.Add(p);
	}
	public int AddParameter(string name, DbType paraType, object value)
	{
		DbParameter p = objFactory.CreateParameter();
		p.ParameterName = name;
		p.Value = value;
		p.DbType = paraType;
		return objCommand.Parameters.Add(p);
	}
	public int AddParameter(string name, DbType paraType, int Size, object value)
	{
		DbParameter p = objFactory.CreateParameter();
		p.ParameterName = name;
		p.Value = value;
		p.DbType = paraType;
		p.Size = Size;
		return objCommand.Parameters.Add(p);
	}
	public int AddParameter(DbParameter parameter)
	{
		if (objCommand != null)
			return objCommand.Parameters.Add(parameter);
		else
			return -1;
	}


	public void ClearParameter()
	{
		if (objCommand != null)
		{
			objCommand.Parameters.Clear();
		}
	}


	public Object GetParameterValue(String pName)
	{
		if (objCommand != null)
			return objCommand.Parameters[pName].Value;
		else
			return null;
	}
	public DbCommand Command
	{
		get
		{
			return objCommand;
		}
	}
	public DbParameter Parameter
	{
		get
		{
			return objFactory.CreateParameter();
		}
	}
	public void BeginTransaction()
	{
		if (objConnection.State == System.Data.ConnectionState.Closed)
		{
			objConnection.Open();
		}
		objCommand.Transaction = objConnection.BeginTransaction();
	}
	public void CommitTransaction()
	{
		if (objCommand != null)
		{
			objCommand.Transaction.Commit();
			objConnection.Close();
		}
	}
	public void RollbackTransaction()
	{

		objCommand.Transaction.Rollback();
		objConnection.Close();



	}
	public int ExecuteNonQuery(string query)
	{
		return ExecuteNonQuery(query, CommandType.Text, ConnectionState.CloseOnExit);
	}
	public int ExecuteNonQuery(string query, CommandType commandtype)
	{
		return ExecuteNonQuery(query, commandtype, ConnectionState.CloseOnExit);
	}
	public int ExecuteNonQuery(string query, ConnectionState connectionstate)
	{
		return ExecuteNonQuery(query, CommandType.Text, connectionstate);
	}
	public int ExecuteNonQuery(string query, CommandType commandtype, ConnectionState connectionstate)
	{
		objCommand.CommandText = query;
		objCommand.CommandType = commandtype;
		int i = -1;
		try
		{
			if (objConnection.State == System.Data.ConnectionState.Closed)
			{
				objConnection.Open();
			}
			i = objCommand.ExecuteNonQuery();
		}
		catch (Exception ex)
		{
			HandleExceptions(ex);
		}
		finally
		{
			if (connectionstate == ConnectionState.CloseOnExit)
			{
				objConnection.Close();
			}
		}
		return i;
	}
	public object ExecuteNonQuery(string query, CommandType commandtype, ConnectionState connectionstate, String outParameter)
	{
		objCommand.CommandText = query;
		objCommand.CommandType = commandtype;
		object i = null;
		try
		{
			if (objConnection.State == System.Data.ConnectionState.Closed)
			{
				objConnection.Open();
			}
			objCommand.ExecuteNonQuery();
			i = objCommand.Parameters[outParameter].Value;
		}
		catch (Exception ex)
		{
			HandleExceptions(ex);
		}
		finally
		{
			objCommand.Parameters.Clear();
			if (connectionstate == ConnectionState.CloseOnExit)
			{
				objConnection.Close();
			}
		}
		return i;
	}
	public object ExecuteScalar(string query)
	{
		return ExecuteScalar(query, CommandType.Text, ConnectionState.CloseOnExit);
	}
	public object ExecuteScalar(string query, CommandType commandtype)
	{
		return ExecuteScalar(query, commandtype, ConnectionState.CloseOnExit);
	}
	public object ExecuteScalar(string query, ConnectionState connectionstate)
	{
		return ExecuteScalar(query, CommandType.Text, connectionstate);
	}
	public object ExecuteScalar(string query, CommandType commandtype, ConnectionState connectionstate)
	{
		objCommand.CommandText = query;
		objCommand.CommandType = commandtype;
		object o = null;
		try
		{
			if (objConnection.State == System.Data.ConnectionState.Closed)
			{
				objConnection.Open();
			}
			o = objCommand.ExecuteScalar();
		}
		catch (Exception ex)
		{
			HandleExceptions(ex);
		}
		finally
		{
			objCommand.Parameters.Clear();
			if (connectionstate == ConnectionState.CloseOnExit)
			{
				objConnection.Close();
			}
		}
		return o;
	}
	public DbDataReader ExecuteReader(string query)
	{
		return ExecuteReader(query, CommandType.Text, ConnectionState.CloseOnExit);
	}
	public DbDataReader ExecuteReader(string query, CommandType commandtype)
	{
		return ExecuteReader(query, commandtype, ConnectionState.CloseOnExit);
	}
	public DbDataReader ExecuteReader(string query, ConnectionState connectionstate)
	{
		return ExecuteReader(query, CommandType.Text, connectionstate);
	}
	public DbDataReader ExecuteReader(string query, CommandType commandtype, ConnectionState connectionstate)
	{
		objCommand.CommandText = query;
		objCommand.CommandType = commandtype;
		DbDataReader reader = null;
		try
		{
			if (objConnection.State == System.Data.ConnectionState.Closed)
			{
				objConnection.Open();
			}
			if (connectionstate == ConnectionState.CloseOnExit)
			{
				reader = objCommand.ExecuteReader(CommandBehavior.CloseConnection);
			}
			else
			{
				reader = objCommand.ExecuteReader();
			}
		}
		catch (Exception ex)
		{
			HandleExceptions(ex);
		}
		finally
		{
			objCommand.Parameters.Clear();
		}
		return reader;
	}
	public DataTable ExecuteDataTable(string query)
	{
		return ExecuteDataTable(query, CommandType.Text, ConnectionState.CloseOnExit);
	}
	public DataTable ExecuteDataTable(string query, bool cache = false)
	{
		return ExecuteDataTable(query, CommandType.Text, ConnectionState.CloseOnExit, cache);
	}
	public DataRow ExecuteDataRow(string query)
	{
		DataTable dt = ExecuteDataTable(query, CommandType.Text, ConnectionState.CloseOnExit);
		if (dt != null && dt.Rows.Count > 0)
			return dt.Rows[0];
		else return null;
	}
	public DataRow ExecuteDataRow(string query, CommandType commandtype)
	{
		DataTable dt = ExecuteDataTable(query, commandtype, ConnectionState.CloseOnExit);
		if (dt != null && dt.Rows.Count > 0)
			return dt.Rows[0];
		else return null;
	}
	public DataRow ExecuteDataRow(string query, CommandType commandtype, ConnectionState connectionstate)
	{
		DataTable dt = ExecuteDataTable(query, commandtype, connectionstate);
		if (dt != null && dt.Rows.Count > 0)
			return dt.Rows[0];
		else return null;
	}
	public DataTable ExecuteDataTable(string query, CommandType commandtype)
	{
		return ExecuteDataTable(query, commandtype, ConnectionState.CloseOnExit);
	}
	public DataTable ExecuteDataTable(string query, CommandType commandtype = CommandType.Text, ConnectionState connectionstate = ConnectionState.CloseOnExit)
	{
		DbDataAdapter adapter = objFactory.CreateDataAdapter();
		objCommand.CommandText = query;
		objCommand.CommandType = commandtype;
		adapter.SelectCommand = objCommand;
		DataTable dt = new DataTable();
		try
		{
			adapter.Fill(dt);
		}
		catch (Exception ex)
		{
			HandleExceptions(ex);
		}
		finally
		{
			objCommand.Parameters.Clear();
			if (connectionstate == ConnectionState.CloseOnExit)
			{
				if (objConnection.State == System.Data.ConnectionState.Open)
				{
					objConnection.Close();
				}
			}
		}
		return dt;
	}
	public DataTable ExecuteDataTable(string query, CommandType commandtype = CommandType.Text, ConnectionState connectionstate = ConnectionState.CloseOnExit, bool cache = false)
	{
		DbDataAdapter adapter = objFactory.CreateDataAdapter();
		objCommand.CommandText = query;
		objCommand.CommandType = commandtype;
		adapter.SelectCommand = objCommand;
		DataTable dt = new DataTable();
		try
		{
			string key = Utils.Zip(query);
			if ((dt = CacheUtitl.Get<DataTable>(key)) == null)
			{

				dt = new DataTable();
				adapter.Fill(dt);
				CacheUtitl.Set<DataTable>(key, dt);

			}

		}
		catch (Exception ex)
		{
			HandleExceptions(ex);
		}
		finally
		{
			objCommand.Parameters.Clear();
			if (connectionstate == ConnectionState.CloseOnExit)
			{
				if (objConnection.State == System.Data.ConnectionState.Open)
				{
					objConnection.Close();
				}
			}
		}
		return dt;
	}

	//public DataTable ExecuteDataTable(string query, CommandType commandtype, ConnectionState connectionstate)
	//{
	//    ClearParameter();
	//    AddParameter("@SQL", query);
	//    DbDataAdapter adapter = objFactory.CreateDataAdapter();
	//    objCommand.CommandText = "SQLEXECUTE";
	//    objCommand.CommandType = CommandType.StoredProcedure;
	//    adapter.SelectCommand = objCommand;
	//    DataTable dt = new DataTable();
	//    try
	//    {
	//        adapter.Fill(dt);
	//    }
	//    catch (Exception ex)
	//    {
	//        HandleExceptions(ex);
	//    }
	//    finally
	//    {
	//        objCommand.Parameters.Clear();
	//        if (connectionstate == ConnectionState.CloseOnExit)
	//        {
	//            if (objConnection.State == System.Data.ConnectionState.Open)
	//            {
	//                objConnection.Close();
	//            }
	//        }
	//    }
	//    return dt;
	//}



	public DataSet ExecuteDataSet(string query)
	{
		return ExecuteDataSet(query, CommandType.Text, ConnectionState.CloseOnExit);
	}
	public DataSet ExecuteDataSet(string query, CommandType commandtype)
	{
		return ExecuteDataSet(query, commandtype, ConnectionState.CloseOnExit);
	}
	public DataSet ExecuteDataSet(string query, ConnectionState connectionstate)
	{
		return ExecuteDataSet(query, CommandType.Text, connectionstate);
	}
	public DataSet ExecuteDataSet(string query, CommandType commandtype, ConnectionState connectionstate)
	{
		DbDataAdapter adapter = objFactory.CreateDataAdapter();
		objCommand.CommandText = query;
		objCommand.CommandType = commandtype;
		adapter.SelectCommand = objCommand;
		DataSet ds = new DataSet();
		try
		{
			adapter.Fill(ds);
		}
		catch (Exception ex)
		{
			HandleExceptions(ex);
		}
		finally
		{
			objCommand.Parameters.Clear();
			if (connectionstate == ConnectionState.CloseOnExit)
			{
				if (objConnection.State == System.Data.ConnectionState.Open)
				{
					objConnection.Close();
				}
			}
		}
		return ds;
	}
	public void HandleExceptions(Exception ex)
	{
		if (LogErrors)
		{
			//WriteToLog(ex.Message);
			log.Error(ex.Message);
		}
		if (HandleErrors)
		{
			strLastError = ex.Message;
		}
		else
		{
			//   throw ex;
		}
	}
	public void WriteToLog(string msg)
	{
		StreamWriter writer = File.AppendText(LogFile);
		writer.WriteLine(DateTime.Now.ToString() + " - " + msg);
		writer.Close();
	}
	public void Dispose()
	{
		objConnection.Close();
		objConnection.Dispose();
		objCommand.Dispose();
	}
	public DataSet GetSchema(DatabaseObjects type)
	{
		DataSet ds = new DataSet();
		try
		{
			if (objConnection.State == System.Data.ConnectionState.Closed)
			{
				objConnection.Open();
			}
			ds.Tables.Add(objConnection.GetSchema(type.ToString()));
		}
		catch (Exception ex)
		{
			HandleExceptions(ex);
		}
		finally
		{
			if (objConnection.State == System.Data.ConnectionState.Open)
			{
				objConnection.Close();
			}
		}
		return ds;
	}
	public DataSet GetTables()
	{
		DataSet ds = new DataSet();
		try
		{
			if (objConnection.State == System.Data.ConnectionState.Closed)
			{
				objConnection.Open();
			}
			ds.Tables.Add(objConnection.GetSchema(DatabaseObjects.Tables.ToString(), new string[] { null, null, null, "TABLE" }));
		}
		catch (Exception ex)
		{
			HandleExceptions(ex);
		}
		finally
		{
			if (objConnection.State == System.Data.ConnectionState.Open)
			{
				objConnection.Close();
			}
		}
		return ds;
	}
	public DataSet GetColumns(string tableName)
	{
		DataSet ds = new DataSet();
		try
		{
			if (objConnection.State == System.Data.ConnectionState.Closed)
			{
				objConnection.Open();
			}
			ds.Tables.Add(objConnection.GetSchema(DatabaseObjects.Columns.ToString(), new string[] { null, null, tableName, null }));
		}
		catch (Exception ex)
		{
			HandleExceptions(ex);
		}
		finally
		{
			if (objConnection.State == System.Data.ConnectionState.Open)
			{
				objConnection.Close();
			}
		}
		return ds;
	}
	public DbType ConvertDataType(string strTypeName)
	{
		DbType TypeReturn = new DbType();
		switch (strTypeName.Trim().ToUpper())
		{
			case "NVARCHAR":
				TypeReturn = DbType.String;
				break;
			case "DATETIME":
				TypeReturn = DbType.DateTime;
				break;
			case "VARCHAR":
				TypeReturn = DbType.String;
				break;
			case "BIT":
				TypeReturn = DbType.Boolean;
				break;
			case "INT":
				TypeReturn = DbType.Int32;
				break;
			case "MONEY":
				TypeReturn = DbType.Currency;
				break;
			case "REAL":
				TypeReturn = DbType.Double;
				break;
			case "NUMERIC":
				TypeReturn = DbType.Decimal;
				break;
			case "SMALLINT":
				TypeReturn = DbType.Int16;
				break;
			case "TINYINT":
				TypeReturn = DbType.Byte;
				break;
			case "FLOAT":
				TypeReturn = DbType.Decimal;
				break;
			default:
				TypeReturn = DbType.String;
				break;
		}
		return TypeReturn;
	}
	public bool DeleteById(int value, string strTableName, bool isDelete)
	{
		bool result = false;
		this.BeginTransaction();
		try
		{
			string Query = "";
			if (isDelete == true)
			{
				Query = "DELETE FROM [" + strTableName + "] WHERE Id=" + value;
			}
			else
			{
				Query = "UPDATE [" + strTableName + "] SET Status = 0, UpdateDate = GetDate() WHERE Id=" + value;
			}
			//this.AddParameter("Id", DbType.Int32, Id);
			//this.AddParameter("TableName", DbType.String, tableName);
			int iRecord = this.ExecuteNonQuery(Query, ConnectionState.KeepOpen);
			result = iRecord == 1 ? true : false;
		}
		catch (Exception)
		{
			this.RollbackTransaction();
			return result = false;
		}
		this.CommitTransaction();
		return result;
		/*Ex
		DatabaseHelper DB = new DatabaseHelper();
		bool result = DB.DeleteById(2, "Test");
		Utils.ShowMessageBox(this.Page, "ket qua" + result);
		* */
	}
	public bool DeleteById(string columname, int value, string strTableName, bool isDelete)
	{
		bool result = false;
		this.BeginTransaction();
		try
		{
			string Query = "";
			if (isDelete == true)
			{
				Query = "DELETE FROM [" + strTableName + "] WHERE " + columname + "=" + value;
			}
			else
			{
				Query = "UPDATE [" + strTableName + "] SET Status = 0, UpdateDate = GetDate() WHERE Id=" + value;
			}
			//this.AddParameter("Id", DbType.Int32, Id);
			//this.AddParameter("TableName", DbType.String, tableName);
			int iRecord = this.ExecuteNonQuery(Query, ConnectionState.KeepOpen);
			result = iRecord == 1 ? true : false;
		}
		catch (Exception)
		{
			this.RollbackTransaction();
			return result = false;
		}
		this.CommitTransaction();
		return result;
		/*Ex
		DatabaseHelper DB = new DatabaseHelper();
		bool result = DB.DeleteById(2, "Test");
		Utils.ShowMessageBox(this.Page, "ket qua" + result);
		* */
	}
	public bool DeleteById(string columname, string value, string strTableName, bool isDelete)
	{
		bool result = false;
		this.BeginTransaction();
		try
		{
			string Query = "";
			if (isDelete == true)
			{
				Query = "DELETE FROM [" + strTableName + "] WHERE " + columname + "=" + value;
			}
			else
			{
				Query = "UPDATE [" + strTableName + "] SET Status = 0, UpdateDate = GetDate() WHERE Id=" + value;
			}
			//this.AddParameter("Id", DbType.Int32, Id);
			//this.AddParameter("TableName", DbType.String, tableName);
			int iRecord = this.ExecuteNonQuery(Query, ConnectionState.KeepOpen);
			result = iRecord == 1 ? true : false;
		}
		catch (Exception)
		{
			this.RollbackTransaction();
			return result = false;
		}
		this.CommitTransaction();
		return result;
		/*Ex
		DatabaseHelper DB = new DatabaseHelper();
		bool result = DB.DeleteById(2, "Test");
		Utils.ShowMessageBox(this.Page, "ket qua" + result);
		* */
	}
	/*public bool SelectById(int value, string strTableName)
	{
	bool result = false;
	this.BeginTransaction();
	try
	{
	string Query = string.Empty;
	Query = "SELECT FROM [" + strTableName + "] WHERE Id=" + value;
	}
	catch (Exception)
	{
	this.RollbackTransaction();
	return result = false;
	}
	this.CommitTransaction();
	return result;
	}*/
	public bool Check_Field(string strTableName, string field)
	{
		bool result = false;
		DataTable dtObj = GetColumnsToDataTable(strTableName);
		foreach (DataRow row in dtObj.Rows)
		{
			string columnName = row["COLUMN_NAME"].ToString().ToLower();
			if (field.ToLower() == columnName)
				return true;
		}
		return result;
	}
	public bool Insert(Hashtable hashValue, string strTableName)
	{
		bool result = false;
		DataTable dtObj = GetColumnsToDataTable(strTableName);
		this.BeginTransaction();
		try
		{
			int len = dtObj.Rows.Count;
			string Query = "INSERT INTO [" + strTableName + "](";
			string listColumnName = "";
			string listValue = "";
			for (int i = 0; i < len; i++)
			{
				string columnNameDB = dtObj.Rows[i]["COLUMN_NAME"].ToString().ToLower();
				Object value = null;
				value = hashValue[columnNameDB];
				columnNameDB = "[" + columnNameDB + "]";

				if (value != null && !String.IsNullOrEmpty(value.ToString()))
				{
					DbType type = ConvertDataType(dtObj.Rows[i]["DATA_TYPE"].ToString());
					if (type == DbType.Boolean)
					{
						value = (value.ToString() == "True" || value.ToString() == "1") ? 1 : 0;
					}
					else if (type == DbType.String)
					{
						value = "N'" + value.ToString() + "'";
					}
					else if (type == DbType.DateTime)
					{
						value = "'" + value.ToString() + "'";
					}
					listColumnName += (listColumnName != "") ? ("," + columnNameDB) : columnNameDB;
					listValue += (listValue != "") ? ("," + value) : value;
				}
			}
			Query += listColumnName + ") VALUES(" + listValue + ");";
			int iRecord = this.ExecuteNonQuery(Query, ConnectionState.KeepOpen);
			result = iRecord == 1 ? true : false;
		}
		catch (Exception ex)
		{
			Utils.WriteLogError("Insert Table[" + strTableName + "]", ex.Message);
			this.RollbackTransaction();
			return result = false;
		}

		this.CommitTransaction();
		string sql = "EXEC sp_pkeys '" + strTableName + "'";
		DataRow dr = this.ExecuteDataRow(sql);
		if (dr != null)
		{
			sql = "SELECT MAX(" + dr["COLUMN_NAME"] + ") AS ID FROM dbo." + strTableName;
			// ID = Int32.Parse(this.ExecuteScalar(sql).ToString());
		}
		return result;
		/*Ex
		DatabaseHelper DB = new DatabaseHelper();
		Hashtable hash = new Hashtable();
		hash.Add("Name","thovh");
		hash.Add("BirthDay",Utils.ConvertDMYtoMMDDYYYY("20/01/1985"));
		hash.Add("Status",1);
		bool result = DB.Insert(hash, "Test");
		* */
	}
	public bool Insert(Hashtable hashValue, string strTableName, ref int ID)
	{
		bool result = false;
		DataTable dtObj = GetColumnsToDataTable(strTableName);
		this.BeginTransaction();
		try
		{
			int len = dtObj.Rows.Count;
			string Query = "INSERT INTO [" + strTableName + "](";
			string listColumnName = "";
			string listValue = "";
			for (int i = 0; i < len; i++)
			{
				string columnNameDB = dtObj.Rows[i]["COLUMN_NAME"].ToString().ToLower();
				Object value = null;
				value = hashValue[columnNameDB];
				columnNameDB = "[" + columnNameDB + "]";

				if (value != null && !String.IsNullOrEmpty(value.ToString()))
				{
					DbType type = ConvertDataType(dtObj.Rows[i]["DATA_TYPE"].ToString());
					if (type == DbType.Boolean)
					{
						value = (value.ToString() == "True" || value.ToString() == "1") ? 1 : 0;
					}
					else if (type == DbType.String)
					{
						value = "N'" + value.ToString() + "'";
					}
					else if (type == DbType.DateTime)
					{
						value = "'" + value.ToString() + "'";
					}
					listColumnName += (listColumnName != "") ? ("," + columnNameDB) : columnNameDB;
					listValue += (listValue != "") ? ("," + value) : value;
				}
			}
			Query += listColumnName + ") VALUES(" + listValue + ");";
			int iRecord = this.ExecuteNonQuery(Query, ConnectionState.KeepOpen);
			result = iRecord == 1 ? true : false;
		}
		catch (Exception ex)
		{
			Utils.WriteLogError("Insert Table[" + strTableName + "]", ex.Message);
			this.RollbackTransaction();
			return result = false;
		}

		this.CommitTransaction();

		if (result)
		{
			string sql = "EXEC sp_pkeys '" + strTableName + "'";
			DataRow dr = this.ExecuteDataRow(sql);
			if (dr != null)
			{
				sql = "SELECT MAX(" + dr["COLUMN_NAME"] + ") AS ID FROM dbo.[" + strTableName + "]";
				ID = Int32.Parse(this.ExecuteScalar(sql).ToString());
			}
		}
		return result;
		/*Ex
		DatabaseHelper DB = new DatabaseHelper();
		Hashtable hash = new Hashtable();
		hash.Add("Name","thovh");
		hash.Add("BirthDay",Utils.ConvertDMYtoMMDDYYYY("20/01/1985"));
		hash.Add("Status",1);
		bool result = DB.Insert(hash, "Test");
		* */
	}

	public bool Update(Hashtable hashValueUpdate, string keyField, Object keyValue, string strTableName)
	{

		bool result = false;
		DataTable dtObj = GetColumnsToDataTable(strTableName);
		this.BeginTransaction();
		try
		{
			int len = dtObj.Rows.Count;
			string Query = "UPDATE [" + strTableName + "] SET ";
			string listCond = "";
			for (int i = 0; i < len; i++)
			{
				string columnNameDB = dtObj.Rows[i]["COLUMN_NAME"].ToString().ToLower();
				if (columnNameDB.ToUpper().Equals(keyField.ToUpper()))
				{
					if (ConvertDataType(dtObj.Rows[i]["DATA_TYPE"].ToString()) == DbType.String)
					{
						keyValue = "'" + keyValue + "'";
					}
				}
				Object value = null;
				value = hashValueUpdate[columnNameDB.ToLower()];
				if (value != null)
				{
					DbType type = ConvertDataType(dtObj.Rows[i]["DATA_TYPE"].ToString());
					if (type == DbType.Boolean)
					{
						value = (value.ToString() == "True" || value.ToString() == "1") ? 1 : 0;
					}
					else if (type == DbType.String)
					{
						value = "N'" + value.ToString() + "'";
					}
					else if (type == DbType.DateTime)
					{
						value = "'" + value.ToString() + "'";
					}


					if (listCond == "")
					{
						listCond += "[" + columnNameDB + "]" + "=" + value.ToString();
					}
					else
					{
						listCond += "," + "[" + columnNameDB + "]" + "=" + value.ToString();
					}
				}
			}
			Query += listCond + " WHERE " + keyField + "=" + keyValue;
			int iRecord = this.ExecuteNonQuery(Query, ConnectionState.KeepOpen);
			result = iRecord == 1 ? true : false;
		}
		catch (Exception ex)
		{
			Utils.WriteLogError("Update Table[" + strTableName + "]-[" + keyField + "]=" + keyValue + "", ex.Message);
			this.RollbackTransaction();
			return result = false;
		}
		this.CommitTransaction();
		return result;
		/*Ex
		DatabaseHelper DB = new DatabaseHelper();
		Hashtable hash = new Hashtable();
		hash.Add("Name","thovh");
		hash.Add("BirthDay",Utils.ConvertDMYtoMMDDYYYY("20/01/1985"));
		hash.Add("Status",1);
		bool result = DB.Insert(hash, "Test");
		* */
	}

	public void HashTableToDataRow(Hashtable hashtable, DataRow dr)
	{
		//if (hashtable != null && hashtable.Count > 0)
		//     DataTable dtObj = GetColumnsToDataTable("product_attributes");

		//  if (dr.Table.Columns.Count>0)
		//  {

		//      foreach(DataColumn col in dr.Table.Columns )
		//      {
		//          var type = col.GetType().Name;

		//      }
		//  }
		//    foreach (DictionaryEntry item in hashtable)
		//    {
		//        if (dr[item.Key.ToString()] != null && item.Value != null)
		//        {
		//          //  DbType type = dr[item.Key.ToString()].GetType();

		//            dr[item.Key.ToString()] = item.Value;
		//        }
		//    }

		//var dataTable = new DataTable(hashtable.GetType().Name);
		//dataTable.TableName = "TableName";
		//foreach (DictionaryEntry entry in hashtable)
		//{
		//    dataTable.Columns.Add(entry.Key.ToString(), typeof(object));
		//}
		//DataRow drs = dataTable.NewRow();
		////fill the new row in the DataTable
		//foreach (DictionaryEntry entry in hashtable)
		//{
		//    drs[entry.Key.ToString()] = entry.Value.ToString();
		//}
		////add the filled up row to the DataTable

		////return the DataTable
		//dr = drs;

	}
	public void HashTableToDataRow(Hashtable hashtable, string strTableName, DataRow dr)
	{
		DataTable dtObj = GetColumnsToDataTable(strTableName);
		int len = dtObj.Rows.Count;
		for (int i = 0; i < len; i++)
		{
			string columnNameDB = dtObj.Rows[i]["COLUMN_NAME"].ToString().ToLower();
			Object value = null;
			value = hashtable[columnNameDB.ToLower()];
			if (value != null && !String.IsNullOrEmpty(value.ToString()))
			{
				try
				{


					DbType type = ConvertDataType(dtObj.Rows[i]["DATA_TYPE"].ToString());
					if (type == DbType.DateTime)
					{
						dr[columnNameDB] = DateUtil.ConvertDateTime(value.ToString(), "MM/dd/yyyy HH:mm");

					}
					else if (type == DbType.Int32)
					{

						dr[columnNameDB] = Int32.Parse(value.ToString());
					}
					else
					{
						dr[columnNameDB] = value.ToString();
					}
				}
				catch (Exception ex)
				{
					Utils.WriteLogError("HashTableToDataRow Table[" + strTableName + "]", ex.Message);

				}

			}
		}

	}

	public void CloneTableToTable(string source, string destinat, Dictionary<string, string> colums)
	{
		sql_query = "";

		string sql_where = SQL_Query(source, "*", sql_query);
		DataTable dtsource = Global.db.ExecuteDataTable(sql_where, CommandType.Text, ConnectionState.CloseOnExit, true);
		if (dtsource.Rows.Count > 0)
		{
			foreach (DataRow row in dtsource.Rows)
			{
				Hashtable hashtable = new Hashtable();

				foreach (var item in colums)
				{
					hashtable.Add(item.Value, row[item.Key]);
				}
				hashtable.Add("lastupdatedby", -1);
				hashtable.Add("createdby", -1);
				try
				{
					Insert(hashtable, destinat);
				}
				catch (Exception)
				{


				}

			}


			//hashtable.Add("idbcategory", Request.Form["bcategory"]);
		}
	}

	public string Ordinal(string table)
	{
		string ordinal = "1";
		try
		{
			ordinal = (Int32.Parse(this.ExecuteScalar("SELECT COUNT(1) AS [Count] FROM [" + table + "]").ToString()) + 1).ToString();
		}
		catch (Exception)
		{
			ordinal = "1";
		}

		return ordinal;
	}
	public string Ordinal(string table, string field, string key)
	{
		string ordinal = "1";
		try
		{
			ordinal = (Int32.Parse(this.ExecuteScalar("SELECT COUNT(1) AS [Count] FROM [" + table + "] WHERE " + field + "=" + key).ToString()) + 1).ToString();
		}
		catch (Exception)
		{
			ordinal = "1";
		}

		return ordinal;
	}
	public string Ordinal(object table, object field, object key)
	{
		string ordinal = "1";
		try
		{
			ordinal = (Int32.Parse(this.ExecuteScalar("SELECT COUNT(1) AS [Count] FROM [" + table.ToString() + "] WHERE " + field.ToString() + "='" + key.ToString() + "'").ToString()) + 1).ToString();
		}
		catch (Exception)
		{
			ordinal = "1";
		}

		return ordinal;
	}
	public DataTable GetColumnsToDataTable(string tableName)
	{
		DataTable dt = new DataTable();
		try
		{
			if (objConnection.State == System.Data.ConnectionState.Closed)
			{
				objConnection.Open();
			}
			dt = objConnection.GetSchema(DatabaseObjects.Columns.ToString(), new string[] { null, null, tableName, null });
		}
		catch (Exception ex)
		{
			HandleExceptions(ex);
		}
		finally
		{
			if (objConnection.State == System.Data.ConnectionState.Open)
			{
				objConnection.Close();
			}
		}
		return dt;
	}
	public bool UpdateLocation(Hashtable hashValueUpdate, string keyField, Object keyValue, string strTableName)
	{
		bool result = false;
		DataTable dtObj = GetColumnsToDataTable(strTableName);
		this.BeginTransaction();
		try
		{
			int len = dtObj.Rows.Count;
			string Query = "UPDATE [" + strTableName + "] SET ";
			string listCond = "";
			for (int i = 0; i < len; i++)
			{
				string columnNameDB = dtObj.Rows[i]["COLUMN_NAME"].ToString();
				if (columnNameDB.Equals(keyField))
				{
					if (ConvertDataType(dtObj.Rows[i]["DATA_TYPE"].ToString()) == DbType.String)
					{
						keyValue = "'" + keyValue + "'";
					}
				}
				Object value = null;
				value = hashValueUpdate[columnNameDB];
				if (columnNameDB == "Order")
				{
					columnNameDB = "[" + columnNameDB + "]";
				}
				if (value != null)
				{
					DbType type = ConvertDataType(dtObj.Rows[i]["DATA_TYPE"].ToString());
					if (type == DbType.Boolean)
					{
						value = (value.ToString() == "True" || value.ToString() == "1") ? 1 : 0;
					}
					else if (type == DbType.String)
					{
						value = "N'" + value.ToString() + "'";
					}
					if (listCond == "")
					{
						listCond += columnNameDB + "=" + value.ToString();
					}
					else
					{
						listCond += "," + columnNameDB + "=" + value.ToString();
					}
				}
			}
			Query += listCond + " WHERE " + keyField + "=" + keyValue;
			int iRecord = this.ExecuteNonQuery(Query, ConnectionState.KeepOpen);
			result = iRecord == 1 ? true : false;
		}
		catch (Exception)
		{
			this.RollbackTransaction();
			return result = false;
		}
		this.CommitTransaction();
		return result;
	}
	#region Encrypt - Decrypt Functions
	public string Encrypt(string original)
	{
		return Encrypt(original, "!@#$%^&*()~_+|");
	}
	public string Encrypt(string original, string key)
	{
		TripleDESCryptoServiceProvider objDESProvider;
		MD5CryptoServiceProvider objHashMD5Provider;
		byte[] keyhash;
		byte[] buffer;
		try
		{
			objHashMD5Provider = new MD5CryptoServiceProvider();
			keyhash = objHashMD5Provider.ComputeHash(UnicodeEncoding.Unicode.GetBytes(key));
			objHashMD5Provider = null;
			objDESProvider = new TripleDESCryptoServiceProvider();
			objDESProvider.Key = keyhash;
			objDESProvider.Mode = CipherMode.ECB;
			buffer = UnicodeEncoding.Unicode.GetBytes(original);
			return Convert.ToBase64String(objDESProvider.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length));
		}
		catch
		{
			return string.Empty;
		}
	}
	public string Decrypt(string encrypted)
	{
		return Decrypt(encrypted, "!@#$%^&*()~_+|");
	}
	public string Decrypt(string encrypted, string key)
	{
		TripleDESCryptoServiceProvider objDESProvider;
		MD5CryptoServiceProvider objHashMD5Provider;
		byte[] keyhash;
		byte[] buffer;
		try
		{
			objHashMD5Provider = new MD5CryptoServiceProvider();
			keyhash = objHashMD5Provider.ComputeHash(UnicodeEncoding.Unicode.GetBytes(key));
			objHashMD5Provider = null;
			objDESProvider = new TripleDESCryptoServiceProvider();
			objDESProvider.Key = keyhash;
			objDESProvider.Mode = CipherMode.ECB;
			buffer = Convert.FromBase64String(encrypted);
			return UnicodeEncoding.Unicode.GetString(objDESProvider.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length));
		}
		catch
		{
			return string.Empty;
		}
	}
	#endregion

	public DataRow NewRow(string table)
	{
		DataRow dr = ExecuteDataTable("SELECT * FROM [" + table.Trim().ToLower() + "] WHERE 1=2").NewRow();
		return dr;
	}
	public void Check_Field(string f, string o, string v)
	{
		sql_query += " AND " + f + " " + o + " " + v;
	}

	public void Check_Field(object f, object o, object v)
	{
		sql_query += " AND " + f.ToString() + " " + o.ToString() + " " + v.ToString();
	}
    public string SQL_Query_Search(string table, string fields, string keyword, string keyID)
    {
        string sql = string.Format("SELECT Top 60 RANK,* FROM {0} tn  INNER JOIN FREETEXTTABLE({0}, ({1}), N'{2}') as ftt ON ftt.[KEY]=tn.{3} ORDER BY ftt.RANK DESC, tn.{3} DESC ", table, fields, keyword, keyID);

 //       SELECT RANK,* FROM CMRC_Products tn 
 //           INNER JOIN FREETEXTTABLE(CMRC_Products, (ModelName, ModelNameUnsigned), N'Truyện ma bóng người dưới trăng') as ftt
 //             ON ftt.[KEY]=tn.ProductID
 //             ORDER BY ftt.RANK DESC, tn.ProductID DESC 

        return sql;
    }


    public string SQL_Query(string table, string fields, string where, string orderby = "")
    {

        string sql = "SELECT " + fields + " FROM " + table + "  WHERE 1=1 " + where + (String.IsNullOrEmpty(orderby) == false ? " ORDER BY " + orderby : "");

        return sql;

    }





	public string SQL_Query_Paging(string table, string fields, string where, string orderby, int page, int limit)
	{
		int offset = (page - 1) * limit;



		string sql = "SELECT  Temp.* FROM ( SELECT " + fields + ", ROW_NUMBER() OVER (ORDER BY " + orderby + ") AS RowNumber, TotalRows = COUNT(1) OVER ( ) FROM " + table + " WHERE 1=1  " + where + " ) AS Temp WHERE Temp.RowNumber > " + offset + " AND Temp.RowNumber <=" + (offset + limit) + " ORDER BY Temp.RowNumber";
		return sql;
	}

	public string SQL_Query_Paging(string table, string fields, string join, string where, string orderby, int page, int limit)
	{
		int offset = (page - 1) * limit;
		string sql = "SELECT  Temp.* FROM ( SELECT " + fields + ", ROW_NUMBER() OVER (ORDER BY " + orderby + ") AS RowNumber FROM " + table + " " + join + "  WHERE 1=1  " + where + " ) AS Temp WHERE Temp.RowNumber > " + offset + " AND " + " Temp.RowNumber <=" + (offset + limit) + " ORDER BY Temp.RowNumber";
		return sql;
	}

	public string Render_Paging(string table, string sql_where, string key, int current, int limit, string url)
	{
		string sql = "SELECT COUNT(" + key + ") AS TotalRow FROM dbo." + table + " WHERE 1=1 " + sql_where;
		StringBuilder sb = new StringBuilder();
		int offset = 0;
		DataRow dr = GetDataRow(sql);
		try
		{
			TotalRow = Int16.Parse(dr["TotalRow"].ToString());
		}
		catch (Exception)
		{

			TotalRow = 0;
		}

		//if (dr != null)
		//{
		//    TotalRow = Int16.Parse(dr["TotalRow"].ToString());
		//    if (TotalRow > 0)
		//    {
		//        offset = (TotalRow + limit - 1) / limit;
		//        sb.Append("<div class='row datatables-footer'>");
		//        sb.Append("<div class='col-sm-12 col-md-4'>");
		//        sb.Append("<div class='dataTables_info' id='datatable-default_info' role='status' aria-live='polite'>Total: " + TotalRow + " items - Page (1 of " + offset + ")</div>");
		//        sb.Append("</div>");

		//        sb.Append("<div class='col-sm-12 col-md-8'>");
		//        sb.Append("<div class='dataTables_paginate paging_bs_normal'>");
		//        sb.Append("<ul class='pagination'>");

		//        if (current == 1)
		//        {
		//            sb.Append("<li class='prev disabled'><a href='" + url + "1' data-toggle='tooltip' data-original-title='First'>|<</a></li>");
		//            sb.Append("<li class='prev disabled'><a href='" + url + "' data-toggle='tooltip' data-original-title='Previous'><</a></li>");
		//        }
		//        else
		//        {
		//            sb.Append("<li><a href='" + url + "1' data-toggle='tooltip' data-original-title='First'>|<</a></li>");
		//            sb.Append("<li><a href='" + url + (current - 1) + "' data-toggle='tooltip' data-original-title='Previous'><</a></li>");
		//        }

		//        for (int i = 1; i <= offset; i++)
		//        {
		//            if (i == current)
		//                sb.Append("<li class='active'><a>" + i + "</a></li>");
		//            else
		//                sb.Append("<li><a href='" + url + i + "' data-toggle='tooltip' data-original-title=''>" + i + " </a></li>");
		//        }
		//        if (current == offset)
		//        {
		//            sb.Append("<li class='prev disabled'><a href='" + url + "1' data-toggle='tooltip' data-original-title='Next'>></a></li>");
		//            sb.Append("<li class='prev disabled'><a href='" + url + "' data-toggle='tooltip' data-original-title='Last'>>|</a></li>");
		//        }
		//        else
		//        {
		//            sb.Append("<li><a href='" + url + (current + 1) + " data-toggle='tooltip' data-original-title='Next'>></a></li>");
		//            sb.Append("<li><a href='" + url + (offset) + "' data-toggle='tooltip' data-original-title='Last'>>|</a></li>");
		//        }
		//        sb.Append("</ul>");
		//        sb.Append("</div>");
		//        sb.Append("</div>");
		//        sb.Append("</div>");
		//    }
		//    else
		//    {
		//        sb.Append("<div class='row datatables-footer'>");
		//        sb.Append("<div class='col-sm-12 col-md-4'>");
		//        sb.Append(Language.K("list_no_record") + "</div>");
		//        sb.Append("</div>");
		//    }
		//}
		//return sb.ToString();

		return Paging.Render_Paging(current, limit, TotalRow, url);
	}

	public string Render_Paging_Client(string table, string sql_where, string key, int current, int limit, string url)
	{
		string sql = "SELECT COUNT(" + key + ") AS TotalRow FROM dbo." + table + " WHERE 1=1 " + sql_where;
		StringBuilder sb = new StringBuilder();
		int offset = 0;
		DataRow dr = GetDataRow(sql);
		try
		{
			TotalRow = Int16.Parse(dr["TotalRow"].ToString());
		}
		catch (Exception)
		{

			TotalRow = 0;
		}


		return Paging.Render_Paging_Client(current, limit, TotalRow, url);
	}
}
public enum Providers
{
	SqlServer, OleDb, Oracle, ODBC, ConfigDefined
}
public enum ConnectionState
{
	KeepOpen, CloseOnExit
}
public enum DatabaseObjects
{
	Columns, Tables
}

