<%@ WebHandler Language="C#" Class="Upload" %>

using System;
using System.Web;
using System.IO;
using Ebis.Utilities;
using System.Drawing;

using System.Collections.Generic;

public class Upload : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        if (!HttpContext.Current.User.Identity.IsAuthenticated)
        {
            HttpContext.Current.Response.Redirect("/");
            return;
        }

        context.Response.ContentType = "text/plain";
        string folder = RequestHelper.GetString("folder", "img");
        string pname = RequestHelper.GetString("pname", "image");
        string dirFullPath = HttpContext.Current.Server.MapPath("~/upload/" + folder + "/");


        if (!Directory.Exists(dirFullPath))
            Directory.CreateDirectory(dirFullPath);
        string str_image = "";

        foreach (string s in context.Request.Files)
        {
            HttpPostedFile file = context.Request.Files[s];
            //  int fileSizeInBytes = file.ContentLength;
            string fileName = file.FileName;
            string fileExtension = file.ContentType;

            if (!string.IsNullOrEmpty(fileName))
            {
                fileExtension = Path.GetExtension(fileName);
                str_image = pname + fileExtension;
                string pathToSave_100 = dirFullPath + str_image;
                if (File.Exists(pathToSave_100))
                {
                    int random = Utils.RandomNumber(1000, 9999);
                    pathToSave_100 = dirFullPath + pname + "_" + random.ToString() + fileExtension;
                    str_image = pname + "_" + random.ToString() + fileExtension;
                }

                file.SaveAs(pathToSave_100);
            }

            context.Response.Write(str_image);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}