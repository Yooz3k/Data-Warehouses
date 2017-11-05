using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class Distributor
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public Country Country { get; set; }
        public string Address { get; set; }
        public Distributor(int ID, string name, string phoneNumber, Country country, string address)
        {
            this.ID = ID;
            Name = name;
            PhoneNumber = phoneNumber;
            Country = country;
            Address = address;
        }
    }
}
