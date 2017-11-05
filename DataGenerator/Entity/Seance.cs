using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class Seance
    {
        public int ID { get; set; }
        public bool Is3D { get; set; }
        public int Income { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int Attendance { get; set; }
        public string FilmID { get; set; }
        public int TranslationID { get; set; }
        public int CinemaID { get; set; }

        public Seance(int ID, bool is3D, int income, DateTime date, TimeSpan time,
            int attendance, string filmID, int translationID, int cinemaID)
        {
            this.ID = ID;
            Is3D = is3D;
            Income = income;
            Date = date;
            Time = time;
            Attendance = attendance;
            FilmID = filmID;
            TranslationID = translationID;
            CinemaID = cinemaID;
        }
    }
}