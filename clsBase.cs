using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// clsBase 的摘要说明
/// </summary>
public class clsBase
{
	public clsBase()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// 判断字段的内容是否是日期格式的
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static bool IsStrRq(string str)
    {
        bool bReturn = false;
        try
        {
            DateTime dt = Convert.ToDateTime(str);
            bReturn = true;
        }
        catch (Exception ex)
        {
        }
        return bReturn;
    }

    /// <summary>
    /// 根据一个时间，得到这个时间所在日期当天的开始时间，比如“2019-9-5 4:25:30”得到“2019-9-5 0:00:00”
    /// </summary>
    /// <param name="strDate"></param>
    /// <returns></returns>
    public static string strBeginDateTime(DateTime dt)
    {
        return dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + " 0:00:00" ;
    }

    /// <summary>
    /// 根据一个时间，得到这个时间所在日期当天的结束时间，比如“2019-9-5 4:25:30”得到“2019-9-5 23:59:59”
    /// </summary>
    /// <param name="strDate"></param>
    /// <returns></returns>
    public static string strEndDateTime(DateTime dt)
    {
        return dt.Year.ToString() + "-" + dt.Month.ToString() + "-" + dt.Day.ToString() + " 23:59:59";
    }
}
