<%@ WebHandler Language="C#" Class="DB" %>

using System;
using System.Web;

public class DB : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {

        string strcs = "";
        try
        {
            System.IO.StreamReader reader = new System.IO.StreamReader(context.Request.InputStream);
            strcs = HttpUtility.UrlDecode(reader.ReadToEnd());
        }
        catch (Exception ex)
        {
        }
        string strDBID = "";
        int iType = 0;
        string[] strs = strcs.Split(':');
        if (strs.Length>=1)
        {
            strDBID = strs[0];
        }
        if (strs.Length >= 2)
        {
            iType = Convert.ToInt32(strs[1]);
        }
        context.Response.Write(clsDB.GetBDData(strDBID, iType));
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}