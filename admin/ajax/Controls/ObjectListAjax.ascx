<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ObjectListAjax.ascx.cs" Inherits="admin_ajax_Controls_ObjectListAjax" %>
<%@ Import Namespace="System.Data" %>

<%
    DataRow drB;
    DataTable dt;
    string slash = @"<i class=""fal fa-angle-right""></i>&nbsp;";
    int CategoryID = ConvertUtility.ToInt32(CookieUtility.GetValueFromCookie("FolderCategory" + tableSql));
    if (CategoryID > 0 && Utils.CommaSQLAdd(fieldSql).ToLower().Contains(",parentid,"))
    {
%>

<div class="link-map-item">
    <a class="beadcrumb" data-id="0">
        <i class="fas fa-home"></i>
    </a>
</div>

<%
        Response.Write(slash);

        Stack<BreadCrumb> bcList = new Stack<BreadCrumb>();
        dt = SqlHelper.SQLToDataTable(tableSql, "ID, ParentID, Name", string.Format("ID='{0}'", CategoryID));
        if (Utils.CheckExist_DataTable(dt))
        {
            drB = dt.Rows[0];

            //add thêm thằng hiện tại
            BreadCrumb bcCurrent = new BreadCrumb();
            bcCurrent.ID = ConvertUtility.ToString(drB["ID"]);
            bcCurrent.Name = ConvertUtility.ToString(drB["Name"]);
            bcList.Push(bcCurrent);


            DataRow drRoot = drB;
            int RootID = ConvertUtility.ToInt32(drRoot["ID"]);
            do
            {
                if (ConvertUtility.ToInt32(drRoot["ParentID"]) > 0)
                {
                    DataTable dtRoot = SqlHelper.SQLToDataTable(tableSql, "ID, ParentID, Name", string.Format("ID={0}", drRoot["ParentID"]));
                    if (Utils.CheckExist_DataTable(dtRoot))
                    {
                        drRoot = dtRoot.Rows[0];
                        RootID = ConvertUtility.ToInt32(drRoot["ID"]);

                        BreadCrumb bc = new BreadCrumb();
                        bc.ID = ConvertUtility.ToString(drRoot["ID"]);
                        bc.Name = ConvertUtility.ToString(drRoot["Name"]);
                        bcList.Push(bc);
                    }
                }
            }
            while (ConvertUtility.ToInt32(drRoot["ParentID"]) > 0);
        }

        int count = 0;
        if (bcList != null && bcList.Count > 0)
        {
            foreach (BreadCrumb bc in bcList)
            {
                if (count > 0)
                    Response.Write(slash);
                Response.Write(@"<div class=""link-map-item"">");
                Response.Write(string.Format(@"<a href=""javascript:void(0);"" class=""beadcrumb"" data-id=""{0}"">{1}</a>", bc.ID, bc.Name));
                Response.Write(@"</div>");
                count++;
            }
        }
    }
    //else if (!string.IsNullOrEmpty(PageInfo.ControlName))
    //{
    //    Response.Write(@"<div class=""link-map-item"">");
    //    Response.Write(string.Format(@"<a href=""{0}"">{1}</a>", Request.RawUrl, PageInfo.ControlName));
    //    Response.Write(@"</div>");
    //}




    string GrandFather = "0";
    bool HasChild = false;
    string FolderCategoryParent = CookieUtility.GetValueFromCookie("FolderCategoryParent" + tableSql);
    if (!string.IsNullOrEmpty(FolderCategoryParent))
    {
        DataTable dtParentCat = SqlHelper.SQLToDataTable(tableSql, "", "ID=" + FolderCategoryParent, "ID", 1, 1);
        if (Utils.CheckExist_DataTable(dtParentCat))
        {
            GrandFather = dtParentCat.Rows[0]["ParentID"].ToString();
        }

        CookieUtility.SetValueToCookie("FolderCategoryParent" + tableSql, null);
%>

<%
    }
%>




