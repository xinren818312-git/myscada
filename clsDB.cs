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
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

/// <summary>
/// clsDB 的摘要说明
/// </summary>
public class clsDB
{
	public clsDB()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    /// <summary>
    /// ping屏幕
    /// </summary>
    private static System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
    /// <summary>
    /// 接收ping返回的东西
    /// </summary>
    private static System.Net.NetworkInformation.PingReply reply = null;

    /// <summary>
    /// 本地配置文件的地址
    /// </summary>
    private static string strPZAddr = AppDomain.CurrentDomain.BaseDirectory + @"/PZ.XML";

    /// <summary>
    /// 本地xml文件的格式
    /// </summary>
    /// <returns></returns>
    private static DataSet dtScam()
    {
        DataSet dsReturn = new DataSet();
        DataTable dt = new DataTable();
        dt.Columns.Add("DBID");
        dt.Columns.Add("IP");
        dt.Columns.Add("PORT");
        dsReturn.Tables.Add(dt);

        DataRow dr = dt.NewRow();
        dr["DBID"] = "DB01";
        dr["IP"] = "192.168.0.254";
        dr["PORT"] = "8000";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["DBID"] = "DB02";
        dr["IP"] = "192.168.0.254";
        dr["PORT"] = "8001";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["DBID"] = "DB03";
        dr["IP"] = "192.168.0.254";
        dr["PORT"] = "8002";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["DBID"] = "DB04";
        dr["IP"] = "192.168.0.254";
        dr["PORT"] = "8003";
        dt.Rows.Add(dr);

        return dsReturn;
    }

    /// <summary>
    /// 根据地磅的编号，得到对应的串口服务器的ip和端口
    /// </summary>
    /// <param name="strDBID"></param>
    private static bool strGetIPAndPortByDBID(string strDBID,out string strIP,out int iPort)
    {
        strIP = "";
        iPort = 0;
        bool bReturn = false;

        if (!File.Exists(strPZAddr))//如果不存在这个文件，那么就新建
        {
            DataSet dss = dtScam();
            dss.WriteXml(strPZAddr,XmlWriteMode.WriteSchema);
        }
        //至此，本地肯定就存在配置文件
        DataSet dsPZ = new DataSet();
        dsPZ.ReadXml(strPZAddr,XmlReadMode.ReadSchema);
        if (dsPZ!=null
            &&dsPZ.Tables.Count>0
            &&dsPZ.Tables[0].Rows.Count>0
            )
        {
            for (int i = 0; i < dsPZ.Tables[0].Rows.Count; i++)
            {
                if (Convert.ToString(dsPZ.Tables[0].Rows[i]["DBID"]).Trim() == strDBID.Trim())
                {
                    strIP = Convert.ToString(dsPZ.Tables[0].Rows[i]["IP"]).Trim();
                    iPort = Convert.ToInt32(dsPZ.Tables[0].Rows[i]["PORT"]);
                    bReturn = true;
                    break;
                }
            }
        }
        return bReturn;
    }

    /// <summary>
    /// 把16进制的数转化成字节
    /// </summary>
    /// <returns></returns>
    private static byte Changebyte(string strH)
    {
        return Convert.ToByte(Convert.ToInt32(strH, 16));
    }

    /// <summary>
    /// 根据要读取地磅数据的类型，得到要发送给地磅的命令
    /// </summary>
    /// <param name="iType">0毛重；1净重；2皮重</param>
    /// <returns></returns>
    private static byte[] GetSendMsgByiType(int iType)
    {
        List<string> lst = new List<string>();
        if (iType == 0)//毛重
        {
            lst.Add("02");
            lst.Add("41");//默认的是第一台，也就是编号为A的那台，所以是41
            lst.Add("42");
            lst.Add("30");
            lst.Add("33");
            lst.Add("03");
        }
        else if (iType == 1)//净重
        {
            lst.Add("02");
            lst.Add("41");
            lst.Add("43");
            lst.Add("30");
            lst.Add("32");
            lst.Add("03");
        }
        else if (iType == 2)//皮重
        {
            lst.Add("02");
            lst.Add("41");
            lst.Add("44");
            lst.Add("30");
            lst.Add("35");
            lst.Add("03");
        }

        List<byte> sendBytes = new List<byte>();
        for (int i = 0; i < lst.Count; i++)
        {
            sendBytes.Add(Changebyte(lst[i].Trim()));
        }

        byte[] SendBs = new byte[sendBytes.Count];
        for (int i = 0; i < SendBs.Length; i++)
        {
            SendBs[i] = sendBytes[i];
        }

        return SendBs;
    }

