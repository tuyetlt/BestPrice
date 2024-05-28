<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GenCode.aspx.cs" Inherits="_tool_GenCode" %>

<%@ Import Namespace="System.Data" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gen code</title>
    <script type="text/javascript" src="/assets/js/jquery-3.5.1.min.js"></script>

    <style type="text/css">
        textarea {
            width: 100%;
            margin-top: 50px
        }
    </style>
</head>
<body>
    <div style="margin: 50px auto; width: 1200px">
        <select id="drTable">
            <%
                foreach (DataRow drTable in dtTable.Rows)
                {
                    string selected = "";
                    if (tableName == drTable[0].ToString())
                        selected = "selected";
            %>
            <option <%= selected %> value="<%= drTable[0].ToString() %>"><%= drTable[0].ToString() %></option>
            <%
                }
            %>
        </select>

       <%-- <select id="drType">
            <option value="HashTable">HashTable</option>
            <option value="Insert">Insert</option>
            <option value="Update">Update</option>
            <option value="InsertUpdate">Insert & Update</option>
        </select>--%>

        <input type="button" id="btnGenCode" value="Gen Code" />

        <div style="float: left; width: 20%">
            <input type="checkbox" id="checkAll" checked="checked" />
            <label for="checkAll">Chọn tất cả</label><br />
            <br />
            <%
                foreach (DataRow drColumn in dtColumn.Rows)
                {
            %>
            <div style="margin-top: 3px">
                <input type="checkbox" class="col" id="<%= drColumn["COLUMN_NAME"] %>" name="<%= drColumn["COLUMN_NAME"] %>" data-type="<%= drColumn["DATA_TYPE"] %>" value="<%= drColumn["COLUMN_NAME"] %>" checked="checked" />
                <label for="<%= drColumn["COLUMN_NAME"] %>"><%= drColumn["COLUMN_NAME"] %></label>
            </div>
            <%
                }
            %>
        </div>

        <div style="float: left; width: 80%">
            <div id="ajaxResult" style="border: 3px solid #dbdbdb; width: 100%;  overflow: scroll; height: auto; margin: 50px 0; padding: 10px"></div>
        </div>

        <script type="text/javascript">
            $("#drTable").change(function () {
                var value = this.value;
                var oldURL = window.location.protocol + "//" + window.location.host + window.location.pathname;
                var newUrl = oldURL + "?tableName=" + value;
                window.location = newUrl;
            });

            $("#btnGenCode").click(function () {
                GetAjax();
            });


            $("#checkAll").click(function () {
                $('input:checkbox').not(this).prop('checked', this.checked);
                GetAjax();
            });

            $(".col").click(function () {
                GetAjax();
            });

            GetAjax();
            function GetAjax() {
                var col_list = $(this).find("input[type=checkbox]");
                var count = 0;
                var allCol = "", dataType = "";
                $("input[type=checkbox].col").each(function () {
                    if (this.checked) {
                        var text = $(this).val();
                        console.log(text);
                        if (count > 0) {
                            allCol += "-";
                            dataType += "-";
                        }
                        allCol += text;
                        dataType += $(this).data("type");
                        count++;
                    }
                });

                //var Type = $('#drType option:selected').val();
                var Table = $('#drTable option:selected').val();

                $.ajax({
                    url: "ajax/ajax.aspx",
                    data: { control: "gencode", table: Table, col: allCol, dataType: dataType }
                })
                    .done(function (html) {
                        $("#ajaxResult").html(html);
                    });
            }
        </script>
    </div>
</body>
</html>
