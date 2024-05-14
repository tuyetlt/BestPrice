using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for Paging
/// </summary>
public static class Paging
{

    private static string url { get; set; }

    private static int TotalRow { get; set; }


    
    private static int PageCount;

    private static int PageSize = 20;
    private static int ItemRow
    {
        get { return ItemRow; }
        set
        {
            ItemRow = value;
            double divide = ItemRow / PageSize;
            double ceiled = System.Math.Ceiling(divide);
            PageCount = Convert.ToInt32(ceiled);
        }
    }


    public static string Render_Paging(int current, int pageize, int totalrow, string url)
    {
        StringBuilder sb = new StringBuilder();
        int CompactedPageCount = 6;
        int NotCompactedPageCount = 6;
        double divide = (double)totalrow / (double)pageize;
        double ceiled = System.Math.Ceiling(divide);
        if (ceiled > 0)
            PageCount = Convert.ToInt32(ceiled);
        else
            PageCount = 1;
        if (current > PageCount)
            current = PageCount;
        if (totalrow > 0)
        {
            sb.Append("<div class='row datatables-footer'>");
            sb.Append("<div class='paging-info'>");
            sb.Append("<div class='dataTables_info' id='datatable-default_info' role='status' aria-live='polite'>Tổng số: " + totalrow + " bản ghi - Trang (" + current + " / " + PageCount + ")</div>");
            sb.Append("</div>");

            sb.Append("<div class='paging-number'>");
            sb.Append("<div class='dataTables_paginate paging_bs_normal'>");
            sb.Append("<ul class='pagination'>");

            if (current == 1)
            {
                sb.Append("<li class='prev disabled'><a href='javascript:void(0)' data-toggle='tooltip' data-original-title='First'>«</a></li>");
                sb.Append("<li class='prev disabled'><a href='javascript:void(0)' data-toggle='tooltip' data-original-title='Previous'>←</a></li>");
            }
            else
            {
                sb.Append("<li><a href='" + url + "1' data-toggle='tooltip' data-original-title='First'>|<</a></li>");
                sb.Append("<li><a href='" + url + (current - 1) + "' data-toggle='tooltip' data-original-title='Previous'>←</a></li>");
            }
            if (current < CompactedPageCount)
            {
                if (CompactedPageCount > PageCount) CompactedPageCount = PageCount;
                for (int i = 1; i < CompactedPageCount + 1; i++)
                {
                    if (i == current)
                    {
                        sb.Append("<li class='active'><a>" + i + "</a></li>");
                    }
                    else
                    {
                        sb.Append("<li><a href='" + url + i + "' data-toggle='tooltip' data-original-title=''>" + i + " </a></li>");
                    }
                }
                
            }
            else if (current >= CompactedPageCount && current < NotCompactedPageCount)
            {
                if (NotCompactedPageCount > PageCount) NotCompactedPageCount = PageCount;
                for (int i = 1; i < NotCompactedPageCount + 1; i++)
                {
                    if (i == current)
                    {
                        sb.Append("<li class='active'><a>" + i + "</a></li>");
                    }
                    else
                    {
                        sb.Append("<li><a href='" + url + i + "' data-toggle='tooltip' data-original-title=''>" + i + " </a></li>");
                    }
                }
            
            }
            else if (current >= NotCompactedPageCount)
            {
                int gapValue = NotCompactedPageCount / 2;
                int leftBand = current - gapValue;
                int rightBand = current + gapValue;
               
                for (int i = leftBand; (i < rightBand + 1) && i < PageCount + 1; i++)
                {
                    if (i == current)
                    {
                        sb.Append("<li class='active'><a>" + i + "</a></li>");
                    }
                    else
                    {
                        sb.Append("<li><a href='" + url + i + "' data-toggle='tooltip' data-original-title=''>" + i + " </a></li>");
                    }
                }
              
            }

            if (current == PageCount)
            {
                sb.Append("<li class='prev disabled'><a href='javascript:void(0)' data-toggle='tooltip' data-original-title='Next'>→</a></li>");
                sb.Append("<li class='prev disabled'><a href='javascript:void(0)' data-toggle='tooltip' data-original-title='Last'>»</a></li>");
            }
            else
            {
                sb.Append("<li><a href='" + url + (current + 1) + "' data-toggle='tooltip' data-original-title='Next'>→</a></li>");
                sb.Append("<li><a href='" + url + (PageCount) + "' data-toggle='tooltip' data-original-title='Last'>»</a></li>");
            }
            sb.Append("</ul>");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</div>");
        }
        else
        {
            sb.Append("<div class='row datatables-footer'>");
            sb.Append("<div class='col-sm-12 col-md-4'>");
            sb.Append("không có bản ghi" + "</div>");
            sb.Append("</div>");
        }

        return sb.ToString();

    }

