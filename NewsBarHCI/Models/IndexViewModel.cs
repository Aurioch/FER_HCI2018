using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NewsBarCore;

namespace NewsBarHCI.Models
{
    public class IndexViewModel : BaseViewModel
    {
        public List<Korisnici> Korisnici { get; set; }
        public List<Vijesti> Vijesti { get; set; }
        public List<Komentari> Komentari { get; set; }
    }
}