    /// <summary>
    /// 根据地磅返回的重量，得到最终的重量
    /// </summary>
    /// <param name="strtempzl"></param>
    /// <param name="strZL"></param>
    /// <param name="strZLSSWR"></param>
    private static void GetZLAndZLSSWR(string strtempzl, out string strZL, out string strZLSSWR)
    {
        strZL = "";
        strZLSSWR = "";

        bool bIsFS = false;//是否负数
        if (strtempzl.Contains("-"))
        {
            bIsFS = true; 
        }

        strtempzl = strtempzl.Replace("+", "");
        strtempzl = strtempzl.Replace("-", "");

        //至此，理论上重量只剩下数字和小数点
        decimal dzl = Convert.ToDecimal(strtempzl);
        if (bIsFS)
        {
            dzl = dzl * Convert.ToInt32("-1");
        }
        strZL = dzl.ToString();
        strZLSSWR = Convert.ToString(Math.Round(dzl, 1)); ;
    }

    /// <summary>
    /// 根据地磅编号和要求输出的重量类型，得到重量
    /// </summary>
    /// <param name="strDBID"></param>
    /// <param name="iType"></param>
    /// <returns></returns>
    public static string GetBDData(string strDBID, int iType)
    {
        //返回的结果信息是一个字符串，格式为“是否成功读取:地磅重量:地磅四舍五入后的重量:错误信息”
        //其中四舍五入只保留一位小数，比如“TRUE:83.283:83.3:”，又比如“TRUE:83.981:87.0:”，又比如“FALSE:::串口服务器无法ping通”

        string strBresult = "FALSE";//只能够是“TRUE”或者“FALSE”,默认是不成功
        string strZL = "";//地磅重量
        string strZLSSWR = "";//地磅四舍五入后的重量
        string strErrMsg = "";//错误信息

        string strLocalZL = clsTestMethord.GetLocalZL();
        if (strLocalZL.Trim() == "")//不存在这个结果的情况下才去拿设备的数据
        {
            string strIP = "";
            int iPort = 0;
            if (strGetIPAndPortByDBID(strDBID, out strIP, out iPort))
            {
                //至此，要读取的地磅对应的ip和端口已经知道

                //首先看看窗口服务器能不能ping通
                reply = pingSender.Send(strIP.Trim());
                if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
                {
                    //可以ping通串口服务器
                    IPEndPoint ie = new IPEndPoint(IPAddress.Parse(strIP), iPort);//建立连接变量

                    //新建一个访问，判断是否就绪
                    Socket newclient_JX = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    newclient_JX.ReceiveTimeout = 3000;//3秒钟没有反应就认为是不行了，不需要太长的时间
                    try
                    {
                        newclient_JX.Connect(ie);//连接到串口服务器

                        //构造要发送的内容
                        byte[] SendBs_Con = GetSendMsgByiType(iType);
                        int Rec3 = newclient_JX.Send(SendBs_Con, SocketFlags.None);//然后发送给串口服务器
                        if (Rec3 == SendBs_Con.Length)//表示成功发送
                        {
                            //成功发送后，就要接收
                            byte[] btyRec1 = new byte[2000];
                            int Rec4 = newclient_JX.Receive(btyRec1);
                            if (Rec4 > 0)//表示串口服务器有东西返回回来
                            {
                                if (btyRec1.Length >= 1
                                    && Convert.ToInt32(btyRec1[0]) == 2)//成功返回数据
                                {
                                    if (btyRec1.Length >= 11)
                                    {
                                        //至此，返回的数据认为是正确的
                                        //第4到11位数据（一共8个数据），构成的数据就是最终的数据
                                        byte[] btJG = new byte[8];
                                        for (int i = 0; i < btJG.Length; i++)
                                        {
                                            btJG[i] = btyRec1[3 + i];
                                        }
                                        //至此，已经把最终地磅的数据保存在btJG
                                        string strtempzl = System.Text.Encoding.Default.GetString(btJG);
                                        GetZLAndZLSSWR(strtempzl, out strZL, out strZLSSWR);
                                        strBresult = "TRUE";
                                    }
                                    else
                                    {
                                        strErrMsg = "返回的数据无法获取4到11的数据，数据有问题！";
                                    }
                                }
                                else
                                {
                                    strErrMsg = "返回的数据不是02开头，不符合规则！";
                                }
                            }
                            else
                            {
                                strErrMsg = "读取命令已经发送，但是无法获取返回数据！";
                            }
                        }
                        else
                        {
                            strErrMsg = "发送给命令给串口服务器失败！";
                        }
                    }
                    catch (Exception ex)
                    {
                        strErrMsg = ex.Message;
                    }
                    finally
                    {
                        newclient_JX.Shutdown(SocketShutdown.Both);
                        newclient_JX.Close();
                    }
                }
                else
                {
                    strErrMsg = "串口服务器无法ping通";
                }
            }
            else
            {
                strErrMsg = "无法获取到地磅对应的串口服务器的ip和端口";
            }
            if (strErrMsg.Trim() != "")
            {
                strErrMsg += "(地磅编号为" + strDBID + " ip为" + strIP + " 端口为" + iPort.ToString() + ")";
            }
        }
        else
        {
            GetZLAndZLSSWR(strLocalZL.Trim(), out strZL, out strZLSSWR);
            strBresult = "TRUE";
        }
        

        return strBresult + ":" + strZL + ":" + strZLSSWR + ":" + strErrMsg; 
    }
}
