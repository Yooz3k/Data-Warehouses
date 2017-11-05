using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataGenerator
{
    [Serializable]
    [XmlRoot("Zamowienie")]
    public class Order
    {
        [XmlAttribute("ID")]
        public int ID { get; set; }

        [XmlElement("ISAN")]
        public string ISAN { get; set; }

        [XmlElement("Cena")]
        public double Price { get; set; }

        [XmlElement("CzyRabat")]
        public bool IsDiscount { get; set; }

        [XmlElement("Status")]
        public OrderStatus Status { get; set; }

        [XmlElement("WaznoscLicencji")]
        public int LicenseExpiry { get; set; }

        [XmlElement("Uwagi")]
        public string Comment { get; set; }
        [XmlIgnore]
        public bool Renewed { get; set; }

        public Order() { }

        public Order(int ID, string ISAN, double price, bool isDiscount, string status, DateTime date, int licenseExpiry, string comment, bool renewed)
        {
            this.ID = ID;
            this.ISAN = ISAN;
            Price = price;
            IsDiscount = isDiscount;
            Status = new OrderStatus(date, status);
            LicenseExpiry = licenseExpiry;
            Comment = comment;
            Renewed = renewed;
        }
    }
}