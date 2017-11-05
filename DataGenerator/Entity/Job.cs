using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class Job
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Job(int ID, string name)
        {
            this.ID = ID;
            Name = name;
        }
    }
}