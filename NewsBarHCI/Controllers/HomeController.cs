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
            var db = new NewsBarEntities();

            var model = new ViewModel()
            {
                Kategorije = db.Kategorije.ToList(),
                PageModel = db.Vijesti.ToList()
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult AddNews()
        {
            var db = new NewsBarEntities();

            return View();
        }

        [HttpPost]
        [ActionName("PostNews")]
        public ActionResult PostNews(Vijesti vijest)
        {
            var db = new NewsBarEntities();

            vijest.AutorId = 1;
            db.Vijesti.Add(vijest);

            return View("AddNews", vijest);
        }

        public ActionResult Prijava()
        {

            return View();
        }

        public ActionResult Registracija()
        {


            return View();
        }

        public ActionResult Pretraga()
        {

            return View();
        }
    }
}