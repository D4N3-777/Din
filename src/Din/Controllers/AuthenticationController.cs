﻿using System;
using System.Linq;
using Din.Data;
using Din.Logic.BrowserDetection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Din.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly DinContext _context;

        public AuthenticationController(DinContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult Login()
        {
            try
            {
                var username = Request.Form["username"];
                var user = _context.User.Include(u => u.Account).FirstOrDefault(u => u.Account.Username.Equals(username));
                if (user != null && BCrypt.Net.BCrypt.Verify(Request.Form["password"], user.Account.Hash))
                {
                    var serializedUser = JsonConvert.SerializeObject(user, Formatting.Indented,
                        new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
                    HttpContext.Session.SetString("UserAgent", Request.Headers["User-Agent"].ToString());
                    HttpContext.Session.SetString("User", serializedUser);
                    return View("../Main/Home");
                }
                else
                {
                    HttpContext.Session.SetString("Login", "BAD");
                    return RedirectToAction("Index", "Main");
                }
            }
            catch (Exception)
            {
                HttpContext.Session.SetString("Login", "BAD");
                return RedirectToAction("Index", "Main");
            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return View("../Main/Logout");
        }
    }
}