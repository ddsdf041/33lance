using DiplomFreelance.Models.FreelanceModels;
using DiplomFreelance.Models.FreelanceModels.DBModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DiplomFreelance.Models
{
    public static class Mapper
    {
        static readonly string connectionString = ConfigurationManager.ConnectionStrings["FreelanceConnection"].ConnectionString;

        //Offer
        static public List<Offer> MapOffers(this SqlDataReader reader)
        {
            List<Offer> query = new List<Offer>() { };

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Offer Offer = new Offer()
                    {
                        ID = (int)reader["ID"],
                        ID_Order = (Guid)reader["ID_Order"],
                        ID_Executor = (string)reader["ID_Executor"]
                    };
                    query.Add(Offer);
                }
            }
            reader.Close();
            return query;
        }
        static public Offer MapOffer(this SqlDataReader reader)
        {
            Offer Offer = new Offer();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Offer.ID = (int)reader["ID"];
                    Offer.ID_Order = (Guid)reader["ID_Order"];
                    Offer.ID_Executor = (string)reader["ID_Executor"];
                }
            }
            reader.Close();
            return Offer;
        }

        //Response
        static public List<Response> MapResponses(this SqlDataReader reader)
        {
            List<Response> query = new List<Response>() { };

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Response response = new Response()
                    {
                        ID = (int)reader["ID"],
                        ID_Order = (Guid)reader["ID_Order"],
                        ID_Executor = (string)reader["ID_Executor"],
                        Price = (decimal)reader["Price"],
                        Notation=(string)reader["Notation"],
                        Date = (DateTime)reader["Date"]
                    };
                    query.Add(response);
                }
            }
            reader.Close();
            return query;
        }
        static public Response MapResponse(this SqlDataReader reader)
        {
            Response response = new Response();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    response.ID = (int)reader["ID"];
                    response.ID_Order = (Guid)reader["ID_Order"];
                    response.ID_Executor = (string)reader["ID_Executor"];
                    response.Price = (decimal)reader["Price"];
                    response.Notation = (string)reader["Notation"];
                    response.Date = (DateTime)reader["Date"];
                }
            }
            reader.Close();
            return response;
        }

        //FileOrder
        static public List<FileOrder> MapFilesOrder(SqlDataReader reader)
        {
            List<FileOrder> query = new List<FileOrder>() { };

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    FileOrder fileOrder = new FileOrder()
                    {
                        ID = (int)reader["ID"],
                        Name = (string)reader["Name"],
                        Image = (string)reader["Image"],
                        ID_Order = (Guid)reader["ID_Order"]
                    };
                    query.Add(fileOrder);
                }
            }
            reader.Close();
            return query;
        }
        static public FileOrder MapFileOrder(SqlDataReader reader)
        {
            FileOrder fileOrder = new FileOrder();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    fileOrder.ID = (int)reader["ID"];
                    fileOrder.Name = (string)reader["Name"];
                    fileOrder.Image = (string)reader["Image"];
                    fileOrder.ID_Order = (Guid)reader["ID_Order"];
                }
            }
            reader.Close();
            return fileOrder;
        }

        //Measure
        static public List<Measure> MapMeasures(SqlDataReader reader)
        {
            List<Measure> query = new List<Measure>() { };

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Measure measure = new Measure()
                    {
                        ID = (int)reader["ID"],
                        Name = (string)reader["Name"],

                    };
                    query.Add(measure);
                }
            }
            reader.Close();
            return query;
        }
        static public Measure MapMeasure(SqlDataReader reader)
        {
            Measure measure = new Measure();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    measure.ID = (int)reader["ID"];
                    measure.Name = (string)reader["Name"];
                }
            }
            reader.Close();
            return measure;
        }
       
        //Place
        static public List<Place> MapPlaces(SqlDataReader reader)
        {
            List<Place> query = new List<Place>() { };

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Place place = new Place()
                    {
                        ID = (int)reader["ID"],
                        Name = (string)reader["Name"],

                    };
                    query.Add(place);
                }
            }
            reader.Close();
            return query;
        }
        static public Place MapPlace(SqlDataReader reader)
        {
            Place place = new Place();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    place.ID = (int)reader["ID"];
                    place.Name = (string)reader["Name"];
                }
            }
            reader.Close();
            return place;
        }

        //status
        static public List<Status> MapStatuses(SqlDataReader reader)
        {
            List<Status> query = new List<Status>() { };

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Status status = new Status()
                    {
                        ID = (int)reader["ID"],
                        Name = (string)reader["Name"],

                    };
                    query.Add(status);
                }
            }
            reader.Close();
            return query;
        }
        static public Status MapStatus(SqlDataReader reader)
        {
            Status status = new Status();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    status.ID = (int)reader["ID"];
                    status.Name = (string)reader["Name"];
                }
            }
            reader.Close();
            return status;
        }
       
        //category
        static public List<Category> MapCategories(SqlDataReader reader)
        {
            List<Category> query = new List<Category>() { };
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Category category = new Category()
                    {
                        ID = (int)reader["ID"],
                        Name = (string)reader["Name"],

                    };
                    query.Add(category);
                }
            }
            reader.Close();
            return query;
        }
        static public Category MapCategory(SqlDataReader reader)
        {
            Category category = new Category();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    category.ID = (int)reader["ID"];
                    category.Name = (string)reader["Name"];
                }
            }
            reader.Close();
            return category;
        }

        //subcategory
        static public List<Subcategory> MapSubcategories(SqlDataReader reader)
        {
            List<Subcategory> query = new List<Subcategory>() { };

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Subcategory subcategory = new Subcategory()
                    {
                        ID = (int)reader["ID"],
                        ID_Category = (int)reader["ID_Category"],
                        Name = (string)reader["Name"]
                    };
                    query.Add(subcategory);

                }
            }
            return query;
        }
        static public Subcategory MapSubcategory(SqlDataReader reader)
        {
            Subcategory subcategory = new Subcategory();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    subcategory.ID = (int)reader["ID"];
                    subcategory.ID_Category = (int)reader["ID_Category"];
                    subcategory.Name = (string)reader["Name"];
                }
            }
            return subcategory;
        }

        //order
        static public List<Order> MapOrders(SqlDataReader reader)
        {
            List<Order> query = new List<Order>() { };
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Order order = new Order()
                    {
                        ID = (Guid)reader["ID"],
                        ID_Subcategory = (int)reader["ID_Subcategory"],
                        ID_Customer = (string)reader["ID_Customer"],
                        ID_Status = (int)reader["ID_Status"],
                        ID_Executor = reader["ID_Executor"] is DBNull ? null : (string)reader["ID_Executor"],
                        Name = (string)reader["Name"],
                        Deadline = (DateTime)reader["Deadline"],
                        Description = (string)reader["Description"],
                        ID_Measure = (int)reader["ID_Measure"],
                        Budget = (decimal)reader["Budget"],
                        ID_Place = (int)reader["ID_Place"],
                        Address = reader["Address"] is DBNull ? "" : (string)reader["Address"],
                        Date = (DateTime)reader["Date"],
                        IsBanned = (bool)reader["IsBanned"]
                    };
                    query.Add(order);
                }
            }
            return query;
        }
        static public Order MapOrder(SqlDataReader reader)
        {
            Order item = new Order();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    item.ID = (Guid)reader["ID"];
                    item.ID_Subcategory = (int)reader["ID_Subcategory"];
                    item.ID_Customer = (string)reader["ID_Customer"];
                    item.ID_Status = (int)reader["ID_Status"];
                    item.ID_Executor = reader["ID_Executor"] is DBNull ? null : (string)reader["ID_Executor"];
                    item.Name = (string)reader["Name"];
                    item.Deadline = (DateTime)reader["Deadline"];
                    item.Description = (string)reader["Description"];
                    item.ID_Measure = (int)reader["ID_Measure"];
                    item.Budget = (decimal)reader["Budget"];
                    item.ID_Place = (int)reader["ID_Place"];
                    item.Address = reader["Address"] is DBNull ? "" : (string)reader["Address"];
                    item.Date = (DateTime)reader["Date"];
                    item.IsBanned = (bool)reader["IsBanned"];
                }
            }
            return item;
        }

        //customer
        static public List<Customer> MapCustomers(SqlDataReader reader)
        {
            List<Customer> query = new List<Customer>() { };

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Customer customer = new Customer()
                    {
                        ID_User = (string)reader["ID_User"],
                        Name = (string)reader["Name"],
                        Email = (string)reader["Email"],
                        IsBanned = (bool)reader["IsBanned"]
                    };
                    query.Add(customer);
                }
            }
            reader.Close();
            return query;
        }
        static public Customer MapCustomer(SqlDataReader reader)
        {
            Customer customer = new Customer();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    customer.ID_User = (string)reader["ID_User"];
                    customer.Name = (string)reader["Name"];
                    customer.Email = (string)reader["Email"];
                    customer.IsBanned = (bool)reader["IsBanned"];
                }
            }
            reader.Close();
            return customer;
        }

        //executor
        static public List<Executor> MapExecutors(SqlDataReader reader)
        {
            List<Executor> query = new List<Executor>() { };

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Executor exec = new Executor()
                    {
                        ID_User = (string)reader["ID_User"],
                        Name = (string)reader["Name"],
                        City = (string)reader["City"],
                        Photo = (string)reader["Photo"],
                        Specialty = (string)reader["Speciality"],
                        Telephone = (string)reader["Telephone"],
                        Email = (string)reader["Email"],
                        Raiting = (decimal)reader["Raiting"],
                        Description = (string)reader["Description"],
                        IsBanned = (bool)reader["IsBanned"]
                    };
                    query.Add(exec);
                }
            }
            return query;
        }
        static public Executor MapExecutor(SqlDataReader reader)
        {
            Executor executor = new Executor();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    executor.ID_User = (string)reader["ID_User"];
                    executor.Name = (string)reader["Name"];
                    executor.City = (string)reader["City"];
                    executor.Photo = (string)reader["Photo"];
                    executor.Specialty = (string)reader["Speciality"];
                    executor.Telephone = (string)reader["Telephone"];
                    executor.Email = (string)reader["Email"];
                    executor.Raiting = (decimal)reader["Raiting"];
                    executor.Description = (string)reader["Description"];
                    executor.IsBanned = (bool)reader["IsBanned"];
                }
            }
            return executor;
        }

        //service
        static public List<Service> MapServices(SqlDataReader reader)
        {
            List<Service> query = new List<Service>() { };

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Service services = new Service()
                    {
                        ID = (int)reader["ID"],
                        Name = (string)reader["Name"],
                        ID_Subcategory = (int)reader["ID_Subcategory"],
                        Time_work = (string)reader["Time_work"],
                        Place = (int)reader["Place"],
                        Expirience = (string)reader["Expirience"],
                        ID_Measure = (int)reader["ID_Measure"],
                        Price = (decimal)reader["Price"],
                        Notation = (string)reader["Notation"],
                        ID_Executor = (string)reader["ID_Executor"],
                        Address = reader["Address"] is DBNull ? "" : (string)reader["Address"]
                    };
                    query.Add(services);
                }
            }
            return query;
        }
        static public Service MapService(SqlDataReader reader)
        {
            Service service = new Service();

            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    service.ID = (int)reader["ID"];
                    service.Name = (string)reader["Name"];
                    service.ID_Subcategory = (int)reader["ID_Subcategory"];
                    service.Time_work = (string)reader["Time_work"];
                    service.Place = (int)reader["Place"];
                    service.Expirience = (string)reader["Expirience"];
                    service.ID_Measure = (int)reader["ID_Measure"];
                    service.Price = (decimal)reader["Price"];
                    service.Notation = (string)reader["Notation"];
                    service.ID_Executor = (string)reader["ID_Executor"];
                    service.Address = reader["Address"] is DBNull ? "" : (string)reader["Address"];

                }
            }
            return service;
        }

        static public Message MapMessage(SqlDataReader reader)
        {
            Message message = new Message();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    message.ID = (int)reader["ID"];
                    message.ID_Chat = (int)reader["ID_Chat"];
                    message.Sender = (string)reader["Sender"];
                    message.Text = (string)reader["Text"];
                    message.Time_send = (DateTime)reader["Time_send"];
                }
            }
            reader.Close();
            return message;
        }

        static public List<Message> MapMessages(SqlDataReader reader)
        {
            List<Message> query = new List<Message>() { };

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Message message = new Message()
                    {
                        ID = (int)reader["ID"],
                        ID_Chat = (int)reader["ID_Chat"],
                        Sender = (string)reader["Sender"],
                        Text = (string)reader["Text"],
                        Time_send = (DateTime)reader["Time_send"],
                       
                    };
                    query.Add(message);
                }
            }
            reader.Close();
            return query;
        }

        static public Chat MapChat(SqlDataReader reader)
        {
            Chat chat = new Chat();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    chat.ID = (int)reader["ID"];
                    chat.ID_User_1 = (string)reader["User_ID_1"];
                    chat.ID_User_2 = (string)reader["User_ID_2"];                
                }
            }
            reader.Close();
            return chat;
        }

        static public List<Chat> MapChats(SqlDataReader reader)
        {
            List<Chat> query = new List<Chat>() { };

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Chat chat = new Chat()
                    {
                        ID = (int)reader["ID"],
                        ID_User_1 = (string)reader["User_ID_1"],
                        ID_User_2 = (string)reader["User_ID_2"],
                     
                    };
                    query.Add(chat);
                }
            }
            reader.Close();
            return query;
        }



    }
}