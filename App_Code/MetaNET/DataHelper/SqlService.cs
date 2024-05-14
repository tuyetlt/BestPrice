#define SQL_PARAM_ALLOW
#define SQL_PREPAIR_TONULL

namespace MetaNET.DataHelper
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.IO;
    using System.Data.Common;
    using System.Xml;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Configuration;
    using System.Linq;

    using System.Text;
    using log4net;



    [DebuggerStepThrough]
    public partial class SqlService : IDisposable
    {
        protected ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public const string PAGING_PROC_DEFAULT = "Paging_Cursor";
        public const string CONNECTION_STRING_DEFAULT_NAME = "LocalSqlServer";
        protected bool _autoCloseConnection;
        protected int _commandTimeout;
        protected SqlConnection _connection;
        protected string _connectionString;
#if SQL_PREPAIR_TONULL
        protected bool _convertZeroValuesToDbNull = false;
        protected bool _convertEmptyValuesToDbNull = true;
        protected bool _convertMaxValuesToDbNull = false;
        protected bool _convertMinValuesToDbNull = false;
#endif
        protected bool _isSingleRow;
        protected SqlParameterCollection _parameterCollection;
        protected List<SqlParameter> _parameters;
        protected SqlTransaction _transaction;
        protected int _execResult = 0;

        #region Properties

        public int ExecuteResult
        {
            get
            {
                return _execResult;
            }
        }

        public List<SqlParameter> ExecuteParameters
        {
            get
            {
                return this._parameters;
            }
        }

        public bool AutoCloseConnection
        {
            get
            {
                return this._autoCloseConnection;
            }
            set
            {
                this._autoCloseConnection = value;
            }
        }

        public int CommandTimeout
        {
            get
            {
                return this._commandTimeout;
            }
            set
            {
                this._commandTimeout = value;
            }
        }

        public SqlConnection Connection
        {
            get
            {
                return this._connection;
            }
            set
            {
                this._connection = value;
                this.ConnectionString = this._connection.ConnectionString;
            }
        }

        public string ConnectionString
        {
            get
            {
                return this._connectionString;
            }
            set
            {
                this._connectionString = value;
            }
        }
#if SQL_PREPAIR_TONULL
        public bool ConvertZeroValuesToDbNull
        {
            get
            {
                return this._convertZeroValuesToDbNull;
            }
            set
            {
                this._convertZeroValuesToDbNull = value;
            }
        }

        public bool ConvertEmptyValuesToDbNull
        {
            get
            {
                return this._convertEmptyValuesToDbNull;
            }
            set
            {
                this._convertEmptyValuesToDbNull = value;
            }
        }

        public bool ConvertMaxValuesToDbNull
        {
            get
            {
                return this._convertMaxValuesToDbNull;
            }
            set
            {
                this._convertMaxValuesToDbNull = value;
            }
        }

        public bool ConvertMinValuesToDbNull
        {
            get
            {
                return this._convertMinValuesToDbNull;
            }
            set
            {
                this._convertMinValuesToDbNull = value;
            }
        }
#endif
        public bool IsSingleRow
        {
            get
            {
                return this._isSingleRow;
            }
            set
            {
                this._isSingleRow = value;
            }
        }

        public SqlParameterCollection Parameters
        {
            get
            {
                return this._parameterCollection;
            }
        }

        public int ReturnValue
        {
            get
            {
                if (!this._parameterCollection.Contains("@ReturnValue"))
                {
                    throw new Exception("You must call the AddReturnValueParameter method before executing your request.");
                }
                return (int)this._parameterCollection["@ReturnValue"].Value;
            }
        }

        public SqlTransaction Transaction
        {
            get
            {
                return this._transaction;
            }
            set
            {
                this._transaction = value;
            }
        }
        #endregion Properties

        #region Constructors
        private void Initial()
        {
            this._connectionString = string.Empty;
            this._parameters = new List<SqlParameter>();
            this._isSingleRow = false;
#if SQL_PREPAIR_TONULL
            this._convertZeroValuesToDbNull = false;
            this._convertEmptyValuesToDbNull = true;
            this._convertMinValuesToDbNull = false;
            this._convertMaxValuesToDbNull = false;
#endif
            this._autoCloseConnection = true;
            this._commandTimeout = 30;
        }
        public SqlService()
        {
            Initial();
            this._connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_DEFAULT_NAME].ConnectionString;
        }

        public SqlService(SqlConnection connection)
        {
            Initial();
            this._connection = connection;
            this._connectionString = connection.ConnectionString;
            this._autoCloseConnection = false;
        }

        public SqlService(string server, string database)
        {
            Initial();
            this.ConnectionString = "Server=" + server + ";Database=" + database + ";Integrated Security=true;";
        }

        public SqlService(string server, string database, string user, string password)
        {
            Initial();
            this.ConnectionString = "Server=" + server + ";Database=" + database + ";User ID=" + user + ";Password=" + password + ";";
        }

        public SqlService(string configKey)
        {
            Initial();
            if (!string.IsNullOrEmpty(configKey))
            {
                if (configKey.Contains("|"))
                {
                    Random r = new Random();
                    string[] connectKeys = configKey.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                    int connectIndex = r.Next(connectKeys.Length);
                    this._connectionString = ConfigurationManager.ConnectionStrings[connectKeys[connectIndex]].ConnectionString;
                }
                else
                    this._connectionString = ConfigurationManager.ConnectionStrings[configKey].ConnectionString;
            }
            else
            {
                this._connectionString = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_DEFAULT_NAME].ConnectionString;
            }
        }

        #endregion Constructor


        #region Static Methods
        private static SqlService _SingletonObj = null;
        private static object objSingletonLocker = new object();
        public static SqlService Singleton(string configKey)
        {
            if (_SingletonObj == null)
            {
                lock (objSingletonLocker)
                {
                    if (_SingletonObj == null)
                    {
                        _SingletonObj = GetSqlService(configKey);
                    }
                }
            }
            return _SingletonObj;
        }

        public static SqlService Singleton()
        {
            return Singleton(string.Empty);
        }

        public static SqlService GetSqlService()
        {
            return new SqlService();
        }
        public static SqlService GetSqlService(string configKey)
        {
            return new SqlService(configKey);
        }
        public static SqlService GetSqlServiceFromConnectionString(string strConn)
        {
            return new SqlService(new SqlConnection(strConn));
        }

        #endregion Static Methods





        #region Methods

        #region Parameters

        #region Prepare Parameters

        private void CopyParameters(SqlCommand command)
        {
            string pname;
            object pvalue;
            SqlDbType ptype;
            ParameterDirection pdir;
            int psize;
            for (int i = 0; i < this._parameters.Count; i++)
            {
                pname = this._parameters[i].ParameterName;
                pdir = this._parameters[i].Direction;
                pvalue = this._parameters[i].Value;
                ptype = this._parameters[i].SqlDbType;
                psize = this._parameters[i].Size;
                var p = new SqlParameter(pname, ptype);
                p.Value = pvalue; p.Direction = pdir;
                p.Size = psize;
                if (command.Parameters.Contains(pname))
                    command.Parameters[pname] = p;
                else
                    command.Parameters.Add(p);
            }
        }

        private object PrepareSqlValue(object value)
        {
#if SQL_PREPAIR_TONULL
            if (!_convertEmptyValuesToDbNull
                && !_convertZeroValuesToDbNull
                && !_convertMaxValuesToDbNull
                && !_convertMinValuesToDbNull)
                return value;

            if (value is string)
            {
                if (this._convertEmptyValuesToDbNull && (((string)value) == string.Empty))
                {
                    return DBNull.Value;
                }
                return value;
            }
            if (value is Guid)
            {
                if (this._convertEmptyValuesToDbNull && (((Guid)value) == Guid.Empty))
                {
                    return DBNull.Value;
                }
                return value;
            }
            if (value is DateTime)
            {
                if ((this._convertMinValuesToDbNull && (((DateTime)value) == DateTime.MinValue))
                    || (this._convertMaxValuesToDbNull && (((DateTime)value) == DateTime.MaxValue)))
                {
                    return DBNull.Value;
                }
                return value;
            }
            if (value is TimeSpan)
            {
                if ((this._convertMinValuesToDbNull && (((TimeSpan)value) == TimeSpan.MinValue))
                    || (this._convertMaxValuesToDbNull && (((TimeSpan)value) == TimeSpan.MaxValue))
                    || (this._convertZeroValuesToDbNull && (((TimeSpan)value) == TimeSpan.Zero)))
                {
                    return DBNull.Value;
                }
                return value;
            }
            if (value is short)
            {
                if (((this._convertMinValuesToDbNull && (((short)value) == -32768))
                    || (this._convertMaxValuesToDbNull && (((short)value) == 0x7fff)))
                    || (this._convertZeroValuesToDbNull && (((short)value) == 0)))
                {
                    return DBNull.Value;
                }
                return value;
            }
            if (value is int)
            {
                if (((this._convertMinValuesToDbNull && (((int)value) == -2147483648))
                    || (this._convertMaxValuesToDbNull && (((int)value) == 0x7fffffff)))
                    || (this._convertZeroValuesToDbNull && (((int)value) == 0)))
                {
                    return DBNull.Value;
                }
                return value;
            }
            if (value is long)
            {
                if (((this._convertMinValuesToDbNull && (((long)value) == -9223372036854775808L))
                    || (this._convertMaxValuesToDbNull && (((long)value) == 0x7fffffffffffffffL)))
                    || (this._convertZeroValuesToDbNull && (((long)value) == 0L)))
                {
                    return DBNull.Value;
                }
                return value;
            }
            if (value is float)
            {
                if (((this._convertMinValuesToDbNull && (((float)value) == float.MinValue))
                    || (this._convertMaxValuesToDbNull && (((float)value) == float.MaxValue)))
                    || (this._convertZeroValuesToDbNull && (((float)value) == 0f)))
                {
                    return DBNull.Value;
                }
                return value;
            }
            if (value is double)
            {
                if (((this._convertMinValuesToDbNull && (((double)value) == double.MinValue))
                    || (this._convertMaxValuesToDbNull && (((double)value) == double.MaxValue)))
                    || (this._convertZeroValuesToDbNull && (((double)value) == 0.0)))
                {
                    return DBNull.Value;
                }
                return value;
            }
            if (value is decimal)
            {
                if (((this._convertMinValuesToDbNull && (((decimal)value) == -79228162514264337593543950335M))
                    || (this._convertMaxValuesToDbNull && (((decimal)value) == 79228162514264337593543950335M)))
                    || (this._convertZeroValuesToDbNull && (((decimal)value) == 0M)))
                {
                    return DBNull.Value;
                }
                return value;
            }
            return value;
#else
            return value;
#endif
        }
        #endregion


        public string GetParamsSql(bool hasValue = false)
        {
            string result = "";
            foreach (SqlParameter p in this._parameters)
            {
                result += p.ParameterName + " " + p.SqlDbType.ToString() + (hasValue ? "=N'" + p.Value.ToString() + "'" : (p.IsNullable ? "=NULL" : "")) + ",\n";
            }
            return result;
        }

        public SqlParameter AddOutputParameter(string name, SqlDbType type)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.Direction = ParameterDirection.Output;
            parameter.ParameterName = name;
            parameter.SqlDbType = type;
            this._parameters.Add(parameter);
            return parameter;
        }

        public SqlParameter AddOutputParameter(string name, SqlDbType type, int size)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.Direction = ParameterDirection.Output;
            parameter.ParameterName = name;
            parameter.SqlDbType = type;
            parameter.Size = size;
            this._parameters.Add(parameter);
            return parameter;
        }

        public void AddParameter(SqlParameter parameter)
        {
            if (parameter.Direction == ParameterDirection.Input || parameter.Direction == ParameterDirection.InputOutput)
                parameter.Value = this.PrepareSqlValue(parameter.Value);
            this._parameters.Add(parameter);
        }

        public static SqlDbType GetSqlDbType<T>()
        {
            return GetSqlDbType(typeof(T));
        }

        public static SqlDbType GetSqlDbType(Type type)
        {
            if (type == typeof(Boolean) || type == typeof(Boolean?))
                return SqlDbType.Bit;
            else if (type == typeof(Byte) || type == typeof(Byte?))
                return SqlDbType.TinyInt;
            else if (type == typeof(String))
                return SqlDbType.NVarChar;
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
                return SqlDbType.DateTime;
            else if (type == typeof(Int16) || type == typeof(Int16?))
                return SqlDbType.SmallInt;
            else if (type == typeof(Int32) || type == typeof(Int32?))
                return SqlDbType.Int;
            else if (type == typeof(Int64) || type == typeof(Int64?))
                return SqlDbType.BigInt;
            else if (type == typeof(Decimal) || type == typeof(Decimal?))
                return SqlDbType.Decimal;
            else if (type == typeof(Double) || type == typeof(Double?))
                return SqlDbType.Float;
            else if (type == typeof(Single) || type == typeof(Single?))
                return SqlDbType.Real;
            else if (type == typeof(TimeSpan)) return SqlDbType.Time;
            else if (type == typeof(Guid) || type == typeof(Guid?))
                return SqlDbType.UniqueIdentifier;
            else if (type == typeof(Byte[]) || type == typeof(Byte?[]))
                return SqlDbType.Binary;
            else if (type == typeof(Char[]) || type == typeof(Char?[]))
                return SqlDbType.NVarChar;
            else if (type == typeof(Char) || type == typeof(Char?))
                return SqlDbType.NChar;
            else if (type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?))
                return SqlDbType.DateTimeOffset;
            else
                return SqlDbType.NVarChar;

        }

        //public static Type GetClrType(SqlDbType sqlType)
        //{
        //    switch (sqlType)
        //    {
        //        case SqlDbType.BigInt:
        //            return typeof(long);

        //        case SqlDbType.Binary:
        //        case SqlDbType.Image:
        //        case SqlDbType.Timestamp:
        //        case SqlDbType.VarBinary:
        //            return typeof(byte[]);

        //        case SqlDbType.Bit:
        //            return typeof(bool);

        //        case SqlDbType.Char:
        //        case SqlDbType.NChar:
        //        case SqlDbType.NText:
        //        case SqlDbType.NVarChar:
        //        case SqlDbType.Text:
        //        case SqlDbType.VarChar:
        //        case SqlDbType.Xml:
        //            return typeof(string);

        //        case SqlDbType.DateTime:
        //        case SqlDbType.SmallDateTime:
        //        case SqlDbType.Date:
        //        case SqlDbType.Time:
        //        case SqlDbType.DateTime2:
        //            return typeof(DateTime);

        //        case SqlDbType.Decimal:
        //        case SqlDbType.Money:
        //        case SqlDbType.SmallMoney:
        //            return typeof(decimal?);

        //        case SqlDbType.Float:
        //            return typeof(double);

        //        case SqlDbType.Int:
        //            return typeof(int);

        //        case SqlDbType.Real:
        //            return typeof(float?);

        //        case SqlDbType.UniqueIdentifier:
        //            return typeof(Guid?);

        //        case SqlDbType.SmallInt:
        //            return typeof(short);

        //        case SqlDbType.TinyInt:
        //            return typeof(byte);

        //        case SqlDbType.Variant:
        //        case SqlDbType.Udt:
        //            return typeof(object);

        //        case SqlDbType.Structured:
        //            return typeof(DataTable);

        //        case SqlDbType.DateTimeOffset:
        //            return typeof(DateTimeOffset?);

        //        default:
        //            throw new ArgumentOutOfRangeException("sqlType");
        //    }
        //}

        public SqlParameter AddParameter<T>(string name, T value)
        {
            SqlDbType type = GetSqlDbType<T>();
            return AddParameter(name, type, value);
        }

        public SqlParameter AddParameterX(string name, object value)
        {
            return AddParameter(name, GetSqlDbType(value.GetType()), value);
        }

        public SqlParameter AddParameter(string name, SqlDbType type)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = name;
            parameter.SqlDbType = type;
            parameter.Value = DBNull.Value;
            this._parameters.Add(parameter);
            return parameter;
        }

        public SqlParameter AddParameter(string name, SqlDbType type, object value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = name;
            parameter.SqlDbType = type;
            parameter.Value = this.PrepareSqlValue(value);
            this._parameters.Add(parameter);
            return parameter;
        }

        public SqlParameter AddParameter(string name, SqlDbType type, object value, bool convertZeroToDBNull)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = name;
            parameter.SqlDbType = type;
