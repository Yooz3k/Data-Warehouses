using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class Cinema
    {
        public int Number { get; set; }
        public int SeatsUsual { get; set; }
        public int SeatsVIP { get; set; }
        public bool Is3DSupported { get; set; }
        public DateTime LastService { get; set; }
        public Cinema(int number, int seatsUsual, int seatsVIP, bool is3DSupported, DateTime lastService)
        {
            Number = number;
            SeatsUsual = seatsUsual;
            SeatsVIP = seatsVIP;
            Is3DSupported = is3DSupported;
            LastService = lastService;
        }
    }
}
