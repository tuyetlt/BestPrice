<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BaseUpdate.ascx.cs" Inherits="admin_Controls_base_BaseUpdate" %>
<%@ Import Namespace="System.Data" %>
<div class="obj-edit">
    <form method="post" enctype="multipart/form-data" id="frm_edit">
        <input type="hidden" name="id" value="<%= dr["ID"].ToString() %>" />
        <div class="container">
            <div class="edit">
                <div class="left">

                    <!-- Name -->
                    <div class="form-group">
                        <div>Tên </div>
                        <div>
                            <input type="text" id="name" name="name" value="<%= dr["Name"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Phone -->
                    <div class="form-group">
                        <div>Phone </div>
                        <div>
                            <input type="text" id="phone" name="phone" value="<%= dr["Phone"].ToString()%>" />
                        </div>
                    </div>

                    <div class="form-group">
                        <div>NickChat </div>
                        <div>
                            <input type="text" id="nickchat" name="nickchat" value="<%= dr["NickChat"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Alt -->
                    <div class="form-group">
                        <div>Alt </div>
                        <div>
                            <input type="text" id="alt" name="alt" value="<%= dr["Alt"].ToString()%>" />
                        </div>
                    </div>

                    <!-- ParentID -->
                    <div class="form-group">
                        <div>Mục cha </div>
                        <div>
                            <select name="parentid" id="parentid">
                                <option value="0">-Mục gốc-</option>
                                <% DataTable dtParent = SqlHelper.SQLToDataTable("tblBase", "ID, Name", "ParentID=0 OR ParentID is null");
                                    if (Utils.CheckExist_DataTable(dtParent))
                                    {

                                        foreach (DataRow drParent in dtParent.Rows)
                                        {
                                            string selected = "";
                                            if (drParent["ID"].ToString() == dr["ParentID"].ToString())
                                                selected = "selected='selected'";
                                %>
                                <option <%= selected %> value="<%= drParent["ID"] %>"><%= drParent["Name"] %></option>
                                <%
                                        }
                                    }

                                %>
                            </select>
                        </div>
                    </div>




                    <!-- Thư viện ảnh -->
                    <div class="form-group">
                        <div>Thư viện ảnh </div>
                        <div>
                            <input type="text" id="gallery" name="gallery" style="display: none" value='<%= dr["Gallery"].ToString()%>' />

                            <div id="dZUpload" class="dropzone" data-folder="base">
                                <div class="dz-default dz-message">
                                    Bấm hoặc kéo ảnh vào vùng này để tải ảnh.
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Sort -->
                    <div class="form-group">
                        <div>Sắp xếp </div>
                        <div>
                            <input type="text" id="sort" name="sort" value="<%= dr["Sort"].ToString()%>" />
                        </div>
                    </div>

                    <div class="form-group">
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

                    <!-- Description -->
                    <div class="form-group">
                        <div>Description </div>
                        <div>
                            <textarea name="description" id="description" rows="5" class="ckeditor"><%= dr["Description"].ToString()%></textarea>
                        </div>
                    </div>
                    <!-- CreatedDate -->


                </div>
                <div class="right">

                    <!-- Image_1 -->
                    <div class="form-group image-ck">
                        <div>Banner 1 </div>

                        <div data-thumb="thumbnail_image_1" data-inputtext="image_1" data-folder="base">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= image_1 %>" id="thumbnail_image_1" alt="Chọn ảnh" />
                            </a>
                            <input type="text" id="image_1" name="image_1" value="<%= dr["Image_1"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Image_2 -->
                    <div class="form-group image-ck">
                        <div>Banner 2 </div>
                        <div data-thumb="thumbnail_image_2" data-inputtext="image_2" data-folder="base">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= image_2 %>" id="thumbnail_image_2" alt="Chọn ảnh" />
                            </a>
                            <input type="text" id="image_2" name="image_2" value="<%= dr["Image_2"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Image_3 -->
                    <div class="form-group image-ck">
                        <div>Banner 3 </div>
                        <div data-thumb="thumbnail_image_3" data-inputtext="image_3" data-folder="base">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= image_3 %>" id="thumbnail_image_3" alt="Chọn ảnh" />
                            </a>
                            <input type="text" id="image_3" name="image_3" value="<%= dr["Image_3"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Icon -->

                    <div class="form-group image-ck">
                        <div>Icon </div>
                        <div data-thumb="thumbnail_icon" data-inputtext="icon" data-folder="folder1/folder2">
                            <a href="javascript:;" class="get_ck">
                                <img src="<%= icon %>" id="thumbnail_icon" alt="Chọn ảnh" />
                            </a>
                            <input type="text" id="icon" name="icon" class="icon" value="<%= dr["Icon"].ToString()%>" />
                        </div>
                    </div>



<%--                    <input class="json" type="text" />

                    <!-- HTML -->
                    <div id="itemContainer">
                        <!-- Các div.form-group hiện có ở đây -->

                        <!-- Nút Thêm Item -->
                        <input type="button" id="btnAddItem" value="Thêm Item" />
                    </div>

