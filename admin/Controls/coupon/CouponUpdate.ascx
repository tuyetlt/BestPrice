<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CouponUpdate.ascx.cs" Inherits="admin_Controls_coupon_CouponUpdate" %>
<%@ Import Namespace="Ebis.Utilities" %>
<%@ Import Namespace="System.Data" %>
<div class="obj-edit">
    <form method="post" enctype="multipart/form-data" id="frm_edit">

        <input type="hidden" name="id" value="<%= dr["ID"].ToString()%>" />
        <div class="container">
            <div class="edit">
                <div class="full">

                    <!-- NewsRelatedIDList -->
                    <div class="form-group">
                        <div></div>
                        <div>
                            <input type="hidden" id="productidlist" name="productidlist" value="<%= dr["ProductIDList"].ToString()%>" />



                            <input class="btnOpenPopup btn" type="button" value="Chọn sản phẩm" style="font-size: 14pt" />
                            <input type="button" class="btn" id="btnButtonAutoClick" style="display: inline-block; visibility: hidden" onclick="callAjax()" value="load lại" />

                            <div class="clear"></div>

                            <div style="max-height: 300px; overflow-y: auto;">
                                <div style="margin: 10px 0" id="Article-list-ajax">
                                </div>
                                <div id="LoadingImage" style="display: none">
                                    <img src="/admin/images/loading.gif" />
                                </div>
                            </div>


                            <script type="text/javascript">
                                $('.btnOpenPopup').popupWindow({
                                    windowURL: "/admin/popup/popup.aspx?ctrl=productrelated&textbox=productidlist&button=btnButtonAutoClick",
                                    windowName: 'swip',
                                    centerScreen: 1,
                                    height: 630,
                                    width: 800
                                });

                                function callAjax() {

                                    var idList = $("#productidlist").val();
                                    if (idList.length >= 0) {
                                        $("#LoadingImage").show();
                                        $.ajax({
                                            url: '/admin/ajax/ajax.aspx',
                                            type: "GET",
                                            data: { btn: "btnButtonAutoClick", txt: "productidlist", productIDList: idList, ctrl: "productselected" },
                                            complete: function (response) {
                                                $('#Article-list-ajax').html(response.responseText);
                                                $("#LoadingImage").hide();
                                            },
                                            error: function () {
                                                $('#Article-list-ajax').html('Bummer: there was an error!');
                                                $("#LoadingImage").hide();
                                            },
                                        });
                                    }
                                    return false;
                                }

                                $(document).ready(function () {
                                    $('#btnButtonAutoClick').click();
                                })

                            </script>









                        </div>
                    </div>

                    <!-- Name -->
                    <div class="form-group">
                        <div>Mã giảm giá </div>
                        <div>
                            <input type="text" id="name" name="name" value="<%= dr["Name"].ToString()%>" />
                        </div>
                    </div>
                    <!-- ValueVND -->
                    <div class="form-group">
                        <div>Giá trị giảm (VNĐ) </div>
                        <div>
                            <input type="text" id="valuevnd" name="valuevnd" class="price" value="<%= dr["ValueVND"].ToString()%>" />
                        </div>
                    </div>

                    <!-- MaxUsing -->
                    <div class="form-group">
                        <div>Sử dụng tối đa </div>
                        <div>
                            <input type="text" id="maxusing" name="maxusing" value="<%= dr["MaxUsing"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Using -->
                    <div class="form-group">
                        <div>Đã sử dụng </div>
                        <div>
                            <input type="text" id="using" name="using" value="<%= dr["Using"].ToString()%>" />
                        </div>
                    </div>
                    <!-- ExpireDate -->
                    <div class="form-group">
                        <div>Ngày hết hạn </div>
                        <div>
                            <input type="text" class="datepicker" id="expiredate" name="expiredate" value="<%= dr["ExpireDate"].ToString()%>" />
                        </div>
                    </div>

                    <!--- Danh mục sản phẩm -->
                    <div class="form-group">
                        <div>Danh mục<span class="required">*</span></div>
                        <div>
                            <input type="text" id="categoryid" name="categoryidlist" value="<%= Utils.CommaSQLRemove(dr["CategoryIDList"].ToString()) %>" style="display: none" />
                            <input type="text" id="categoryname" name="categorynamelist" value="<%= CatNameList %>" style="display: none" />
                            <select id="drCategory" multiple data-level="1" name="itemSelect" style="width: 100%"></select>
                        </div>
                    </div>

                     <!--- Thuộc tính -->
                    <div class="form-group">
                        <div>Bộ lọc</div>
                    </div>
                    <div class="clear"></div>

                    <input type="hidden" id="attrCheckValue" name="attributesidlist" value="<%= Utils.CommaSQLRemove(dr["AttributesIDList"].ToString()) %>" />
                    <div class="attrCheck" style="display: none">
                        <%-- ajax result--%>
                    </div>

                    <div class="clear"></div>

                    <script type="text/javascript">

                        $(document).ready(function () {
                            var catid = $("#categoryid");
                            if (catid.val().length)
                                GetAttributeProduct(catid.val());
                        });


                        function GetAttributeProduct(categoryIDList) {
                            //categoryIDList = categoryIDList.replace(",", "_");
                            var htmlContent = '';
                            var count = 0;
                            $.getJSON('/ajax/ajax.aspx', { control: "attributeproduct", categoryList: String(categoryIDList) }, function (data) {
                                var jsonContent = JSON.parse(JSON.stringify(data));
                                var divAttrAjax = $(".attrCheck");
                                if (jsonContent != null && jsonContent.length) {
                                    for (var i = 0; i < jsonContent.length; i++) {
                                        var item = jsonContent[i];

                                        if (item.Name != "RootID") {
                                            count++;
                                            htmlContent += "<div>"
                                            htmlContent += "<h3>" + item.Name + "</h3>";

                                            var jsonChild = JSON.parse(JSON.stringify(item.attributeProductChild));
                                            for (var j = 0; j < jsonChild.length; j++) {
                                                var itemChild = jsonChild[j];

                                                htmlContent += "<input type='checkbox' class='checkboxAttr' onclick='GetValueFromAttr()' id='checkboxAttr_" + itemChild.ID + "' name='home' />";
                                                if (itemChild.Image != '')
                                                    htmlContent += "<label style='cursor:pointer' for='checkboxAttr_" + itemChild.ID + "'><img src='" + itemChild.Image + "' style='height:25px;vertical-align: middle;'></label><br />";
                                                else
                                                    htmlContent += "<label style='cursor:pointer' for='checkboxAttr_" + itemChild.ID + "'>" + itemChild.Name + "</label><br />";


                                            }
                                            htmlContent += "</div>"
                                        }
                                    }
                                }
                                //console.log(count);
                                if (jsonContent != null && jsonContent.length) {

                                    divAttrAjax.show();
                                    divAttrAjax.html(htmlContent);
                                    BindDataToAttr();
                                }

                                //alert(jsonContent.length);


                            });

                            //alert("ok: " + count);
                            if (count == 0) {
                                $(".attrCheck").hide();
                            }
                            else {
                                $(".attrCheck").show();
                                //alert("ok: " + count);
                            }
                        }




                        function BindDataToAttr() {
                            var datas = '<%= Utils.CommaSQLRemove(dr["AttributesIDList"].ToString()) %>';

                            var arr = datas.split(',');
                            $.each(arr, function (index, value) {
                                var checkbox = $("#checkboxAttr_" + value);
                                if (checkbox.length) {
                                    checkbox.prop('checked', true);
                                    console.log(value.Access + '_' + value.ID);
                                }
                            });
                        }

                        function GetValueFromAttr() {
                            var list_id = "";
                            $('.checkboxAttr:checked').each(function () {
                                if (list_id != null && list_id != '')
                                    list_id += ",";
                                var checkboxValue = $(this).attr("id");
                                list_id += checkboxValue.replace("checkboxAttr_", "");
                            });
                            console.log(list_id);
                            $("#attrCheckValue").val(list_id);
                        }
                    </script>



                    <!--- Submit Button -->
                    <div class="clear"></div>
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

                            <button type="submit" data-value="cancel" class="btnSubmit btnCancel"><i class="fas fa-share"></i>Bỏ Qua</button>


                            <input type="hidden" id="done" name="done" value="0" />

                            <script type="text/javascript">
                                $(".btnSubmit").click(function () {
                                    var dataValue = $(this).attr("data-value");
                                    if (dataValue == "delete")
                                        DeleteByID('<%= dr["ID"].ToString() %>', '<%= table %>', '<%= ControlAdminInfo.ShortName %>');
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
        </div>
    </form>
</div>
