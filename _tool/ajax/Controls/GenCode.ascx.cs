using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class _tool_ajax_Controls_GenCode : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string HashTable = "", Type = "", UpdateQuery = "", InsertQuery1 = "", InsertQuery2 = "", InsertQuery = "", Parameter = "";
        string Return = "";
        string col = RequestHelper.GetString("col", "");
        string Table = RequestHelper.GetString("table", "");
        string DataType = RequestHelper.GetString("dataType", "");
        Type = RequestHelper.GetString("type", "");
        string[] allCol = col.Split('-');
        string[] allDataType = DataType.Split('-');

        int count = 0;
        foreach (string column in allCol)
        {
            if (count > 0)
            {
                UpdateQuery += ",";
                InsertQuery1 += ",";
                InsertQuery2 += ",";
            }

            UpdateQuery += string.Format("[{0}]=@{0}", column);
            HashTable += string.Format(@"hashtable[""{0}""] =  Utils.KillChars(Request.Form[""{0}""]);<br />", column);
            InsertQuery1 += string.Format(@"[{0}]", column);
            InsertQuery2 += string.Format(@"@{0}", column);


            string dataType = allDataType[count];
            Parameter += string.Format(@"db.AddParameter(""@{0}"", {1}, hashtable[""{0}""].ToString());<br />", column, GetDataType(dataType));

            count++;
        }

        UpdateQuery = string.Format(@"sqlQuery = @""UPDATE[dbo].[{0}] SET {1} WHERE [ID] = @ID"";", Table, UpdateQuery);
        InsertQuery = string.Format(@"sqlQuery = @""INSERT INTO [dbo].[{0}]({1}) OUTPUT INSERTED.ID VALUES ({2})"";", Table, InsertQuery1, InsertQuery2);



        StringBuilder FullQuery = new StringBuilder();
        FullQuery.AppendLine(string.Format(@"CacheUtility.PurgeCacheItems(""{0}"");", Table));
        FullQuery.AppendLine("<br /><br />");
        FullQuery.AppendLine("bool IsUpdate = false;");
        FullQuery.AppendLine("<br />");
        FullQuery.AppendLine("System.Collections.Hashtable hashtable = new System.Collections.Hashtable();");
        FullQuery.AppendLine("<br /><br />");
        FullQuery.AppendLine(HashTable);
        FullQuery.AppendLine("<br /><br />");
        FullQuery.AppendLine("using (var db = MetaNET.DataHelper.SqlService.GetSqlService())");
        FullQuery.AppendLine("<br />{");
        FullQuery.AppendLine("<br />");
        FullQuery.AppendLine("string sqlQuery = string.Empty;");
        FullQuery.AppendLine("<br />");
        FullQuery.AppendLine("if (IsUpdate)");
        FullQuery.AppendLine("<br />");
        FullQuery.AppendLine("  " + UpdateQuery);
        FullQuery.AppendLine("<br />");
        FullQuery.AppendLine("else");
        FullQuery.AppendLine("<br />");
        FullQuery.AppendLine("  " + InsertQuery);
        FullQuery.AppendLine("<br /><br />");
        FullQuery.AppendLine(Parameter);
        FullQuery.AppendLine("}");
        FullQuery.AppendLine("<br />");
        FullQuery.AppendLine("");
        FullQuery.AppendLine("");
        FullQuery.AppendLine("");
        FullQuery.AppendLine("");
        FullQuery.AppendLine("");
        FullQuery.AppendLine("");
        FullQuery.AppendLine("");





        Return += FullQuery;

        Response.Write(FullQuery);
    }

    protected string GetDataType(string type)
    {
        string Return = string.Empty;

        if (type == "nvarchar")
            Return = "System.Data.SqlDbType.NVarChar";
        else if (type == "int")
            Return = "System.Data.SqlDbType.Int";
        else if (type == "datetime")
            Return = "System.Data.SqlDbType.DateTime";
        else if (type == "money")
            Return = "System.Data.SqlDbType.Money";
        else if (type == "bit")
            Return = "System.Data.SqlDbType.Bit";
        else if (type == "decimal")
            Return = "System.Data.SqlDbType.Decimal";
        else
            Return = "System.Data.SqlDbType." + type;
        return Return;
    }


    protected void Test()
    {

    }

}