using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly CandidateContext _context;
        private readonly IWebHostEnvironment _env;

        public HomeController(CandidateContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index() => View();

        public IActionResult About() => View();

        [HttpPost]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CandidateRegistration candidate)
        {
            if (!ModelState.IsValid)
            {
                return View("Login", candidate);
            }

            candidate.CreatedAt = DateTime.Now;
            _context.Candidates.Add(candidate);
            _context.SaveChanges();

            ViewBag.Message = "Account created successfully!";
            return RedirectToAction("Login");
        }


        public IActionResult Contact() => View();

        [HttpPost]
        public IActionResult Contactus(Contact Contacts)
        {
            if (!ModelState.IsValid)
            {
                return View("Contact", Contacts);
            }

            _context.Contacts.Add(Contacts);
            _context.SaveChanges();

            SendAcknowledgementEmail(Contacts.Email, Contacts.Name);
            SendAdminNotificationEmail(Contacts);

            TempData["Message"] = "Message sent successfully!";
            return RedirectToAction("Index");
        }


        public IActionResult Products()
        {
            var categories = _context.Categories.Include(c => c.Products).ToList();
            return View(categories);
        }

        public IActionResult Quote() => View();

[HttpPost]
public IActionResult Quoteus(Quote quotes)
{
    if (!ModelState.IsValid)
    {
        return View("Quote", quotes);
    }

    quotes.CreatedAt = DateTime.Now;
    _context.Quotes.Add(quotes);
    _context.SaveChanges();

    SendQuoteAcknowledgementEmail(quotes.Email, quotes.Full_Name);
    SendQuoteNotificationToAdmin(quotes);

    return RedirectToAction("Index");
}


        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Profile(string email, string password)
        {
            var candidate = _context.Candidates.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
            if (candidate == null)
            {
                ViewBag.Message = "Invalid Email or Password";
                return View("Login");
            }

            HttpContext.Session.SetInt32("CandidateId", candidate.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var id = HttpContext.Session.GetInt32("CandidateId");
            if (id == null) return RedirectToAction("Login");

            var candidate = _context.Candidates.FirstOrDefault(c => c.Id == id);
            if (candidate == null) return RedirectToAction("Login");

            return View(candidate);
        }

        [HttpPost]
        public IActionResult UpdateProfile(CandidateRegistration updatedCandidate)
        {
            var candidateId = HttpContext.Session.GetInt32("CandidateId");
            if (candidateId == null) return RedirectToAction("Login");

            var candidate = _context.Candidates.FirstOrDefault(c => c.Id == candidateId);
            if (candidate == null) return RedirectToAction("Login");

            candidate.UpdatedAt = DateTime.Now;
            candidate.Name = updatedCandidate.Name;
            candidate.Phone = updatedCandidate.Phone;
            candidate.Password = updatedCandidate.Password;
            candidate.Address = updatedCandidate.Address;

            _context.SaveChanges();

            ViewBag.Message = "Profile updated successfully!";
            return View("Profile", candidate);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Careers()
        {
            var candidateId = HttpContext.Session.GetInt32("CandidateId");
            if (candidateId == null)
                return RedirectToAction("Login");

            var latestCareer = _context.Careers
                .Where(c => c.CandidateId == candidateId)
                .OrderByDescending(c => c.SubmittedAt)
                .FirstOrDefault();

            ViewBag.LatestCareer = latestCareer;
            return View(); 
        }


        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Careers(CareerRequest request, IFormFile file)
{

    string filestore = Path.Combine(_env.WebRootPath, "uploads");
    Directory.CreateDirectory(filestore);

    string filename = Path.GetFileName(file.FileName);
    string filepath = Path.Combine(filestore, filename);

    using (var stream = new FileStream(filepath, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }

    var candidateId = HttpContext.Session.GetInt32("CandidateId");
    if (candidateId == null)
        return RedirectToAction("Login");

    request.Resume = $"/uploads/{filename}";
    request.SubmittedAt = DateTime.Now;
    request.Status = "Pending";
    request.CandidateId = candidateId;

    _context.Careers.Add(request);
    await _context.SaveChangesAsync();

    TempData["Message"] = "Career request submitted successfully!";
    return RedirectToAction("Careers");
}


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void SendAcknowledgementEmail(string toEmail, string userName)
        {
            var fromEmail = "jawadulbahar@gmail.com";
            var fromPassword = "delwgxougwyvdpzi";

            var message = new MailMessage();
            message.From = new MailAddress(fromEmail, "InnovaMedix Pharmora");
            message.To.Add(toEmail);
            message.Subject = "Thank you for contacting us";
            message.Body = $"Dear {userName},\n\nThank you for contacting us.\nOur team will contact you soon.\n\nRegards,\nPharma Team";

            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true
            };

            smtp.Send(message);
        }

        private void SendAdminNotificationEmail(Contact contact)
        {
            var fromEmail = "jawadulbahar@gmail.com";
            var fromPassword = "delwgxougwyvdpzi";
            var toEmail = "jawadulbahar@gmail.com";

            var messageBody = $@"
            New contact form submission:
            Name: {contact.Name}
            Email: {contact.Email}
            Subject: {contact.Subject}
            Message:
            {contact.Message}";

            var message = new MailMessage();
            message.From = new MailAddress(fromEmail, "Pharma Contact Form");
            message.To.Add(toEmail);
            message.Subject = $"New Contact Message from {contact.Name}";
            message.Body = messageBody;

            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true
            };

            smtp.Send(message);
        }

        private void SendQuoteAcknowledgementEmail(string toEmail, string userName)
        {
            var fromEmail = "jawadulbahar@gmail.com";
            var fromPassword = "delwgxougwyvdpzi";

            var message = new MailMessage();
            message.From = new MailAddress(fromEmail, "Pharma Team");
            message.To.Add(toEmail);
            message.Subject = "Thank you for your quote request";
            message.Body = $"Dear {userName},\n\nThank you for submitting your quote request.\nOur team will contact you soon.\n\nRegards,\nPharma Team";

            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true
            };

            smtp.Send(message);
        }

        private void SendQuoteNotificationToAdmin(Quote quote)
        {
            var fromEmail = "jawadulbahar@gmail.com";
            var fromPassword = "delwgxougwyvdpzi";
            var toEmail = "jawadulbahar@gmail.com";

            var messageBody = $@"
            New Quote Request:
            Full Name: {quote.Full_Name}
            Company Name: {quote.Company_Name}
            Address: {quote.Address}
            City: {quote.City}
            State: {quote.State}
            Postal Code: {quote.Postal_Code}
            Country: {quote.Country}
            Email Address: {quote.Email}
            Phone: {quote.Phone}
            Comments: {quote.Comments}";

            var message = new MailMessage();
            message.From = new MailAddress(fromEmail, "Pharma Quote Form");
            message.To.Add(toEmail);
            message.Subject = $"New Quote Request from {quote.Full_Name}";
            message.Body = messageBody;

            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(fromEmail, fromPassword),
                EnableSsl = true
            };

            smtp.Send(message);
        }
    }
}
