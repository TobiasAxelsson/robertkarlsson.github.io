using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Telefonbok.Models;
using Telefonbok.TelefonService;
using System.Xml.Linq;


namespace Telefonbok.Controllers
{
    public class KontaktController : Controller
    {
        KontaktServiceClient kontaktClient;
        //
        // GET: /Kontakt/

        public ActionResult Index()
        {
            kontaktClient = new KontaktServiceClient();
            XElement toLagring= kontaktClient.HämtaAllaKontakter();
            kontaktClient.Close();

            var lagring = from kontakter in toLagring.Elements("Kontakt")
                          select Kontakt.FromXml(kontakter);
                
            return View(lagring);
        }

        public ActionResult LaggTillKontakt()
        {
            Kontakt kontakt = new Kontakt();

           return View(kontakt);
        }

        public ActionResult AndraKontakt(int id)
        {
            return View();
        }

        public ActionResult SokKontakter()
        {
           
            return View();
        }

        public ActionResult SokResultat(IEnumerable<Kontakt> Sökresultat)
        {
            return View(Sökresultat);
        }

        //HTTPOSTS
        [HttpPost]
        public ActionResult TaBortKontakt(int id)
        {

            kontaktClient = new KontaktServiceClient();
            kontaktClient.TaBortKontakt(id);
            kontaktClient.Close();
            return new RedirectResult("/");
        }

        [HttpPost]
        public ActionResult LaggTillKontakt(Kontakt kontakt)
        {

            kontaktClient = new KontaktServiceClient();
            kontaktClient.LäggTillKontakt(kontakt.ToXml());
            kontaktClient.Close();
          
            return new RedirectResult("LaggTillKontakt");
        }

        [HttpPost]
        public ActionResult AndraKontakt(Kontakt kontakt)
        {

            kontaktClient = new KontaktServiceClient();
            kontaktClient.ÄndraKontakt(kontakt.ToXml());
            kontaktClient.Close();
            return new RedirectResult("/");
        }

        [HttpPost]
        public ActionResult SokKontakter(string sökfras)
        {
            List<Kontakt> SökResultat = new List<Kontakt>();

            XElement sökkriterier = new XElement("Sökfras", sökfras);
            kontaktClient = new KontaktServiceClient();
            var Sök = kontaktClient.SökKontakter(sökkriterier);
            kontaktClient.Close();

            foreach (var kontakt in Sök.Elements("Kontakt"))
                SökResultat.Add(Kontakt.FromXml(kontakt));

            

            
         //  IEnumerable<Kontakt> SökResultat = Lagring.SökKontakt(sökfras);
          return View(SökResultat);
        }
    }
}