<script type="text/javascript">

    $(".beadcrumb").click(function () {
        var id = $(this).attr("data-id");
        setCookie('FolderCategory<%= tableSql %>', id, 1);
        getval(0);
    });



    $("#getparentfoldercategory").click(function () {
        setCookie('FolderCategory<%= tableSql %>', '<%= FolderCategoryParent %>', 1);
        setCookie('FolderCategoryParent<%= tableSql %>', '<%= GrandFather %>', 1);
        getval(0);
    });

    function setCookie(name, value, days) {
        var expires = "";
        if (days) {
            var date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            expires = "; expires=" + date.toUTCString();
        }
        document.cookie = name + "=" + (value || "") + expires + "; path=/";
    }
</script>

<div class="freezetable">
    <table style="min-width: <%= totalWidthTable %>px;">
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



            <%

                string editLink = Utils.GetEditControl();

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        if (Utils.CommaSQLAdd(fieldSql).ToLower().Contains(",parentid,"))
                        {
                            DataTable dtchildCat = SqlHelper.SQLToDataTable(tableSql, "", "ParentID=" + dr["ID"].ToString(), "ID", 1, 1);
                            if (Utils.CheckExist_DataTable(dtchildCat))
                            {
                                HasChild = true;
                            }
                        }
            %>

            <tr data-id="<%= dr["ID"].ToString() %>">

                <% if (Level.ToLower() == "false")
                    {
                        Response.Write("<td>");
                        if (HasChild)
                        {
                %>

                <a class="price-edit-edit" href="javascript:;" id="getchildfoldercategory<%= dr["ID"].ToString() %>" style="float: right"><i style="font-size: 30px; color: #ffd800" class="fas fa-folder-open"></i></a>
                <script type="text/javascript">
                    $("#getchildfoldercategory<%= dr["ID"].ToString() %>").click(function () {
                        setCookie('FolderCategory<%= tableSql %>', '<%= dr["ID"].ToString() %>', 1);
                        setCookie('FolderCategoryParent<%= tableSql %>', '<%= dr["ParentID"].ToString() %>', 1);
                        getval(0);
                    });
                </script>



                <%}
                        Response.Write("</td>");
                    }

                %>

                <%
                    bool isPriceTemporaryByDate = false;
                    bool isPriceTemporary_NonDate = false;

                    string price = SqlHelper.GetPrice(ConvertUtility.ToInt32(dr["ID"]), "Price", "VNĐ", true, out isPriceTemporaryByDate);
                    SqlHelper.GetPrice(ConvertUtility.ToInt32(dr["ID"]), "Price", "VNĐ", false, out isPriceTemporary_NonDate);

                    string classPriceTemporary = "";
                    if (isPriceTemporaryByDate)
                        classPriceTemporary = "priceTemporaryDateActive";
                    else if (isPriceTemporary_NonDate)
                        classPriceTemporary = "priceTemporaryActive";



                    string image = string.Empty;
                    //Response.Write(string.Format("<tr data-id='{0}'>", dr["ID"]));
                    Response.Write(string.Format(@"<td><input type=""checkbox"" id=""select_{0}"" /></td>", dr["ID"]));
                    string linkEdit = string.Format(@"{0}?id={1}", editLink, dr["ID"]);

                    string levelCSS = "";
                    if (Level.ToLower() == "true")
                        levelCSS = string.Format(@" class=""level_{0}""", dr["Level_Bullet"].ToString());

                    foreach (DataColumn dc in dataTable.Columns)
                    {
                        string Column = dc.ColumnName.ToString();

                        if (Column == "CreatedDate")
                        {
                            Response.Write("<td class='member-info'>");

                            DataTable dtUser = dtUser = SqlHelper.SQLToDataTable("tblAdminUser", "Gallery,Name", "ID=" + ConvertUtility.ToInt32(dr["CreatedBy"]));
                            if (dtUser != null && dtUser.Rows.Count > 0)
                            {
                                string avatar = C.NO_IMG_PATH;
                                List<GalleryImage> galleryList = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<GalleryImage>>(dtUser.Rows[0]["Gallery"].ToString());
                                if (galleryList != null && galleryList.Count > 0)
                                    avatar = galleryList[0].Path;

                                Response.Write(string.Format(@"<img src=""{0}"" /><p><b>{1}</b></p><p>{2}</p>", avatar, dtUser.Rows[0]["Name"], dr["CreatedDate"]));
                            }
                            Response.Write("</td>");
                        }
                        else if (Column == "EditedDate")
                        {
                            Response.Write("<td class='member-info'>");

                            DataTable dtUser = dtUser = SqlHelper.SQLToDataTable("tblAdminUser", "Gallery,Name", "ID=" + ConvertUtility.ToInt32(dr["EditedBy"]));
                            if (dtUser != null && dtUser.Rows.Count > 0)
                            {
                                string avatar = C.NO_IMG_PATH;
                                List<GalleryImage> galleryList = Newtonsoft.Json.JsonConvert.DeserializeObject<System.Collections.Generic.List<GalleryImage>>(dtUser.Rows[0]["Gallery"].ToString());
                                if (galleryList != null && galleryList.Count > 0)
                                    avatar = galleryList[0].Path;

                                Response.Write(string.Format(@"<img src=""{0}"" /><p><b>{1}</b></p><p>{2}</p>", avatar, dtUser.Rows[0]["Name"], dr["EditedDate"]));
                            }
                            Response.Write("</td>");
                        }
                        else if (Column == "CreatedBy" || Column == "EditedBy")
                        {
                            Response.Write("");
                        }
                        else if (SqlHelper.GetForeignTable_Check(Column))
                        {
                            string ColumnValue = SqlHelper.GetForeignTable(Column, dr[Column].ToString());
                            Response.Write(string.Format(@"<td>{0}</td>", ColumnValue));
                        }
                        else if (Column == "Name")
                        {
                            Response.Write(string.Format(@"<td{0}><a href=""{1}"">{2}</a></td>", levelCSS, linkEdit, dr["Name"].ToString()));
                        }
                        else if (Column == "PriceFinal")
                        {
                            Response.Write(string.Format(@"<td class=""price"">{0:N0} VNĐ</td>", dr[Column]));
                        }
                        else if (Column == "Price" || Column == "Price1")
                        {
                            int IDProduct = ConvertUtility.ToInt32(dr["ID"]);
                            if (tableSql == "tblPrice")
                                IDProduct = ConvertUtility.ToInt32(dr["ProductID"]);
                %>

                <td class="price">

                    <span><%= SqlHelper.GetPrice(IDProduct, Column, false) %></span>
                    <a class="price-edit-edit" href="javascript:;" style="float: right"><i class="fal fa-edit"></i></a>
                    <input type="text" value="<%= string.Format("{0:N0}", dr[Column]) %>" class="edit-price" data-field="<%=Column%>" data-id="<%= dr["ID"] %>" />
                    <span class="entertoupdate">Enter để lưu giá</span>

<%--                    <% if (Utils.GetFolderControlAdmin() == "product" && Column == "Price")
                        { %>
                    <a href="javascript:;" class="btnPrice <%= classPriceTemporary %>" data-value="<%= dr["ID"] %>" aria-label="Giá tạm thời" data-microtip-position="top" role="tooltip">
                        <i class="fad fa-usd-circle"></i>
                    </a>
                    <% } %>--%>

                </td>
                <%
                    }
                    else if (Column == "AttrProductFlag" && tableSql == C.PRODUCT_TABLE)
                    {
                        int AttrInt = ConvertUtility.ToInt32(dr["AttrProductFlag"]);
                        AttrProductFlag attrProFlag = (AttrProductFlag)AttrInt;
                %>
                <td class="checkbox flag" data-field="AttrProductFlag" data-id="<%= dr["ID"].ToString() %>">
                    <input type="checkbox" id="home<%= dr["ID"].ToString() %>" name="home" data-value="<%= (int)AttrProductFlag.Home %>" <%= Utils.SetChecked(attrProFlag.HasFlag(AttrProductFlag.Home)) %> />
                    <label for="home<%= dr["ID"].ToString() %>">Đặt lên trang chủ</label>
                    <input type="checkbox" id="home1<%= dr["ID"].ToString() %>" name="home1" data-value="<%= (int)AttrProductFlag.Home1 %>" <%= Utils.SetChecked(attrProFlag.HasFlag(AttrProductFlag.Home1)) %> />
                    <label for="home1<%= dr["ID"].ToString() %>">Đặt lên trang chủ (dưới)</label>
                    <input type="checkbox" id="priority<%= dr["ID"].ToString() %>" name="priority" data-value="<%= (int)AttrProductFlag.Priority %>" <%= Utils.SetChecked(attrProFlag.HasFlag(AttrProductFlag.Priority))%> />
                    <label for="priority">Sản phẩm ưu tiên</label>
                    <input type="checkbox" id="chkNew<%= dr["ID"].ToString() %>" name="chkNew" data-value="<%= (int)AttrProductFlag.New %>" <%= Utils.SetChecked(attrProFlag.HasFlag(AttrProductFlag.New))%> />
                    <label for="chkNew<%= dr["ID"].ToString() %>">Sản phẩm mới</label>
                </td>

                <% }
                    else if (Column == "PositionMenuFlag" && tableSql == C.CATEGORY_TABLE)
                    {
                        int AttrInt = ConvertUtility.ToInt32(dr["PositionMenuFlag"]);
                        PositionMenuFlag positionFlag = (PositionMenuFlag)AttrInt;
                %>
                <td class="checkbox flag" data-field="PositionMenuFlag" data-id="<%= dr["ID"].ToString() %>">
                    <input type="checkbox" name="Main" id="Main<%= dr["ID"].ToString() %>" data-value="<%= (int)PositionMenuFlag.Main %>" <%= Utils.SetChecked(positionFlag.HasFlag(PositionMenuFlag.Main)) %> />
                    <label for="Main<%= dr["ID"].ToString() %>">Menu chính</label>
                    <input type="checkbox" name="Top" id="Top<%= dr["ID"].ToString() %>" data-value="<%= (int)PositionMenuFlag.Top %>" <%= Utils.SetChecked(positionFlag.HasFlag(PositionMenuFlag.Top))%> />
                    <label for="Top<%= dr["ID"].ToString() %>">Menu trên</label>
                    <input type="checkbox" name="Bottom" id="Bottom<%= dr["ID"].ToString() %>" data-value="<%= (int)PositionMenuFlag.Bottom %>" <%= Utils.SetChecked(positionFlag.HasFlag(PositionMenuFlag.Bottom))%> />
                    <label for="Bottom<%= dr["ID"].ToString() %>">Menu dưới</label>
                    <input type="checkbox" id="MenuSub<%= dr["ID"].ToString() %>" name="MenuSub" data-value="<%= (int)PositionMenuFlag.MenuSub %>" <%= Utils.SetChecked(positionFlag.HasFlag(PositionMenuFlag.MenuSub))%> />
                    <label for="MenuSub<%= dr["ID"].ToString() %>">Danh mục con</label>
                    <input type="checkbox" id="MenuSubMainHome<%= dr["ID"].ToString() %>" name="MenuSubMainHome" data-value="<%= (int)PositionMenuFlag.MenuSubMainHome %>" <%= Utils.SetChecked(positionFlag.HasFlag(PositionMenuFlag.MenuSubMainHome))%> />
                    <label for="MenuSubMainHome<%= dr["ID"].ToString() %>">Sub home 1</label>
                    <input type="checkbox" id="MenuSubMainHome2<%= dr["ID"].ToString() %>" name="MenuSubMainHome2" data-value="<%= (int)PositionMenuFlag.MenuSubMainHome2 %>" <%= Utils.SetChecked(positionFlag.HasFlag(PositionMenuFlag.MenuSubMainHome2))%> />
                    <label for="MenuSubMainHome2<%= dr["ID"].ToString() %>">Sub home 2</label>
                    <input type="checkbox" id="Style1<%= dr["ID"].ToString() %>" name="Style1" data-value="<%= (int)PositionMenuFlag.Style1 %>" <%= Utils.SetChecked(positionFlag.HasFlag(PositionMenuFlag.Style1))%> />
                    <label for="Style1<%= dr["ID"].ToString() %>">Style 1</label>
                    <input type="checkbox" id="Style2<%= dr["ID"].ToString() %>" name="Style2" data-value="<%= (int)PositionMenuFlag.Style2 %>" <%= Utils.SetChecked(positionFlag.HasFlag(PositionMenuFlag.Style2))%> />
                    <label for="Style2<%= dr["ID"].ToString() %>">Style 2</label>
                </td>

                <% }
                    else if (Column == "Sort")
                    {
                %>

                <td class="sort">
                    <span><%= dr["Sort"] %></span>
                    <a class="price-edit-edit" href="javascript:;" style="float: right"><i class="fal fa-edit"></i></a>
                    <input type="text" value="<%= dr["Sort"] %>" class="edit-sort" data-id="<%= dr["ID"] %>" />
                    <span class="entertoupdate">Enter để lưu vị trí</span>
                </td>


                <%
                        }
                        else if (Column == "Gallery")
                        {
                            string jSon = dr[dc.ColumnName].ToString();
                            string imgPath = Utils.GetFirstImageInGallery_Json(jSon);
                            image = string.Format(@"<img src=""{1}?width=200&quality=100"" />", linkEdit, imgPath);
                            Response.Write(string.Format(@"<td><a href=""{0}""><img src=""{1}?width=40&height=40&quality=100&mode=crop"" /></a></td>", linkEdit, imgPath));
                        }
                        else if (dc.ColumnName.StartsWith("Image_") || dc.ColumnName == "Icon" || dc.ColumnName == "Thumbnail")
                        {
                            string img = dr[dc.ColumnName].ToString();
                            if (string.IsNullOrEmpty(img))
                                img = C.NO_IMG_PATH;

                            Response.Write(string.Format(@"<td><a href=""{0}""><img class=""bg"" src=""{1}?height=40&quality=100"" /></a></td>", linkEdit, img));
                        }
                        else if (Column == "Level_Bullet")
                        {
                            // No thing
                            //Response.Write("<td>Nothing</td>");
                        }
                        else if (Column == "Link")
                        {
                            Response.Write(string.Format(@"<td><a target=""_blank"" href=""{0}"">{0}</a></td>", dr[dc.ColumnName].ToString()));
                        }
                        else if (Column == "OrderDate") // Ngày đặt hàng
                        {
                            Response.Write(string.Format(@"<td>{0} <i>({1})</i></td>", dr[dc.ColumnName].ToString(), Utils.TimeAgo(ConvertUtility.ToDateTime(dr[dc.ColumnName].ToString()))));
                        }
                        else if (Column == "Status" && tableSql == "tblOrder")
                        {
                            if (dr[dc.ColumnName].ToString() == "1")
                                Response.Write(string.Format(@"<td><a href=""{0}""><b style='color:#6032a8'>{1}</b></a></td>", linkEdit, "Đơn hàng mới"));
                            else if (dr[dc.ColumnName].ToString() == "2")
                                Response.Write(string.Format(@"<td><a href=""{0}""><b style='color:#038c1c'>{1}</b></a></td>", linkEdit, "Đã chốt"));
                            else if (dr[dc.ColumnName].ToString() == "3")
                                Response.Write(string.Format(@"<td><a href=""{0}""><b style='color:#adb51d'>{1}</b></a></td>", linkEdit, "Chờ khách xem thêm"));
                              else if (dr[dc.ColumnName].ToString() == "4")
                                Response.Write(string.Format(@"<td><a href=""{0}""><b style='color:#8f1a1a'>{1}</b></a></td>", linkEdit, "Hết hàng"));
                              else if (dr[dc.ColumnName].ToString() == "20")
                                Response.Write(string.Format(@"<td><a href=""{0}""><b style='color:#8f1a1a'>{1}</b></a></td>", linkEdit, "Huỷ đơn"));
                            else if (dr[dc.ColumnName].ToString() == "10")
                                Response.Write(string.Format(@"<td style='color:#32a852'><a href=""{0}"">{1}</a></td>", linkEdit, "Đã xử lý"));
                        }
                        else if (Column == "Json" && tableSql == "tblOrder")
                        {
                            Response.Write(string.Format(@"<td><a href=""{0}"">", linkEdit));

                            List<OrderInfo> orderInfoList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderInfo>>(dr[dc.ColumnName].ToString());
                            if (orderInfoList.Count > 0)
                            {
                                foreach (OrderInfo orderInfo in orderInfoList)
                                {
                                    string img = orderInfo.Image;
                                    Response.Write(string.Format(@"<img src='{0}?width=40&height=40&quality=100' alt='{1}' style='margin-right:5px' />", img, orderInfo.Name));
                                }
                            }
                            Response.Write(string.Format(@"</a></td>", linkEdit));
                        }
                        else
                        {
                            Response.Write(string.Format(@"<td><a href=""{0}"">{1}</a></td>", linkEdit, dr[dc.ColumnName].ToString()));
                        }
                    }
                %>
                <td class="action">

                    <a href="#" aria-label="Xem thử" data-microtip-position="top" role="tooltip">
                        <i class="fas fa-eye"></i>
                    </a>
                    <a href="/admin/<%= control %>/add/?idCopy=<%= dr["ID"].ToString() %>" aria-label="Nhân bản để thêm mới" data-microtip-position="top" role="tooltip">
                        <i class="fas fa-copy"></i>
                    </a>
                    <a href="<%= linkEdit %>" class="edit" aria-label="Sửa" data-microtip-position="top" role="tooltip">
                        <i class="fas fa-edit"></i></a>
                    <a href="javascript:;" onclick="DeleteByID('<%= dr["ID"].ToString() %>', '<%= tableSql %>', '<%= controlName %>')" class="delete" aria-label="Xoá" data-microtip-position="top" role="tooltip">
                        <i class="fas fa-trash-alt"></i></a>
                </td>
            </tr>
            <%   
                    }
                }
                else
                {
            %>

            <tr class="no-record">

                <td colspan="10">
                    <p>
                        <img src="/admin/images/no-record.png" />
                    </p>
                </td>
            </tr>

            <% } %>

            <% if (Utils.GetFolderControlAdmin() == "product")
                { %>
            <tr class="no-record">
                <td colspan="10">
                    <div class="modal">
                        <div class="modal-content">
                            <span class="close">&times;</span>
                            <h2>Cập nhật giá tạm thời</h2>
                            <div style="width: 100%; color: #ff0000; font-size: 1.2em">Sản phẩm <b id="pname"></b></div>
                            <div class="left">
                                <img src="<%= C.NO_IMG_PATH %>" id="imgPrice" style="max-width: 100%" />
                            </div>
                            <div class="right">
                                <input type="hidden" class="productid" id="productid" name="productid" />
                                <input type="hidden" class="priceid" name="priceid" id="priceid" />
                                <!-- Price -->
                                <div class="form-group">
                                    <div>Giá </div>
                                    <div>
                                        <input type="text" class="price priceTemp" name="priceTemp" />
                                    </div>
                                </div>
                                <!-- Price1 -->
                                <div class="form-group">
                                    <div>Giá cũ </div>
                                    <div>
                                        <input type="text" class="price priceTemp1" name="priceTemp1" />
                                    </div>
                                </div>
                                <!-- StartDate -->
                                <div class="form-group">
                                    <div>Ngày bắt đầu </div>
                                    <div>
                                        <input type="text" class="startdate datepicker" autocomplete="off" name="startdate" />
                                    </div>
                                </div>
                                <!-- EndDate -->
                                <div class="form-group">
                                    <div>Ngày kết thúc </div>
                                    <div>
                                        <input type="text" class="enddate datepicker" autocomplete="off" name="enddate" />
                                    </div>
                                </div>

                                <!--- Submit Button -->
                                <div class="clear"></div>
                                <div class="form-group">

                                    <div>
                                        <button type="submit" data-value="save" class="btnSubmit btnSave"><i class="fas fa-save"></i>Lưu</button>
                                        <button type="submit" data-value="delete" class="btnSubmit btnDelete"><i class="fas fa-trash-alt"></i>Xoá</button>
                                        <button type="submit" data-value="cancel" class="btnCancel"><i class="fas fa-share"></i>Bỏ Qua</button>
                                        <script type="text/javascript">
                                            $(".btnSubmit").click(function () {
                                                var control = "dynamic";
                                                var pid = $(".productid").val();
                                                var price = $(".priceTemp").val();
                                                var priceid = $(".priceid").val();
                                                var price1 = $(".priceTemp1").val();
                                                var startdate = $(".startdate").val();
                                                var enddate = $(".enddate").val();
                                                $.ajax({
                                                    url: "/admin/ajax/ajax.aspx",
                                                    data: { ctrl: control, Action: "setPriceTemp", pid: pid, price: price, price1: price1, startdate: startdate, enddate: enddate, priceid: priceid, t: Math.random() },
                                                    success: function (html) {
                                                        GetNotify(html, "success");
                                                        $('.modal').hide();
                                                        getval(0);
                                                    }
                                                });
                                            });

                                            $(".btnDelete").click(function () {
                                                var control = "dynamic";
                                                var action = "priceTemp";
                                                var pid = $(".productid").val();
                                                var priceid = $(".priceid").val();
                                                $.ajax({
                                                    url: "/admin/ajax/ajax.aspx",
                                                    data: { ctrl: control, Action: "delPriceTemp", priceid: priceid, t: Math.random() },
                                                    success: function (html) {
                                                        GetNotify(html, "success");
                                                        $('.modal').hide();
                                                        getval(0);
                                                    }
                                                });
                                            });
                                        </script>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </td>
            </tr>
            <script type="text/javascript">


                $(".btnPrice").click(function () {
                    var modal = $('.modal');
                    var span = $('.close');
                    var pid = $(this).attr("data-value");
                    ShowLoading();
                    $.getJSON("/admin/ajax/ajax.aspx", { ctrl: "dynamic", Action: "getPriceTemp", pid: pid, t: Math.random() }, function (data) {
                        modal.show();
                        $.each(data, function (key, val) {
                            console.log(key + "-" + val);

                            if (key == "ProductID") {
                                $("#productid").val(val);
                            }
                            else if (key == "Price") {
                                if (parseInt(val) > 0)
                                    $(".priceTemp").val(val);
                            }
                            else if (key == "Price1") {
                                if (parseInt(val) > 0)
                                    $(".priceTemp1").val(val);
                            }
                            else if (key == "ProductName") {
                                $("#pname").html(val);
                            }
                            else if (key == "Image") {
                                $("#imgPrice").attr("src", val);
                            }
                            else if (key == "StartDate") {
                                $(".startdate").val(val);
                            }
                            else if (key == "EndDate") {
                                $(".enddate").val(val);
                            }
                            else if (key == "PriceID") {
                                $("#priceid").val(val);
                            }
                        });

                        HideLoading();

                    });

                    span.click(function () {
                        modal.hide();
                    });

                    $(".btnCancel").click(function () {
                        modal.hide();
                    });

                    $(window).on('click', function (e) {
                        if ($(e.target).is('.modal')) {
                            modal.hide();
                        }
                    });
                });



                $(function () {
                    var $form = $(".modal");
                    var $input = $form.find(".price");

                    if ($form.length && $input.length) {
                        $input.on("keyup", function (event) {
                            // When user select text in the document, also abort.
                            var selection = window.getSelection().toString();
                            if (selection !== '') {
                                return;
                            }
                            // When the arrow keys are pressed, abort.
                            if ($.inArray(event.keyCode, [38, 40, 37, 39]) !== -1) {
                                return;
                            }
                            var $this = $(this);
                            // Get the value.
                            var input = $this.val();
                            var input = input.replace(/[\D\s\._\-]+/g, "");
                            input = input ? parseInt(input, 10) : 0;
                            $this.val(function () {
                                return (input === 0) ? "" : input.toLocaleString("en-US");
                            });
                        });
                    }
                });


                if ($(".datepicker").length) {
                    $('.datepicker').datetimepicker({
                        ownerDocument: document,
                        contentWindow: window,

                        value: '',
                        rtl: false,

                        format: 'd/m/Y H:i',
                        formatTime: 'H:i',
                        formatDate: 'd/m/Y',

                        startDate: false, // new Date(), '1986/12/08', '-1970/01/05','-1970/01/05',
                        step: 60,
                        monthChangeSpinner: true,

                        closeOnDateSelect: false,
                        closeOnTimeSelect: true,
                        closeOnWithoutClick: true,
                        closeOnInputClick: true,
                        openOnFocus: true,

                        timepicker: true,
                        datepicker: true,
                        weeks: false,

                        defaultTime: false, // use formatTime format (ex. '10:00' for formatTime: 'H:i')
                        defaultDate: false, // use formatDate format (ex new Date() or '1986/12/08' or '-1970/01/05' or '-1970/01/05')

                        minDate: false,
                        maxDate: false,
                        minTime: false,
                        maxTime: false,
                        minDateTime: false,
                        maxDateTime: false,

                        allowTimes: [],
                        opened: false,
                        initTime: true,
                        inline: false,
                        theme: '',
                        touchMovedThreshold: 5,

                        onSelectDate: function () { },
                        onSelectTime: function () { },
                        onChangeMonth: function () { },
                        onGetWeekOfYear: function () { },
                        onChangeYear: function () { },
                        onChangeDateTime: function () { },
                        onShow: function () { },
                        onClose: function () { },
                        onGenerate: function () { },

                        withoutCopyright: true,
                        inverseButton: false,
                        hours12: false,
                        next: 'xdsoft_next',
                        prev: 'xdsoft_prev',
                        dayOfWeekStart: 0,
                        parentID: 'body',
                        timeHeightInTimePicker: 25,
                        timepickerbar: true,
                        todayButton: true,
                        prevButton: true,
                        nextButton: true,
                        defaultSelect: true,

                        scrollMonth: true,
                        scrollTime: true,
                        scrollInput: true,

                        lazyInit: false,
                        mask: false,
                        validateOnBlur: true,
                        allowBlank: true,
                        yearStart: 1950,
                        yearEnd: 2050,
                        monthStart: 0,
                        monthEnd: 11,
                        style: '',
                        id: '',
                        fixed: false,
                        roundTime: 'round', // ceil, floor
                        className: '',
                        weekends: [],
                        highlightedDates: [],
                        highlightedPeriods: [],
                        allowDates: [],
                        allowDateRe: null,
                        disabledDates: [],
                        disabledWeekDays: [],
                        yearOffset: 0,
                        beforeShowDay: null,

                        enterLikeTab: true,
                        showApplyButton: false
                    });
                }




            </script>
            <% }
            %>
        </tbody>
    </table>
