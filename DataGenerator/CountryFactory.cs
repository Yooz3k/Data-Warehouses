using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGenerator
{
    public class CountryFactory
    {
        public List<Country> countries { get; set; }

        public CountryFactory()
        {
            countries = new List<Country>();
            Country country;
            
            country = new Country(1, "Polska", 48);
            country.addCities("Warszawa", "Kraków", "Gdańsk", "Poznań", "Wrocław", "Szczecin", "Białystok", "Rzeszów", "Bydgoszcz", "Malbork");
            country.addStreets("ul. Sikorskiego", "ul. Mickiewicza", "ul. Sienkiewicza", "ul. Piłsudskiego", "ul. Armii Krajowej", "ul. Konopnickiej", "ul. Reymonta", "ul. Warszawska", "ul. Chrobrego", "ul. Sobieskiego");
            countries.Add(country);
            
            country = new Country(2, "Niemcy", 49);
            country.addCities("Berlin", "Monachium", "Frankfurt", "Hamburg", "Stuttgart", "Dortmund", "Erfurt", "Lipsk");
            country.addStreets("Goethestrasse", "Kohlstrasse", "Erstestrasse", "Bismarcksstrasse", "Hindenburgsstrasse", "Berlinerstrasse", "Wagnersstrasse");
            countries.Add(country);
            
            country = new Country(3, "Wielka Brytania", 44);
            country.addCities("Londyn", "Birmingham", "Liverpool", "Manchester", "Bristol", "Newcastle", "Sheffield", "Southampton", "Durham");
            country.addStreets("Norfolk St", "York St", "Mill Rd", "Stutton St", "Gilbert Rd", "Victoria Ave", "Hawkins Rd", "Scotland Rd", "St Philip's Rd", "Coleridge Rd");
            countries.Add(country);
            
            country = new Country(4, "USA", 1);
            country.addCities("Nowy Jork", "Los Angeles", "San Francisco", "Baltimore", "Dallas", "Seattle", "Miami", "Filadelfia", "Chicago");
            country.addStreets("W Pershing Rd", "S California Ave", "S Morgan St", "W Roosevelt Rd", "S Washington Ave", "W Pulaski Ave", "W Wilson Rd", "N Ashfield St", "N Kennedy Ave");
            countries.Add(country);
            
            country = new Country(5, "Kanada", 1);
            country.addCities("Montreal", "Toronto", "Vancouver", "Ottawa", "Calgary", "Quebec City");
            country.addStreets("Rogers Rd", "Oakwood Ave", "Annette St", "Davenport Rd", "Lawrance Ave", "Moore Ave", "Finch Ave");
            countries.Add(country);

            country = new Country(6, "Francja", 33);
            country.addCities("Paryż", "Nicea", "Marsylia", "Tuluza", "Lyon", "Bordeaux", "Montpellier", "Dijon", "Strasburg");
            country.addStreets("Rue de Medicis", "Rue Guisarde", "Rue Bonaparte", "Rue Lagrange", "Rue Censier", "Boulevard Arago", "Rue de la Glaciere", "Rue Bobillot");
            countries.Add(country);

            country = new Country(7, "Australia", 61);
            country.addCities("Melbourne", "Sydney", "Adelaide", "Brisbane", "Newcastle");
            country.addStreets("Clarendon St", "Pickles St", "Canterbury Rd", "Victoria Ave", "Alexandra Ave", "Stephenson St", "Palmer St", "Button St", "Murphy St", "Ricciardo St");
            countries.Add(country);

            country = new Country(8, "Czechy", 420);
            country.addCities("Praga", "Ostrawa", "Brno", "Ołomuniec", "Hradec Kralove");
            country.addStreets("Podebradova", "Hradecka", "Stefanikova", "Zborovska", "Dvorska", "Pospisilova", "Simkova", "Prazska");
            countries.Add(country);

            country = new Country(9, "Hiszpania", 34);
            country.addCities("Madryt", "Barcelona", "Sewilla", "Walencja", "Alicante", "Grenada", "Malaga", "Saragossa", "Murcja");
            country.addStreets("Calle Cartagena", "Calle Torre de Romo", "Av. de la Fama", "Av. de Santiago", "Av. de la Nora", "Camino Hondo", "Calle Campo");
            countries.Add(country);

            country = new Country(10, "Włochy", 39);
            country.addCities("Rzym", "Mediolan", "Turyn", "Florencja", "Wenecja", "Bolonia", "Neapol", "Palermo", "Bari", "Genua", "Udine");
            country.addStreets("Via Appia", "Via Santa Sofia", "Via Pavia", "Via Carlo Bazzi", "Via Lorenteggio", "Via Lombardia", "Via Montesanto", "Via Romea", "Via Enrico Mattei");
            countries.Add(country);
        }
    }
}