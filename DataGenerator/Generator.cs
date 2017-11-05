using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class Generator
    {
        Database db;
        Diary d;
        private Random rand = new Random();
        List<string> maleNames = new List<string>();
        List<string> femaleNames = new List<string>();
        List<string> surnames = new List<string>();
        List<Country> countries { get; set; } = new CountryFactory().countries;

        public Generator(Database db, Diary d)
        {
            this.db = db;
            this.d = d;

            string imionaPath = "imiona.txt";
            string nazwiskaPath = "nazwiska.txt";
            StreamReader sr;

            sr = new StreamReader(imionaPath);
            Console.WriteLine(sr.CurrentEncoding);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (line[line.Length - 1].Equals('a')) femaleNames.Add(line);
                else maleNames.Add(line);
            }

            sr = new StreamReader(nazwiskaPath);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                surnames.Add(line);
            }
        }

        public string nextISAN(int id)
        {
            Func<char> nextHex = () => { return rand.Next(16).ToString("X")[0]; };
            StringBuilder result = new StringBuilder("0000-0000-0000-0000-0-0000-0000-0");
            string idHex = id.ToString("X").PadLeft(6, '0');
            //NNNN-NNNN-NNNN-NNNN-X-NNNN-NNNN-Y
            result[8] = nextHex();
            result[10] = nextHex();
            result[11] = idHex[4];
            result[12] = idHex[0];
            result[13] = idHex[1];
            result[15] = nextHex();
            result[16] = idHex[2];
            result[17] = idHex[5];
            result[18] = idHex[3];
            result[20] = nextHex();
            result[32] = nextHex();
            return result.ToString();
        }
        public DateTime nextDateInYear(int year)
        {
            DateTime result = new DateTime(year, 1, 1);
            return result.AddDays(rand.Next(364));
        }
        public DateTime nextDateAfter(DateTime oldDate, int maxTimeSpanDays)
        {
            return oldDate.AddDays(rand.Next(maxTimeSpanDays)+1);
        }

        public DateTime nextDateAfter(DateTime oldDate, int minTimeSpanDays, int maxTimeSpanDays)
        {
            return oldDate.AddDays(rand.Next(minTimeSpanDays, maxTimeSpanDays) + 1);
        }

        public DateTime nextDateBefore(DateTime oldDate, int maxTimeSpanDays)
        {
            return oldDate.AddDays(-rand.Next(maxTimeSpanDays)+1);
        }

        public DateTime nextDateBefore(DateTime oldDate, int minTimeSpanDays, int maxTimeSpanDays)
        {
            return oldDate.AddDays(-rand.Next(minTimeSpanDays, maxTimeSpanDays) + 1);
        }

        public DateTime nextDateBeetween(DateTime d1, DateTime d2)
        {
            return d1.AddDays(rand.Next((int)((d2 - d1).TotalDays)));
        }

        public bool nextBool()
        {
            if (rand.Next(2) == 0) return true;
            else return false;
        }

        public int nextInt(int maxValueExclude)
        {
            return rand.Next(maxValueExclude);
        }

        public int nextInt(int minValueInclude, int maxValueExclude)
        {
            return rand.Next(minValueInclude, maxValueExclude);
        }
        public int nextID(ICollection list)
        {
            return nextInt(1, list.Count + 1);
        }

        public T getFromID<T>(int ID, List<T> list)
        {
            return list[ID - 1];
        }

        public string nextPolishName(int sex = 2)
        {
            string result = "";
            if (sex != 0 && sex != 1)
            {
                sex = rand.Next(2);
            }
            if (sex==0)
            {
                result = femaleNames[rand.Next(femaleNames.Count)] + " " + surnames[rand.Next(surnames.Count)];
                if (result[result.Length - 1].Equals('i')) result = result.Remove(result.Length - 1, 1) + "a";
            } else
            {
                result = maleNames[rand.Next(maleNames.Count)] + " " + surnames[rand.Next(surnames.Count)];
            }
            return result;
        }

        public int nextDistributor()
        {
            return db.Distributors[rand.Next(db.Distributors.Count)].ID;
        }
        public string nextPhoneNumber(Country country, int length = 9)
        {
            string result = "+" + country.PhoneNumberPrefix.ToString();
            for (int i = 0; i < length; i++)
            {
                result += rand.Next(10).ToString();
            }
            return result;
        }
        public Country nextCountry()
        {
            return countries[rand.Next(countries.Count)];
        }

        public string nextCity(Country country)
        {
            return country.Cities[rand.Next(country.Cities.Count)];
        }
        public string nextStreet(Country country)
        {
            return country.Streets[rand.Next(country.Streets.Count)];
        }
        public string nextComment(int statusNumber)
        {
            List<string> commentsPlanned = new List<string>(); //0
            List<string> commentsCompleted = new List<string>(); //2
            List<string> commentsCancelled = new List<string>(); //3

            commentsPlanned.AddRange(new string[] { "Potrzebna negocjacja ceny", "Może być hitem", "Może przyciągnąć nowych klientów" });
            commentsCompleted.AddRange(new string[] { "Szybka realizacja", "Niska cena", "Dobry kontakt z dystrybutorem" });
            commentsCancelled.AddRange(new string[] { "Problemy z wykonaniem płatności", "Anulowano z winy dystrybutora", "Zbyt wysoka cena" });

            int commentIndex = nextInt(3);
            if (statusNumber == 0)
                return commentsPlanned[commentIndex];
            else if (statusNumber == 2)
                return commentsCompleted[commentIndex];
            else
                return commentsCancelled[commentIndex];

        }
    }
}