#if SQL_PREPAIR_TONULL
            this._convertZeroValuesToDbNull = convertZeroToDBNull;
#endif
            parameter.Value = this.PrepareSqlValue(value);
            this._parameters.Add(parameter);
            return parameter;
        }

        public SqlParameter AddParameter(string name, SqlDbType type, object value, ParameterDirection direction)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.Direction = direction;
            parameter.ParameterName = name;
            parameter.SqlDbType = type;
            parameter.Value = this.PrepareSqlValue(value);
            this._parameters.Add(parameter);
            return parameter;
        }

        public SqlParameter AddParameter(string name, SqlDbType type, object value, int size)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = name;
            parameter.SqlDbType = type;
            parameter.Size = size;
            parameter.Value = this.PrepareSqlValue(value);
            this._parameters.Add(parameter);
            return parameter;
        }

        public SqlParameter AddParameter(string name, SqlDbType type, object value, int size, ParameterDirection direction)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.Direction = direction;
            parameter.ParameterName = name;
            parameter.SqlDbType = type;
            parameter.Size = size;
            parameter.Value = this.PrepareSqlValue(value);
            this._parameters.Add(parameter);
            return parameter;
        }

        public SqlParameter AddReturnValueParameter()
        {
            SqlParameter parameter = new SqlParameter();
            parameter.Direction = ParameterDirection.ReturnValue;
            parameter.ParameterName = "@ReturnValue";
            parameter.SqlDbType = SqlDbType.Int;
            this._parameters.Add(parameter);
            return parameter;
        }

        public SqlParameter AddStreamParameter(string name, Stream value)
        {
            return this.AddStreamParameter(name, value, SqlDbType.Image);
        }

        public SqlParameter AddStreamParameter(string name, Stream value, SqlDbType type)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = name;
            parameter.SqlDbType = type;
            value.Position = 0L;
            byte[] buffer = new byte[value.Length];
            value.Read(buffer, 0, (int)value.Length);
            parameter.Value = buffer;
            this._parameters.Add(parameter);
            return parameter;
        }

        public SqlParameter AddTextParameter(string name, string value)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.Direction = ParameterDirection.Input;
            parameter.ParameterName = name;
            parameter.SqlDbType = SqlDbType.Text;
            parameter.Value = this.PrepareSqlValue(value);
            this._parameters.Add(parameter);
            return parameter;
        }

        #endregion Parameter

        #region Transaction
        public void BeginTransaction()
        {
            if (this._connection == null)
            {
                //throw new InvalidOperationException("You must have a valid connection object before calling BeginTransaction.");
                this.Connect();
            }
            this._transaction = this._connection.BeginTransaction();
        }

        public void BeginTransaction(string name)
        {
            if (this._connection == null)
            {
                //throw new InvalidOperationException("You must have a valid connection object before calling BeginTransaction.");
                this.Connect();
            }
            this._transaction = this._connection.BeginTransaction(name);
        }

        public void BeginTransaction(IsolationLevel iso)
        {
            if (this._connection == null)
            {
                //throw new InvalidOperationException("You must have a valid connection object before calling BeginTransaction.");
                this.Connect();
            }
            this._transaction = this._connection.BeginTransaction(iso);
        }

        public void BeginTransaction(IsolationLevel iso, string name)
        {
            if (this._connection == null)
            {
                //throw new InvalidOperationException("You must have a valid connection object before calling BeginTransaction.");
                this.Connect();
            }
            this._transaction = this._connection.BeginTransaction(iso, name);
        }

        public void CommitTransaction()
        {
            if (this._transaction != null)
            {
                try
                {
                    this._transaction.Commit();
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            else
            {
                //throw new InvalidOperationException("You must call BeginTransaction before calling CommitTransaction.");
            }
        }

        public void RollbackTransaction()
        {
            if (this._transaction == null)
            {
                throw new InvalidOperationException("You must call BeginTransaction before calling RollbackTransaction.");
            }
            try
            {
                this._transaction.Rollback();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
        #endregion

        #region Connecting
        public void Connect()
        {
            if (this._connection != null)
            {
                if (this._connection.State != ConnectionState.Open)
                {
                    this._connection.Open();
                }
            }
            else
            {
                if (this._connectionString == string.Empty)
                {
                    throw new InvalidOperationException("You must set a connection object or specify a connection string before calling Connect.");
                }
                this._connection = new SqlConnection(this._connectionString);
                this._connection.Open();
            }
        }

        public void Reset()
        {
            if (this._parameters != null)
            {
                this._parameters.Clear();
            }
            if (this._parameterCollection != null)
            {
                this._parameterCollection = null;
            }
        }

        public void Reconnect()
        {
            this.Reset();
            this.Disconnect();
            this.Connect();
        }

        public void Disconnect()
        {
            if ((this._connection != null) && (this._connection.State != ConnectionState.Closed))
            {
                this._connection.Close();
            }
            if (this._connection != null)
            {
                this._connection.Dispose();
            }
            if (this._transaction != null)
            {
                this._transaction.Dispose();
            }
            this._transaction = null;
            this._connection = null;
        }

        public void Dispose()
        {
            this.Disconnect();
        }
        #endregion Connecting




        #region Exec Page
        public SqlDataReader ExecutePaging(string pagingProcedureName, string tableName, string pkFieldName, string sortExpression, string selectFields, string filterExpression, string groupExpression, int currentPage, int pageSize, out int totalPages, out int totalRows)
        {
            if (string.IsNullOrEmpty(pagingProcedureName)) pagingProcedureName = PAGING_PROC_DEFAULT;
            return this.ExecutePagingReader(tableName, pkFieldName, sortExpression, selectFields, filterExpression, groupExpression, currentPage, pageSize, out totalPages, out totalRows, pagingProcedureName);
        }
        public SqlDataReader ExecutePagingReader(string tableName, string pkFieldName, string sortExpression, string selectFields, string filterExpression, string groupExpression, int currentPage, int pageSize, out int totalPages, out int totalRows, string pagingProcedureName = PAGING_PROC_DEFAULT)
        {
            totalPages = 0;
            totalRows = 0;
            if (pageSize > 0)
            {
                if (currentPage < 1)
                {
                    currentPage = 1;
                }
                if (string.IsNullOrEmpty(filterExpression))
                {
                    filterExpression = "(1=1)";
                }
                using (SqlCommand command = new SqlCommand())
                {
                    this.Connect();
                    command.Connection = this._connection;
                    command.CommandTimeout = this.CommandTimeout;
                    command.CommandText = string.Format("SELECT COUNT(*) FROM {0} WHERE {1}", tableName, filterExpression);
                    if (this._transaction != null)
                    {
                        command.Transaction = this._transaction;
                    }
                    command.CommandType = CommandType.Text;
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        totalRows = reader.GetInt32(0);
                        if (totalRows == 0)
                        {
                            return null;
                        }
                        reader.Close();
                        command.CommandText = pagingProcedureName;
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@Tables", tableName));
                        command.Parameters.Add(new SqlParameter("@PK", pkFieldName));
                        command.Parameters.Add(new SqlParameter("@Sort", sortExpression));
                        command.Parameters.Add(new SqlParameter("@PageNumber", currentPage));
                        command.Parameters.Add(new SqlParameter("@PageSize", pageSize));
                        command.Parameters.Add(new SqlParameter("@Fields", selectFields));
                        command.Parameters.Add(new SqlParameter("@Filter", filterExpression));
                        command.Parameters.Add(new SqlParameter("@Group", groupExpression));
                        CommandBehavior behavior = CommandBehavior.Default;
                        if (this.AutoCloseConnection)
                        {
                            behavior |= CommandBehavior.CloseConnection;
                        }
                        if (this._isSingleRow)
                        {
                            behavior |= CommandBehavior.SingleRow;
                        }
                        reader = command.ExecuteReader(behavior);
                        totalPages = totalRows / pageSize;
                        if (totalRows > (totalPages * pageSize))
                        {
                            totalPages++;
                        }
                        return reader;
                    }
                }
            }
            return null;
        }
        public DataTable ExecutePagingDataTable(string tableName, string pkFieldName, string sortExpression, string selectFields, string filterExpression, string groupExpression, int currentPage, int pageSize, out int totalPages, out int totalRows, string pagingProcedureName = PAGING_PROC_DEFAULT)
        {
            DataTable result = new DataTable();
            IDataReader rd = this.ExecutePagingReader(tableName, pkFieldName, sortExpression, selectFields, filterExpression, groupExpression, currentPage, pageSize, out totalPages, out totalRows, pagingProcedureName);
            result.Load(rd);
            rd.Close();
            rd.Dispose();
            return result;
        }
        #endregion

        #region Exec
        //------------------------------------------------------------------------------------------------
        public void Execute(string commandText, CommandType commandType)
        {
            using (SqlCommand command = new SqlCommand())
            {
                this.Connect();
                command.CommandTimeout = this.CommandTimeout;
                command.CommandText = commandText;
                command.Connection = this._connection;
                if (this._transaction != null)
                {
                    command.Transaction = this._transaction;
                }
                command.CommandType = commandType;
                this.CopyParameters(command);
                this._execResult = command.ExecuteNonQuery();
                this._parameterCollection = command.Parameters;
            }
            if (this.AutoCloseConnection)
            {
                this.Disconnect();
            }
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            using (SqlCommand command = new SqlCommand())
            {
                this.Connect();
                command.CommandTimeout = this.CommandTimeout;
                command.CommandText = commandText;
                command.Connection = this._connection;
                if (this._transaction != null)
                {
                    command.Transaction = this._transaction;
                }
                command.CommandType = commandType;
                this.CopyParameters(command);
                this._execResult = command.ExecuteNonQuery();
                this._parameterCollection = command.Parameters;
            }
            if (this.AutoCloseConnection)
            {
                this.Disconnect();
            }
            return this._execResult;
        }

        public DataSet GetDataSet(string commandText, CommandType commandType)
        {
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand())
            {
                this.Connect();
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    command.CommandTimeout = this.CommandTimeout;
                    command.CommandText = commandText;
                    command.Connection = this._connection;
                    if (this._transaction != null)
                    {
                        command.Transaction = this._transaction;
                    }
                    command.CommandType = commandType;
                    this.CopyParameters(command);
                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet);
                    this._parameterCollection = command.Parameters;
                }
            }
            if (this.AutoCloseConnection)
            {
                this.Disconnect();
            }
            return dataSet;
        }

        public DataSet GetDataSet(string commandText, CommandType commandType, int startRecord, int maxRecords)
        {
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand())
            {
                this.Connect();
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {

                    command.CommandTimeout = this.CommandTimeout;
                    command.CommandText = commandText;
                    command.Connection = this._connection;
                    if (this._transaction != null)
                    {
                        command.Transaction = this._transaction;
                    }
                    command.CommandType = commandType;
                    this.CopyParameters(command);
                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet, startRecord, maxRecords, SqlDataAdapter.DefaultSourceTableName);
                    this._parameterCollection = command.Parameters;
                }
            }
            if (this.AutoCloseConnection)
            {
                this.Disconnect();
            }
            return dataSet;
        }

        public DataSet GetDataSet(string commandText, CommandType commandType, string tableName, int startRecord, int maxRecords)
        {
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand())
            {
                this.Connect();
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    command.CommandTimeout = this.CommandTimeout;
                    command.CommandText = commandText;
                    command.Connection = this._connection;
                    if (this._transaction != null)
                    {
                        command.Transaction = this._transaction;
                    }
                    command.CommandType = commandType;
                    this.CopyParameters(command);
                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet, startRecord, maxRecords, tableName);
                    this._parameterCollection = command.Parameters;
                }
            }
            if (this.AutoCloseConnection)
            {
                this.Disconnect();
            }
            return dataSet;
        }

        public DataSet GetDataSet(string commandText, CommandType commandType, string tableName)
        {
            DataSet dataSet = new DataSet();
            using (SqlCommand command = new SqlCommand())
            {
                this.Connect();
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    command.CommandTimeout = this.CommandTimeout;
                    command.CommandText = commandText;
                    command.Connection = this._connection;
                    if (this._transaction != null)
                    {
                        command.Transaction = this._transaction;
                    }
                    command.CommandType = commandType;
                    this.CopyParameters(command);
                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet, tableName);
                    this._parameterCollection = command.Parameters;
                }
            }
            if (this.AutoCloseConnection)
            {
                this.Disconnect();
            }
            return dataSet;
        }

        public void LoadToDataSet(ref DataSet dataSet, string commandText, CommandType commandType, string tableName)
        {
            using (SqlCommand command = new SqlCommand())
            {
                this.Connect();
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    command.CommandTimeout = this.CommandTimeout;
                    command.CommandText = commandText;
                    command.Connection = this._connection;
                    if (this._transaction != null)
                    {
                        command.Transaction = this._transaction;
                    }
                    command.CommandType = commandType;
                    this.CopyParameters(command);
                    adapter.SelectCommand = command;
                    adapter.Fill(dataSet, tableName);
                    this._parameterCollection = command.Parameters;
                }
            }
            if (this.AutoCloseConnection)
            {
                this.Disconnect();
            }
        }



        public IEnumerable<T> GetList<T>(string commandText, CommandType commandType, Func<IDataReader, T> func)
        {
            this._autoCloseConnection = false;
            using (IDataReader rd = this.GetDataReader(commandText, commandType))
            {
                while (rd.Read())
                {
                    yield return func(rd);
                }
                rd.Close();
            }
        }


        public IEnumerable<Dictionary<string, object>> GetList_Dict(string commandText, CommandType commandType)
        {
            this._autoCloseConnection = false;
            using (IDataReader reader = this.GetDataReader(commandText, commandType))
            {
                var names = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                foreach (IDataRecord record in reader as IEnumerable)
                    yield return names.ToDictionary(n => n, n => record[n]);
            }
        }



        public DataTable GetDataTable(string commandText, CommandType commandType)
        {
            DataTable result = new DataTable();
            using (IDataReader rd = this.GetDataReader(commandText, commandType))
            {
                result.Load(rd);
                rd.Close();
            }

            //DataTable result = new DataTable();
            //string key = Utils.Zip(commandText);

            //if ((result = CacheUtitl.Get<DataTable>(key)) == null)
            //{
            //    //log.Info("cache null");
            //    result = new DataTable();
            //    using (IDataReader rd = this.GetDataReader(commandText, commandType))
            //    {
            //        result.Load(rd);
            //        rd.Close();
            //    }
            //    CacheUtitl.Set<DataTable>(key, result);
            //}

            return result;
        }

        public SqlDataReader GetDataReader(string commandText, CommandType commandType)
        {
            using (SqlCommand command = new SqlCommand())
            {
                this.Connect();
                command.CommandTimeout = this.CommandTimeout;
                command.CommandText = commandText;
                command.Connection = this._connection;
                if (this._transaction != null)
                {
                    command.Transaction = this._transaction;
                }
                command.CommandType = commandType;
                this.CopyParameters(command);
                CommandBehavior behavior = CommandBehavior.Default;
                if (this.AutoCloseConnection)
                {
                    behavior |= CommandBehavior.CloseConnection;
                }
                if (this._isSingleRow)
                {
                    behavior |= CommandBehavior.SingleRow;
                }
                SqlDataReader reader = command.ExecuteReader(behavior);
                this._parameterCollection = command.Parameters;
                return reader;
            }
        }

        public T GetScalar<T>(string commandText, CommandType commandType, T alt)
        {
            var o = GetScalar(commandText, commandType);
            if (o == null || o == DBNull.Value)
                return alt;
            else
                return (T)o;
        }

        public object GetScalar(string commandText, CommandType commandType)
        {
            object obj2 = null;
            using (SqlCommand command = new SqlCommand())
            {
                this.Connect();
                command.CommandTimeout = this.CommandTimeout;
                command.CommandText = commandText;
                command.Connection = this._connection;
                if (this._transaction != null)
                {
                    command.Transaction = this._transaction;
                }
                command.CommandType = commandType;
                this.CopyParameters(command);
                obj2 = command.ExecuteScalar();
                this._parameterCollection = command.Parameters;
            }
            return obj2;
        }

        public XmlReader GetXmlReader(string commandText, CommandType commandType)
        {
            using (SqlCommand command = new SqlCommand())
            {
                this.Connect();
                command.CommandTimeout = this.CommandTimeout;
                command.CommandText = commandText;
                command.Connection = this._connection;
                if (this._transaction != null)
                {
                    command.Transaction = this._transaction;
                }
                command.CommandType = commandType;
                this.CopyParameters(command);
                XmlReader reader = command.ExecuteXmlReader();
                this._parameterCollection = command.Parameters;
                return reader;
            }
        }
        #endregion

        #region Exec Procedure
        //------------------------------------------------------------------------------------------------
        public void ExecuteSP(string procedureName)
        {
            Execute(procedureName, CommandType.StoredProcedure);
        }

        public int ExecuteSPNonQuery(string procedureName)
        {
            return ExecuteNonQuery(procedureName, CommandType.StoredProcedure);
        }

        public DataSet ExecuteSPDataSet(string procedureName)
        {
            return GetDataSet(procedureName, CommandType.StoredProcedure);
        }

        public DataSet ExecuteSPDataSet(string procedureName, int startRecord, int maxRecords)
        {
            return GetDataSet(procedureName, CommandType.StoredProcedure, startRecord, maxRecords);
        }

        public DataSet ExecuteSPDataSet(string procedureName, string tableName, int startRecord, int maxRecords)
        {
            return GetDataSet(procedureName, CommandType.StoredProcedure, tableName, startRecord, maxRecords);
        }

        public DataSet ExecuteSPDataSet(string procedureName, string tableName)
        {
            return GetDataSet(procedureName, CommandType.StoredProcedure, tableName);
        }

        public void ExecuteSPDataSet(ref DataSet dataSet, string procedureName, string tableName)
        {
            LoadToDataSet(ref dataSet, procedureName, CommandType.StoredProcedure, tableName);
        }



        public IEnumerable<T> ExecuteSPList<T>(string procedureName, Func<IDataReader, T> func)
        {
            return GetList<T>(procedureName, CommandType.StoredProcedure, func);
        }



        public DataTable ExecuteSPDataTable(string procedureName)
        {
            return GetDataTable(procedureName, CommandType.StoredProcedure);
        }

        public SqlDataReader ExecuteSPReader(string procedureName)
        {
            return GetDataReader(procedureName, CommandType.StoredProcedure);
        }

        public T ExecuteSPScalar<T>(string procedureName, T alt)
        {
            return GetScalar<T>(procedureName, CommandType.StoredProcedure, alt);
        }

        public object ExecuteSPScalar(string procedureName)
        {
            return GetScalar(procedureName, CommandType.StoredProcedure);
        }

        public XmlReader ExecuteSPXmlReader(string procedureName)
        {
            return GetXmlReader(procedureName, CommandType.StoredProcedure);
        }

        #endregion Exec Procedure

        #region Exec Sql
        //------------------------------------------------------------------------------------------------

        public DataRow NewRow(string table)
        {
            DataRow dr = ExecuteSqlDataTable("SELECT * FROM [" + table.Trim().ToLower() + "] WHERE 1=2").NewRow();
            return dr;
        }

        public void ExecuteSql(string sql)
        {
            Execute(sql, CommandType.Text);
        }

        public int ExecuteSqlNonQuery(string sql)
        {
            return ExecuteNonQuery(sql, CommandType.Text);
        }

        public DataSet ExecuteSqlDataSet(string sql)
        {
            return GetDataSet(sql, CommandType.Text);
        }

        public DataSet ExecuteSqlDataSet(string sql, int startRecord, int maxRecords)
        {
            return GetDataSet(sql, CommandType.Text, startRecord, maxRecords);
        }


        public DataSet ExecuteSqlDataSet(string sql, string tableName)
        {
            return GetDataSet(sql, CommandType.Text, tableName);
        }

        public void ExecuteSqlDataSet(ref DataSet dataSet, string sql, string tableName)
        {
            LoadToDataSet(ref dataSet, sql, CommandType.Text, tableName);
        }



        public IEnumerable<T> ExecuteSqlList<T>(string sql, Func<IDataReader, T> func)
        {
            return GetList<T>(sql, CommandType.Text, func);
        }



        public DataTable ExecuteSqlDataTable(string sql)
        {
            return GetDataTable(sql, CommandType.Text);
        }

        public SqlDataReader ExecuteSqlReader(string sql)
        {
            return GetDataReader(sql, CommandType.Text);
        }

        public T ExecuteSqlScalar<T>(string sql, T alt)
        {
            return GetScalar<T>(sql, CommandType.Text, alt);
        }

        public object ExecuteSqlScalar(string sql)
        {
            return GetScalar(sql, CommandType.Text);
        }

        public XmlReader ExecuteSqlXmlReader(string sql)
        {
            return GetXmlReader(sql, CommandType.Text);
        }
        #endregion Exec SQL 

        #endregion Methods


    }
}

