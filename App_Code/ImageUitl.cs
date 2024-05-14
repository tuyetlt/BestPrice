using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

/// <summary>
/// Summary description for ImageUitl
/// </summary>
public class ImageUitl
{
    public ImageUitl()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static bool UrlExists(string url)
    {
        //try
        //{
        //    new System.Net.WebClient().DownloadData(url);
        //    return true;
        //}
        //catch (System.Net.WebException e)
        //{

        //    return false;
        //}
        if (!String.IsNullOrEmpty(url))
        {
            try
            {
                return File.Exists(HttpContext.Current.Server.MapPath(url));
            }
            catch (Exception)
            {
                return false;
            }
        }
        else
            return false;

    }
    public static void CreateThumb(int newWidth, string src, string path)
    {
        // System.Drawing.Image image = System.Drawing.Image.FromFile(src);
        //Bitmap loBMP = new Bitmap(src);
        //string filename = Path.GetFileName(src);
        //int newHeight = (int)Math.Round((decimal)(newWidth * image.Height) / image.Width,0);
        //System.Drawing.Image thumbnailImage = image.GetThumbnailImage(newWidth, newHeight, new Image.GetThumbnailImageAbort(Abort), IntPtr.Zero);

        ////Save Compress 
        //ImageCodecInfo[] Info = ImageCodecInfo.GetImageEncoders();
        //EncoderParameters Params = new EncoderParameters(1);
        //Params.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, 100L);
        //image.Dispose();
        //thumbnailImage.Save(path, Info[1], Params);

        //thumbnailImage.Dispose();
        System.Drawing.Bitmap bmpOut = null;

        Bitmap loBMP = new Bitmap(src);
        ImageFormat loFormat = loBMP.RawFormat;
        int newHeight = (int)Math.Round((decimal)(newWidth * loBMP.Height) / loBMP.Width, 0);
        //*** If the image is smaller than a thumbnail just return it
        bmpOut = new Bitmap(newWidth, newHeight);
        Graphics g = Graphics.FromImage(bmpOut);
        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
        g.FillRectangle(Brushes.White, 0, 0, newWidth, newWidth);
        g.DrawImage(loBMP, 0, 0, newWidth, newWidth);

        loBMP.Dispose();
        bmpOut.Save(src);
    }
    public static void CreateThumb(int newWidth, int newHeight, string src, string path)
    {
        System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(src));
        string filename = Path.GetFileName(src);
        System.Drawing.Image thumbnailImage = image.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

