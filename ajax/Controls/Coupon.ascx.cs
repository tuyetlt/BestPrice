using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ajax_Controls_Coupon : System.Web.UI.UserControl
{
    DataTable dt; 
    protected void Page_Load(object sender, EventArgs e)
    {
        string code = RequestHelper.GetString("code", "");
        string idProductList = RequestHelper.GetString("idProductList", "");
        idProductList = Utils.CommaSQLRemove(idProductList);
        string coupon = "";
        Response.Clear();
        Response.Headers.Add("Content-type", "application/json");
        Hashtable hashtable = new Hashtable();

        dt = SqlHelper.SQLToDataTable("tblCoupon", "", string.Format("Name=N'{0}'", code.Trim()));
        if (Utils.CheckExist_DataTable(dt))
        {
            foreach(DataRow dr in dt.Rows)
            {
                hashtable.Add("valuevnd", dr["valuevnd"].ToString());
                coupon = Utils.CommaSQLRemove(dr["ProductIDList"].ToString());
            }    
        }


        // Chia chuỗi thành mảng các chuỗi con
        string[] idProducts = idProductList.Split(',').Select(s => s.Trim()).ToArray();
        string[] couponProducts = coupon.Split(',').Select(s => s.Trim()).ToArray();

        // Kiểm tra chuỗi rỗng
        if (idProducts.Length == 1 && string.IsNullOrEmpty(idProducts[0]))
        {
            //Console.WriteLine("Danh sách sản phẩm rỗng.");
            hashtable.Add("proExist", "nonePro");
            return;
        }

        if (couponProducts.Length == 1 && string.IsNullOrEmpty(couponProducts[0]))
        {
            //Console.WriteLine("Coupon rỗng.");
            hashtable.Add("proExist", "noneCoupon");
            return;
        }

        // Kiểm tra sự trùng lặp giữa các phần tử
        var commonProducts = idProducts.Intersect(couponProducts);

        // In ra kết quả
        if (commonProducts.Any())
        {
            //Console.WriteLine("Coupon chứa ít nhất một sản phẩm từ danh sách sản phẩm.");
            //Console.WriteLine("Sản phẩm trùng lặp: " + string.Join(", ", commonProducts));
            hashtable.Add("proExist", commonProducts);
        }
        else
        {
            //Console.WriteLine("Coupon không chứa sản phẩm nào từ danh sách sản phẩm.");
            //Console.WriteLine("Coupon rỗng.");
            hashtable.Add("proExist", "noneMatching");
        }

        Response.Write(JSONHelper.ToJSON(hashtable));
        Response.End();
    }
}