using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

/// <summary>
/// clsTestMethord 的摘要说明
/// </summary>
public class clsTestMethord
{
	public clsTestMethord()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 临时文件
    /// </summary>
    private static string strLSWJ = AppDomain.CurrentDomain.BaseDirectory + @"/LSWJ.txt";

    /// <summary>
    /// 得到本地文件的临时写入的重量
    /// </summary>
    /// <returns></returns>
    public static string GetLocalZL()
    {
        string strReturn = "";
        if (File.Exists(strLSWJ))
        {
            StreamReader sr = new StreamReader(strLSWJ);
            strReturn = sr.ReadToEnd();
            sr.Close();
        }
        return strReturn;
    }
}
