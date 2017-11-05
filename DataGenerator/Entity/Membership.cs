using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class Membership
    {
        public int PersonID { get; set; }
        public string FilmID { get; set; }
        public int JobID { get; set; }
        public Membership(int personID, string filmID, int jobID)
        {
            this.PersonID = personID;
            this.FilmID = filmID;
            this.JobID = jobID;
        }
    }
}