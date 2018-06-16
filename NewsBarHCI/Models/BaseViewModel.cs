using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewsBarCore;

namespace NewsBarHCI.Models
{
    public class BaseViewModel
    {
        public Korisnici Korisnik { get; set; }
        public List<Kategorije> Kategorije { get; set; }
    }
}