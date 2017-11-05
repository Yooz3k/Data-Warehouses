using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Sex { get; set; }
        public bool IsOscar { get; set; }
        public Person(int ID, string name, string surname, string sex, bool isOscar)
        {
            this.ID = ID;
            Name = name;
            Surname = surname;
            Sex = sex;
            IsOscar = isOscar;
        }
    }
}