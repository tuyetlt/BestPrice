﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_UCHomeProduct : System.Web.UI.UserControl
{
    public string Title { get; set; }
    public string ReadMore { get; set; }
    public string Filter { get; set; }
    public int CategoryID { get; set; }
    public bool IsHome { get; set; }
    public bool HightLight { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {

    }
}