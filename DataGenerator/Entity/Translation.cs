using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class Translation
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Translation(int ID, string name)
        {
            this.ID = ID;
            Name = name;
        }
    }
}