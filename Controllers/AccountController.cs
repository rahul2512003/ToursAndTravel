using LoginRegistrationMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace LoginRegistrationMVC.Controllers
{
    public class AccountController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                // Check if user already exists
                var existingUser = db.Users.FirstOrDefault(u => u.Email == user.Email);
                if (existingUser == null)
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Error = "Email already exists.";
                }
            }
            return View(user);
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                Session["UserId"] = user.Id;
                Session["Username"] = user.Username;
                return RedirectToAction("Dashboard");
            }
            else
            {
                ViewBag.Error = "Invalid email or password.";
            }
            return View();
        }

        public ActionResult Dashboard()
        {
            if (Session["UserId"] == null)
                return RedirectToAction("Login");

            ViewBag.Username = Session["Username"];
            //return View();

            var tours = new List<Tour>
            {
            new Tour
            {
                TourName = "Statue Of Unity",
                Place = "Vadodara, Gujarat, India",
                Photos = new List<string> { "/images/s1.jpg", "/images/s2.jpg" },
                Description = "The Statue of Unity is the world's tallest statue, with a height of 182 metres (597 feet),[3] located near Kevadia in the state of Gujarat, India. It depicts Indian statesman and independence activist Vallabhbhai Patel (1875–1950), who was the first deputy prime minister and home minister of independent India and an adherent of Mahatma Gandhi. Patel is highly respected for playing a significant role in the political integration of India. The statue is located in Gujarat on the Narmada River in the Kevadiya colony, facing the Sardar Sarovar Dam 100 kilometres (62 miles) southeast of the city of Vadodara.",
                Price = 10000,
                ContactNumber = "+123456789",
                Rating = 4.5
            },
            new Tour
            {
                TourName = "Dwarkadhish Temple",
                Place = "Dwarka, Gujarat, India",
                Photos = new List<string> { "/images/d1.jpg", "/images/d2.jpg" },
                Description = "The Dwarkadhish temple, also known as the Jagat Mandir and occasionally spelled Dwarakadheesh, is a Hindu temple dedicated to Krishna, who is worshiped here by the name Dwarkadhish (Dvārakādhīśa), or 'King of Dwarka'. The temple is located at Dwarka city of Gujarat, India, which is one of the destinations of Char Dham, a Hindu pilgrimage circuit. The main shrine of the five-storied building, supported by 72 pillars, is known as Jagat Mandir or Nija Mandir. Archaeological findings suggest the original temple was built in 200 BCE at the earliest.[1][2][3] The temple was rebuilt and enlarged in the 15th–16th century.",
                Price = 799.99m,
                ContactNumber = "+123456789",
                Rating = 4.7
            },
            new Tour
            {
                TourName = "Adalaj Stepwell",
                Place = "Adalaj, Gujarat, India",
                Photos = new List<string> { "/images/a1.jpg", "/images/a2.jpg" },
                Description = "The flamboyant 15th-century stepwell, has lost only little of its grandeur over the last few centuries. Till date, the intricate carvings on the pillars that support the five storeys are mostly intact; the beams work as pit stops for pigeons flying in and out and the structure still leaves jaws dropped for swarms of people. The step-well represents the Indo-Islamic fusion architecture that percolated through the many stepwells of the period. There are some fascinating features of the vav that make this an important emblem of superior architecture.",
                Price = 799.99m,
                ContactNumber = "+123456789",
                Rating = 4.7
            }
            };

            return View(tours);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
