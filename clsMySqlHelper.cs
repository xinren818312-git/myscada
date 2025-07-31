using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

/// <summary>
/// clsMySqlHelper 的摘要说明
/// </summary>
public class clsMySqlHelper
{
	public clsMySqlHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    //数据库连接字符串(web.config来配置)，可以动态更改connectionString支持多数据库.    
    //public static string connectionString = @"Database=hunjpro;Server=rm-bp143iuikt3luh2275o.mysql.rds.aliyuncs.com;Port=3306;Uid=root;Pwd=Syf123456";
    public static string connectionString = @"Database=hunj_pro;Server=192.168.0.100;Port=3306;Uid=root;Pwd=tianq666";

    /// <summary>
    /// 执行查询语句，返回DataSet
    /// </summary>
    /// <param name="SQLString">查询语句</param>
    /// <returns>DataSet</returns>
    public static DataSet Query(string SQLString,string strTableName)
    {
        using (MySqlConnection connection = new MySqlConnection(connectionString))
        {
            DataSet ds = new DataSet();
            try
            {
                connection.Open();
                MySqlDataAdapter command = new MySqlDataAdapter(SQLString, connection);
                command.Fill(ds, strTableName);
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return ds;
        }
    }
}
