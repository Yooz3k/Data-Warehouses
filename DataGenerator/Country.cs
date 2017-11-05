using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class Country
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PhoneNumberPrefix { get; set; }
        public List<string> Cities { get; set; }
        public List<string> Streets { get; set; }

        public Country(int ID, string name, int prefix)
        {
            this.ID = ID;
            this.Name = name;
            PhoneNumberPrefix = prefix;
            Cities = new List<string>();
            Streets = new List<string>();
        }

        public void addCities(params string[] cityNames)
        {
            foreach(string city in cityNames)
                Cities.Add(city);
        }

        public void addStreets(params string[] streetNames)
        {
            foreach (string street in streetNames)
                Streets.Add(street);
        }
    }
}