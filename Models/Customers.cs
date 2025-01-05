using System.Collections;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace MiniProject.Models
{
    public class Customers
    {
        [Display(Name = "Customer ID")]
        public int CustId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string Gender { get; set; }
        public int CId { get; set; }
        public bool isRemember { get; set; }

        public static List<SelectListItem> getAllCities()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=MiniProjectDB;Integrated Security=True;";
            List<Cities> allCities = new List<Cities>();
            List<SelectListItem> cityOptionsList = new List<SelectListItem>();
            try
            {
                conn.Open();
                Console.WriteLine("Connection Succesfull");
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select * from Cities";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cities? objCity = new Cities()
                    {
                        CId = Convert.ToInt32(reader["Cid"]),
                        CName = reader["Cname"].ToString()
                    };
                    allCities.Add(objCity);
                }

                foreach (Cities city in allCities)
                {
                    //string value = city.CId.ToString();
                    //string? text = city.CName;
                    //SelectListItem selectListItem = new SelectListItem(text, value);
                    SelectListItem selectListItem = new SelectListItem(city.CName, city.CId.ToString());
                    cityOptionsList.Add(selectListItem);
                }
                return cityOptionsList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                cityOptionsList = null;
                return cityOptionsList;
            }
            finally
            {
                conn.Close();
                Console.WriteLine("Connection Closed");
            }
        }

        public static int RegisterCustomer(Customers objCust)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=MiniProjectDB;Integrated Security=True;";
            try
            {
                conn.Open();
                Console.WriteLine("Connection Succesfull");
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "RegisterCustomer";

                cmd.Parameters.AddWithValue("@CustId", objCust.CustId);
                cmd.Parameters.AddWithValue("@FullName", objCust.FullName);
                cmd.Parameters.AddWithValue("@UserName", objCust.UserName);
                cmd.Parameters.AddWithValue("@Password", objCust.Password);
                cmd.Parameters.AddWithValue("@EmailId", objCust.EmailId);
                cmd.Parameters.AddWithValue("@PhoneNo", objCust.PhoneNo);
                cmd.Parameters.AddWithValue("@Gender", objCust.Gender);
                cmd.Parameters.AddWithValue("@CId", objCust.CId);

                int n = cmd.ExecuteNonQuery();
                Console.WriteLine($"Register Query Executed Succefully, {n} Rows Affected");
                return n;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            finally
            {
                conn.Close();
                Console.WriteLine("Connection Closed");
            }
        }

        public static Customers LoginCustomer(Customers objCustUP)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=MiniProjectDB;Integrated Security=True;";

            Customers? objCust = null;

            try
            {
                conn.Open();
                Console.WriteLine("Connection Succesfull");
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "LoginCustomer";

                cmd.Parameters.AddWithValue("@UserName", objCustUP.UserName);
                cmd.Parameters.AddWithValue("@Password", objCustUP.Password);

                SqlDataReader reader = cmd.ExecuteReader();
                
                if (reader.Read())
                {
                    objCust = new Customers()
                    {
                        CId = Convert.ToInt32(reader["CId"]),
                        CustId = Convert.ToInt32(reader["CId"]),
                        EmailId = reader["EmailId"].ToString(),
                        FullName = reader["FullName"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        Password = reader["Password"].ToString(),
                        PhoneNo = reader["PhoneNo"].ToString(),
                        UserName = reader["UserName"].ToString(),
                    };
                    
                }
                Console.WriteLine($"Login Query Executed Succefully");
                return objCust;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return objCust;
            }
            finally
            {
                conn.Close();
                Console.WriteLine("Connection Closed");
            }
        }

        public static Customers GetCustomerById(int CustId)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=MiniProjectDB;Integrated Security=True;";
            Customers? objCust = null;
            try
            {
                conn.Open();
                Console.WriteLine("Connection Succesfull");
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select * from Customers where CustId = @CustId";
                cmd.Parameters.AddWithValue("@CustId", CustId);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    objCust = new Customers()
                    {
                        CustId = Convert.ToInt32(reader["CustId"]),
                        FullName = reader["FullName"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        Password = reader["Password"].ToString(),
                        EmailId = reader["EmailId"].ToString(),
                        PhoneNo = reader["PhoneNo"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        CId = Convert.ToInt32(reader["Cid"])
                    };
                    return objCust;
                }
                else
                {
                    throw new Exception("Invalid Customer Number ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return objCust;
            }
            finally
            {
                conn.Close();
                Console.WriteLine("Connection Closed");
            }
        }

        public static List<Customers> GetAllCustomer()
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=MiniProjectDB;Integrated Security=True;";
            List<Customers>? allCustomers = new List<Customers>();
            try
            {
                conn.Open();
                Console.WriteLine("Connection Succesfull");
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "select * from Customers";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Customers? objCust = new Customers()
                    {
                        CustId = Convert.ToInt32(reader["CustId"]),
                        FullName = reader["FullName"].ToString(),
                        UserName = reader["UserName"].ToString(),
                        Password = reader["Password"].ToString(),
                        EmailId = reader["EmailId"].ToString(),
                        PhoneNo = reader["PhoneNo"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        CId = Convert.ToInt32(reader["Cid"])
                    };
                    allCustomers.Add(objCust);
                }
                return allCustomers;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return allCustomers;
            }
            finally
            {
                conn.Close();
                Console.WriteLine("Connection Closed");
            }
        }

        public static int UpdateCustomers(Customers objCust)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=MiniProjectDB;Integrated Security=True;";
            try
            {
                conn.Open();
                Console.WriteLine("Connection Succesfull");
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "Update Customers " +
                                    "set " +
                                    "FullName = @FullName, " +
                                    "UserName = @UserName, " +
                                    "Password = @Password,  " +
                                    "PhoneNo = @PhoneNo,  " +
                                    "Gender = @Gender,  " +
                                    "Cid = @CId  " +
                                    "where CustId = @CustId";

                cmd.Parameters.AddWithValue("@FullName", objCust.FullName);
                cmd.Parameters.AddWithValue("@UserName", objCust.UserName);
                cmd.Parameters.AddWithValue("@Password", objCust.Password);
                cmd.Parameters.AddWithValue("@PhoneNo", objCust.PhoneNo); 
                cmd.Parameters.AddWithValue("@Gender", objCust.Gender);
                cmd.Parameters.AddWithValue("@CId", objCust.CId);
                cmd.Parameters.AddWithValue("@CustId", objCust.CustId);

                int n = cmd.ExecuteNonQuery();
                Console.WriteLine($"Register Query Executed Succefully, {n} Rows Affected");
                return n;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
            finally
            {
                conn.Close();
                Console.WriteLine("Connection Closed");
            }
        }

    }
}
