using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestTask.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public string Retrieve()
        {
            List<string> messages = new List<string>();
            using (SqlConnection cnn = new SqlConnection())
            {
                cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = @"select Text from [TestTask].[dbo].[Texts] order by id";
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        messages.Add(Convert.ToString(reader[0]));   
                    }
                }
            }
            return JsonConvert.SerializeObject(messages);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public int InsertText(string text)
        {
            int r = 0;
            using(SqlConnection cnn = new SqlConnection())
            {
                cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
                cnn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = @"insert into [TestTask].[dbo].[Texts] values (N'" + text.Replace("'", "''") + "')";
                    r = cmd.ExecuteNonQuery();
                }
            }
            return r;
        }
    }
}