using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class Name
    {
        public string firstName { get; set; }
        public string secondName { get; set; }

        public Name(string firstName, string secondName)
        {
            this.firstName = firstName;
            this.secondName = secondName;

            if (this.firstName.EndsWith("a") && this.secondName.EndsWith("i"))
                this.secondName = this.secondName.Remove(this.secondName.Length - 1, 1) + "a";
        }

        public static void Main()
        {
            Name name1 = new Name("Alicja", "Kowalski");
            Name name2 = new Name("Alicja", "Kowalczyk");
            Name name3 = new Name("Krzysztof", "Kowalski");

            Console.Write(name1.firstName + " " + name1.secondName + "\n" +
            name2.firstName + " " + name2.secondName + "\n" +
            name3.firstName + " " + name3.secondName + "\n");
        }
    }
}