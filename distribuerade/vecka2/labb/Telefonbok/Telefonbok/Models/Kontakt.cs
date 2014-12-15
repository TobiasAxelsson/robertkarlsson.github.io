using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Telefonbok.Models
{
    public class Kontakt
    {
        public int Id { get; set; }

        /* Till del 2 */

        /*public XElement ToXml()
        {
            var newXML = new XElement("Kontakt",
                new XElement("Id", Id),
                new XElement("Förnamn", Förnamn),
                new XElement("Efternamn", Efternamn),
                new XElement("Telefonnummer", Telefonnummer));

            return newXML;
        }
        
        public static Kontakt FromXml(XElement xml)
        {
            Kontakt nyKontakt = new Kontakt();
            nyKontakt.Id = (int)xml.Element("Id");
            nyKontakt.Förnamn = xml.Element("Förnamn").Value;
            nyKontakt.Efternamn = xml.Element("Efternamn").Value;
            nyKontakt.Telefonnummer = xml.Element("Telefonnummer").Value;

            return nyKontakt;
        }*/
    }
}