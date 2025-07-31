using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class ctrShowJSTH : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 设计数据
    /// 其中ds是只包含本胶水的数据
    /// 表0：抬头数据；
    /// 表1：明细数据；
    /// 表2：上胶数据；
    /// 表3：修改数据；
    /// </summary>
    public void SetData(DataSet ds)
    {
        if (ds != null
            && ds.Tables.Contains("JP") 
            && ds.Tables["JP"].Rows.Count > 0
            )
        {
            LabJSTH.Text = Convert.ToString(ds.Tables[0].Rows[0]["tg_zjpjh_docno"]).Trim();//胶水桶号
            LabFYF.Text = Convert.ToString(ds.Tables[0].Rows[0]["t_boiler_name"]).Trim();//反应釜
            LabCJSJ.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["tg_create_time"]).ToString();//创建时间

            ctrShowJSLX ctrJSLX = (ctrShowJSLX)LoadControl("ctrShowJSLX.ascx");
            ctrJSLX.SetData(ds);
            PlaceHolder1.Controls.Add(ctrJSLX);

            ctrShowJSBZ ctrJSBZ = (ctrShowJSBZ)LoadControl("ctrShowJSBZ.ascx");
            ctrJSBZ.SetData(ds);
            PlaceHolder2.Controls.Add(ctrJSBZ);
        }
    }
}
