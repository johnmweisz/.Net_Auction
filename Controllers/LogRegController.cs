using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BeltExam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BeltExam.Controllers
{
    
    public class LogRegController : Controller
    {
        private Context _db;
        public LogRegController(Context context)
        {
            _db = context;
        }
        [HttpGet("")]
        public IActionResult LogReg()
        {
            return View();
        }
        [HttpPost("register")]
        public IActionResult Register(LogReg LogReg)
        {
            if(ModelState.IsValid)
            {
                // Check if Email already exists.
                if(_db.Users.Any(u => u.Email == LogReg.Register.Email))
                {
                    ModelState.AddModelError("Register.Email", "already in use, please log in.");
                    return View("LogReg");
                }
                // Hash password.
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                LogReg.Register.Password = Hasher.HashPassword(LogReg.Register, LogReg.Register.Password);
                _db.Add(LogReg.Register);
                _db.SaveChanges();
                // Save ID to Session.
                User GetID = _db.Users.FirstOrDefault(u => u.Email == LogReg.Register.Email);
                HttpContext.Session.SetInt32("UserId", GetID.UserId);
                // Redirect to Main App.
                return RedirectToAction("Index", "Home");
            }
            // Render form with errors.
            return View("LogReg");
        }
        [HttpPost("login")]
        public IActionResult Login(LogReg LogReg)
        {
            if(ModelState.IsValid)
            {
                // Find user that matches Email provided.
                User FindUserById = _db.Users.FirstOrDefault(u => u.Email == LogReg.Login.Email);
                // Check if email does not exist.
                if(FindUserById == null)
                {
                    ModelState.AddModelError("Login.Email", "does not exist, please register.");
                    return View("LogReg");
                }
                // Convert string to hash and compare against hashed hassword in the _db.
                var hasher = new PasswordHasher<User>();
                var result = hasher.VerifyHashedPassword(FindUserById, FindUserById.Password, LogReg.Login.Password);
                // If hash matches, add User Id to session.
                if(result == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetInt32("UserId", FindUserById.UserId);
                    // Redirect to Main App.
                    return RedirectToAction("Index", "Home");
                }
                // If hash does not match, render form with errors.
                ModelState.AddModelError("Login.Password", "incorrect!");
                return View("LogReg");
            }
            return View("LogReg");
        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LogReg");
        }
    }
}