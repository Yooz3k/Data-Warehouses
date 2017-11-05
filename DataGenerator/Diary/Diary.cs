using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DataGenerator
{
    [Serializable]
    [XmlRoot("Dziennik")]
    public class Diary
    {
        [XmlArray("Zamowienia"), XmlArrayItem(typeof(Order), ElementName = "Zamowienie")]
        public List<Order> Orders { get; set; } = new List<Order>();

        public Diary() { }

        public bool isFilmExploitable(string ISAN, DateTime date)
        {
            List<Order> ordersWithThisFilm = new List<Order>();
            foreach (Order o in Orders)
            {
                if (o.ISAN.Equals(ISAN) && !o.Renewed && o.Status.Status.Equals("Zrealizowane"))
                    if (o.Status.Date <= date && o.Status.Date.AddDays(o.LicenseExpiry) >= date)
                        return true;
            }
            return false;
        }

        public void export(string path)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(this.GetType());
                FileStream fs = new FileStream(path, FileMode.Create);
                serializer.Serialize(fs, this);
                fs.Close();
            }
            catch { }
        }
    }
}
