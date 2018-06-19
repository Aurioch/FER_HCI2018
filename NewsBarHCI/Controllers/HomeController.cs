using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewsBarHCI.Models;
using NewsBarCore;
using System.Collections;

namespace NewsBarHCI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int Id = -1)
        {
            var db = new NewsBarEntities();

            if (Id == -1)
            {

                var model = new ViewModel()
                {
                    Kategorije = db.Kategorije.ToList(),
                    PageModel = db.Vijesti.ToList()
                };

                return View(model);
            }
            else
            {

                var model = new ViewModel()
                {
                    Kategorije = db.Kategorije.ToList(),
                    PageModel = db.Kategorije.Find(Id).Vijesti.ToList()
                };

                return View(model);

            }
    
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

        [HttpGet]
        public ActionResult NewsView(int Id)
        {
            var db = new NewsBarEntities();

            var vijest = db.Vijesti.Find(Id);

            var model = new ViewModel()
            {
                Kategorije = db.Kategorije.ToList(),
                PageModel = vijest
            };

            return View(model);
           
        }

        public ActionResult ProfilePage(int Id = -1) 
        {

            var db = new NewsBarEntities();

            if(Id == -1)
            {
                var model = new ViewModel()
                {
                    Kategorije = db.Kategorije.ToList(),
                    PageModel = null
                };

                return View(model);
            }
            else
            {
                var korisnik = db.Korisnici.Find(Id);

                var model = new ViewModel()
                {
                    Kategorije = db.Kategorije.ToList(),
                    PageModel = korisnik
                };


                return View(model);

            }
          
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