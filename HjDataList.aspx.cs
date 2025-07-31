using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using MySql.Data.MySqlClient;   //引用MySql

public partial class _Default : System.Web.UI.Page 
{
    /// <summary>
    /// 初始化数据
    /// </summary>
    private void IniData()
    {
        if (!Page.IsPostBack)
        {
            if (txtCJRQ1.Text.Trim() == "")
            {
                txtCJRQ1.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
            }
            if (txtCJRQ2.Text.Trim() == "")
            {
                txtCJRQ2.Text = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
            }
        }
    }
    /// <summary>
    /// 窗体导入
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        IniData();
    }
    /// <summary>
    /// 是否能够让用户查询
    /// </summary>
    /// <param name="strErrMsg"></param>
    /// <returns></returns>
    private bool CanSearch(out string strErrMsg)
    {
        strErrMsg = "";
        bool bReturn = true;//默认是没有问题的，可以查询
        if (txtCJRQ1.Text.Trim()==""
            || txtCJRQ2.Text.Trim() == "")
        {
            strErrMsg = "起始或结束创建日期不能为空！";
            bReturn = false;
        }
        else if (!clsBase.IsStrRq(txtCJRQ1.Text.Trim())
            || !clsBase.IsStrRq(txtCJRQ2.Text.Trim())
            )
        {
            strErrMsg = "起始或结束创建日期格式有误！";
            bReturn = false;
        }
        else if (Convert.ToDateTime(txtCJRQ1.Text.Trim()) > Convert.ToDateTime(txtCJRQ2.Text.Trim()))
        {
            strErrMsg = "起始创建日期不能大于结束创建日期！";
            bReturn = false;
        }
        return bReturn;
    }
    /// <summary>
    /// 根据页面条件，得到查询结果
    /// </summary>
    /// <returns></returns>
    private DataSet GetDataByTJ()
    {
        DataSet dsReturn = null;
        try
        {
            DataSet dsJP = null;
            DataSet dsBZ = null;

            //首先看看有没有胶水桶号
            string strSqlJSTH = "";
            if (txtJSTH1.Text.Trim() != "" //两个胶水桶号都不为空
                && txtJSTH2.Text.Trim() != ""
                )
            {
                strSqlJSTH = " ( tg_zjpjh_docno between '" + txtJSTH1.Text.Trim() + "' AND '" + txtJSTH2.Text.Trim()+"' ) ";
            }
            else if (txtJSTH1.Text.Trim() != "" //第一个胶水桶号不为空
                && txtJSTH2.Text.Trim() == ""//第二个胶水桶号为空
                )
            {
                strSqlJSTH = " tg_zjpjh_docno = '" + txtJSTH1.Text.Trim() + "' ";
            }
            else if (txtJSTH1.Text.Trim() == "" //第一个胶水桶号为空
                && txtJSTH2.Text.Trim() != ""//第二个胶水桶号不为空
               )
            {
                strSqlJSTH = " tg_zjpjh_docno = '" + txtJSTH2.Text.Trim() + "' ";
            }

            //然后构造创建日期
            string strSqlCJRQ = " ( tg_create_time >= '"+clsBase.strBeginDateTime(Convert.ToDateTime(txtCJRQ1.Text.Trim()))
                + "' and tg_create_time <= '" + clsBase.strEndDateTime(Convert.ToDateTime(txtCJRQ2.Text.Trim()))+"' ) ";
            

            //首先根据条件查询出所有数据            
            string strWhereTJ = "";//where后面的条件
            if (strSqlJSTH.Trim() != "")
            {
                if (strWhereTJ.Trim() == "")
                {
                    strWhereTJ = strSqlJSTH.Trim();
                }
                else
                {
                    strWhereTJ += " and " + strSqlJSTH.Trim();
                }
            }
            if (strSqlCJRQ.Trim() != "")
            {
                if (strWhereTJ.Trim() == "")
                {
                    strWhereTJ = strSqlCJRQ.Trim();
                }
                else
                {
                    strWhereTJ += " and " + strSqlCJRQ.Trim();
                }
            }
            //首先查询出拣配数据
            //string strsql = @" SELECT * FROM `hunjpro`.`t_zjpmx` ";
            string strsql = @" SELECT * FROM `t_zjpmx` ";
            if (strWhereTJ.Trim()!="")
            {
                strsql += " where " + strWhereTJ;
            }                    
            dsJP = clsMySqlHelper.Query(strsql,"JP");

            //然后根据得到的拣配数据，提炼出胶水桶号，查询出步骤数据
            if (dsJP!=null
                &&dsJP.Tables.Count > 0 
                &&dsJP.Tables[0].Rows.Count>0//证明是有拣配数据查询出来的
                )
            {
                List<string> lstjsth = new List<string>();
                for (int i = 0; i < dsJP.Tables[0].Rows.Count; i++)
                {
                    bool IsIn = false;//是否已经存在于列表里面
                    for (int j = 0; j < lstjsth.Count; j++)
                    {
                        if (lstjsth[j].Trim() == Convert.ToString(dsJP.Tables[0].Rows[i]["tg_zjpjh_docno"]).Trim())
                        {
                            IsIn = true;
                            break;
                        }
                    }
                    if (!IsIn)//如果不在，那么插入
                    {
                        lstjsth.Add(Convert.ToString(dsJP.Tables[0].Rows[i]["tg_zjpjh_docno"]).Trim());
                    }
                }
                //至此，拣配得到的胶水桶号就保存在lstjsth。
                //然后构造查询步骤的sql语句
                //首先构造步骤的where条件
                string strWhereTJZB = "";
                for (int i = 0; i < lstjsth.Count; i++)
                {
                    string strsqlzbwhere = " tg_docno = '" + lstjsth[i].Trim() + "'";
                    if (strWhereTJZB.Trim() == "")
                    {
                        strWhereTJZB += strsqlzbwhere;
                    }
                    else
                    {
                        strWhereTJZB += " or " + strsqlzbwhere;
                    }
                }
                //最后构造完整的查询语句
                //string strsqlzb = @" SELECT * FROM `hunjpro`.`t_zjplc_records` ";
                string strsqlzb = @" SELECT * FROM `t_zjplc_records` ";
                if (strWhereTJZB.Trim() != "")
                {
                    strsqlzb += " where ( " + strWhereTJZB +" )";
                }
                dsBZ = clsMySqlHelper.Query(strsqlzb,"BZ");
            }
            
            //然后把两个表的数据插入到结果里面
            if (dsJP != null)
            {
                if (dsReturn==null)
                {
                    dsReturn = new DataSet();
                }
                dsReturn.Tables.Add(dsJP.Tables["JP"].Copy());
            }
            if (dsBZ != null
                )
            {
                if (dsReturn == null)
                {
                    dsReturn = new DataSet();
                }
                dsReturn.Tables.Add(dsBZ.Tables["BZ"].Copy());
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return dsReturn;
    }

    /// <summary>
    /// 每一桶胶水的所有数据，包括拣配和步骤数据
    /// </summary>
    private class DsJSData
    {
        public string strJSTH = "";
        public DataSet Ds = null;
        public DsJSData(string _strJSTH, DataSet _Ds)
        {
            strJSTH = _strJSTH;
            Ds = _Ds;
        }
    }
    
    /// <summary>
    /// 显示所有的胶水数据
    /// </summary>
    /// <param name="dsAll"></param>
    private void ShowJSTH(DataSet dsAll)
    {
        //把传进来的查询得到的所有胶水桶的数据按照胶水桶号来拆分
        List<DsJSData> lstDsJSData = new List<DsJSData>();
        //首先按照胶水桶号来创建所有的数据
        if (dsAll != null
            && dsAll.Tables.Contains("JP")//包含拣配的表
            && dsAll.Tables["JP"].Rows.Count > 0
            )
        {
            for (int i = 0; i < dsAll.Tables["JP"].Rows.Count; i++)
            {
                bool bIn = false;//是否已经存在于lstDsJSData
                for (int j = 0; j < lstDsJSData.Count; j++)
                {
                    if (lstDsJSData[j].strJSTH.Trim() == Convert.ToString(dsAll.Tables["JP"].Rows[i]["tg_zjpjh_docno"]).Trim())
                    {
                        bIn = true;
                        break;
                    }
                }
                if (!bIn)//如果不在里面，那么新建一条,并且插入到lstDsJSData
                {
                    lstDsJSData.Add(new DsJSData(Convert.ToString(dsAll.Tables["JP"].Rows[i]["tg_zjpjh_docno"]).Trim(), dsAll.Clone()));
                }
            }
        }

        if (dsAll != null
            && dsAll.Tables.Contains("JP")//包含拣配的表
            )
        {
            for (int i = 0; i < dsAll.Tables["JP"].Rows.Count; i++)
            {
                string strjsth = Convert.ToString(dsAll.Tables["JP"].Rows[i]["tg_zjpjh_docno"]).Trim();
                //然后看看到底要插入到那个dataset里面
                DataSet dsThis = null;
                for (int j = 0; j < lstDsJSData.Count; j++)
                {
                    if (lstDsJSData[j].strJSTH.Trim() == strjsth)
                    {
                        dsThis = lstDsJSData[j].Ds;
                        break;
                    }
                }
                if (dsThis!=null)
                {
                    dsThis.Tables["JP"].ImportRow(dsAll.Tables["JP"].Rows[i]);
                }
            }
        }

        if (dsAll != null
            && dsAll.Tables.Contains("BZ")//包含步骤的表
            )
        {
            for (int i = 0; i < dsAll.Tables["BZ"].Rows.Count; i++)
            {
                string strjsth = Convert.ToString(dsAll.Tables["BZ"].Rows[i]["tg_docno"]).Trim();
                //然后看看到底要插入到那个dataset里面
                DataSet dsThis = null;
                for (int j = 0; j < lstDsJSData.Count; j++)
                {
                    if (lstDsJSData[j].strJSTH.Trim() == strjsth)
                    {
                        dsThis = lstDsJSData[j].Ds;
                        break;
                    }
                }
                if (dsThis != null)
                {
                    dsThis.Tables["BZ"].ImportRow(dsAll.Tables["BZ"].Rows[i]);
                }
            }
        }

        //至此，按照胶水桶号存放的数据已经构造好，然后循环显示
        for (int i = 0; i < lstDsJSData.Count; i++)
        {
            ctrShowJSTH ctr = (ctrShowJSTH)LoadControl("ctrShowJSTH.ascx");
            ctr.SetData(lstDsJSData[i].Ds);
            PlaceHolder1.Controls.Add(ctr);
        }
    }

    /// <summary>
    /// 提交按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButRun_Click(object sender, EventArgs e)
    {
        try
        {
            //首先判断查询条件是否有问题
            string strErrMsg = "";
            if (!CanSearch(out strErrMsg))
            {
                Response.Write("<script>alert('" + strErrMsg + "');</script>");
                return;
            }

            DataSet dsResult = GetDataByTJ();

            //最后就是绑定到控件显示
            ShowJSTH(dsResult);
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message.Replace(@"'", @"\'") + "');</script>");
        }
    }

   
}
