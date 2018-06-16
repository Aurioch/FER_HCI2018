using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsBarHCI.Models;
using NewsBarCore;

namespace NewsBarHCI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddNews(int userId)
        {
            var model = new AddNewsViewModel();

            var db = new NewsBarEntities();

            model.Korisnik = db.Korisnici.Find(userId);
            model.Vijest = new Vijesti()
            {
                AutorId = userId,                
            };

            return View(model);
        }
    }
}