<!-- Script -->
<script>
    // Mảng để lưu trữ giá trị của các text box
    var textBoxValues = [];

    document.getElementById('btnAddItem').addEventListener('click', function () {
        // Tạo một div.form-group mới
        var newItemDiv = document.createElement('div');
        newItemDiv.className = 'form-group image-ck';

        // Thêm nội dung cho div.form-group mới
        var itemId = getNextItemId();
        newItemDiv.innerHTML = `
            <div>Icon </div>
            <div data-thumb="thumbnail_icon${itemId}" data-inputtext="icon" data-folder="base">
                <a href="javascript:;" class="get_ck">
                    <img src="" id="thumbnail_icon${itemId}" alt="Chọn ảnh" />
                </a>
                <input type="text" id="icon${itemId}" name="icon${itemId}" class="icon${itemId}" value="" />
            </div>
        `;

        // Thêm div.form-group mới vào container
        document.getElementById('itemContainer').appendChild(newItemDiv);

        // Lưu giá trị của text box vào mảng
        textBoxValues.push({
            id: itemId,
            iconValue: ""
        });

        // Cập nhật hiển thị chuỗi JSON
        updateJsonString();
    });

    // Hàm để lấy ID tiếp theo (đảm bảo duy nhất)
    function getNextItemId() {
        var existingItems = document.querySelectorAll('[id^="thumbnail_icon"]');
        return existingItems.length + 1;
    }

    // Hàm để cập nhật hiển thị chuỗi JSON
    function updateJsonString() {
        // Chuyển đổi mảng thành chuỗi JSON
        var jsonString = JSON.stringify(textBoxValues);

        // Hiển thị chuỗi JSON trong input có class "json"
        document.querySelector('.json').value = jsonString;
    }

    // Lắng nghe sự kiện keyup trong các textbox để cập nhật giá trị
    document.addEventListener('keyup', function (event) {
        // Kiểm tra xem sự kiện keyup có xảy ra trong các textbox hay không
        if (event.target && event.target.matches('.icon')) {
            // Lấy ID của textbox từ tên của textbox (ví dụ: icon1 -> 1)
            var itemId = event.target.id.replace('icon', '');

            // Tìm và cập nhật giá trị của textbox trong mảng
            var textBoxIndex = textBoxValues.findIndex(function (item) {
                return item.id == itemId;
            });

            if (textBoxIndex !== -1) {
                textBoxValues[textBoxIndex].iconValue = event.target.value;
                // Cập nhật hiển thị chuỗi JSON
                updateJsonString();
            }
        }
    });
</script>--%>


                    <div class="flex">
                        <div>Thuộc tính</div>
                        <div>
                            <input type="hidden" id="flag" name="flag" value="<%= Flag.ToString() %>" />

                            <input type="radio" name="flags" id="Manufacturer" <%= Utils.SetChecked(Flag.HasFlag(BaseTableFlag.Manufacturer)) %> />
                            <label for="InStock">Thương hiệu</label><br>
                            <input type="radio" name="flags" id="Partner" <%= Utils.SetChecked(Flag.HasFlag(BaseTableFlag.Partner))%> />
                            <label for="OutStock">Đối tác</label><br>
                            <input type="radio" name="flags" id="Social" <%= Utils.SetChecked(Flag.HasFlag(BaseTableFlag.Social))%> />
                            <label for="Social">Mạng xã hội</label><br>
                            <input type="radio" name="flags" id="Support" <%= Utils.SetChecked(Flag.HasFlag(BaseTableFlag.Support))%> />
                            <label for="Social">Nick hỗ trợ</label><br>
                            <input type="radio" name="flags" id="Service" <%= Utils.SetChecked(Flag.HasFlag(BaseTableFlag.Service))%> />
                            <label for="Service">Dịch vụ</label><br>

                            <script type="text/javascript">
                                $("input[name='flags']").change(function () {
                                    if ($(this).is(":checked")) {
                                        var val = $(this).attr("id");
                                        $("#flag").val(val);
                                    }
                                });
                                $(document).ready(function () {
                                    if ($("#flag").val() == "None") {
                                        $("#Manufacturer").prop("checked", true);
                                    }
                                });

                            </script>
                        </div>
                        <div>&nbsp;</div>
                    </div>

                </div>

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
                        <button type="submit" data-value="delete" class="btnSubmit validate btnDelete"><i class="fas fa-trash-alt"></i>Xoá</button>
                        <button type="submit" data-value="view" class="btnSubmit btnView"><i class="fas fa-eye"></i>Xem thử</button>

                        <%}
                            else
                            { %>
                        <button type="submit" data-value="saveandadd" class="btnSubmit btnAddAndAdd"><i class="fas fa-plus"></i>Thêm</button>
                        <button type="submit" data-value="saveandback" class="btnSubmit btnAddAndBack"><i class="fas fa-plus"></i>Thêm và Quay Lại</button>
                        <button type="submit" data-value="saveandcopy" class="btnSubmit btnAddAndCopy"><i class="fas fa-copy"></i>Thêm và Sao Chép</button>
                        <% } %>

                        <button type="submit" data-value="cancel" class="btnSubmit validate btnCancel"><i class="fas fa-share"></i>Bỏ Qua</button>
                        <input type="hidden" id="done" name="done" value="0" />


                        <script type="text/javascript">
                            $(".btnSubmit").click(function () {
                                //var myDropzone = Dropzone.forElement(".dropzone");
                                //myDropzone.processQueue();

                                var dataValue = $(this).attr("data-value");
                                if (dataValue == "delete")
                                    DeleteByID('<%= dr["ID"].ToString() %>', '<%= table %>', '<%= ControlAdminInfo.ShortName %>');

                                if ($(this).hasClass("validate") || ValidateForm()) {
                                    $('#frm_edit #done').val(dataValue);
                                    $(this).attr('disabled', 'disabled');
                                    $(this).html('Loading...');
                                    $("#frm_edit").submit();
                                }
                                else {
                                    return false;
                                }
                            });
                        </script>
                    </div>
                </div>
            </div>
        </div>
    </form>
</div>
