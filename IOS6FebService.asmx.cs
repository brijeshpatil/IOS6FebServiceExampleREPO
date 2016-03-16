using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for IOS6FebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class IOS6FebService : System.Web.Services.WebService
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string AddNewState(string StateName)
        {
            string Status = "";
            cmd = new SqlCommand("insert into StateTbl values(@SName)", con);
            cmd.Parameters.AddWithValue("@SName", StateName);
            con.Open();
            Status = cmd.ExecuteNonQuery().ToString();
            con.Close();
            if (Status != "")
            {
                return "Record Saved";
            }
            else
            {
                return "Error";
            }
        }

        [WebMethod]
        public DataSet GetAllStates()
        {
            da = new SqlDataAdapter("select * from StateTbl", con);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public string UpdateState(int StateID, string StateName)
        {
            string asd = "";
            cmd = new SqlCommand("update StateTbl set StateName=@SName where StateID=@SID", con);
            cmd.Parameters.AddWithValue("@SName", StateName);
            cmd.Parameters.AddWithValue("@SID", StateID);
            con.Open();
            asd = cmd.ExecuteNonQuery().ToString();
            con.Close();

            if (asd != "")
            {
                return "Record Updated";
            }
            else
            {
                return "Error";
            }
        }

        [WebMethod]
        public string AddNewCity(string CityName, int StateID)
        {
            string asd = "";
            cmd = new SqlCommand("insert into CityTbl values(@CName,@SID)", con);
            cmd.Parameters.AddWithValue("@CName", CityName);
            cmd.Parameters.AddWithValue("@SID", StateID);
            con.Open();
            asd = cmd.ExecuteNonQuery().ToString();
            con.Close();

            if (asd != "")
            {
                return "Record Saved";
            }
            else
            {
                return "Error";
            }
        }

        [WebMethod]
        public DataSet GetCityByState(int StateID)
        {
            da = new SqlDataAdapter("select * from CityTbl where FkStateID=@SID", con);
            da.SelectCommand.Parameters.AddWithValue("@SID", StateID);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        [WebMethod]
        public string RegNewUser(string FirstName,string LastName,int StateID,int CityID,string UserName,string Password)
        {
            cmd = new SqlCommand("insert into UserInfo values(@Fname,@Lname,@SID,CID,UName,@Pass)",con);
            cmd.Parameters.AddWithValue("@Fname", FirstName);
            cmd.Parameters.AddWithValue("@Lname", LastName);
            cmd.Parameters.AddWithValue("@SID", StateID);
            cmd.Parameters.AddWithValue("@CID", CityID);
            cmd.Parameters.AddWithValue("@UName", UserName);
            cmd.Parameters.AddWithValue("@Pass", Password);
            con.Open();
            string asd = "";
            asd = cmd.ExecuteNonQuery().ToString();
            con.Close();
            if(asd!="")
            {
                return "Record Saved";
            }
            else
            {
                return "Error";
            }
        }

        [WebMethod]
        public DataSet CheckUSerLogin(string UserName,string Password)
        {
            da = new SqlDataAdapter("select * from UserInfo where uname=@UName and Pass=@Pass",con);
            da.SelectCommand.Parameters.AddWithValue("@UName", UserName);
            da.SelectCommand.Parameters.AddWithValue("@Pass", Password);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }
    }
}
