
using DiplomFreelance.Models.FreelanceModels;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiplomFreelance.Controllers
{
    public class HomeController : Controller
    {      
        static readonly string connectionString = ConfigurationManager.ConnectionStrings["FreelanceConnection"].ConnectionString; /*@"Data Source=DESKTOP-HPCDTVI;Initial Catalog=freelance;Integrated Security=True";*/
        //SqlConnection connection = new SqlConnection(connectionString);


        public ActionResult Catalog(string nameCategory)
        {
            if (!String.IsNullOrEmpty(nameCategory))
            {
                //List<Subcategory> subcategories = new List<Subcategory>();
                //subcategories = (from subcat in _db.Subcategories where subcat.Category.Name == nameCategory select subcat).ToList();
                List<Subcategory> query = new List<Subcategory>() { };
                string sqlExpression = $@"SELECT Subcategory.ID, Subcategory.Name FROM Subcategory 
                                          join Category on (Category.ID = Subcategory.ID_Category)
                                          where Category.Name = '{nameCategory}'";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Subcategory subcategory = new Subcategory()
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                            query.Add(subcategory);
                        }
                    }
                    connection.Close();
                }
                return (ActionResult)PartialView("_SubcategoryPartial", query);
            }
            else
            {
                List<Category> query = new List<Category>() { };
               string sqlExpression = $@"SELECT * FROM Category";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Category category = new Category()
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1)
                            };
                            query.Add(category);

                        }
                    }
                    connection.Close();
                }
                return View(query);
            }
        }

        //if (!String.IsNullOrEmpty(nameCategory))
        //{
        //    List<Subcategory> subcategories = new List<Subcategory>();
        //    subcategories = (from subcat in _db.Subcategories where subcat.Category.Name == nameCategory select subcat).ToList();
        //    return (ActionResult)PartialView("_SubcategoryPartial", subcategories);
        //}
        //else
        //{
        //    var categories = _db.Categories;
        //    return View(categories);
        //}
        //}

        public ActionResult Services(int IdSubcategory)
        {
            List<Executor> executors = new List<Executor>() { };
            List<Service> services = new List<Service>() { };
            ServicesViewModel SVM = new ServicesViewModel();
            string sqlExpression = $@"SELECT 
                                   Executor.ID_User,
                                   Executor.ID,
                                   Executor.Name,
                                   Executor.Photo,
                                   Executor.Speciality,
                                   Executor.Telephone,
                                   Executor.Raiting,
                                   Executor.Description
                                   FROM Executor
                                   join Service on(Service.ID_Executor = Executor.ID)
                                   join Subcategory on(Subcategory.ID = Service.ID_Subcategory)
                                   where Subcategory.ID = {IdSubcategory}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
              
                    while (reader.Read())
                    {

                        Executor exec = new Executor()
                        {
                            ID_User = reader.GetString(0),
                            ID = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            Photo = reader.IsDBNull(3) ? new byte[1] : (byte[])reader.GetValue(3),
                            Specialty = reader.GetString(4),
                            Telephone = reader.GetString(5),
                            Raiting = reader.GetDecimal(6),
                            Description = reader.GetString(7),
                            Services = services
                        };
                        executors.Add(exec);
                    }
                }
             
                reader.Close();


                string sqlExpressionForService = $@"Select * from Service";

                SqlCommand commandForService = new SqlCommand(sqlExpressionForService, connection);
                SqlDataReader readerForService = commandForService.ExecuteReader();
                if (readerForService.HasRows)
                {
                    while (readerForService.Read())
                    {
                        Service serv = new Service()
                        {
                            ID = readerForService.GetInt32(0),
                            Name = readerForService.GetString(1),
                            ID_Subcategory = readerForService.GetInt32(2),
                            Time_work = readerForService.GetString(3),
                            Place = readerForService.GetInt32(4),
                            Expirience = readerForService.GetString(5),
                            ID_Measure = readerForService.GetInt32(6),
                            Price = readerForService.GetDecimal(7),
                            Notation = readerForService.GetString(8),
                            ID_Executor = readerForService.GetInt32(9),
                            Address = readerForService.IsDBNull(10) ? "" : readerForService.GetString(10)
                        };
                        services.Add(serv);
                    }
                }
                readerForService.Close();
                connection.Close();

                foreach (var item in executors)
                {
                    item.Services = services.Where(x => x.ID_Executor == item.ID).ToList();
                }

            }

            return View(executors);
        }

        public ActionResult Order()
        {

            return View();
        }

        public ActionResult Support()
        {

            return View();
        }
        public ActionResult MyOrders()
        {

            return View();
        }
    }
}