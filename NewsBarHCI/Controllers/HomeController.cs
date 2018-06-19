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
            var model = new IndexViewModel()
            {
                Korisnici = null,
                Komentari = null,
                Vijesti = db.Vijesti.ToList(),
                Kategorije = db.Kategorije.ToList()
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult AddNews(int Id)
        {
            var model = new AddNewsViewModel();

            var db = new NewsBarEntities();

            model.Korisnik = db.Korisnici.Find(Id);
            model.Vijest = new Vijesti()
            {
                AutorId = Id,                
            };

            return View(model);
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