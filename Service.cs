using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Service : System.Web.Services.WebService
{
    public Service () {

        //如果使用设计的组件，请取消注释以下行 
        //InitializeComponent(); 
    }

    /// <summary>
    /// 获取地磅数据
    /// </summary>
    /// <param name="strDBID"></param>
    /// <param name="iType"></param>
    /// <returns></returns>
    [WebMethod]
    public string GetBDData(string strDBID,int iType) {
        return clsDB.GetBDData(strDBID, iType);
    }
    
}
