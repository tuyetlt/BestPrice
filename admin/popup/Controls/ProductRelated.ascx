<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductRelated.ascx.cs" Inherits="admin_popup_Controls_ProductRelated" %>
<%@ Import Namespace="System.Data" %>
<%@ Register Src="~/admin/Controls/AdminControl/HeaderLoad.ascx" TagPrefix="uc1" TagName="HeaderLoad" %>
<uc1:HeaderLoad runat="server" ID="HeaderLoad" />

<div class="popup-table">
    <div class="filter">
        <form method="post" enctype="multipart/form-data" id="frm_search">

          <%--  <select id="drCat" name="category" style="width: 300px">
                <option value="0">Chọn danh mục</option>
            </select>

            <script type="text/javascript">

                $(document).ready(function () {
                    var defaultCatID = '<%= category %>';
                    var selected = "";
                    $.getJSON("/admin/ajax/ajax.aspx", { ctrl: "select2", multilevel: "1", table: "tblCategories", moduls: "category", filterJson: '[{"Field":"Moduls","Value":"category", "Type":"equal"}]', t: Math.random() },
                        function (json) {
                            //cấp 1
                            $.each(json,
                                function (key, value) {
                                    if (value.id == defaultCatID)
                                        selected = "selected='selected'";
                                    $("#drCat").append("<option " + selected + " value='" + value.id + "'>" + value.text + "</option>");
                                    selected = "";
                                    //cấp 2
                                    $.each(value.children,
                                        function (key, value) {
                                            if (value.id == defaultCatID)
                                                selected = "selected='selected'";
                                            $("#drCat").append("<option " + selected + " value='" + value.id + "'> ⟶ " + value.text + "</option>");
                                            selected = "";
                                            //cấp 3
                                            $.each(value.children,
                                                function (key, value) {
                                                    if (value.id == defaultCatID)
                                                        selected = "selected='selected'";
                                                    $("#drCat").append("<option " + selected + " value='" + value.id + "'> ⟶⟶ " + value.text + "</option>");
                                                    selected = "";
                                                    //cấp 4
                                                    $.each(value.children,
                                                        function (key, value) {
                                                            if (value.id == defaultCatID)
                                                                selected = "selected='selected'";
                                                            $("#drCat").append("<option " + selected + " value='" + value.id + "'> ⟶⟶⟶ " + value.text + "</option>");
                                                            selected = "";
                                                        });
                                                });
                                        });
                                });
                        });



                    var defaultCatID = $("#categoryid").val();
                    console.log(defaultCatID);
                });


                $("#drCat").change(function () {
                    $("#categoryid").val($(this).val());
                });

            </script>--%>


            <input type="text" id="searchbox" name="keyword" placeholder="Từ khóa tìm kiếm" />
            <input type="submit" id="btnSearch" class="btn" value="Tìm kiếm" />

            <input type="hidden" id="done_search" name="done_search" value="0" />
            <script type="text/javascript">

                $("#btnSearch").click(function () {
                    $('#frm_search #done_search').val("done_search");
                    $("#frm_search").submit();
                });
            </script>
        </form>

    </div>
    <div class="clear"></div>

    <%
        if (Utils.CheckExist_DataTable(dtProducts))
        {
            int count = 0;
    %>
    <div class="news-list">

        <% for (int i = 0; i < dtProducts.Rows.Count; i++)
            {
        %>
        <a class="item" onclick="SelectValue('<%= dtProducts.Rows[i]["ID"].ToString() %>');" href="javascript:;">
            <span class="img">
                <img src="<%= Utils.GetFirstImageInGallery_Json(dtProducts.Rows[i]["Gallery"].ToString(), 50, 30) %>" alt="<%= dtProducts.Rows[i]["Name"].ToString() %>" /></span>

            <div class="caption">
                <p class="text">
                    <%= dtProducts.Rows[i]["Name"].ToString() %>
                </p>
            </div>
        </a>
        <div class="clear"></div>
        <%  count++;
                }
            } %>
    </div>
</div>


<script type="text/javascript">
                function SelectValue(value) {
                    var val = window.opener.$("#" + queryString('textbox')).val();
                    var dauphay = '';
                    if (val.length > 0) {
                        dauphay = ",";
                    }
                    //kiểm tra thằng nào trùng thì thôi không add nữa
                    var name_array = val.split(',');
                    for (var i = 0; i < name_array.length; i++) {
                        if (name_array[i] == value) {
                            alert("Sản phẩm này đã tồn tại");
                            return;
                        }
                    }
                    window.opener.$("#" + queryString('textbox')).val(val + dauphay + value);
                    window.opener.$("#" + queryString('button')).click();
                    //window.close();
                }



                function queryString(parameter) {
                    var loc = location.search.substring(1, location.search.length);
                    var param_value = false;
                    var params = loc.split("&");

                    for (i = 0; i < params.length; i++) {
                        param_name = params[i].substring(0, params[i].indexOf('='));
                        if (param_name == parameter) {
                            param_value = params[i].substring(params[i].indexOf('=') + 1)
                        }
                    }

                    if (param_value) {
                        return param_value;
                    }
                    else {
                        return false; //Here determine return if no parameter is found
                    }
                }


</script>

