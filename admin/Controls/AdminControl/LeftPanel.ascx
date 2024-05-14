<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftPanel.ascx.cs" Inherits="admin_Controls_AdminControl_LeftPanel" %>
<aside class="left-panel">
    <div class="logo">
        <a href="/admin/"><img src="<%= ConfigWeb.LogoAdmin %>" alt="Admin" style="height:40px; margin:5px 0" /></a>
    </div>
    <div class="clear"></div>
    <ul>
                <li><a href="<%= C.ROOT_URL %>" target="_blank"><i class="fad fa-home-lg-alt"></i>Về trang chủ</a>
        </li>

        <%

            using (var db = MetaNET.DataHelper.SqlService.GetSqlService())
            {
                string sqlQuery = string.Format(string.Format("SELECT * FROM tblAdminMenu WHERE ParentID=0 AND {0} ORDER BY Sort", Utils.CreateFilterHide));
                var dt = db.ExecuteSqlDataTable(sqlQuery);
                if (dt.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow dr in dt.Rows)
                    {
                        string child = "";
                        string linkDefault = "";
                        using (var dbx = MetaNET.DataHelper.SqlService.GetSqlService())
                        {
                             string sqlQueryx = string.Format(string.Format("SELECT * FROM tblAdminMenu WHERE ParentID={0} AND {1} ORDER BY Sort", dr["ID"], Utils.CreateFilterHide));
                            var dtx = dbx.ExecuteSqlDataTable(sqlQueryx);
                            if (dtx.Rows.Count > 0)
                            {
                                if (Utils.GetFolderControlAdmin().ToLower() == dr["Control"].ToString().ToLower())
                                    child = "<ul style='display: block;'>";
                                else
                                    child = "<ul>";
                                foreach (System.Data.DataRow drx in dtx.Rows)
                                {
                                    string controlParent = dr["Control"].ToString().ToLower();

                                    string active = "";
                                    if (drx["Control"].ToString().ToLower() == Utils.GetControlAdmin())
                                        active = "active";

                                    string linkDetail = string.Format(@"/admin/{0}/{1}", controlParent, drx["Control"].ToString().ToLower());
                                    child += string.Format(@"<li><a class='{0}' href=""{1}"">{2}</a></li>", active, linkDetail, drx["Name"].ToString());
                                    if (drx == dtx.Rows[0])
                                        linkDefault = linkDetail;
                                }
                                child += "</ul>";
                            }
                        }

                        string access = Utils.AccessPermission(dr["Control"].ToString().ToLower());
                        if (access != "DENIED")
                        {
        %>
        <li>
            <a href="<%= linkDefault %>"><i class="<%= dr["Icon"] %>"></i><%= dr["Name"].ToString() %>
            </a>
            <a class="expan"><i class="fas fa-chevron-down"></i></a>
            <%= child %>
        </li>
        <%}
                    }
                }
            } %>
        <li>
           
        </li>
    </ul>
</aside>
