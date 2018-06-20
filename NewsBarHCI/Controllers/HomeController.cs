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
                    PageModel = new object[] { db.Vijesti.ToList() }
                };

                return View(model);
            }
            else
            {

                var model = new ViewModel()
                {
                    Kategorije = db.Kategorije.ToList(),
                    PageModel = new object[] { db.Kategorije.Find(Id).Vijesti.ToList() }
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

            var news = db.Vijesti.ToList();
            var last = news.Count > 0 ? news[news.Count - 1] : null;

            vijest.Korisnici = db.Korisnici.FirstOrDefault(k => k.Id == vijest.AutorId);
            vijest.Created = DateTime.Now;
            vijest.Slika = @"~\Images\" + Guid.NewGuid().ToString() + "_" + Request.Files[0].FileName;
            vijest.Id = last?.Id + 1 ?? 0; //TODO: Ukloniti nakon auto increment

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
        public ActionResult IzvrsiPrijavu(string nick, string password)
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
        [ActionName("Logout")]
        public ActionResult Logout()
        {
            var db = new NewsBarEntities();

            var model = new ViewModel()
            {
                Kategorije = db.Kategorije.ToList(),
                PageModel = new object[] { null, db.Vijesti.ToList() }
            };

            return View("Index", model);
        }

        [HttpGet]
        public ActionResult NewsView(int Id)
        {
            var db = new NewsBarEntities();

            var vijest = db.Vijesti.Find(Id);
            var komentar = new Komentari()
            {
                News = vijest.Id,
                Vijesti = vijest,
            };

            var model = new ViewModel()
            {
                Kategorije = db.Kategorije.ToList(),
                PageModel = new object[] { vijest, komentar }
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

        [HttpGet]
        public ActionResult Registracija()
        {
            var db = new NewsBarEntities();

            var model = new ViewModel()
            {
                Kategorije = db.Kategorije.ToList(),
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Register")]
        public ActionResult ObaviRegistraciju(string nick, string email, string password)
        {
            var db = new NewsBarEntities();

            var model = new ViewModel()
            {
                Kategorije = db.Kategorije.ToList(),
                PageModel = true
            };

            var users = db.Korisnici.ToList();
            var last = users.Count > 0 ? users[users.Count - 1] : null;

            var user = new Korisnici()
            {
                Id = last?.Id + 1 ?? 0,
                Ime = nick,
                Email = email,
                Avatar = "",
                Created = DateTime.Now,
                Pass = ComputeMD5(password),
            };

            try
            {
                db.Korisnici.Add(user);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                return View("Registracija", model);
            }

            return View("Prijava", model);
        }

        public ActionResult Pretraga()
        {

            return View();
        }

        [HttpPost]
        [ActionName("PostComment")]
        public ActionResult PostComment(Komentari komentar)
        {
            var db = new NewsBarEntities();

            komentar.Created = DateTime.Now;
            komentar.Vijesti = db.Vijesti.First(v => v.Id == komentar.News);
            komentar.Korisnici = db.Korisnici.FirstOrDefault(k => k.Id == komentar.Osoba);
            komentar.Id = db.Komentari.Count() == 0 ? 0 : db.Komentari.Last().Id + 1;

            var model = new ViewModel()
            {
                Kategorije = db.Kategorije.ToList(),
                PageModel = new object[] { komentar.Vijesti, komentar }
            };

            try
            {
                db.Komentari.Add(komentar);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                // Ako nije uspjelo postanje, treba dojaviti
                model.PageModel = new object[] { model.PageModel, null };
            }

            return View("NewsView", model);
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