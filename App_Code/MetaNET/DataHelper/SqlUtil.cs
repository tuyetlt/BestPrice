
namespace MetaNET.DataHelper
{
    using System;
    using System.Configuration;
    using System.Data;
    using log4net;


    [CLSCompliant(true)]
    public static class SqlUtil
    {
        public static readonly string AND = "AND";
        public static readonly string ASC = "ASC";
        public static readonly string COMMA = ",";
        public static readonly string DESC = "DESC";
        public static readonly string LEFT = "(";
        public static readonly string NULL = "NULL";
        public static readonly string OR = "OR";
        public static readonly string QUOTE = "\"";
        public static readonly string RIGHT = ")";
        public static readonly string STAR = "*";
        public static readonly string TOKEN = "@@@";
        public static readonly string WILD = "%";
        public static readonly string TRUE = "(1=1)";
        public static readonly string ALL = "*";

        static ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static DataTable SQLToDataTable(string table)
        {
            return SQLToDataTable(table, string.Empty, string.Empty);
        }

        public static DataTable SQLToDataTable(string table, string fields, string where)
        {
            return SQLToDataTable(table, fields, where, string.Empty);
        }

        public static DataTable SQLToDataTable(string table, string fields, string where, string orderby)
        {
            return SQLToDataTable(table, fields, where, orderby, 0, 0);
        }

        public static DataTable SQLToDataTable(string table, string fields, string where, string orderby, int pageIndex, int pageSize)
        {
            int totalRows = 0;
            return SQLToDataTable(table, fields, where, orderby, pageIndex, pageSize, out totalRows);
        }

        public static DataTable SQLToDataTable(string table, string fields, string where, string orderby, int pageIndex, int pageSize, out int totalRows)
        {
            totalRows = 0;
            string sqlQuery = string.Empty;

            if (string.IsNullOrEmpty(fields.Trim()))
                fields = "*";
            if (!string.IsNullOrEmpty(where.Trim()))
                where = " WHERE " + where;
            if (!string.IsNullOrEmpty(orderby.Trim()))
                orderby = " ORDER BY " + orderby;

            if (pageIndex < 1 || pageSize < 1)
            {
                sqlQuery = string.Format("SELECT {0} FROM {1}{2}{3}", fields, table, where, orderby);
            }
            else
            {
                string pagingSql = string.Format(@" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY", pageSize * (pageIndex - 1), pageSize);
                sqlQuery = string.Format("SELECT {0} FROM {1}{2}{3}{4}", fields, table, where, orderby, pagingSql);
            }

            DataTable dtReturn = null;
            string key = table + "_" + fields + "_" + where + "_" + orderby + "_" + pageIndex + "_" + pageSize;
            int CACHE_TIME_MINUTES = ConvertUtility.ToInt16(C.CACHE_TIME_MINUTES);

            if (CACHE_TIME_MINUTES>0)
            {
                if (CacheUtility.GetValueFromCache(key) == null)
                {
                    dtReturn = Exe_SQLToDataTable(table, sqlQuery, out totalRows);

                    CacheUtility.SetValueToCache(key, dtReturn, CACHE_TIME_MINUTES);
                    CacheUtility.SetValueToCache(key + "_totalRows", totalRows);
                }
                else
                {
                    dtReturn = (DataTable)CacheUtility.GetValueFromCache(key);
                    totalRows = ConvertUtility.ToInt32(CacheUtility.GetValueFromCache(key + "_totalRows"));

                    log.Info(table + " lấy từ Cache " + key);
                }
            }
            else
            {
                dtReturn = Exe_SQLToDataTable(table, sqlQuery, out totalRows);
            }

            return dtReturn;
        }

        public static DataTable Exe_SQLToDataTable(string table, string sqlQuery, out int totalRows)
        {
            log.Info(table + " lấy từ SQL");
            DataTable dtReturn = null;
            string query_count = string.Format("Select COUNT(ID) FROM {0}", table);
            using (var dbx = SqlService.GetSqlService())
            {
                dtReturn = dbx.ExecuteSqlDataTable(sqlQuery);
                DataTable dtCount = dbx.ExecuteSqlDataTable(query_count);
                totalRows = ConvertUtility.ToInt32(dtCount.Rows[0][0]);
            }
            return dtReturn;
        }

        public static string SQL_Query_Paging(string table, string fields, string where, string orderby, int page, int limit)
        {
            int offset = (page - 1) * limit;
            string sql = "SELECT  Temp.* FROM ( SELECT " + fields + ", ROW_NUMBER() OVER (ORDER BY " + orderby + ") AS RowNumber, TotalRows = COUNT(1) OVER ( ) FROM " + table + " WHERE 1=1  " + where + " ) AS Temp WHERE Temp.RowNumber > " + offset + " AND Temp.RowNumber <=" + (offset + limit) + " ORDER BY Temp.RowNumber";
            return sql;
        }





        public static string Contains(string value)
        {
            return string.Format("%{0}%", Encode(value));
        }