</div>
<script type="text/javascript">

    <% if (!Utils.isMobileBrowser)
    { %>

    $(document).ready(function () {
        $(".freezetable").freezeTable({
            freezeHead: true,
            freezeColumn: true,
            freezeColumnHead: true,
            scrollBar: true,
            fixedNavbar: '.navbar-fixed-top',
            namespace: 'freezetable',
            container: false,
            scrollable: false,
            columnNum: 3,
            columnKeep: false,
            columnBorderWidth: 1,
            columnWrapStyles: null,
            headWrapStyles: { 'box-shadow': '0px 9px 10px -5px rgba(159, 159, 160, 0.8)' },
            columnHeadWrapStyles: null,
            backgroundColor: 'white',
            shadow: true,
            fastMode: false,
            callback: null,
            'headWrapStyles': { 'box-shadow': '0px 9px 10px -5px rgba(159, 159, 160, 0.8)' },
        });
    });

    <%}%>

    $('#selectAll').click(function (e) {
        $(this).closest('table').find('td input:checkbox').prop('checked', this.checked);
    });


    $('td.flag input[type="checkbox"]').change(function () {
        //var count = 0;
        //$(this).siblings().each(function () {
        //    var tx = $(this).attr("data-value");
        //    count++;
        //    console.log(count);
        //});



        var count = 0;
        var td = $(this).parent();
        $(td).children('input[type="checkbox"]').each(function () {
            if ($(this).is(':checked')) {
                var tx = $(this).attr("data-value");
                count += parseInt(tx);
            }
        });
        console.log(count);

        var field = $(td).attr("data-field");
        var id = $(td).attr("data-id");
        $.ajax({
            url: "/admin/ajax/ajax.aspx",
            data: { ctrl: "dynamic", Action: "setFlag", table: "<%= tableSql %>", pid: id, field: field, total_flags: count, t: Math.random() },
            success: function (html) {
                GetNotify("Cập nhật thành công", "success");
            }
        });

    });


</script>
