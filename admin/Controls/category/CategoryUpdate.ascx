<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryUpdate.ascx.cs" Inherits="admin_Controls_CategoryUpdate" %>

<div class="obj-edit">
    <form method="post" enctype="multipart/form-data" id="frm_edit">
        <div class="container">
            <div class="edit">
                <div class="left">
                    <!-- Name -->
                    <div class="form-group">
                        <div>Tên danh mục </div>
                        <div>
                            <input type="text" id="name" name="name" value="<%= dr["Name"].ToString()%>" />
                        </div>
                    </div>
                    <!-- FriendlyUrl -->
                    <div class="form-group">
                        <div>Url </div>
                        <div>
                            <input type="hidden" id="id" name="id" value="<%= dr["ID"].ToString()%>" />
                            <input type="hidden" id="table" value="<%= C.CATEGORY_TABLE %>" />

                            <input type="text" id="friendlyurl" name="friendlyurl" value="<%= dr["FriendlyUrl"].ToString()%>" />
                            <input type="hidden" id="hdfUrlValid" />
                            <a href="javascript:;" id="apply_url"><i class="fad fa-comment-alt-edit"></i><span>Tạo lại Url</span></a>
                            <div id="url_valid">
                                <i class="fad fa-check-square"></i><span>Url hợp lệ</span>
                            </div>
                            <div id="url_invalid">
                                <i class="fad fa-exclamation-circle"></i><span>Url đã tồn tại, vui lòng chọn Url khác</span>
                            </div>
                        </div>
                    </div>
                    <!-- ParentID -->
                    <div class="form-group">
                        <div>Danh mục cha </div>
                        <div>
                            <%
                                string ParentID = "0";

                                if (!string.IsNullOrEmpty(ConvertUtility.ToString(CookieUtility.GetValueFromCookie("FolderCategory"))))
                                    ParentID = ConvertUtility.ToString(CookieUtility.GetValueFromCookie("FolderCategory"));

                                if (!string.IsNullOrEmpty(Utils.CommaSQLRemove(dr["ParentID"].ToString())))
                                    ParentID = Utils.CommaSQLRemove(dr["ParentID"].ToString());


                            %>
                            <input type="hidden" id="old_parent_id" name="old_parent_id" value="<%= ParentID %>" />
                            <input type="hidden" id="categoryid" name="ParentID" value="<%= ParentID %>" />
                            <input type="hidden" id="categoryname" name="categorynamelist" value="<%= CatNameList %>" />
                            <select id="drCat" name="itemSelect" style="width: 300px">
                                <option value="0">= Danh mục gốc =</option>
                            </select>

                            <script type="text/javascript">

                                var defaultCatID = $("#categoryid").val();
                                var selected = "";
                                $.getJSON("/admin/ajax/ajax.aspx", { ctrl: "select2", multilevel: "1", table: "tblCategories", moduls: "category", cid: "<%= dr["ID"].ToString()%>", t: Math.random() },
                                    function (json) {
                                        //cấp 1
                                        $.each(json,
                                            function (key, value) {
                                                if (value.id == defaultCatID)
                                                    selected = "selected='selected'";
                                                $("#drCat").append("<option " + value.disabled + " " + selected + " value= '" + value.id + "' > " + value.text + "</option > ");
                                                selected = "";
                                                //cấp 2
                                                $.each(value.children,
                                                    function (key, value) {
                                                        if (value.id == defaultCatID)
                                                            selected = "selected='selected'";
                                                        $("#drCat").append("<option " + value.disabled + " " + selected + " value='" + value.id + "'> ⟶ " + value.text + "</option>");
                                                        selected = "";
                                                        //cấp 3
                                                        $.each(value.children,
                                                            function (key, value) {
                                                                if (value.id == defaultCatID)
                                                                    selected = "selected='selected'";
                                                                $("#drCat").append("<option " + value.disabled + " " + selected + " value='" + value.id + "'> ⟶⟶ " + value.text + "</option>");
                                                                selected = "";
                                                                //cấp 4
                                                                $.each(value.children,
                                                                    function (key, value) {
                                                                        if (value.id == defaultCatID)
                                                                            selected = "selected='selected'";
                                                                        $("#drCat").append("<option " + value.disabled + " " + selected + " value='" + value.id + "'> ⟶⟶⟶ " + value.text + "</option>");
                                                                        selected = "";
                                                                    });
                                                            });
                                                    });
                                            });
                                    });


                                $(document).ready(function () {
                                    var defaultCatID = $("#categoryid").val();
                                    console.log(defaultCatID);

                                    //$('#drCat > option[value="4"]').prop("selected", true);
                                });


                                $("#drCat").change(function () {
                                    var vl = $(this).val();
                                    //alert($(this).val());
                                    $("#categoryid").val($(this).val());
                                });

                            </script>

                        </div>
                    </div>



                    <!-- Sort -->
                    <div class="form-group">
                        <div>Sắp xếp </div>
                        <div>
                            <input type="text" id="sort" name="sort" value="<%= dr["Sort"].ToString()%>" />
                        </div>
                    </div>


                    <!--- Tags -->
                    <div class="form-group">
                        <div>Tags</div>
                        <div>
                            <%
                                string TagNameList = Utils.CommaSQLRemove(dr["TagNameList"].ToString());
                                TagNameList = TagNameList.Replace(",", System.Environment.NewLine);
                            %>
                            <textarea id="tag_name" style="height: 250px" name="tag_name"><%= TagNameList %></textarea>

                            <input type="text" id="tag_id" name="tagidlist" value="<%= Utils.CommaSQLRemove(dr["TagIDList"].ToString()) %>" style="display: none" />
                        </div>
                    </div>


                    <!--- Tag Ẩn -->
                    <div class="form-group">
                        <div>Tags Ẩn</div>
                        <div>
                            <input type="text" id="hashtag_id" name="hashtagidlist" value="<%= Utils.CommaSQLRemove(dr["HashTagIDList"].ToString()) %>" style="display: none" />
                            <input type="text" id="hashtag_name" name="hashtag_name" value="<%= Utils.CommaSQLRemove(dr["HashTagNameList"].ToString()) %>" style="display: none" />
                            <select id="drHashTag" data-idreturn="Name" multiple data-level="0" data-folder="<%= Utils.GetFolderControlAdmin() %>"></select>
                        </div>
                    </div>



                    <!--- Thuộc tính -->

                    <div class="form-group">
                        <div>Bộ lọc</div>
                        <div>
                            <input type="text" id="attr_id" name="attributesidlist" value="<%= Utils.CommaSQLRemove(dr["AttributesIDList"].ToString()) %>" style="display: none" />
                            <input type="text" id="attr_name" value="<%= AttributesNameList %>" style="display: none" />
                            <select id="drAttr" multiple data-level="2" name="drAtrr" style="width: 100%"></select>
                        </div>
                    </div>



                    <!-- FilterJson -->
                    <div class="form-group">
                        <div>Chọn bộ lọc </div>
                        <div>
                            <input type="hidden" id="filterjson" name="filterjson" value="<%= dr["FilterJson"].ToString()%>" />
                        </div>
                    </div>
                    <input type="hidden" id="attrCheckValue" name="attributesidlist" />
                    <div class="attrCheck" style="display: none">
                        <%-- ajax result--%>
                    </div>

                    <div class="clear"></div>


                    <script type="text/javascript">

                       /* $(document).ready(function () {*/
                            var catid = $("#attr_id");
                            if (catid.val().length)
                                GetAttributeProduct(catid.val());
                        /*});*/


                        function GetAttributeProduct(attrIDList) {
                            var htmlContent = '';
                            var count = 0;
                            $.getJSON('/ajax/ajax.aspx', { control: "attributeproduct", bindto: "category", attrIDList: String(attrIDList) }, function (data) {
                                var jsonContent = JSON.parse(JSON.stringify(data));
                                var divAttrAjax = $(".attrCheck");
                                for (var i = 0; i < jsonContent.length; i++) {
                                    count++;
                                    var item = jsonContent[i];
                                    htmlContent += "<div>"
                                    htmlContent += "<input onclick='parentClick(" + item.ID + ")' type='checkbox' class='attrParent' id='checkboxAttr_" + item.ID + "' data-id='" + item.ID + "' data-name='" + item.Name + "' data-url='" + item.FriendlyUrl + "'" + "' data-image='" + item.Image + "' data-display='" + item.NameDisplay + "' data-description='" + item.Description + "' />";
                                    htmlContent += "<label style='font-weight:bold; cursor:pointer' for='checkboxAttr_" + item.ID + "'>" + item.Name + "</label><br />";
                                    var jsonChild = JSON.parse(JSON.stringify(item.attributeProductChild));
                                    //alert(jsonChild);
                                    if (jsonChild != null) {
                                        for (var j = 0; j < jsonChild.length; j++) {
                                            var itemChild = jsonChild[j];

                                            htmlContent += "<input onclick='childClick(" + itemChild.ID + ")' type='checkbox' class='attrChild' id='checkboxAttr_" + itemChild.ID + "' data-url='" + itemChild.FriendlyUrl + "' data-id='" + itemChild.ID + "' data-name='" + itemChild.Name + "' data-image='" + itemChild.Image + "' data-display='" + itemChild.NameDisplay + "' data-description='" + itemChild.Description + "' />";

                                            if (itemChild.Image != '')
                                                htmlContent += "<label style='cursor:pointer' for='checkboxAttr_" + itemChild.ID + "'><img src='" + itemChild.Image + "' style='height:12px;max-width:100px;vertical-align: middle;'></label><br />";
                                            else
                                                htmlContent += "<label style='cursor:pointer' for='checkboxAttr_" + itemChild.ID + "'>" + itemChild.Name + "</label><br />";
                                        }
                                    }
                                    htmlContent += "</div>"
                                }
                                if (data.length) {

                                    divAttrAjax.show();
                                    divAttrAjax.html(htmlContent);
                                    BindDataToAttr();
                                    GetValueFromAttr();
                                }
                            });
                            if (count == 0) {
                                $(".attrCheck").hide();
                            }
                            else {
                                $(".attrCheck").show();
                            }
                        }

                        function BindDataToAttr() {
                            <% 
                        if (Utils.CommaSQLRemove(dr["FilterJson"].ToString()).Length > 5)
                        {
                            %>
                                    var jsonContent = <%= Utils.CommaSQLRemove(dr["FilterJson"].ToString()) %>;

                            <%
                        }
                        else
                        {
                            %>
                                    var data = '<%= Utils.CommaSQLRemove(dr["FilterJson"].ToString()) %>';
                                    var jsonContent = JSON.parse(JSON.stringify(data));
                            <%
                        }
                        %>

                                    var divAttrAjax = $(".attrCheck");
                                    for (var i = 0; i < jsonContent.length; i++) {
                                        var item = jsonContent[i];
                                        var checkbox = $("#checkboxAttr_" + item.ID);
                                        if (checkbox.length) {
                                            checkbox.prop('checked', true);
                                        }
                                        var jsonChild = JSON.parse(JSON.stringify(item.attributeProductChild));
                                        for (var j = 0; j < jsonChild.length; j++) {
                                            var itemChild = jsonChild[j];

                                            var checkbox = $("#checkboxAttr_" + itemChild.ID);
                                            if (checkbox.length) {
                                                checkbox.prop('checked', true);
                                            }
                                        }
                                    }

                           <%-- var datas = '<%= Utils.CommaSQLRemove(dr["AttributesIDList"].ToString()) %>';
                            var arr = datas.split(',');
                            $.each(arr, function (index, value) {
                                var checkbox = $("#checkboxAttr_" + value);
                                if (checkbox.length) {
                                    checkbox.prop('checked', true);
                                    console.log(value.Access + '_' + value.ID);
                                }
                            });--%>
                        }

                        function GetValueFromAttr() {
                            var jsonObj = [];
                            $('.attrParent:checked').each(function () {
                                var cat_parent_id = $(this).attr("data-id");
                                var cat_parent_name = $(this).attr("data-name");
                                var cat_parent_url = $(this).attr("data-url");
                                var cat_parent_image = $(this).attr("data-image");
                                var cat_parent_name_display = $(this).attr("data-display");
                                var cat_parent_description = $(this).attr("data-description");



                                //console.log("-" + cat_parent_id);

                                var itemParent = { "ID": cat_parent_id, "Name": cat_parent_name, "FriendlyUrl": cat_parent_url, "Image": cat_parent_image, "NameDisplay": cat_parent_name_display, "Description": cat_parent_description, "attributeProductChild": [] };
                                $(this).parent().find('.attrChild:checked').each(function () {
                                    var cat_child_id = $(this).attr("data-id");
                                    var cat_child_name = $(this).attr("data-name");
                                    var cat_child_url = $(this).attr("data-url");
                                    var cat_child_image = $(this).attr("data-image");
                                    var cat_child_name_display = $(this).attr("data-display");
                                    var cat_child_description = $(this).attr("data-description");

                                    //console.log("-----" + cat_child_id);
                                    var itemChild = { "ID": cat_child_id, "Name": cat_child_name, "FriendlyUrl": cat_child_url, "FriendlyUrlParent": cat_parent_url, "Image": cat_child_image, "NameDisplay": cat_child_name_display, "Description": cat_child_description };
                                    itemParent.attributeProductChild.push(itemChild);
                                });
                                jsonObj.push(itemParent);
                            });
                            console.log(jsonObj);
                            $("#filterjson").val(JSON.stringify(jsonObj));
                        }


                        function parentClick(id) {

                            console.log(id);
                            var el = $("#checkboxAttr_" + id);
                            $(el).parent().find('.attrChild').each(function () {
                                if (el.is(':checked'))
                                    $(this).prop("checked", true);
                                else
                                    $(this).prop("checked", false);
                            });
                            GetValueFromAttr()
                        }

                        function childClick(id) {

                            console.log(id);
                            var el = $("#checkboxAttr_" + id);
                            if (el.is(':checked'))
                                $(el).parent().find(".attrParent").prop("checked", true);
                            GetValueFromAttr()
                        }
                    </script>





                    <div class="flex">

                        <div>
                            <div>Kiểu menu</div>
                            <div>

                                <input type="hidden" id="linktypeflag" name="linktypeflag" value="<%= linkTypeFlag.ToString() %>" />

                                <input type="radio" name="linktype" id="Product" <%= Utils.SetChecked(linkTypeFlag.HasFlag(LinkTypeMenuFlag.Product)) %> />
                                <label for="Product">Danh mục sản phẩm</label><br>
                                <input type="radio" name="linktype" id="Article" <%= Utils.SetChecked(linkTypeFlag.HasFlag(LinkTypeMenuFlag.Article))%> />
                                <label for="Article">Danh mục bài viết</label><br>
                                <input type="radio" name="linktype" id="Link" <%= Utils.SetChecked(linkTypeFlag.HasFlag(LinkTypeMenuFlag.Link))%> />
                                <label for="Link">Dạng liên kết</label><br>
                                <input type="radio" name="linktype" id="Content" <%= Utils.SetChecked(linkTypeFlag.HasFlag(LinkTypeMenuFlag.Content))%> />
                                <label for="Content">Nội dung chi tiết</label><br>

                                <script type="text/javascript">
                                    $("input[name='linktype']").change(function () {
                                        if ($(this).is(":checked")) {
                                            var val = $(this).attr("id");
                                            $("#linktypeflag").val(val);
                                            show_content(val);
                                        }
                                    });
                                    $(document).ready(function () {
                                        if ($("#linktypeflag").val() == "None") {
                                            $("#Product").prop("checked", true);
                                        }

                                        show_content("<%= linkTypeFlag.ToString() %>");
                                    });



                                    function show_content(val) {
                                        if (val == "Link")
                                            $("#div_link").show();
                                        else
                                            $("#div_link").hide();
                                    }

                                </script>
                            </div>
                        </div>

                        <div>
                            <div>Vị trí Menu</div>
                            <div>

                                <input type="hidden" id="positionmenuflag" name="positionmenuflag" value="<%= positionFlag.ToString() %>" />

                                <input type="checkbox" name="Main" id="Main" <%= Utils.SetChecked(positionFlag.HasFlag(PositionMenuFlag.Main)) %> />
                                <label for="Main">Menu chính</label><br>
                                <input type="checkbox" name="Top" id="Top" <%= Utils.SetChecked(positionFlag.HasFlag(PositionMenuFlag.Top))%> />
                                <label for="Top">Menu ngang</label><br>
                                 <input type="checkbox" name="SupperTop" id="SupperTop" <%= Utils.SetChecked(positionFlag.HasFlag(PositionMenuFlag.SupperTop))%> />
                                <label for="SupperTop">Menu trên</label><br>
                                <input type="checkbox" name="Bottom" id="Bottom" <%= Utils.SetChecked(positionFlag.HasFlag(PositionMenuFlag.Bottom))%> />
                                <label for="Bottom">Menu dưới</label><br>
                                <input type="checkbox" id="MenuSub" name="MenuSub" <%= Utils.SetChecked(positionFlag.HasFlag(PositionMenuFlag.MenuSub))%> />
                                <label for="MenuSub">Danh mục con</label><br>
                                <input type="checkbox" id="MenuSubMainHome" name="MenuSubMainHome" <%= Utils.SetChecked(positionFlag.HasFlag(PositionMenuFlag.MenuSubMainHome))%> />
                                <label for="MenuSubMainHome">Sub home 1</label><br>
                                <input type="checkbox" id="MenuSubMainHome2" name="MenuSubMainHome2" <%= Utils.SetChecked(positionFlag.HasFlag(PositionMenuFlag.MenuSubMainHome2))%> />
                                <label for="MenuSubMainHome2">Sub home 2</label><br>
                                <input type="checkbox" id="Style1" name="Style1" <%= Utils.SetChecked(positionFlag.HasFlag(PositionMenuFlag.Style1))%> />
                                <label for="Style1">Style 1</label><br>
                                <input type="checkbox" id="Style2" name="Style2" <%= Utils.SetChecked(positionFlag.HasFlag(PositionMenuFlag.Style2))%> />
                                <label for="Style2">Style 2</label><br>

                                <script type="text/javascript">
                                    $("input[name='positionmenu']").change(function () {
                                        if ($(this).is(":checked")) {
                                            var val = $(this).attr("id");
                                            $("#positionmenuflag").val(val);
                                        }
                                    });
                                    $(document).ready(function () {
                                        if ($("#positionmenuflag").val() == "None" && <%= ConvertUtility.ToInt32(dr["ID"], 0) %> == 0) {
                                            $("#Main").prop("checked", true);
                                        }
                                    });

                                </script>
                            </div>
                        </div>

                        <div>
                            <div>Tùy chọn</div>
                            <div>
                                <input type="checkbox" id="MenuHome" name="MenuHome" <%= Utils.SetChecked(attrMenuFlag.HasFlag(AttrMenuFlag.MenuHome)) %> />
                                <label for="MenuHome">Đặt lên trang chủ</label><br>
                                <input type="checkbox" id="MenuPriority" name="MenuPriority" <%= Utils.SetChecked(attrMenuFlag.HasFlag(AttrMenuFlag.MenuPriority))%> />
                                <label for="MenuPriority">Menu ưu tiên</label><br>
                                <input type="checkbox" id="MenuHotIcon" name="MenuHotIcon" <%= Utils.SetChecked(attrMenuFlag.HasFlag(AttrMenuFlag.MenuHotIcon))%> />
                                <label for="MenuHotIcon">Biểu tượng Hot</label><br>
                                <input type="checkbox" id="OpenNewWindows" name="OpenNewWindows" <%= Utils.SetChecked(attrMenuFlag.HasFlag(AttrMenuFlag.OpenNewWindows))%> />
                                <label for="OpenNewWindows">Mở trong cửa sổ mới</label><br>
                                <input type="checkbox" id="PriorityAds" name="PriorityAds" <%= Utils.SetChecked(attrMenuFlag.HasFlag(AttrMenuFlag.PriorityAds))%> />
                                <label for="PriorityAds">Feed Data</label><br>
                            </div>
                        </div>
                        <div>
                            <div>Ẩn</div>
                            <div>
                                <%
                                    string isHide = "";
                                    if (!string.IsNullOrEmpty(dr["Hide"].ToString()) && ConvertUtility.ToBoolean(dr["Hide"]))
                                        isHide = " checked";
                                %>
                                <input type="checkbox" name="hide" id="hide" <%= isHide %> />
                                <label for="hide">Tạm ẩn</label><br>
                            </div>
                        </div>

                    </div>

                    <!-- Mô tả sản phẩm -->
                    <div class="form-group" id="div_noidung">
                        <div>Nội dung </div>
                        <div>
                            <textarea name="longdescription" id="longdescription" rows="5" class="ckeditor"><%= dr["LongDescription"].ToString()%></textarea>
                        </div>
                    </div>

                    <!-- Link -->
                    <div class="form-group" id="div_link">
                        <div>Link </div>
                        <div>
                            <input type="text" id="link" name="link" value="<%= dr["Link"].ToString()%>" />
                        </div>
                    </div>


                </div>
                <div class="right">
                    <!-- MetaTitle -->
                    <div class="form-group">
                        <div>Meta Title </div>
                        <div>
                            <span class="count-char">1234</span>
                            <textarea id="metatitle" name="metatitle"><%= dr["MetaTitle"].ToString()%></textarea>
                        </div>
                    </div>
                    <!-- MetaKeyword -->
                    <div class="form-group">
                        <div>Meta Keyword </div>
                        <div>
                            <span class="count-char"></span>
                            <%
                                string MetaKeyword = Utils.CommaSQLRemove(dr["MetaKeyword"].ToString());
                                MetaKeyword = MetaKeyword.Replace(",", System.Environment.NewLine);
                            %>
                            <textarea id="metakeyword" name="metakeyword"><%= MetaKeyword %></textarea>
                        </div>
                    </div>
                    <!-- MetaDescription -->
                    <div class="form-group">
                        <div>Meta Description </div>
                        <div>
                            <span class="count-char">123</span>
                            <textarea id="metadescription" name="metadescription"><%= dr["MetaDescription"].ToString()%></textarea>
                        </div>
                    </div>

                    <div class="google-demo">
                        <p>
                            <img src="/admin/images/google-logo.png" />
                        </p>
                        <p class="title">Mô phỏng Website của bạn khi xuất hiện trên Google</p>
                        <p class="description">Lưu ý đây chỉ là mô phỏng website xuất hiện trên Google, để lên thực sự thì cần rất nhiều yếu tố khác</p>
                    </div>

                    <!-- Image_1 -->
                    <div class="form-group image-ck">
                        <div>Ảnh đại diện MXH </div>

                        <div data-thumb="thumbnail_image_1" data-inputtext="image_1" data-folder="category_banner/thumbnail_category">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= image_1 %>" id="thumbnail_image_1" alt="Chọn ảnh" />
                            </a>
                            <input type="text" id="image_1" name="image_1" value="<%= dr["Image_1"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Image_2 -->
                    <div class="form-group image-ck">
                        <div>Banner dọc trang chủ </div>
                        <div data-thumb="thumbnail_image_2" data-inputtext="image_2" data-folder="category_banner/poster_category">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= image_2 %>" id="thumbnail_image_2" alt="Chọn ảnh" />
                            </a>
                            <input type="text" id="image_2" name="image_2" value="<%= dr["Image_2"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Image_3 -->
                    <div class="form-group image-ck">
                        <div>Hình nền danh mục </div>
                        <div data-thumb="thumbnail_image_3" data-inputtext="image_3" data-folder="category_icon/logo_danh_muc">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= image_3 %>" id="thumbnail_image_3" alt="Chọn ảnh" />
                            </a>
                            <input type="text" id="image_3" name="image_3" value="<%= dr["Image_3"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Image_4 -->
                    <div class="form-group image-ck">
                        <div>Ảnh trong chi tiết sản phẩm </div>
                        <div data-thumb="thumbnail_image_3" data-inputtext="image_4" data-folder="category_icon/logo_danh_muc">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= image_4 %>" id="thumbnail_image_4" alt="Chọn ảnh" />
                            </a>
                            <input type="text" id="image_4" name="image_4" value="<%= dr["Image_4"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Icon -->

                    <div class="form-group image-ck">
                        <div>Icon </div>
                        <div data-thumb="thumbnail_icon" data-inputtext="icon" data-folder="category_icon/icon_danh_muc">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= icon %>" id="thumbnail_icon" alt="Chọn ảnh" />
                            </a>
                            <input type="text" id="icon" name="icon" class="icon" value="<%= dr["Icon"].ToString()%>" />
                        </div>
                    </div>


                    <!-- SchemaBestRating -->
                    <div class="form-group" style="display: none">
                        <div>SchemaBestRating </div>
                        <div>
                            <input type="text" id="schemabestrating" name="schemabestrating" value="100" />
                        </div>
                    </div>
                    <!-- SchemaRatingValue -->
                    <div class="form-group" style="display: none">
                        <div>SchemaRatingValue </div>
                        <div>
                            <%
                                int SchemaRatingValue = ConvertUtility.ToInt32(dr["SchemaRatingValue"]);
                                if (SchemaRatingValue == 0)
                                    SchemaRatingValue = Utils.RandomNumber(92, 100);
                            %>
                            <input type="text" id="schemaratingvalue" name="schemaratingvalue" value="<%= SchemaRatingValue %>" />
                        </div>
                    </div>
                    <!-- SchemaRatingCount -->
                    <div class="form-group" style="display: none">
                        <div>SchemaRatingCount </div>
                        <div>
                            <%
                                int SchemaRatingCount = ConvertUtility.ToInt32(dr["SchemaRatingCount"]);
                                if (SchemaRatingCount == 0)
                                    SchemaRatingCount = Utils.RandomNumber(10, 200);
                            %>
                            <input type="text" id="schemaratingcount" name="schemaratingcount" value="<%= SchemaRatingCount %>" />
                        </div>
                    </div>

                </div>


                <!--- Submit Button -->

                <div class="form-group submit">
                    <div>&nbsp;</div>
                    <div>
                        <% if (IsUpdate)
                            { %>
                        <button type="submit" data-value="save" class="btnSubmit btnSave"><i class="fas fa-save"></i>Lưu</button>
                        <button type="submit" data-value="saveandadd" class="btnSubmit btnSaveAndAdd"><i class="fas fa-save"></i>Lưu và Thêm</button>
                        <button type="submit" data-value="saveandback" class="btnSubmit btnSaveAndBack"><i class="fas fa-save"></i>Lưu và Quay Lại</button>
                        <button type="submit" data-value="saveandcopy" class="btnSubmit btnSaveAndCopy"><i class="fas fa-copy"></i>Lưu và Sao Chép</button>
                        <button type="submit" data-value="delete" class="btnSubmit btnDelete"><i class="fas fa-trash-alt"></i>Xoá</button>
                        <%}
                            else
                            { %>
                        <button type="submit" data-value="saveandadd" class="btnSubmit btnAddAndAdd"><i class="fas fa-plus"></i>Thêm</button>
                        <button type="submit" data-value="saveandback" class="btnSubmit btnAddAndBack"><i class="fas fa-plus"></i>Thêm và Quay Lại</button>
                        <button type="submit" data-value="saveandcopy" class="btnSubmit btnAddAndCopy"><i class="fas fa-copy"></i>Thêm và Sao Chép</button>
                        <% } %>
                        <button type="submit" data-value="cancel" class="btnSubmit btnCancel"><i class="fas fa-arrow-left"></i>Bỏ Qua</button>
                        <input type="hidden" id="done" name="done" value="0" />
                        <script type="text/javascript">
                            $(".btnSubmit").click(function () {
                                var dataValue = $(this).attr("data-value");
                                      <%--  if (dataValue == "delete")
                                            DeleteByID('<%= dr["ID"].ToString() %>', '<%= table %>', '<%= ControlAdminInfo.ShortName %>');--%>
                                        $('#frm_edit #done').val(dataValue);
                                        $(this).attr('disabled', 'disabled');
                                        $(this).html('Loading...');
                                        $("#frm_edit").submit();
                                    });
                        </script>
                    </div>
                </div>

            </div>
        </div>
    </form>
</div>
