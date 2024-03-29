﻿using SessionManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Security;

namespace SessionManagement.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["username"] != null)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Login()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            FormsAuthentication.SetAuthCookie(user.Username, false);
            Session["username"] = user.Username.ToString();
            Session["tokenkey"] = "X12345678";
            return RedirectToAction("Index", "Dashboard");

            //IEnumerable<Users> users = null;

            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:52924/api/");
            //    //HTTP GET
            //    var responseTask = client.GetAsync("Users");
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var readTask = result.Content.ReadAsAsync<IList<Users>>();
            //        readTask.Wait();

            //        users = readTask.Result;
            //        bool isValidUser = users.Any(u => u.Username == user.Username && u.Password == user.Password);

            //        if (isValidUser)
            //        {
            //            FormsAuthentication.SetAuthCookie(user.Username, false);
            //            Session["username"] = user.Username.ToString();
            //            Session["tokenkey"] = "X12345678";
            //            return RedirectToAction("Index", "Dashboard");
            //        }
            //        else
            //        {
            //            return View("Index");
            //        }
            //    }
            //    else //web api sent error response 
            //    {
            //        //log response status here.

            //        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            //        return View();
            //    }
            //}

            //UsersBusinessLayer usersBusinessLayer = new UsersBusinessLayer();
            //List<Users> users = usersBusinessLayer.users.ToList();
            //bool isValidUser = users.Any(u => u.Username == user.Username && u.Password == user.Password);
            //if (isValidUser)
            //{
            //    FormsAuthentication.SetAuthCookie(user.Username, false);
            //    Session["username"] = user.Username.ToString();
            //   return RedirectToAction("Index", "Dashboard");
            //}
            // else if (!isValidUser || !ModelState.IsValid)
            //{
            //   ModelState.AddModelError("", "Invalid username or password");
            //   return View();
            //}
            //else
            //{
            //   return View("Index");
            //}
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

        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}