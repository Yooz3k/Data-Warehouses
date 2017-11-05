using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class Database
    {
        public List<Translation> Translations { get; set; } = new List<Translation>();
        public List<Genre> Genres { get; set; } = new List<Genre>();
        public List<Job> Jobs { get; set; } = new List<Job>();
        public List<Cinema> Cinemas { get; set; } = new List<Cinema>();
        public List<Distributor> Distributors { get; set; } = new List<Distributor>();
        public List<Person> People { get; set; } = new List<Person>();
        public List<Film> Films { get; set; } = new List<Film>();
        public List<Membership> Memberships { get; set; } = new List<Membership>();
        public List<Seance> Seances { get; set; } = new List<Seance>();
        
        public void print()
        {
            Console.WriteLine("TŁUMACZENIA");
            foreach (Translation t in Translations)
            {
                Console.WriteLine(t.ID + "\t" + t.Name);
            }
            Console.WriteLine("GATUNKI");
            foreach (Genre g in Genres)
            {
                Console.WriteLine(g.ID + "\t" + g.Name);
            }
            Console.WriteLine("FUNKCJE_OBSADY");
            foreach (Job j in Jobs)
            {
                Console.WriteLine(j.ID + "\t" + j.Name);
            }
            Console.WriteLine("SALE");
            foreach (Cinema c in Cinemas)
            {
                Console.WriteLine(c.Number + "\t" + c.SeatsUsual + "\t" + c.SeatsVIP + "\t" + c.LastService);
            }
            Console.WriteLine("DYSTRYBUTORZY");
            foreach (Distributor d in Distributors)
            {
                Console.WriteLine(d.ID + "\t" + d.Name + "\t" + d.Country.Name + "\t" + d.PhoneNumber + "\t" + d.Address);
            }
            Console.WriteLine("CZŁONKOWIE_OBSADY");
            foreach (Person p in People)
            {
                Console.WriteLine(p.ID + "\t" + p.Name + "\t" + p.Surname + "\t" + p.Sex + "\t" + p.IsOscar);
            }
            Console.WriteLine("FILMY");
            foreach (Film f in Films)
            {
                Console.WriteLine(f.ISAN + "\t" + f.Title + "\t" + f.Duration + "\t" + f.PremiereDate + "\t" + f.AmountOfAward + "\t" + f.IsOscar);
            }
            Console.WriteLine("CZŁONKOSTWO");
            foreach (Membership m in Memberships)
            {
                Console.WriteLine(m.PersonID + "\t" + m.FilmID + "\t" + m.JobID);
            }
            Console.WriteLine("SEANSE");
            foreach (Seance s in Seances)
            {
                Console.WriteLine( "\t" );
            }
        }

        public void export()
        {
            FileStream fs = new FileStream("db_spolszczenia.txt", FileMode.Create);
            using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding(1250)))
            {
                sw.NewLine = "\n";
                foreach (Translation t in Translations)
                {
                    sw.WriteLine(t.ID + "~" + t.Name);
                }
            }

            fs = new FileStream("db_gatunki.txt", FileMode.Create);
            using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding(1250)))
            {
                sw.NewLine = "\n";
                foreach (Genre g in Genres)
                {
                    sw.WriteLine(g.ID + "~" + g.Name);
                }
            }

            fs = new FileStream("db_funkcjeobsady.txt", FileMode.Create);
            using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding(1250)))
            {
                sw.NewLine = "\n";
                foreach (Job j in Jobs)
                {
                    sw.WriteLine(j.ID + "~" + j.Name);
                }
            }

            fs = new FileStream("db_dystrybutorzy.txt", FileMode.Create);
            using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding(1250)))
            {
                sw.NewLine = "\n";
                foreach (Distributor d in Distributors)
                {
                    sw.WriteLine(d.ID + "~" + d.Name + "~" + d.PhoneNumber + "~" + d.Country.Name + "~" + d.Address);
                }
            }

            fs = new FileStream("db_filmy.txt", FileMode.Create);
            using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding(1250)))
            {
                sw.NewLine = "\n";
                foreach (Film f in Films)
                {
                    sw.WriteLine(f.ISAN + "~" + f.Title + "~" + f.Duration + "~" + f.PremiereDate.ToShortDateString() + "~" +
                        f.AmountOfAward + "~" + Convert.ToInt32(f.IsOscar) + "~" + f.GenreID + "~" + f.DistributorID);
                }
            }

            fs = new FileStream("db_sale.txt", FileMode.Create);
            using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding(1250)))
            {
                sw.NewLine = "\n";
                foreach (Cinema c in Cinemas)
                {
                    sw.WriteLine(c.Number + "~" + c.SeatsUsual + "~" + c.SeatsVIP + "~" + Convert.ToInt32(c.Is3DSupported) + "~" + c.LastService.ToShortDateString());
                }
            }

            fs = new FileStream("db_seanse.txt", FileMode.Create);
            using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding(1250)))
            {
                sw.NewLine = "\n";
                foreach (Seance s in Seances)
                {
                    sw.WriteLine(s.ID + "~" + Convert.ToInt32(s.Is3D) + "~" + s.Income + "~" + s.Date.ToShortDateString() + "~" + s.Time + "~" + 
                        s.Attendance + "~" + s.FilmID + "~" + s.CinemaID + "~" + s.TranslationID);
                }
            }

            fs = new FileStream("db_czlonkowieobsady.txt", FileMode.Create);
            using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding(1250)))
            {
                sw.NewLine = "\n";
                foreach (Person p in People)
                {
                    sw.WriteLine(p.ID + "~" + p.Name + "~" + p.Surname + "~" + p.Sex + "~" + Convert.ToInt32(p.IsOscar));
                }
            }

            fs = new FileStream("db_czlonkostwo.txt", FileMode.Create);
            using (StreamWriter sw = new StreamWriter(fs, Encoding.GetEncoding(1250)))
            {
                sw.NewLine = "\n";
                foreach (Membership m in Memberships)
                {
                    sw.WriteLine(m.PersonID + "~" + m.FilmID + "~" + m.JobID);
                }
            }
        }
    }
}