    public static string Render_Paging_Client(int current, int pageize, int totalrow, string url)
    {
        StringBuilder sb = new StringBuilder();
        int CompactedPageCount = 10;
        int NotCompactedPageCount = 15;
        int offset = 0;
        double divide = (double)totalrow / (double)pageize;
        double ceiled = System.Math.Ceiling(divide);
        if (ceiled > 0)
            PageCount = Convert.ToInt32(ceiled);
        else
            PageCount = 1;
        if (current > PageCount)
            current = PageCount;
        if (totalrow > 0)
        {
            // offset = (TotalRow + limit - 1) / limit;
            sb.Append("<div class='row datatables-footer'>");
          

            sb.Append("<div class='col-sm-12 col-md-12'>");
            sb.Append("<div class='dataTables_paginate paging_bs_normal'>");
            sb.Append("<ul class='pagination'>");

            if (current == 1)
            {
                sb.Append("<li class='prev disabled'><a href='javascript:void(0)' data-toggle='tooltip' data-original-title='First'><i class='fa fa-angle-double-left'></i></a></li>");
                sb.Append("<li class='prev disabled'><a href='javascript:void(0)' data-toggle='tooltip' data-original-title='Previous'><i class='fa fa-angle-left'></i></a></li>");
            }
            else
            {
                sb.Append("<li><a href='" + url + "1' data-toggle='tooltip' data-original-title='First'><i class='fa fa-angle-double-left'></i></a></li>");
                sb.Append("<li><a href='" + url + (current - 1) + "' data-toggle='tooltip' data-original-title='Previous'><i class='fa fa-angle-left'></i></a></li>");
            }

            //for (int i = 1; i <= offset; i++)
            //{
            //    if (i == current)
            //        sb.Append("<li class='active'><a>" + i + "</a></li>");
            //    else
            //        sb.Append("<li><a href='" + url + i + "' data-toggle='tooltip' data-original-title=''>" + i + " </a></li>");
            //}
            if (current < CompactedPageCount)
            {
                if (CompactedPageCount > PageCount) CompactedPageCount = PageCount;
                for (int i = 1; i < CompactedPageCount + 1; i++)
                {
                    if (i == current)
                    {
                        sb.Append("<li class='active'><a>" + i + "</a></li>");
                    }
                    else
                    {
                        sb.Append("<li><a href='" + url + i + "' data-toggle='tooltip' data-original-title=''>" + i + " </a></li>");
                    }
                }

            }
            else if (current >= CompactedPageCount && current < NotCompactedPageCount)
            {
                if (NotCompactedPageCount > PageCount) NotCompactedPageCount = PageCount;
                for (int i = 1; i < NotCompactedPageCount + 1; i++)
                {
                    if (i == current)
                    {
                        sb.Append("<li class='active'><a>" + i + "</a></li>");
                    }
                    else
                    {
                        sb.Append("<li><a href='" + url + i + "' data-toggle='tooltip' data-original-title=''>" + i + " </a></li>");
                    }
                }

            }
            else if (current >= NotCompactedPageCount)
            {
                int gapValue = NotCompactedPageCount / 2;
                int leftBand = current - gapValue;
                int rightBand = current + gapValue;

                for (int i = leftBand; (i < rightBand + 1) && i < PageCount + 1; i++)
                {
                    if (i == current)
                    {
                        sb.Append("<li class='active'><a>" + i + "</a></li>");
                    }
                    else
                    {
                        sb.Append("<li><a href='" + url + i + "' data-toggle='tooltip' data-original-title=''>" + i + " </a></li>");
                    }
                }

            }

            if (current == PageCount)
            {
                sb.Append("<li class='prev disabled'><a href='javascript:void(0)' data-toggle='tooltip' data-original-title='Next'><i class='fa fa-angle-right'></i></a></li>");
                sb.Append("<li class='prev disabled'><a href='javascript:void(0)' data-toggle='tooltip' data-original-title='Last'><i class='fa fa-angle-double-right'></i></a></li>");
            }
            else
            {
                sb.Append("<li><a href='" + url + (current + 1) + "' data-toggle='tooltip' data-original-title='Next'><i class='fa fa-angle-right'></i></a></li>");
                sb.Append("<li><a href='" + url + (PageCount) + "' data-toggle='tooltip' data-original-title='Last'><i class='fa fa-angle-double-right'></i></a></li>");
            }
            sb.Append("</ul>");
            sb.Append("</div>");
            sb.Append("</div>");
            sb.Append("</div>");
        }
        else
        {
            sb.Append("<div class='row datatables-footer'>");
            sb.Append("<div class='col-sm-12 col-md-4'>");
            sb.Append("không có bản ghi" + "</div>");
            sb.Append("</div>");
        }

        return sb.ToString();

    }
}