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

            var model = new ViewModel()
            {
                Kategorije = db.Kategorije.ToList(),
                PageModel = new Vijesti()
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("PostNews")]
        public ActionResult PostNews(Vijesti vijest)
        {
            var db = new NewsBarEntities();
            var model = new ViewModel()
            {
                Kategorije = db.Kategorije.ToList(),
                PageModel = null
            };

            vijest.Korisnici = db.Korisnici.FirstOrDefault(k => k.Id == vijest.AutorId);
            vijest.Created = DateTime.Now;
            vijest.Slika = "";
            vijest.Id = db.Vijesti.Last().Id + 1; //TODO: Ukloniti nakon auto increment

            try
            {
                db.Vijesti.Add(vijest);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return View("AddNews", model);
            }

            model.PageModel = vijest;

            return View("AddNews", model);
        }

        [HttpGet]
        public ActionResult Prijava()
        {
            var db = new NewsBarEntities();

            var model = new ViewModel()
            {
                Kategorije = db.Kategorije.ToList(),
                PageModel = null
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult IzvršiPrijavu(string nick, string password)
        {
            var db = new NewsBarEntities();
            var hash = ComputeMD5(password);

            var korisnik = db.Korisnici.FirstOrDefault(u => u.Ime == nick && u.Pass == hash);

            if (korisnik == null)
            {
                return View("Prijava");
            }

            var model = new ViewModel()
            {
                Kategorije = db.Kategorije.ToList(),
                PageModel = new object[] { korisnik, db.Vijesti.ToList() }
            };

            return View("Index", model);
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

        public ActionResult ProfilePage() 
        {

            //         var db = new NewsBarEntities();
            /*
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
              */


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

        public ActionResult PostComment(Komentari komentar)
        {
            return View("Index"); //TODO: Preusmjeriti na stranicu za jednu vijest
        }

        public static string ComputeMD5(string input)
        {
            var md5 = System.Security.Cryptography.MD5.Create();

            var inputBytes = System.Text.Encoding.Unicode.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            var result = new System.Text.StringBuilder();

            for (int i = 0; i < hash.Length; i++)
                result.Append(hash[i].ToString("X2"));

            return result.ToString();
        }
    }
}