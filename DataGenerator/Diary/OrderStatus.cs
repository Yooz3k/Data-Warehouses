using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataGenerator
{
    public class OrderStatus
    {
        [XmlIgnore]
        public DateTime Date { get; set; }
        [XmlAttribute("Data")]
        public string DateString { get; set; }

        [XmlText]
        public string Status { get; set; }

        public OrderStatus() { }

        public OrderStatus(DateTime date, string status)
        {
            Date = date;
            DateString = date.ToString("yyyy-MM-dd");
            Status = status;
        }
    }
}
