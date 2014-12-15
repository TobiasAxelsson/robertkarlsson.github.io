﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Telefonbok.Models
{
    /// <summary>
    /// Klass som hanterar lagring av kontakter.
    /// Klassen lagrar enbart kontakter i minnet och försvinner
    /// således när applikationen stängs ned.
    /// </summary>
    public static class Lagring
    {
        // Statisk lista för att hålla en samling av kontakter
        static List<Kontakt> internLagring = new List<Kontakt>();

        // Räknare för att ge varje kontakt ett unikt id
        static int id = 0;

        /// <summary>
        /// Hämtar alla kontakter.
        /// </summary>
        public static IEnumerable<Kontakt> HämtaAllaKontakter()
        {
            return internLagring;
        }

        /// <summary>
        /// Lägger till en kontakt.
        /// </summary>
        public static void LäggTillKontakt(Kontakt kontakt)
        {
            // ifall argumentet är null avbryt med exception
            if (kontakt == null)
                throw new ArgumentNullException("kontakt");

            kontakt.Id = id;
            internLagring.Add(kontakt);
            id++;
        }
        /// <summary>
        /// Tar bort en kontakt.
        /// </summary>
        public static void TaBortKontakt(int id)
        {
            Kontakt kontaktAttTaBort = internLagring.SingleOrDefault(kontakt => kontakt.Id == id);

            internLagring.Remove(kontaktAttTaBort);
        }

        /// <summary>
        /// Editerar en kontakt.
        /// </summary>
        public static void ÄndraKontakt(Kontakt ändradKontakt)
        {
            // ifall argumentet är null avbryt med exception
            if (ändradKontakt == null)
                throw new ArgumentNullException("ändradKontakt");

            Kontakt lagradKontakt = internLagring.SingleOrDefault(contact => contact.Id == ändradKontakt.Id);

            if (lagradKontakt != null)
            {
                internLagring.Remove(lagradKontakt);
                internLagring.Add(ändradKontakt);
            }
        }

        public static IEnumerable<Kontakt> SökKontakt(string sökfras)
        {
            if (sökfras == null)
                throw new ArgumentNullException("sökFras");

            IEnumerable<Kontakt> sökResultat = internLagring.Where(s => s.Förnamn.Contains(sökfras) || s.Efternamn.Contains(sökfras) || s.Telefonnummer.Contains(sökfras));

            return sökResultat;
        }
        
       
    }
    

}