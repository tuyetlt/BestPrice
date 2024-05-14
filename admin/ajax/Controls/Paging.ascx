<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Paging.ascx.cs" Inherits="admin_ajax_Controls_Paging" %>


<% if(totalRecord>0){ %>

<nav aria-label="Page navigation">
    <ul class="pagination" id="pagination"></ul>
    Tổng số: <%= totalRecord %> bản ghi
</nav>

<script type="text/javascript">
    $(document).ready(function () {
        var firstLoad = 1;
            window.pagObj = $('#pagination').twbsPagination({
                currentPage: <%= pageIndex %>,
                prev: 'Trước',
                next: 'Sau',
                first: '«',
                last: '»',
                totalPages: <%= totalPage %>,
                visiblePages: 6,
                onPageClick: function (event, page) {
                    if (page === this.totalpages) {
                       
                        getval(0);
                    }
                    $("#pageIndex").val(page);
                    //$("#loadpaging").val("false");
                    //alert(page);
                    
                }
            }).on('page', function (event, page) {
                $("#loadpaging").val("false");
                getval(0);

                console.info(page + ' (from event listening)');
            });

    });
</script>

<% } %>