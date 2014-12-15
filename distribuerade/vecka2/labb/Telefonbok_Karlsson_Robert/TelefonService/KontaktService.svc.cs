using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml.Linq;
using System.Web.Hosting;

namespace TelefonService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class KontaktService : IKontaktService
    {
        int id = 0;
        string appdata = HostingEnvironment.MapPath("/App_Data/");
        XElement KontaktXml;
        public KontaktService()
        {
            KontaktXml = XElement.Load(appdata + "kontaktlista.xml");
        }

       public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }


        public XElement HämtaAllaKontakter()
        {
            return KontaktXml;
        }


        public void LäggTillKontakt(XElement kontakt)
        {
            if (kontakt == null)
                throw new ArgumentNullException("kontakt");

            if (KontaktXml.IsEmpty == true)
            {
                kontakt.Element("Id").SetValue(id);
            }
            else
            {
                id = ((int)KontaktXml.Elements("Kontakt").Last().Element("Id")) + 1;
                kontakt.Element("Id").SetValue(id);
            }
            KontaktXml.Add(kontakt);
            Save();
        }

        public void TaBortKontakt(int id)
        {
            XElement kontakt = (from element in KontaktXml.Descendants("Kontakt")
                                where (int)element.Element("Id") == id
                                select element).First();
            kontakt.Remove();
            Save();
           
        }

        public void ÄndraKontakt(XElement ändradKontakt)
        {
            if (ändradKontakt == null)
                throw new ArgumentNullException("ändradKontakt");

            XElement kontakt = (from element in KontaktXml.Descendants("Kontakt")
                                where (int)element.Element("Id") == (int)ändradKontakt.Element("Id")
                                select element).First();

            kontakt.ReplaceWith(ändradKontakt);
            Save();
        }

        public XElement SökKontakter(XElement kriterier)
        {

               XElement kontaker = new XElement("Kontakt",
                (from element in KontaktXml.Elements("Kontakt")
                 where
                 element.Element("Förnamn").Value.ToLower().Contains(kriterier.Value.ToLower()) ||
                 element.Element("Efternamn").Value.ToLower().Contains(kriterier.Value.ToLower()) ||
                 element.Element("Telefonnummer").Value.Contains(kriterier.Value)
                 select element));
             
            return kontaker;
        }

        private void Save()
        {
            KontaktXml.Save(appdata + "kontaktlista.xml");
        }
    }
}
