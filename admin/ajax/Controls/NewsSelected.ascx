<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewsSelected.ascx.cs" Inherits="admin_ajax_Controls_NewsSelected" %>
<%@ Import Namespace="System.Data" %>
<% if (!Utils.IsNullOrEmpty(articleIDList))
        { %>
<div class="popup-table1">
    <div class="clear"></div>
    <%
       
            string filter = string.Format("ID in ({0})", articleIDList);
            DataTable dtNews = SqlHelper.SQLToDataTable(C.ARTICLE_TABLE, "ID,FriendlyUrl, Name, Gallery, Description", filter, "EditedDate DESC");
            if (Utils.CheckExist_DataTable(dtNews))
            {
                int count = 0;
    %>
    <div class="news-list news-selected">
        <% for (int i = 0;i < dtNews.Rows.Count; i++)
            {
        %>
        <div class="item">
            <a href="<%= TextChanger.GetLinkRewrite_Article(dtNews.Rows[i]["FriendlyUrl"].ToString()) %>">
            <img src="<%= Utils.GetFirstImageInGallery_Json(dtNews.Rows[i]["Gallery"].ToString(), 50, 40) %>" alt="<%= dtNews.Rows[i]["Name"].ToString() %>" /></a>
            <div class="caption">
                <p class="text">
                    <%= dtNews.Rows[i]["Name"].ToString() %>
                </p>
            </div>
            <a class="delete" href="javascript:void(0);" onclick="RemoveID_Article(<%= dtNews.Rows[i]["ID"].ToString() %>)">
                <i class="fas fa-minus-hexagon"></i>
            </a>
        </div>
        <div class="clear"></div>
        <%  count++;
                    }
                } %>
    </div>
</div>
<script type="text/javascript">
    function RemoveID_Article(aid) { 
        //lấy chuỗi trong
        var name_array = [<%= articleIDList %>];

        //kiểm tra thằng nào đúng id cần xóa thì xóa đi
        for (var i = 0; i < name_array.length; i++) {
            if (name_array[i] == aid) {
                name_array.splice(i, 1);
            }
        }

        //alert(name_array.join());

        //đặt lại trong TXT chuỗi mới
        $("#<%= txt %>").val(name_array.join());

        //nhấn nút Call Ajax để nó gọi lại
        $("#<%= btn %>").click();
    }
</script>

<% } %>