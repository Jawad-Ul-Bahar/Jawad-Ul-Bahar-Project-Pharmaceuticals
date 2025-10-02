using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Project.Data;
using Project.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace Project.Controllers
{
    public class AdminController : Controller
    {
        private readonly CandidateContext _context;
        private object? email;
        private object? password;

        public AdminController(CandidateContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
           return View();
        }

        [HttpGet]
        public IActionResult Careers()
        {
            var careers = _context.Careers.ToList();
            return View(careers);
        }

        [HttpPost]
        public IActionResult ApproveCareer(int id)
        {
            var career = _context.Careers.Find(id);
            if (career != null)
            {
                career.Status = "Approved";
                _context.SaveChanges();
            
            }
            return RedirectToAction("Careers");
        }

        [HttpPost]
        public IActionResult RejectCareer(int id)
        {
            var career = _context.Careers.Find(id);
            if (career != null)
            {
                career.Status = "Reject";
                _context.SaveChanges();
            }

            return RedirectToAction("Careers");
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SendCareerEmail(int CareerId, string EmailTo, string EmailSubject, string EmailText)
        {
            try
            {
                var career = _context.Careers.Find(CareerId);
                if (career == null)
                {
                    TempData["Error"] = "Invalid request.";
                    return RedirectToAction("Careers");
                }

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("jawadulbahar@gmail.com", "delwgxougwyvdpzi"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("jawadulbahar@gmail.com", "InnovaMedix Pharmora"),
                    Subject = EmailSubject,
                    Body = EmailText,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(EmailTo);
                smtpClient.Send(mailMessage);

                TempData["Message"] = $"Email sent to {EmailTo} successfully.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Failed to send email: " + ex.Message;
            }

            return RedirectToAction("Careers");
        }
        [HttpGet]
        [HttpPost]
        public IActionResult AdminRegister(AdminLogin Admin)
        {
            if (!_context.AdminLogins.Any(x => x.Email == "admin@gmail.com"))
            {
                var defaultAdmin = new AdminLogin
                {
                    Name = "admin",
                    Email = "admin@gmail.com",
                    Phone = "03123456789",
                    Password = "admin",
                    Address = "admin",
                    Role = "admin",
                    CreatedAt = DateTime.Now
                };

                _context.AdminLogins.Add(defaultAdmin);
                _context.SaveChanges();
            }

            if (ModelState.IsValid)
            {
                Admin.CreatedAt = DateTime.Now;
                _context.AdminLogins.Add(Admin);
                _context.SaveChanges();
                ViewBag.Message = "Account created successfully!";
                return RedirectToAction("Login");
            }

            return View(Admin);
        }



        [HttpGet]
        public IActionResult Login()
        {
             return View();
        }

        [HttpPost]
        public IActionResult login(string email, string password)
        {
            var Admin = _context.AdminLogins.FirstOrDefault(x =>
                x.Email.Equals(email) && x.Password.Equals(password) 
                && (x.Role.Equals("admin") || x.Role.Equals("subadmin"))
            );

            if (Admin == null)
            {
                ViewBag.Message = "Invalid Email or Password";
                return View("Login");
            }

            HttpContext.Session.SetInt32("AdminId", Admin.Id);
            HttpContext.Session.SetString("AdminRole", Admin.Role);
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

       

        public IActionResult ProfileView()
        {
            var AdminId = HttpContext.Session.GetInt32("AdminId");
            if (AdminId == null)
            {
                return RedirectToAction("Login");
            }

            var admin = _context.AdminLogins.FirstOrDefault(a => a.Id == AdminId);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        public IActionResult ProfileUpdate(AdminLogin Admindata)
        {
            var AdminId = HttpContext.Session.GetInt32("AdminId");
            if (AdminId == null)
            {
                return RedirectToAction("Login");
            }

            var Admin = _context.AdminLogins.FirstOrDefault(a => a.Id == AdminId);
            if (Admin == null)
            {
                return RedirectToAction("Login");
            }

            Admin.Name = Admindata.Name;
            Admin.Email = Admindata.Email;
            Admin.Phone = Admindata.Phone;
            Admin.Password = Admindata.Password;
            Admin.Address = Admindata.Address;

            _context.SaveChanges();

            ViewBag.Message = "Profile updated successfully!";
            return View("ProfileView", Admin);
        }
        [HttpGet]
        public IActionResult ContactUs()
        {
            var Contact = _context.Contacts.ToList();
            return View("ContactUs", Contact);
        }

        [HttpGet]
        public IActionResult Products()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Quotes()
        {
            var Quote = _context.Quotes.ToList();
            return View(Quote);
        }

        [HttpGet]
        public IActionResult CandidateView()
        {
            var candidates = _context.Candidates.ToList();
            return View(candidates);
        }

        [HttpPost]
        public IActionResult DeleteCandidate(int id)
        {
            var candidate = _context.Candidates.FirstOrDefault(c => c.Id == id);
            if (candidate != null)
            {
                _context.Candidates.Remove(candidate);
                _context.SaveChanges();
            }

            return RedirectToAction("CandidateView");
        }

        public IActionResult ManageAdmins()
        {
            var pending = _context.AdminLogins.Where(a => a.Role == "pending").ToList();
            var approved = _context.AdminLogins.Where(a => a.Role == "subadmin").ToList();

            ViewBag.Pending = pending;
            ViewBag.Approved = approved;

            return View();
        }

        [HttpPost]
        public IActionResult Approve(int id)
        {
            var admin = _context.AdminLogins.Find(id);
            if (admin != null)
            {
                admin.Role = "subadmin";
                _context.SaveChanges();
            }
            return RedirectToAction("ManageAdmins");
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            var admin = _context.AdminLogins.Find(id);
            if (admin != null)
            {
                _context.AdminLogins.Remove(admin);
                _context.SaveChanges();
            }
            return RedirectToAction("ManageAdmins");
        }

        [HttpPost]
        public IActionResult DeleteApproved(int id)
        {
            var admin = _context.AdminLogins.Find(id);
            if (admin != null && admin.Role == "subadmin")
            {
                _context.AdminLogins.Remove(admin);
                _context.SaveChanges();
            }
            return RedirectToAction("ManageAdmins");
        }
    }
}
