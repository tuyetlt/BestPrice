<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ArticleUpdate.ascx.cs" Inherits="admin_Controls_ArticleUpdate" %>
<%@ Import Namespace="Ebis.Utilities" %>
<%@ Import Namespace="System.Data" %>
<div class="obj-edit">
    <form method="post" enctype="multipart/form-data" id="frm_edit">
        <input type="hidden" name="id" value="<%= dr["ID"].ToString() %>" />
        <div class="container">
            <div class="edit">
                <div class="left">
                    <!-- Tên sản phẩm -->
                    <div class="form-group">
                        <div>Tiêu đề<span class="required">*</span></div>
                        <div>
                            <input type="text" required id="name" name="name" value="<%= dr["Name"].ToString()%>" />
                        </div>
                    </div>
                    <!-- Url Thân thiện -->
                    <div class="form-group">
                        <div>Url Thân thiện <span class="required">*</span></div>
                        <div>
                            <input type="text" id="friendlyurl" name="friendlyurl" value="<%= dr["FriendlyUrl"].ToString()%>" />
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

                    <!-- NewsRelatedIDList -->
                    <div class="form-group">
                        <div> </div>
                        <div>
                            <input type="hidden" id="newsrelatedidlist" name="newsrelatedidlist" value="<%= dr["NewsRelatedIDList"].ToString()%>" />
                            <%--<input type="text" id="newsrelatedidlist" name="newsrelatedidlist" value="710,711,712,713" />--%>


                            

                              <input class="btnOpenPopup btn" type="button" value="Chọn tin liên quan" />
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
                                    windowURL: "/admin/popup/popup.aspx?ctrl=newsrelated&textbox=newsrelatedidlist&button=btnButtonAutoClick&Category=" + $("#categoryid").val(),
                                    windowName: 'swip',
                                    centerScreen: 1,
                                    height: 630,
                                    width: 800
                                });

                                function callAjax() {

                                    var idList = $("#newsrelatedidlist").val();
                                    if (idList.length >= 0) {
                                        $("#LoadingImage").show();
                                        $.ajax({
                                            url: '/admin/ajax/ajax.aspx',
                                            type: "GET",
                                            data: { btn: "btnButtonAutoClick", txt: "newsrelatedidlist", articleIDList: idList, ctrl: "newsselected" },
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



                    <!--- Tags -->

                    <div class="form-group">
                        <div>Tags</div>
                        <div>
                            <input type="text" id="tag_id" name="tagidlist" value="<%= Utils.CommaSQLRemove(dr["TagIDList"].ToString()) %>" style="display: none" />
                            <input type="text" id="tag_name" name="tag_name" value="<%= Utils.CommaSQLRemove(dr["TagNameList"].ToString()) %>" style="display: none" />
                            <select id="drTag" data-idreturn="Name" multiple data-level="0" data-folder="<%= Utils.GetFolderControlAdmin() %>"></select>
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



                    <!-- Thư viện ảnh -->
                    <div class="form-group">
                        <div>Thư viện ảnh </div>
                        <div>
                            <input type="text" id="gallery" name="gallery" style="display: none" value='<%= dr["Gallery"].ToString()%>' />
                            <div id="dZUpload" class="dropzone" data-folder="article">
                                <div class="dz-default dz-message">
                                    Bấm hoặc kéo ảnh vào vùng này để tải ảnh.
                                </div>
                            </div>
                        </div>
                    </div>


                  

                    <!-- StartDate -->
                    <div class="form-group">
                        <div>Ngày xuất bản </div>
                        <div>
                            <input type="text" id="startdate" class="datepicker" name="startdate" value="<%= dr["StartDate"].ToString()%>" />
                        </div>

                    </div>





                    <!-- Sort -->
                    <div class="form-group">
                        <div>Sắp xếp </div>
                        <div>
                            <input type="text" id="sort" name="sort" value="<%= dr["Sort"].ToString()%>" />
                        </div>
                    </div>


                    <!--- Thuộc tính hệ thống -->

                    <%--  <div class="form-group">
                        <div>Trạng thái</div>
                        <div>
                            <input type="text" id="attr_c_id" name="attributeconfigidlist" value="<%= Utils.CommaSQLRemove(dr["AttributeConfigIDList"].ToString()) %>" style="display: none" />
                            <input type="text" id="attr_c_name" value="<%= Attr_Config_NameList %>" style="display: none" />
                            <select id="drAtrrC" multiple data-level="1" data-control="<%= Utils.GetControlAdmin() %>" name="drAtrrC" style="width: 100%"></select>
                        </div>
                    </div>--%>

                    <div class="flex threecol">
                        <div></div>
                        <div>
                            <div>Tùy chọn</div>
                            <div>
                                <input type="checkbox" id="HomeArticle" name="HomeArticle" <%= Utils.SetChecked(_flag.HasFlag(ArticleFlag.HomeArticle)) %> />
                                <label for="HomeArticle">Đặt lên Trang chủ</label><br>
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
                            <span class="count-char">123</span>
                            <textarea id="metakeyword" name="metakeyword"><%= dr["MetaKeyword"].ToString()%></textarea>
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
                </div>
                <div class="clear"></div>
                <div class="full1">
                    
                    <!-- Description -->
                    <div class="form-group">
                        <div>Tóm tắt </div>
                        <div>
                            <textarea name="description" id="description" rows="5" class="ckeditor"><%= dr["Description"].ToString()%></textarea>
                        </div>
                    </div>

                      <!-- Long Description -->
                    <div class="form-group">
                        <div>Nội dung </div>
                        <div>
                            <textarea name="longdescription" id="longdescription" rows="10" class="ckeditor"><%= dr["LongDescription"].ToString()%></textarea>
                        </div>
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
                        <button type="submit" data-value="delete" class="btnSubmit btnDelete"><i class="fas fa-trash-alt"></i>Xoá</button>
                        <%}
                            else
                            { %>
                        <button type="submit" data-value="saveandadd" class="btnSubmit btnAddAndAdd"><i class="fas fa-plus"></i>Thêm</button>
                        <button type="submit" data-value="saveandback" class="btnSubmit btnAddAndBack"><i class="fas fa-plus"></i>Thêm và Quay Lại</button>
                        <button type="submit" data-value="saveandcopy" class="btnSubmit btnAddAndCopy"><i class="fas fa-copy"></i>Thêm và Sao Chép</button>
                        <% } %>

                         <a target="_blank" href="<%= TextChanger.GetLinkRewrite_Article(dr["FriendlyUrl"].ToString()) %>" class="btnView"><i class="fas fa-eye"></i>Xem thử</a>
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
    </form>
</div>
