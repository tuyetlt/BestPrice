<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AjaxFlashSaleControl.ascx.cs" Inherits="admin_ajax_Controls_AjaxFlashSaleControl" %>
<div class="freezetable">
    <table>
               <thead>
            <tr>
                <% if (Level.ToLower() == "false")
                    { %>
                <th style="width: 40px"></th>
                <%} %>
                <th style="width: 40px">
                    <input type="checkbox" id="selectAll" /></th>
                <%               
                    foreach (MenuAdminJson json in jsonHeaderField)
                    {
                        if (json.Show)
                        {
                            int Width = ConvertUtility.ToInt32(json.Width);
                            string strWidth = "auto";
                            if (Width > 0)
                                strWidth = Width + "px";
                            string Sort = "";
                            if (dataFieldSort == json.Field)
                                Sort = dataSort;


                            if (json.Field == "CreatedDate")
                            {
                                Response.Write("<th>Tạo bởi</th>");
                            }
                            else if (json.Field == "EditedDate")
                            {
                                Response.Write("<th>Sửa bởi</th>");
                            }
                            else if (json.Field == "CreatedBy" || json.Field == "EditedBy")
                            {
                                Response.Write("");
                            }
                            else
                            {
                %>
                <th class="sort" data-field="<%= json.Field %>" data-sort="<%= Sort %>" style="width: <%= strWidth %>"><%= json.Text %></th>
                <%
                            }
                        }
                    }
                %>
                <th style="width: 300px">Thao tác</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>

                </td>
            </tr>
        </tbody>
    </table>
</div>