        public static string SelectFrom(int? top, string tableName, params string[] columns)
        {
            return "SELECT" + TOP(top, columns) + " FROM " + tableName;
        }
        public static string TOP(int? number, params string[] columns)
        {
            string ret = string.Empty;
            if (number != null && number.Value > 0)
            {
                ret += string.Format(" TOP {0} {1} ", number, string.Join(",", columns));
            }
            else
            {
                ret += string.Format(" {0} ", string.Join(",", columns));
            }
            return ret;
        }

        public static string Contains(string column, string value)
        {
            return Contains(column, value, false);
        }

        public static string Contains(string column, string value, bool ignoreCase)
        {
            return Contains(column, value, ignoreCase, true);
        }
        public static string Contains(string column, string value, bool ignoreCase, bool surround)
        {
            if (string.IsNullOrEmpty(value))
            {
                return IsNull(column);
            }
            return string.Format(GetLikeFormat(ignoreCase, surround), column, Contains(value));
        }

        public static string Encode(string value)
        {
            return Encode(value, false);
        }

        public static string Encode(string[] values)
        {
            return Encode(values, false);
        }

        public static string Encode(string value, bool surround)
        {
            if (string.IsNullOrEmpty(value))
            {
                return NULL;
            }
            string format = surround ? "'{0}'" : "{0}";
            return string.Format(format, value.Replace("'", "''"));
        }

        public static string Encode(string[] values, bool surround)
        {
            if ((values == null) || (values.Length < 1))
            {
                return NULL;
            }
            CommaDelimitedStringCollection strings = new CommaDelimitedStringCollection();
            foreach (string str in values)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    strings.Add(Encode(str.Trim(), surround));
                }
            }
            return strings.ToString();
        }

        public static string EndsWith(string value)
        {
            return string.Format("%{0}", Encode(value));
        }

        public static string EndsWith(string column, string value)
        {
            return EndsWith(column, value, false);
        }

        public static string EndsWith(string column, string value, bool ignoreCase)
        {
            return EndsWith(column, value, ignoreCase, true);
        }

        public static string EndsWith(string column, string value, bool ignoreCase, bool surround)
        {
            if (string.IsNullOrEmpty(value))
            {
                return IsNull(column);
            }
            return string.Format(GetLikeFormat(ignoreCase, surround), column, EndsWith(value));
        }

        public static string Equals(string value)
        {
            return string.Format("{0}", Encode(value));
        }

        public static string Equals(string column, string value)
        {
            return Equals(column, value, false);
        }

        public static string Equals(string column, string value, bool ignoreCase)
        {
            return Equals(column, value, ignoreCase, true);
        }

        public static string Equals(string column, string value, bool ignoreCase, bool surround)
        {
            if (string.IsNullOrEmpty(value))
            {
                return IsNull(column);
            }
            return string.Format(GetEqualFormat(ignoreCase, surround), column, Equals(value));
        }

        public static string GetEqualFormat(bool ignoreCase)
        {
            return GetEqualFormat(ignoreCase, true);
        }

        public static string GetEqualFormat(bool ignoreCase, bool surround)
        {
            if (surround)
            {
                return (ignoreCase ? "UPPER({0}) = UPPER('{1}')" : "{0} = '{1}'");
            }
            return (ignoreCase ? "UPPER({0}) = UPPER({1})" : "{0} = {1}");
        }

        public static string GetLikeFormat(bool ignoreCase)
        {
            return GetLikeFormat(ignoreCase, true);
        }

        public static string GetLikeFormat(bool ignoreCase, bool surround)
        {
            if (surround)
            {
                return (ignoreCase ? "UPPER({0}) LIKE UPPER('{1}')" : "{0} LIKE '{1}'");
            }
            return (ignoreCase ? "UPPER({0}) LIKE UPPER({1})" : "{0} LIKE {1}");
        }

        public static string IsNotNull(string column)
        {
            return string.Format("{0} IS NOT NULL", column);
        }

        public static string IsNull(string column)
        {
            return string.Format("{0} IS NULL", column);
        }

        public static string Like(string value)
        {
            return string.Format("{0}", Encode(value));
        }

        public static string Like(string column, string value)
        {
            return Like(column, value, false);
        }

        public static string Like(string column, string value, bool ignoreCase)
        {
            return Like(column, value, ignoreCase, true);
        }

        public static string Like(string column, string value, bool ignoreCase, bool surround)
        {
            if (string.IsNullOrEmpty(value))
            {
                return IsNull(column);
            }
            return string.Format(GetLikeFormat(ignoreCase, surround), column, Like(value));
        }

        public static string StartsWith(string value)
        {
            return string.Format("{0}%", Encode(value));
        }

        public static string StartsWith(string column, string value)
        {
            return StartsWith(column, value, false);
        }

        public static string StartsWith(string column, string value, bool ignoreCase)
        {
            return StartsWith(column, value, ignoreCase, true);
        }

        public static string StartsWith(string column, string value, bool ignoreCase, bool surround)
        {
            if (string.IsNullOrEmpty(value))
            {
                return IsNull(column);
            }
            return string.Format(GetLikeFormat(ignoreCase, surround), column, StartsWith(value));
        }
    }
}

