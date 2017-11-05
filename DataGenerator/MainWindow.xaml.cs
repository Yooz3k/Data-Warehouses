using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Database db;
        Diary d;
        Generator g;
        DateTime t0;
        DateTime t1;
        DateTime t2;
        BackgroundWorker downloadPeople = new BackgroundWorker();
        BackgroundWorker downloadFilms = new BackgroundWorker();
        BackgroundWorker generate = new BackgroundWorker();
        List<string> statuses = new List<string> { "Planowane", "Złożone", "Zrealizowane", "Anulowane" };


        public MainWindow()
        {
            db = new Database();
            d = new Diary();
            g = new Generator(db, d);

            downloadFilms.WorkerReportsProgress = true;
            downloadFilms.ProgressChanged += DownloadFilms_ProgressChanged;
            downloadFilms.WorkerSupportsCancellation = true;

            downloadPeople.WorkerSupportsCancellation = true;
            downloadPeople.WorkerReportsProgress = true;
            downloadPeople.ProgressChanged += DownloadPeople_ProgressChanged;

            InitializeComponent();

            t0DatePicker.DisplayDate = DateTime.Today.AddMonths(-1);
            t1DatePicker.DisplayDate = DateTime.Today;
            t2DatePicker.DisplayDate = DateTime.Today.AddDays(7);

            t0DatePicker.SelectedDate = DateTime.Today.AddMonths(-1);
            t1DatePicker.SelectedDate = DateTime.Today;
            t2DatePicker.SelectedDate = DateTime.Today.AddDays(7);
        }

        private void filmsDownloadButton_Click(object sender, RoutedEventArgs e)
        {
            filmsDownloadButton.IsEnabled = false;
            int amountOfFilms = int.Parse(filmsAmountOfTextBox.Text);
            int filmsCounter = 0;
            int pages = amountOfFilms / 10 + 1;
            string path = "filmy.txt";
            File.Create(path).Dispose();
            downloadFilms.RunWorkerCompleted += (s, args) => { filmsFileTextBlock.Text = path; filmsDownloadButton.IsEnabled = true; };

            if (!downloadFilms.IsBusy) downloadFilms.RunWorkerAsync();
            downloadFilms.DoWork += (s, args) =>
            {
                WebClient web = new WebClient();
                web.Encoding = Encoding.UTF8;
                downloadFilms.ReportProgress(0);
                for (int i = 1; i <= pages; i++)
                {
                    string filmweb = "http://www.filmweb.pl/search/film?page=" + i;
                    string page = web.DownloadString(filmweb);
                    HtmlDocument document = new HtmlDocument();
                    document.LoadHtml(WebUtility.HtmlDecode(page));
                    HtmlNodeCollection films = document.DocumentNode.SelectNodes("//div[@id='searchResult']/div/ul/li");

                    foreach (HtmlNode film in films)
                    {
                        if (filmsCounter < amountOfFilms)
                        {
                            //Pobranie zagranicznego tytułu
                            HtmlNode title = film.SelectSingleNode(".//span[@class='filmSubtitle']");
                            HtmlNode year;
                            HtmlNode time = film.SelectSingleNode(".//span[@class='filmTime']");
                            HtmlNode genre = film.SelectSingleNode(".//li[@class='filmGenres']/ul/li/a");
                            HtmlNode country = film.SelectSingleNode(".//li[@class='filmCountries']/ul/li/a");

                            if (title == null)
                            {
                                //Kiedy nie ma zagranicznego tytulu pobierz polski
                                title = film.SelectSingleNode(".//a[@class='filmTitle gwt-filmPage']");
                                year = film.SelectSingleNode(".//span[@class='titleYear']");
                            }
                            else
                            {
                                //Kiedy jest zagraniczny tytul rozdziel rok
                                year = title.SelectSingleNode(".//span[@class='infoYear']");
                                title.RemoveChild(year);
                            }

                            int duration;
                            string[] timeString = time.InnerText.Trim().Split(' ');
                            if (timeString[1].Equals("min."))
                            {
                                duration = int.Parse(timeString[0]);
                            }
                            else
                            {
                                duration = int.Parse(timeString[0]) * 60;
                            }
                            if (timeString.Length > 2)
                            {
                                duration += int.Parse(timeString[2]);
                            }

                            filmsCounter++;
                            string text = (title.InnerText.Trim() + ";" + year.InnerText.Replace("(", "").Replace(")", "").Trim() + ";" + duration + ";" + genre.InnerText.Trim() + ";" + country.InnerText.Trim() + "\r\n");
                            File.AppendAllText(path, text.ToString());
                            downloadFilms.ReportProgress((int)((filmsCounter / (double)amountOfFilms) * 100));

                        }
                    }
                }
            };
        }

        private void DownloadFilms_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.filmsDownloadProgressBar.Value = e.ProgressPercentage;
        }

        private void peopleDownloadButton_Click(object sender, RoutedEventArgs e)
        {
            peopleDownloadButton.IsEnabled = false;
            int amountOfPeople = int.Parse(peopleAmountOfTextBox.Text);
            int peopleCounter = 0;
            int pages = amountOfPeople / 10 + 1;

            string path = "czlonkowie_obsady.txt";
            File.Create(path).Dispose();
            downloadPeople.RunWorkerCompleted += (s, args) => { peopleFileTextBlock.Text = path; peopleDownloadButton.IsEnabled = true; };


            while (downloadPeople.IsBusy) ;
            downloadPeople.RunWorkerAsync();
            downloadPeople.DoWork += (s, args) =>
            {
                downloadPeople.ReportProgress(0);
                WebClient web = new WebClient();
                web.Encoding = Encoding.UTF8;
                int i = 1;
                while (peopleCounter < amountOfPeople)
                {
                    string filmweb = "http://www.filmweb.pl/search/person?sex=" + (i % 2 + 1) + "&page=" + i;
                    string page = web.DownloadString(filmweb);

                    HtmlDocument document = new HtmlDocument();
                    document.LoadHtml(WebUtility.HtmlDecode(page));

                    HtmlNodeCollection people = document.DocumentNode.SelectNodes("//div[@id='searchResult']/div/ul/li");
                    string sex;
                    if (i % 2 == 0) sex = "Kobieta";
                    else sex = "Mężczyzna";

                    foreach (HtmlNode person in people)
                    {
                        if (peopleCounter < amountOfPeople)
                        {
                            string[] name = person.SelectSingleNode(".//a[@class='hdr hdr-medium hitTitle']").InnerText.Trim().Split(' ');
                            string text;
                            if (name.Length > 1) text = (name[0] + ";" + name[1] + ";" + sex + "\r\n");
                            else text = (name[0] + ";" + ";" + sex + "\r\n");
                            File.AppendAllText(path, text.ToString());
                            peopleCounter++;
                            downloadPeople.ReportProgress((int)((peopleCounter / (double)amountOfPeople) * 100));
                        }
                    }
                    i++;
                }
            };
        }

        private void DownloadPeople_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.peopleDownloadProgressBar.Value = e.ProgressPercentage;
        }


        private string getFileDialog(string failed)
        {
            System.Windows.Forms.OpenFileDialog fileDialog = new System.Windows.Forms.OpenFileDialog();
            System.Windows.Forms.DialogResult result = fileDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                return fileDialog.FileName;
            }
            else return failed;
        }

        private void filmsFileButton_Click(object sender, RoutedEventArgs e)
        {
            this.filmsFileTextBlock.Text = getFileDialog(filmsFileTextBlock.Text);
        }

        private void peopleFileButton_Click(object sender, RoutedEventArgs e)
        {
            this.peopleFileTextBlock.Text = getFileDialog(peopleFileTextBlock.Text);
        }

        private void translationButton_Click(object sender, RoutedEventArgs e)
        {
            this.translationTextBlock.Text = getFileDialog(translationTextBlock.Text);
        }

        private void jobsButton_Click(object sender, RoutedEventArgs e)
        {
            this.jobsTextBlock.Text = getFileDialog(jobsTextBlock.Text);
        }

        private void distributorsButton_Click(object sender, RoutedEventArgs e)
        {
            this.distributorsTextBlock.Text = getFileDialog(distributorsTextBlock.Text);
        }

        private void generatorButton_Click(object sender, RoutedEventArgs e)
        {
            t0 = t0DatePicker.DisplayDate;
            t1 = t1DatePicker.DisplayDate;
            t2 = t2DatePicker.DisplayDate;

            statusLabel.Content = "Generowanie...";


            insertFiles();
            generateProgress(5);
            //Memberships
            List<Person> usesPeople = new List<Person>();
            foreach (Film film in db.Films)
            {
                foreach (Job job in db.Jobs)
                {
                    int personID = g.nextID(db.People);
                    db.Memberships.Add(new Membership(personID, film.ISAN, job.ID));
                    usesPeople.Add(g.getFromID(personID, db.People));
                }
            }
            foreach (Person p in db.People)
            {
                if (!usesPeople.Contains(p))
                {
                    db.Memberships.Add(new Membership(p.ID, db.Films[g.nextInt(db.Films.Count)].ISAN, g.nextID(db.Jobs)));
                }
            }
            generateProgress(10);
            createOrders(t0, t1, db.Films);
            generateProgress(20);
            generateSeances(t0, t1);
            generateProgress(30);

            /*******************************T2******************************/

            if (t2CheckBox.IsChecked == true)
            {
                //+zmiana daty przeglądu w 3 salach(między T1 i T2)
                //? zmiana liczby miejsc w sali albo stosunku miejsc zwykłych i VIP
                //Cinemas
                int cinemasUpdate = int.Parse(amountOfCinemaT2TextBox.Text);

                for (int i = 1; i <= cinemasUpdate; i++)
                {
                    Cinema c = g.getFromID(i, db.Cinemas);
                    DateTime lastService = g.nextDateBeetween(t1, t2);
                    c.LastService = lastService;
                    int newVIP = g.nextInt(c.SeatsUsual / 2);
                    c.SeatsVIP += newVIP;
                    c.SeatsUsual -= newVIP;
                }
                generateProgress(35);

                //?kilkunastu nowych członków obsady - teraz trzeba im przypisać filmy CZŁONKOSTWO
                int amountOfPeople = int.Parse(peopleAmountOfT2TextBox.Text);
                int amountOfPeopleSkip = db.People.Count;
                readPeople(amountOfPeople, db.People.Count);
                generateProgress(45);
                
                //+ kilka nowych filmów
                //Films - gatunki na podstawie filmów :D
                int amountOfFilms = int.Parse(filmsAmountOfT2TextBox.Text);
                int amountOfFilmsSkip = db.Films.Count;
                readFilms(amountOfFilms, amountOfFilmsSkip);
                generateProgress(55);

                //Membership
                List<Film> newFilms = db.Films.GetRange(amountOfFilmsSkip, db.Films.Count - amountOfFilmsSkip);
                foreach (Film film in newFilms)
                {
                    foreach (Job job in db.Jobs)
                    {
                        int personID = g.nextInt(amountOfPeopleSkip, db.People.Count);
                        db.Memberships.Add(new Membership(personID, film.ISAN, job.ID));
                        usesPeople.Add(g.getFromID(personID, db.People));
                    }
                }
                foreach (Person p in db.People)
                {
                    if (!usesPeople.Contains(p))
                    {
                        db.Memberships.Add(new Membership(p.ID, db.Films[g.nextInt(db.Films.Count)].ISAN, g.nextID(db.Jobs)));
                    }
                }
                generateProgress(60);

                //+ dodanie kilku zamówień (tu musimy połączyć daty zamówień i premiery nowych filmów)
                createOrders(t1, t2, newFilms);
                generateProgress(70);
                //+kilkadziesiąt(ile wyjdzie w tym czasie) seansów
                generateSeances(t1, t2);
                generateProgress(80);

                // ? zmiana numeru telefonu lub siedziby kilku dystrybutorów
                int distributorsUpdate = int.Parse(amountOfDistributorsT2TextBox.Text);
                for (int i = 1; i <= distributorsUpdate; i++)
                {
                    Distributor d = g.getFromID(i, db.Distributors);
                    d.PhoneNumber = g.nextPhoneNumber(d.Country);
                }
                generateProgress(85);

                //+zmiana statusu jakiegoś zamówienia(np.anulowanie takiego, które wcześniej było złożone)
                int ordersUpdate = int.Parse(amountOfOrdersT2TextBox.Text);
                int orderID = 1;
                while (orderID < ordersUpdate && orderID < d.Orders.Count)
                {
                    Order o = g.getFromID(orderID, d.Orders);
                    if (o.Status.Status.Equals("Złożone"))
                    {
                        o.Status.Status = "Anulowane";
                        o.Status.Date = g.nextDateBeetween(t1, t2);
                        
                    }
                    orderID++;
                }

                generateProgress(90);
            }

            d.export("dziennik.xml");
            generateProgress(93);

            //T2
            db.export();
            generateProgress(100);
            this.statusLabel.Content = "Gotowe!";
        }

        private void generateProgress(int value)
        {
            this.statusLabel.Content = value;
        }



        /********************FUNCTIONS**********************/

        private void insertFiles()
        {
            StreamReader sr;
            string path;

            //Translations
            path = translationTextBlock.Text;
            sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                db.Translations.Add(new Translation(db.Translations.Count + 1, line));
            }

            //Jobs
            path = jobsTextBlock.Text;
            sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                db.Jobs.Add(new Job(db.Jobs.Count + 1, line));
            }

            //Cinemas
            int cinemas = int.Parse(amountOfCinemaTextBox.Text);

            for (int i = 1; i <= cinemas; i++)
            {
                int seatsUsual = g.nextInt(50, 300);
                int seatsVIP = g.nextInt(seatsUsual);
                DateTime lastService = g.nextDateBeetween(t0, t1);
                bool is3DSupported = g.nextBool();
                Cinema c = new Cinema(i, seatsUsual, seatsVIP, is3DSupported, lastService);
                db.Cinemas.Add(c);
            }

            //Dist
            path = distributorsTextBlock.Text;
            sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                Country c = g.nextCountry();
                string address = g.nextCity(c) + " " + g.nextStreet(c) + " " + g.nextInt(1, 200);
                db.Distributors.Add(new Distributor(db.Distributors.Count + 1, line, g.nextPhoneNumber(c), c, address));
            }

            int amountOfPeople = int.Parse(peopleAmountOfTextBox.Text);
            readPeople(amountOfPeople, 0);

            int amountOfFilms = int.Parse(filmsAmountOfTextBox.Text);
            readFilms(amountOfFilms, 0);

        }

        List<string> genres = new List<string>();
        private void readFilms(int amountOfFilms, int amountOfFilmsSkip)
        {
            StreamReader sr;
            string path;
            //Films - gatunki na podstawie filmów
            path = filmsFileTextBlock.Text;
            sr = new StreamReader(path);
            for (int i = 0; i < amountOfFilmsSkip; i++) sr.ReadLine();
            for (int i = 0; i < amountOfFilms; i++)
            {
                string line = sr.ReadLine();
                string[] fields = line.Split(';');
                bool isOscar = g.nextBool();
                if (!genres.Contains(fields[3]))
                {
                    genres.Add(fields[3]);
                    db.Genres.Add(new Genre(db.Genres.Count + 1, fields[3]));
                }
                int genreID = 0;
                foreach (Genre g in db.Genres)
                {
                    if (g.Name.Equals(fields[3])) genreID = g.ID;
                }
                db.Films.Add(new Film(
                    g.nextISAN(db.Films.Count + 1),
                    fields[0],
                    int.Parse(fields[2]),
                    g.nextDateInYear(int.Parse(fields[1])),
                    isOscar ? g.nextInt(50) : g.nextInt(5),
                    isOscar,
                    g.nextDistributor(),
                    genreID));

            }
        }
        private void readPeople(int amountOfPeople, int amountOfPeopleSkip)
        {
            StreamReader sr;
            string path;
            //People
            path = peopleFileTextBlock.Text;
            sr = new StreamReader(path);
            for (int i = 0; i < amountOfPeopleSkip; i++) sr.ReadLine();
            for (int i = 0; i < amountOfPeople; i++)
            {
                string line = sr.ReadLine();
                string[] fields = line.Split(';');
                db.People.Add(new Person(db.People.Count + 1, fields[0], fields[1], fields[2], g.nextBool()));
            }
        }
        private void generateSeances(DateTime start, DateTime end)
        {
            //Seances
            int numberOfSeancesPerCinema = int.Parse(amountOfSeancesTextBox.Text);
            int hourOpen = int.Parse(hourOpenTextBox.Text);
            int hourClose = int.Parse(hourCloseTextBox.Text);
            DateTime open = start.AddHours(hourOpen);
            DateTime close = start.AddHours(hourClose);
            if (hourClose < hourOpen) close = start.AddDays(1);

            while (open < end)
            {

                foreach (Cinema c in db.Cinemas)
                {
                    DateTime now = open;
                    int numberOfSeances = 0;
                    List<Film> avaiableFilms = new List<Film>();
                    foreach (Film f in db.Films)
                    {
                        if (d.isFilmExploitable(f.ISAN, open)) { avaiableFilms.Add(f); }
                    }

                    if (avaiableFilms.Count > 0)
                    {
                        Film f = avaiableFilms[g.nextInt(avaiableFilms.Count)];
                        while (numberOfSeances < numberOfSeancesPerCinema && now.AddMinutes(f.Duration + 15) < close)
                        {
                            bool is3D = c.Is3DSupported ? g.nextBool() : false;
                            int att = g.nextInt(c.SeatsUsual + c.SeatsVIP);
                            int translation = g.nextID(db.Translations);
                            Seance s = new Seance(db.Seances.Count + 1, is3D, att * 15, now, now.TimeOfDay, att, f.ISAN, translation, c.Number);
                            db.Seances.Add(s);
                            now = now.AddMinutes(f.Duration + 15);
                            f = avaiableFilms[g.nextInt(avaiableFilms.Count)];
                            numberOfSeances++;
                        }
                    }
                }
                open = open.AddDays(1);
                close = close.AddDays(1);
            }
        }
        private void createOrders(DateTime start, DateTime end, List<Film> films)
        {
            //Orders
            DateTime statusDate = new DateTime();

            // 0 planowane
            // 1 złożone
            // 2 zrealizowane
            // 3 anulowane
            foreach (Film f in films)
            {
                int expiredLicense = (g.nextInt(4) + 1) * 7;
                double price = g.nextInt(1000, 3000) * expiredLicense;
                bool isDiscount = g.nextBool();

                int status = g.nextInt(statuses.Count);
                if (status == 2)
                {
                    DateTime date = g.nextDateBeetween(start, end);
                    if (f.PremiereDate < date) statusDate = date;
                    else
                    {
                        status = 1;
                        statusDate = g.nextDateBeetween(start, end);
                    }
                }
                else
                {
                    statusDate = g.nextDateBeetween(start, end);
                }
                //Uwagi
                string comment = "";
                int orderHasComment = g.nextInt(20); //5% szansy na uwagę
                if (orderHasComment == 0 && status != 1)
                {
                    comment = g.nextComment(status);
                }
                //jezeli wazność licencji upłyneła to jeszcze raz na rok wprzod
                d.Orders.Add(new Order(d.Orders.Count + 1, f.ISAN, price, isDiscount, statuses[status], statusDate, expiredLicense, comment, false));
            }

            //przedłużenie zamówienia
            int amountOfAddOrders = int.Parse(amountOfOrdersTextBox.Text);
            int addOrdersCounter = 0;
            List<Order> toRenewOrders = d.Orders.Where(o => (!o.Renewed && o.Status.Status.Equals("Zrealizowane"))).ToList();
            while (toRenewOrders.Count > 0 && addOrdersCounter < amountOfAddOrders)
            {
                foreach (Order o in toRenewOrders)
                {
                    statusDate = g.nextDateAfter(o.Status.Date.AddDays(o.LicenseExpiry), 4);
                    if (statusDate < end && addOrdersCounter < amountOfAddOrders)
                    {
                        int expiredLicense = (g.nextInt(3) + 1) * 7;
                        double price = g.nextInt(500, 1500) * expiredLicense;
                        bool isDiscount = g.nextBool();
                        int status = g.nextInt(statuses.Count);
                        o.Renewed = true;
                        d.Orders.Add(new Order(d.Orders.Count + 1, o.ISAN, price, isDiscount, statuses[status], statusDate, expiredLicense, "Odnowienie licencji", false));
                        addOrdersCounter++;
                    }
                }
                toRenewOrders = d.Orders.Where(o => (o.Status.Status.Equals("Zrealizowane") && !o.Renewed && statusDate < end)).ToList();
            }
            d.Orders.Sort((x, y) => x.Status.Date.CompareTo(y.Status.Date));
        }
    }
}