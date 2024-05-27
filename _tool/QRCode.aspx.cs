using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using QRCoder;

public partial class _tool_QRCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string url = "https://maps.app.goo.gl/Who4DkFiVHtKEpax6";
        string logoPath = Server.MapPath("~/upload/images/logo/thadaco.png");
        //GenerateStyledQRCode(url, logoPath);
        GenerateQRCode(url);
    }

    private void GenerateStyledQRCode(string url, string logoPath)
    {
        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        {
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);

            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                // Tăng giá trị iconSizePercent để làm logo to hơn
                using (Bitmap qrCodeImage = qrCode.GetGraphic(30, Color.Black, Color.White, GetLogoBitmap(logoPath), 30, 6, false))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        qrCodeImage.Save(ms, ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        string base64Image = Convert.ToBase64String(byteImage);
                        imgQRCode.ImageUrl = "data:image/png;base64," + base64Image;
                    }
                }
            }
        }
    }

    private Bitmap GetLogoBitmap(string logoPath)
    {
        Bitmap logo = (Bitmap)Image.FromFile(logoPath);

        int maxLogoSize = 200;
        int logoWidth = logo.Width;
        int logoHeight = logo.Height;

        if (logoWidth > maxLogoSize || logoHeight > maxLogoSize)
        {
            float ratio = Math.Min((float)maxLogoSize / logoWidth, (float)maxLogoSize / logoHeight);
            logoWidth = (int)(logoWidth * ratio);
            logoHeight = (int)(logoHeight * ratio);
        }

        return new Bitmap(logo, new Size(logoWidth, logoHeight));
    }



    // Không logo
    private void GenerateQRCode(string url)
    {
        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
        {
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            using (QRCode qrCode = new QRCode(qrCodeData))
            {
                using (Bitmap qrCodeImage = qrCode.GetGraphic(20))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        qrCodeImage.Save(ms, ImageFormat.Png);
                        byte[] byteImage = ms.ToArray();
                        string base64Image = Convert.ToBase64String(byteImage);
                        imgQRCode.ImageUrl = "data:image/png;base64," + base64Image;
                    }
                }
            }
        }
    }
}