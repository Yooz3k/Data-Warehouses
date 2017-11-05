using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class Genre
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Genre(int ID, string name)
        {
            this.ID = ID;
            Name = name;
        }
    }
}