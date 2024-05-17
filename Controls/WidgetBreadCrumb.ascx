<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WidgetBreadCrumb.ascx.cs" Inherits="Controls_WidgetBreadCrumb" %>
<%--<nav aria-label="breadcrumb">
    <ul class="breadcrumb mb-0">
        <%           
            foreach (BreadCrumb breadCrumb in breadCrumbList)
            {
        %>
        <li class="breadcrumb-item"><a href="<%= breadCrumb.Link %>"><%= breadCrumb.Name %></a></li>
        <%
            }
        %>
    </ul>
</nav>--%>


<%@ Import Namespace="System.Data" %>
<%
    string slash = "";
    DataRow dr;
    DataTable dt;
%>
<ul class="breadcrumb mb-0">
    <li class="breadcrumb-item"><a href="/">Trang chủ</a></li>
    <%
        Response.Write(slash);
        int CategoryID = ConvertUtility.ToInt32(PageInfo.CategoryID);


        if (CategoryID > 0)
        {
            Stack<BreadCrumb> bcList = new Stack<BreadCrumb>();

            dt = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID, ParentID, LinkTypeMenuFlag, FriendlyUrl, Name, Link", string.Format("ID='{0}'", CategoryID), "Sort");
            if (Utils.CheckExist_DataTable(dt))
            {
                dr = dt.Rows[0];

                //add thêm thằng hiện tại
                BreadCrumb bcCurrent = new BreadCrumb();
                bcCurrent.Link = Utils.CreateCategoryLink(dr["LinkTypeMenuFlag"], dr["FriendlyUrl"], dr["Link"]);
                bcCurrent.Name = ConvertUtility.ToString(dr["Name"]);
                bcCurrent.ID = ConvertUtility.ToString(dr["ID"]);
                bcList.Push(bcCurrent);


                int countP = 0;
                DataRow drRoot = dr;
                int RootID = ConvertUtility.ToInt32(drRoot["ID"]);
                do
                {

                    if (ConvertUtility.ToInt32(drRoot["ParentID"]) > 0)
                    {
                        countP = countP + 1;
                        DataTable dtRoot = SqlHelper.SQLToDataTable(C.CATEGORY_TABLE, "ID, ParentID, LinkTypeMenuFlag, FriendlyUrl, Name, Link", string.Format("ID={0}", drRoot["ParentID"]), "Sort");
                        if (Utils.CheckExist_DataTable(dtRoot))
                        {
                            drRoot = dtRoot.Rows[0];
                            RootID = ConvertUtility.ToInt32(drRoot["ID"]);

                            BreadCrumb bc = new BreadCrumb();
                            bc.Link = Utils.CreateCategoryLink(drRoot["LinkTypeMenuFlag"], drRoot["FriendlyUrl"], drRoot["Link"]);
                            bc.Name = ConvertUtility.ToString(drRoot["Name"]);
                            bc.ID = ConvertUtility.ToString(drRoot["ID"]);
                            bcList.Push(bc);
                        }
                        else
                            break;
                    }
                }
                while (ConvertUtility.ToInt32(drRoot["ParentID"]) > 0 && countP <= 5);
            }

            int count = 0;
            if (bcList != null && bcList.Count > 0)
            {
                foreach (BreadCrumb bc in bcList)
                {
                    if (count > 0)
                        Response.Write(slash);
                    Response.Write(@"<li class=""breadcrumb-item"">");
                    Response.Write(string.Format(@"<a href=""{0}"">{1}</a>", bc.Link, bc.Name));
                    Response.Write(@"</li>");
                    count++;
                }
            }

            if (Page.User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(PageInfo.LinkEdit))
                    Response.Write(string.Format(@"<a rel=""nofollow"" class=""edit"" target=""_blank"" href=""{0}""><i class=""fad fa-edit""></i>{1}</a>", PageInfo.LinkEdit, "Admin"));
                else
                    Response.Write(string.Format(@"<a rel=""nofollow"" class=""edit"" target=""_blank"" href=""{0}""><i class=""fad fa-edit""></i>Admin ({1})</a>", "/admin/category/categoryupdate?id=" + CategoryID, CategoryID));
            }


            // Schema

            if (bcList != null && bcList.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}", Environment.NewLine);
                sb.AppendLine(@"<script type=""application/ld+json"">");
                sb.AppendLine(@"{");
                sb.AppendLine(@"""@context"": ""https://schema.org"",");
                sb.AppendLine(@"""@type"": ""BreadcrumbList"",");
                sb.Append(@"""itemListElement"": [");
                int countSchema = 0;
                foreach (BreadCrumb bc in bcList)
                {
                    countSchema++;
                    sb.Append(@"{");
                    sb.Append(@"""@type"": ""ListItem"",");
                    sb.AppendFormat(@"""position"": {0},", countSchema);
                    sb.Append(string.Format(@"""name"": ""{0}"",", bc.Name));
                    sb.Append(string.Format(@"""item"": ""{0}""", bc.Link));
                    sb.Append("}");
                    if (countSchema < bcList.Count)
                        sb.AppendLine(",");
                }
                sb.AppendLine("]");
                sb.AppendLine("}");
                sb.AppendLine(@"</script>");

                Response.Write(sb);
            }
        }
        else if (!string.IsNullOrEmpty(PageInfo.ControlName))
        {
            Response.Write(@"<li class=""breadcrumb-item"">");
            Response.Write(string.Format(@"<a href=""{0}"">{1}</a>", Request.RawUrl, PageInfo.ControlName));
            Response.Write(@"</li>");
        }
        else
        {
            Response.Write(@"<li class=""breadcrumb-item"">");
          
            if (Utils.CheckIsHomePage)
                Response.Write(string.Format(@"<h1><a href=""{0}"">{1}</a></h1>", C.ROOT_URL, ConfigWeb.Slogan));
            else
                Response.Write(string.Format(@"<a href=""{0}"">{1}</a>", C.ROOT_URL, ConfigWeb.Slogan));
            Response.Write(@"</li>");
        }
    %>
</ul>
