using DataGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class Film
    {
        public string ISAN { get; set; }
        public string Title { get; set; }
        public int Duration { get; set; }
        public DateTime PremiereDate { get; set; }
        public int AmountOfAward { get; set; }
        public bool IsOscar { get; set; }
        public int DistributorID { get; set; }
        public int GenreID { get; set; }

        public Film(string ISAN, string title, int duration, DateTime premiereDate, int amountOfAward, bool isOscar, int distributorID, int genreID)
        {
            this.ISAN = ISAN;
            Title = title;
            Duration = duration;
            PremiereDate = premiereDate;
            AmountOfAward = amountOfAward;
            IsOscar = isOscar;
            DistributorID = distributorID;
            GenreID = genreID;
        }
    }
}