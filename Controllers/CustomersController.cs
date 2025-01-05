using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProject.Models;

namespace MiniProject.Controllers
{
    public class CustomersController : Controller
    {


        //------ Login Methods -----------------------------------------
        // GET: Customers/Login
        public ActionResult login()
        {
            //Customers? validCust;
            if (Request.Cookies.ContainsKey("remUser"))
            {
                String? jsonValidCust = Request.Cookies["remUser"];
                HttpContext.Session.SetString("validCust", jsonValidCust);
                //validCust = JsonSerializer.Deserialize<Customers>(jsonValidCust);
                return RedirectToAction("Home");
            }
            TempData["Msg"] = null;
            return View();

        }

        //POST : Customers/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Customers objCustUP)
        {
            try
            {
                Customers validCust = Customers.LoginCustomer(objCustUP);
                if (validCust != null)
                {
                    TempData["Msg"] = "Login Success";
                    String jsonValidCust = JsonSerializer.Serialize(validCust);
                    HttpContext.Session.SetString("validCust", jsonValidCust);
                    if (objCustUP.isRemember == true)
                    {
                        CookieOptions options = new CookieOptions
                        {
                            Expires = DateTime.Now.AddMinutes(2), // Cookie expires in 2 Minutes
                            HttpOnly = true                    // Protect cookie from client-side scripts
                        };
                        Response.Cookies.Append("remUser", jsonValidCust, options);
                    }
                    
                }
                else
                {
                    throw new Exception("Login Failed, Invalid Credentials");
                }
                return RedirectToAction("Home");
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
                return View();
            }
            
        }

        //------- Home Methods -------------------------------------------
        public ActionResult Home()
        {
            Customers? validCust = null;
            String? jsonValidCust = HttpContext.Session.GetString("validCust");
            if (jsonValidCust != null)
            {
                validCust = JsonSerializer.Deserialize<Customers>(jsonValidCust);
            }
            return View(validCust);
        }

        //------ Update Profile Method ------------------------------------
        public ActionResult UpdateProfile(int CustId)
        {
            try
            {
                Customers validCust = Customers.GetCustomerById(CustId);
                if (validCust == null)
                {
                    Console.WriteLine("Inside If of ValidCust");
                    throw new Exception("Customer Not Found");
                }
                return View(validCust);

            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
                return RedirectToAction("Home");

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateProfile(Customers objCust)
        {
            try
            {
                int n = Customers.UpdateCustomers(objCust);
                if (n == 1)
                {
                    TempData["Msg"] = " Profile Updated Succefully";
                }
                else
                {
                    throw new Exception("Profile Updation Failed");
                }
                return RedirectToAction("Logout");
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
                return View();
            }
           
        }
        

        //--------- Register Methods ------------------------------------
        //GET: Customers/Register
        public ActionResult Register()
        {
            return View();
        }
        //POST : Customers/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Customers objCust)
        {
            try
            {
                int n = Customers.RegisterCustomer(objCust);
                if (n == 1)
                {
                    TempData["Msg"] = " User Registerd Succefully";
                }
                else
                {
                    throw new Exception("Registration Failed");
                }
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
                return View();
            }
        }


        //----------- Logout Methods ----------------------
        public ActionResult Logout()
        {
            String? jsonValidCust = HttpContext.Session.GetString("validCust");
            if (jsonValidCust != null)
            {
                HttpContext.Session.Remove(jsonValidCust);
            }
            return RedirectToAction("Login");
        }



        //----------- Delete Cookies -------------------
        public ActionResult DeleteCookies()
        {
            if (Request.Cookies.ContainsKey("remUser"))
            {
                Response.Cookies.Delete("remUser");
                TempData["cookieStat"] = "Cookie Deleted";
            }
            else
            {
                TempData["cookieStat"] = "No Cookie Existed";
            }
            return RedirectToAction("Home");
        }
    }
}