        //Save Compress 
        ImageCodecInfo[] Info = ImageCodecInfo.GetImageEncoders();
        EncoderParameters Params = new EncoderParameters(1);
        Params.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, 100L);
        thumbnailImage.Save(HttpContext.Current.Server.MapPath(path), Info[1], Params);
        image.Dispose();
        thumbnailImage.Dispose();
    }

    public static void UploadImage(string src, string path, string name)
    {
        System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(src));
        string filename = Path.GetFileName(src);
        ImageCodecInfo[] Info = ImageCodecInfo.GetImageEncoders();
        EncoderParameters Params = new EncoderParameters(1);
        Params.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, 100L);
        image.Save(HttpContext.Current.Server.MapPath(path) + name, Info[1], Params);
    }
    public static void ResizeImage(int size, string filePath, string saveFilePath)
    {
        //variables for image dimension/scale
        double newHeight = 0;
        double newWidth = 0;
        double scale = 0;

        //create new image object
        Bitmap curImage = new Bitmap(HttpContext.Current.Server.MapPath(filePath));

        //Determine image scaling
        if (curImage.Height > curImage.Width)
        {
            scale = Convert.ToSingle(size) / curImage.Height;
        }
        else
        {
            scale = Convert.ToSingle(size) / curImage.Width;
        }
        if (scale < 0 || scale > 1) { scale = 1; }

        //New image dimension
        newHeight = Math.Floor(Convert.ToSingle(curImage.Height) * scale);
        newWidth = Math.Floor(Convert.ToSingle(curImage.Width) * scale);

        //Create new object image
        Bitmap newImage = new Bitmap(curImage, Convert.ToInt32(newWidth), Convert.ToInt32(newHeight));
        Graphics imgDest = Graphics.FromImage(newImage);
        imgDest.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        imgDest.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        imgDest.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
        imgDest.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
        EncoderParameters param = new EncoderParameters(1);
        param.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);

        //Draw the object image
        imgDest.DrawImage(curImage, 0, 0, newImage.Width, newImage.Height);

        //Save image file
        newImage.Save(HttpContext.Current.Server.MapPath(saveFilePath), info[1], param);

        //Dispose the image objects
        curImage.Dispose();
        newImage.Dispose();
        imgDest.Dispose();
    }
    //public static void ResizeImage(int newWidth, int newHeight, string filePath, string saveFilePath)
    //{
    //    Image curImage = new Bitmap(HttpContext.Current.Server.MapPath(filePath));
    //    ImageFormat imageformat = ImageType(System.IO.Path.GetExtension(filePath));

    //    Image newImage = new Bitmap(curImage, Convert.ToInt32(newWidth), Convert.ToInt32(newHeight));

    //    Graphics imgDest = Graphics.FromImage(newImage);
    //    using (System.Drawing.Graphics graphicsHandle = System.Drawing.Graphics.FromImage(newImage))
    //    {
    //        graphicsHandle.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
    //        graphicsHandle.InterpolationMode =
    //                   System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
    //        graphicsHandle.DrawImage(curImage, 0, 0, newWidth, newHeight);
    //    }
    //    newImage.Save(HttpContext.Current.Server.MapPath(saveFilePath), imageformat);
    //    curImage.Dispose();
    //    newImage.Dispose();
    //    imgDest.Dispose();
    //}
    public static void ResizeImage(int newWidth, int newHeight, string filePath, string saveFilePath) //Bỏ server.mappath
    {
        Image curImage = new Bitmap(filePath);
        ImageFormat imageformat = ImageType(System.IO.Path.GetExtension(filePath));

        Image newImage = new Bitmap(curImage, Convert.ToInt32(newWidth), Convert.ToInt32(newHeight));

        Graphics imgDest = Graphics.FromImage(newImage);
        using (System.Drawing.Graphics graphicsHandle = System.Drawing.Graphics.FromImage(newImage))
        {
            graphicsHandle.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            graphicsHandle.InterpolationMode =
                       System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphicsHandle.DrawImage(curImage, 0, 0, newWidth, newHeight);
        }
        newImage.Save(saveFilePath, imageformat);
        curImage.Dispose();
        newImage.Dispose();
        imgDest.Dispose();
    }
    public static void ResizeImage(int newWidth, int newHeight, string filePath, string saveFilePath, bool d)
    {

        Image curImage = new Bitmap(HttpContext.Current.Server.MapPath(filePath));
        ImageFormat imageformat = ImageType(System.IO.Path.GetExtension(filePath));

        Size newSize = ResizeKeepAspect(curImage.Width, curImage.Height, newWidth, newHeight);
        
        Image newImage = FixedSize(curImage, newWidth, newHeight);

        newImage.Save(HttpContext.Current.Server.MapPath(saveFilePath), imageformat); curImage.Dispose();
        newImage.Dispose();


    }

    public static Size ResizeKeepAspect(int curW, int curH, int maxWidth, int maxHeight)
    {
        int newHeight = curW;
        int newWidth = curH;
        if (maxWidth > 0 && newWidth > maxWidth) //WidthResize
        {
            Decimal divider = Math.Abs((Decimal)newWidth / (Decimal)maxWidth);
            newWidth = maxWidth;
            newHeight = (int)Math.Round((Decimal)(newHeight / divider));
        }
        if (maxHeight > 0 && newHeight > maxHeight) //HeightResize
        {
            Decimal divider = Math.Abs((Decimal)newHeight / (Decimal)maxHeight);
            newHeight = maxHeight;
            newWidth = (int)Math.Round((Decimal)(newWidth / divider));
        }
        return new Size(newWidth, newHeight);
    }
    static Image FixedSize(Image imgPhoto, int Width, int Height)
    {
        int sourceWidth = imgPhoto.Width;
        int sourceHeight = imgPhoto.Height;
        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((Width -
                          (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = System.Convert.ToInt16((Height -
                          (sourceHeight * nPercent)) / 2);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        Bitmap bmPhoto = new Bitmap(Width, Height,
                          PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                         imgPhoto.VerticalResolution);

        Graphics grPhoto = Graphics.FromImage(bmPhoto);
        grPhoto.Clear(Color.White);
        grPhoto.InterpolationMode =
                InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new Rectangle(destX, destY, destWidth, destHeight),
            new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            GraphicsUnit.Pixel);

        grPhoto.Dispose();
        return bmPhoto;
    }

    public static ImageFormat ImageType(string type)
    {
        switch (type)
        {
            case ".JPG": return ImageFormat.Jpeg; break;
            case ".jpg": return ImageFormat.Jpeg; break;
            case ".png": return ImageFormat.Png; break;
            case ".PNG": return ImageFormat.Png; break;
            case ".gif": return ImageFormat.Gif; break;
            case ".GIF": return ImageFormat.Gif; break;
            default: return ImageFormat.Jpeg;
        }
    }

    public static string ImageToBase64(Image image, ImageFormat format)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            // Convert Image to byte[]
            image.Save(ms, format);
            byte[] imageBytes = ms.ToArray();

            // Convert byte[] to Base64 String
            string base64String = Convert.ToBase64String(imageBytes);
            return "data:image/" + format + ";base64," + base64String;

        }
    }

    public static Size ResizeKeepAspect1(Size CurrentDimensions, int maxWidth, int maxHeight)
    {
        int newHeight = CurrentDimensions.Height;
        int newWidth = CurrentDimensions.Width;
        if (maxWidth > 0 && newWidth > maxWidth) //WidthResize
        {
            Decimal divider = Math.Abs((Decimal)newWidth / (Decimal)maxWidth);
            newWidth = maxWidth;
            newHeight = (int)Math.Round((Decimal)(newHeight / divider));
        }
        if (maxHeight > 0 && newHeight > maxHeight) //HeightResize
        {
            Decimal divider = Math.Abs((Decimal)newHeight / (Decimal)maxHeight);
            newHeight = maxHeight;
            newWidth = (int)Math.Round((Decimal)(newWidth / divider));
        }
        return new Size(newWidth, newHeight);
    }


    public static string WaterMark(string src, string watermark, string Copyright)
    {
        //create a image object containing the photograph to watermark

        try
        {

            Image imgPhoto = Image.FromFile(HttpContext.Current.Server.MapPath(src));
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;



            //create a Bitmap the Size of the original photograph
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);


            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //load the Bitmap into a Graphics object 
            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            //create a image object containing the watermark
            Image imgWatermark = new Bitmap(HttpContext.Current.Server.MapPath(watermark));

            int wmWidth = imgWatermark.Width;
            int wmHeight = imgWatermark.Height;


            //Set the rendering quality for this Graphics object
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

            //Draws the photo Image object at original size to the graphics object.
            grPhoto.DrawImage(
                imgPhoto,                               // Photo Image object
                new Rectangle(0, 0, phWidth, phHeight), // Rectangle structure
                0,                                      // x-coordinate of the portion of the source image to draw. 
                0,                                      // y-coordinate of the portion of the source image to draw. 
                phWidth,                                // Width of the portion of the source image to draw. 
                phHeight,                               // Height of the portion of the source image to draw. 
                GraphicsUnit.Pixel);                    // Units of measure 

            //-------------------------------------------------------
            //to maximize the size of the Copyright message we will 
            //test multiple Font sizes to determine the largest posible 
            //font we can use for the width of the Photograph
            //define an array of point sizes you would like to consider as possiblities
            //-------------------------------------------------------
            //int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };

            int[] sizes = new int[] { 30, 28, 26, 24, 22, 20, 18 };

            Font crFont = null;
            SizeF crSize = new SizeF();

            //Loop through the defined sizes checking the length of the Copyright string
            //If its length in pixles is less then the image width choose this Font size.
            for (int i = 0; i < 7; i++)
            {
                //set a Font object to Arial (i)pt, Bold
                crFont = new Font("arial", sizes[i], FontStyle.Bold);
                //Measure the Copyright string in this Font
                crSize = grPhoto.MeasureString(Copyright, crFont);

                if ((ushort)crSize.Width < (ushort)phWidth)
                    break;
            }

            //Since all photographs will have varying heights, determine a 
            //position 5% from the bottom of the image
            int yPixlesFromBottom = (int)(phHeight * .05);

            //Now that we have a point size use the Copyrights string height 
            //to determine a y-coordinate to draw the string of the photograph
            float yPosFromBottom = ((phHeight - yPixlesFromBottom) - (crSize.Height / 2));

            //Determine its x-coordinate by calculating the center of the width of the image
            float xCenterOfImg = (phWidth / 2);

            //Define the text layout by setting the text alignment to centered
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;

            //define a Brush which is semi trasparent black (Alpha set to 153)
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));

            //Draw the Copyright string
            grPhoto.DrawString(Copyright,                 //string of text
                crFont,                                   //font
                semiTransBrush2,                           //Brush
                new PointF(xCenterOfImg + 1, yPosFromBottom + 1),  //Position
                StrFormat);

            //define a Brush which is semi trasparent white (Alpha set to 153)
            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

            //Draw the Copyright string a second time to create a shadow effect
            //Make sure to move this text 1 pixel to the right and down 1 pixel
            grPhoto.DrawString(Copyright,                 //string of text
                crFont,                                   //font
                semiTransBrush,                           //Brush
                new PointF(xCenterOfImg, yPosFromBottom),  //Position
                StrFormat);                               //Text alignment



            //------------------------------------------------------------
            //Step #2 - Insert Watermark image
            //------------------------------------------------------------

            //Create a Bitmap based on the previously modified photograph Bitmap
            Bitmap bmWatermark = new Bitmap(bmPhoto);
            bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
            //Load this Bitmap into a new Graphic Object
            Graphics grWatermark = Graphics.FromImage(bmWatermark);

            //To achieve a transulcent watermark we will apply (2) color 
            //manipulations by defineing a ImageAttributes object and 
            //seting (2) of its properties.
            ImageAttributes imageAttributes = new ImageAttributes();

            //The first step in manipulating the watermark image is to replace 
            //the background color with one that is trasparent (Alpha=0, R=0, G=0, B=0)
            //to do this we will use a Colormap and use this to define a RemapTable
            ColorMap colorMap = new ColorMap();

            //My watermark was defined with a background of 100% Green this will
            //be the color we search for and replace with transparency
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            //The second color manipulation is used to change the opacity of the 
            //watermark.  This is done by applying a 5x5 matrix that contains the 
            //coordinates for the RGBA space.  By setting the 3rd row and 3rd column 
            //to 0.3f we achive a level of opacity
            float[][] colorMatrixElements = { 
												new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},       
												new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},        
												new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},        
												new float[] {0.0f,  0.0f,  0.0f,  0.3f, 0.0f},        
												new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}};
            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);

            //For this example we will place the watermark in the upper right
            //hand corner of the photograph. offset down 10 pixels and to the 
            //left 10 pixles

            //int xPosOfWm = ((phWidth - wmWidth) - 10); // Right-Top
            //int xPosOfWm = (((phWidth/2) - (wmWidth/2)) - 10); //Center-Top
            int xPosOfWm = 10; //Left-Top
            int yPosOfWm = 10;

            grWatermark.DrawImage(imgWatermark,
                new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight),  //Set the detination Position
                0,                  // x-coordinate of the portion of the source image to draw. 
                0,                  // y-coordinate of the portion of the source image to draw. 
                wmWidth,            // Watermark Width
                wmHeight,		    // Watermark Height
                GraphicsUnit.Pixel, // Unit of measurment
                imageAttributes);   //ImageAttributes Object

            //Replace the original photgraphs bitmap with the new Bitmap
            imgPhoto = bmWatermark;
            grPhoto.Dispose();
            grWatermark.Dispose();


            // Convert byte[] to Base64 String
            string base64String = ImageToBase64(imgPhoto, ImageType(System.IO.Path.GetExtension(src)));
            imgPhoto.Dispose();
            imgWatermark.Dispose();


            return base64String;

        }
        catch (Exception ex)
        {
            return "";
        }



    }



    public static string WaterMark(string src, string watermark, string Copyright, int Position = 1)
    {
        try
        {
            Image imgPhoto = Image.FromFile(HttpContext.Current.Server.MapPath(src));
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            Image imgWatermark = null;
            if (!Utils.IsNullOrEmpty(watermark))
                imgWatermark = new Bitmap(HttpContext.Current.Server.MapPath(watermark));
            else
                imgWatermark = new Bitmap(1, 1);
            int wmWidth = imgWatermark.Width;
            int wmHeight = imgWatermark.Height;
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;
            grPhoto.DrawImage(
                imgPhoto,                               // Photo Image object
                new Rectangle(0, 0, phWidth, phHeight), // Rectangle structure
                0,                                      // x-coordinate of the portion of the source image to draw. 
                0,                                      // y-coordinate of the portion of the source image to draw. 
                phWidth,                                // Width of the portion of the source image to draw. 
                phHeight,                               // Height of the portion of the source image to draw. 
                GraphicsUnit.Pixel);                    // Units of measure 

            int[] sizes = new int[] { 30, 28, 26, 24, 22, 20, 18 };

            Font crFont = null;
            SizeF crSize = new SizeF();

            for (int i = 0; i < 7; i++)
            {
                crFont = new Font("Tahoma", sizes[i], FontStyle.Bold);
                crSize = grPhoto.MeasureString(Copyright, crFont);

                if ((ushort)crSize.Width < (ushort)phWidth)
                    break;
            }

            int yPixlesFromBottom = (int)(phHeight * .05);
            float yPosFromBottom = ((phHeight - yPixlesFromBottom) - (crSize.Height / 2));
            float xCenterOfImg = (phWidth / 2);
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));

            grPhoto.DrawString(Copyright,                 //string of text
                crFont,                                   //font
                semiTransBrush2,                           //Brush
                new PointF(xCenterOfImg + 1, yPosFromBottom + 1),  //Position
                StrFormat);

            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

            grPhoto.DrawString(Copyright,                 //string of text
                crFont,                                   //font
                semiTransBrush,                           //Brush
                new PointF(xCenterOfImg, yPosFromBottom),  //Position
                StrFormat);                               //Text alignment

            Bitmap bmWatermark = new Bitmap(bmPhoto);
            bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
            Graphics grWatermark = Graphics.FromImage(bmWatermark);
            ImageAttributes imageAttributes = new ImageAttributes();

            ColorMap colorMap = new ColorMap();

            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

            ColorMap[] remapTable = { colorMap };
            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            //float[][] colorMatrixElements = { 
            //                                    new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},       
            //                                    new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},        
            //                                    new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},        
            //                                    new float[] {0.0f,  0.0f,  0.0f,  0.3f, 0.0f},        
            //                                    new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}};
            //ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);
            //imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default,
            //    ColorAdjustType.Bitmap);

            int xPosOfWm = 10, yPosOfWm = 10;

            if (Position == 1)//Left-Top
                xPosOfWm = yPosOfWm = 10;
            else if (Position == 2)//Center-Top
            {
                xPosOfWm = (((phWidth / 2) - (wmWidth / 2)) - 10);
                yPosOfWm = 10;
            }
            else if (Position == 3) // Right-Top
            {
                xPosOfWm = ((phWidth - wmWidth) - 10);
                yPosOfWm = 10;
            }

            else if (Position == 4)
            {
                xPosOfWm = 10;
                yPosOfWm = (((phHeight / 2) - (wmHeight / 2)) - 10);
            }
            else if (Position == 5)
            {
                yPosOfWm = (((phHeight / 2) - (wmHeight / 2)) - 10);
                xPosOfWm = (((phWidth / 2) - (wmWidth / 2)) - 10);
            }
            else if (Position == 6)
            {
                yPosOfWm = (((phHeight / 2) - (wmHeight / 2)) - 10);
                xPosOfWm = ((phWidth - wmWidth) - 10);
            }
            else if (Position == 7)
            {
                yPosOfWm = (phHeight - wmHeight) - 10;
                xPosOfWm = 10;
            }
            else if (Position == 8)
            {
                yPosOfWm = (phHeight - wmHeight) - 10;
                xPosOfWm = (((phWidth / 2) - (wmWidth / 2)) - 10);
            }
            else if (Position == 9)
            {
                yPosOfWm = (phHeight - wmHeight) - 10;
                xPosOfWm = ((phWidth - wmWidth) - 10);
            }

            grWatermark.DrawImage(imgWatermark,
                new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight),  //Set the detination Position
                0,                  // x-coordinate of the portion of the source image to draw. 
                0,                  // y-coordinate of the portion of the source image to draw. 
                wmWidth,            // Watermark Width
                wmHeight,		    // Watermark Height
                GraphicsUnit.Pixel, // Unit of measurment
                imageAttributes);   //ImageAttributes Object

            imgPhoto = bmWatermark;
            grPhoto.Dispose();
            grWatermark.Dispose();


            string base64String = ImageToBase64(imgPhoto, ImageType(System.IO.Path.GetExtension(src)));
            imgPhoto.Dispose();
            imgWatermark.Dispose();
            bmWatermark.Dispose();
            return base64String;

        }
        catch (Exception ex)
        {
            return "";
        }



    }



    public static string Watermark_Generator(string Url)
    {
        return Url;// WaterMark(Url, "/Templates/default/images/watermark.png", "", 5);
    }

    //public static void Watermark_Generator_Thumb(int newWidth, int newHeight, string src, string path)
    //{
    //    int newWidth = 500;
    //    int newHeight = 500;

    //    System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(src));
    //    string filename = Path.GetFileName(src);
    //    System.Drawing.Image thumbnailImage = image.GetThumbnailImage(newWidth, newHeight, null, IntPtr.Zero);

    //    //Save Compress 
    //    ImageCodecInfo[] Info = ImageCodecInfo.GetImageEncoders();
    //    EncoderParameters Params = new EncoderParameters(1);
    //    Params.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, 100L);
    //    thumbnailImage.Save(HttpContext.Current.Server.MapPath(path), Info[1], Params);
    //    image.Dispose();
    //    thumbnailImage.Dispose();
    //}

}