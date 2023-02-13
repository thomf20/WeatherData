using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace WeatherData
{
    internal class Methods
    {
        enum MainMenu
        {
            Inside = 1,
            Outside = 2
        }
        enum InsideMenu
        {

            Average_temperature_for_selected_date = 1,
            Hottest_to_coldest,
            Driest_to_moistest,
            Least_to_greatest_risk_of_mold,
            Go_Back

        }
        enum OutsideMenu
        {
            Average_temperature_and_humidity_per_day = 1,
            Hottest_to_Coldest,
            Driest_to_moistest,
            Least_to_greatest_risk_of_mold,
            Date_of_meteorological_Autumn,
            Date_of_meteorological_winter,
            Go_Back
        }
        //ADMIN CODE
        public static void RunMe()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Mohammed's & Thom's weatherdata app");
            Console.WriteLine("Please choose one of the options below");
            foreach (int c in Enum.GetValues(typeof(MainMenu)))
            {
                Console.WriteLine($"{c}: " + $"{Enum.GetName(typeof(MainMenu), c).Replace('_', ' ')}");
            }

            MainMenu mainmenu = (MainMenu)99;
            int number;
            if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out number))
            {
                mainmenu = (MainMenu)number;
                Console.Clear();
            }
            switch (mainmenu)
            {
                case MainMenu.Inside:
                    Inside();
                    break;
                case MainMenu.Outside:
                    Outside();
                    break;
            }
        }
        public static void Pattern()
        {
            //  ([12]\d{ 3}-(0[6 - 9] | 1[0 - 2]) - (0[1 - 9] |[12]\d | 3[01]))     -TAR BORT MAJ
            // (2016-05|2017-01)    -matcher det vi inte villmatcha
            //      ^(2017-(0[2-9]|[2-9][0-9])|2016-(0[0-4]|0[6-9]|[1-9][0-9])).* - matchar det vi vill.
            // @"^" + userInput + ".*(?<=Ute,).*"
            // @"^" + userInput + ".*(?<=Inne,).*"
            // @"^" + userInput + ".*"
            // @"(\d+\.\d+),(\d+)" - matchar temp samt luftfuktighet.
        }
        public static void CalculateMatches()
        {
            string path = "../../../../WeatherData/Files/TextFile1.txt";

            Regex regex = new Regex("^(2017-(0[2-9]|[2-9][0-9])|2016-(0[0-4]|0[6-9]|[1-9][0-9])).*");

            string[] lines = File.ReadAllLines(path);

            foreach (string match in lines)
            {
                Console.WriteLine($"{match} matchar med {regex}: " + (regex.IsMatch(match) ? "Korrekt" : "Icke korrekt"));
            }

        }
        public static List<WeatherData> CreateCorrectData()
        {
            string path = "../../../../WeatherData/Files/TextFile1.txt";
            List<WeatherData> weatherDataList = new List<WeatherData>();

            using (StreamReader sr = new StreamReader(path))
            {
                List<string> dataList = new List<string>();
                dataList = File.ReadAllLines(path).ToList();

                

                string[] parts;

                foreach (string data in dataList)
                {
                    parts = data.Split(' ', ',');

                    string dateTime;
                    string time;
                    string location;
                    double temp;
                    int moist;

                    dateTime = parts[0];
                    time = parts[1];
                    location = parts[2];
                    temp = Convert.ToDouble(parts[3].Replace('.', ','));
                    moist = Convert.ToInt32(parts[4]);

                    WeatherData weatherData = new WeatherData
                    {
                        Datetime = dateTime,
                        Time = time,
                        Location = location,
                        Temprature = temp,
                        Moist = moist
                    };
                    weatherDataList.Add(weatherData);

                }
            }
            return weatherDataList;
        }

        //INSIDE CODE
        public static void Inside()
        {
            Console.Clear();
            foreach (int c in Enum.GetValues(typeof(InsideMenu)))
            {
                Console.WriteLine($"{c}: " + $"{Enum.GetName(typeof(InsideMenu), c).Replace('_', ' ')}");
            }

            InsideMenu insideMenu = (InsideMenu)99;
            int number;
            if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out number))
            {
                insideMenu = (InsideMenu)number;
                Console.Clear();
            }
            switch (insideMenu)
            {
                case InsideMenu.Average_temperature_for_selected_date:
                    Methods.InsideTemp();
                    break;
                case InsideMenu.Hottest_to_coldest:
                    HottestToColdestInside();
                    break;
                case InsideMenu.Driest_to_moistest:
                    DriestToMoistiestsInside();
                    break;
                case InsideMenu.Least_to_greatest_risk_of_mold:

                    break;
                case InsideMenu.Go_Back:
                    RunMe();
                    break;
            }
            Console.WriteLine("Please enter X to go back to the menu");
            string UserInput = Console.ReadLine();
            if (UserInput == "X" || UserInput == "x")
            {
                RunMe();
            }
        }
        public static void InsideTemp()
        {
            Console.Clear();
            Console.WriteLine("Please enter a date you would like to see (yyyy-mm-dd)");
            string userInput = Console.ReadLine();

            string path = "../../../../WeatherData/Files/TextFile1.txt";
            List<WeatherData> weatherDataList = new List<WeatherData>();

            using (StreamReader sr = new StreamReader(path))
            {
                List<string> dataList = new List<string>();
                dataList = File.ReadAllLines(path).ToList();

                string[] parts;

                foreach (string data in dataList)
                {
                    parts = data.Split(' ', ',');

                    string dateTime = parts[0];
                    string time = parts[1];
                    string location = parts[2];
                    double temp = Convert.ToDouble(parts[3].Replace('.', ','));
                    int moist = Convert.ToInt32(parts[4]);

                    WeatherData weatherData = new WeatherData
                    {
                        Datetime = dateTime,
                        Time = time,
                        Location = location,
                        Temprature = temp,
                        Moist = moist
                    };
                    weatherDataList.Add(weatherData);
                }

                var hittaDatum = (from t in weatherDataList
                                  where t.Datetime.Contains(userInput) &&
                                  t.Location.Contains("Inne")
                                  select t);

                var tempAve = hittaDatum.Average(a => a.Temprature);

                foreach (var h in hittaDatum)
                {
                    Console.WriteLine(h.Datetime + " är medeltemperaturen: " + Math.Round(tempAve, 2) + " grader celsius " + h.Location);
                    break;
                }
                Console.WriteLine("Please enter X to go back to the menu");
                string UserInput = Console.ReadLine();
                if (UserInput == "X" || UserInput == "x")
                {
                    Inside();
                }
            }
        }
        public static void HottestToColdestInside()
        {
            Console.Clear();
            string path = "../../../../WeatherData/Files/TextFile1.txt";
            List<WeatherData> weatherDataList = new List<WeatherData>();

            using (StreamReader sr = new StreamReader(path))
            {
                List<string> dataList = new List<string>();
                dataList = File.ReadAllLines(path).ToList();

                string[] parts;

                foreach (string data in dataList)
                {
                    parts = data.Split(' ', ',');

                    string dateTime = parts[0];
                    string time = parts[1];
                    string location = parts[2];
                    double temp = Convert.ToDouble(parts[3].Replace('.', ','));
                    int moist = Convert.ToInt32(parts[4]);

                    WeatherData weatherData = new WeatherData
                    {
                        Datetime = dateTime,
                        Time = time,
                        Location = location,
                        Temprature = temp,
                        Moist = moist
                    };
                    weatherDataList.Add(weatherData);
                }

                var sortedList = (from t in weatherDataList
                                  where t.Location.Contains("Inne")
                                  group t by t.Datetime into g
                                  orderby g.Average(a => a.Temprature) descending
                                  select new { Date = g.Key, AverageTemp = Math.Round(g.Average(a => a.Temprature), 2) });

                Console.WriteLine("Varmaste dagarna i ordning:");

                foreach (var sorted in sortedList)
                {
                    Console.WriteLine(sorted.Date + " har en medeltemperatur på: " + sorted.AverageTemp + " grader celsius");
                }

                Console.WriteLine("Please enter X to go back to the menu");
                string UserInput = Console.ReadLine();
                if (UserInput == "X" || UserInput == "x")
                {
                    Inside();
                }

                //using (StreamWriter streamWriter = new StreamWriter("../../../../WeatherData/Files/newfile.txt", true))
                //{
                //    streamWriter.WriteLine("Medeltemperatur inomhus: ");
                //    foreach (var s in sortedList)
                //    {
                //        streamWriter.WriteLine(s.Date + " har en medeltemperatur på " + s.AverageTemp + " grader celsius");
                //    }
                //    streamWriter.WriteLine("---------------------------------------------------------------------");
                //}
            }
        }
        public static void DriestToMoistiestsInside()
        {
            Console.Clear();
            string path = "../../../../WeatherData/Files/TextFile1.txt";
            List<WeatherData> weatherDataList = new List<WeatherData>();

            using (StreamReader sr = new StreamReader(path))
            {
                List<string> dataList = new List<string>();
                dataList = File.ReadAllLines(path).ToList();

                string[] parts;

                foreach (string data in dataList)
                {
                    parts = data.Split(' ', ',');

                    string dateTime = parts[0];
                    string time = parts[1];
                    string location = parts[2];
                    double temp = Convert.ToDouble(parts[3].Replace('.', ','));
                    int moist = Convert.ToInt32(parts[4]);

                    WeatherData weatherData = new WeatherData
                    {
                        Datetime = dateTime,
                        Time = time,
                        Location = location,
                        Temprature = temp,
                        Moist = moist
                    };
                    weatherDataList.Add(weatherData);
                }

                var sortedList = (from t in weatherDataList
                                  where t.Location.Contains("Inne")
                                  group t by t.Datetime into g
                                  orderby g.Average(a => a.Moist) descending
                                  select new { Date = g.Key, AverageMoist = Math.Round(g.Average(a => a.Moist), 2) });

                Console.WriteLine("Fuktigaste dagarna i ordning:");

                foreach (var sorted in sortedList)
                {
                    Console.WriteLine(sorted.Date + " har en medelluftfuktighet " + sorted.AverageMoist + " %");
                }
                using (StreamWriter streamWriter = new StreamWriter("../../../../WeatherData/Files/newfile.txt", true))
                {
                    streamWriter.WriteLine("Medelluftfuktigheten inomhus: \n");
                    foreach (var s in sortedList)
                    {
                        streamWriter.WriteLine(s.Date + " har en medelluftfuktighet " + s.AverageMoist + " %");
                    }
                    streamWriter.WriteLine("---------------------------------------------------------------------");
                }

                Console.WriteLine("Please enter X to go back to the menu");
                string UserInput = Console.ReadLine();
                if (UserInput == "X" || UserInput == "x")
                {
                    Inside();
                }
            }
        }
        public static void OutsideAvrageTempAndHumidity()
        {
            Console.Clear();
            Console.WriteLine("Please enter a date you would like to see (yyyy-mm-dd)");
            string userInput = Console.ReadLine();

            string path = "../../../../WeatherData/Files/TextFile1.txt";
            List<WeatherData> weatherDataList = new List<WeatherData>();

            using (StreamReader sr = new StreamReader(path))
            {
                List<string> dataList = new List<string>();
                dataList = File.ReadAllLines(path).ToList();

                string[] parts;

                foreach (string data in dataList)
                {
                    parts = data.Split(' ', ',');

                    string dateTime = parts[0];
                    string time = parts[1];
                    string location = parts[2];
                    double temp = Convert.ToDouble(parts[3].Replace('.', ','));
                    int moist = Convert.ToInt32(parts[4]);

                    WeatherData weatherData = new WeatherData
                    {
                        Datetime = dateTime,
                        Time = time,
                        Location = location,
                        Temprature = temp,
                        Moist = moist
                    };
                    weatherDataList.Add(weatherData);
                }

                var hittaDatum = (from t in weatherDataList
                                  where t.Datetime.Contains(userInput) &&
                                  t.Location.Contains("Ute")
                                  select t);

                var tempAve = hittaDatum.Average(a => a.Temprature);
                var moistAve = hittaDatum.Average(a => a.Moist);

                foreach (var h in hittaDatum)
                {
                    Console.WriteLine(h.Datetime + " är medeltemperaturen: " + Math.Round(tempAve, 2) + " grader celsius " + h.Location);
                    Console.WriteLine("Medelluftfuktigheten är: " + Math.Round(moistAve));
                    break;
                }
            }
        }

        //OUTSIDE CODE
        public static void Outside()
        {
            Console.Clear();
            foreach (int c in Enum.GetValues(typeof(OutsideMenu)))
            {
                Console.WriteLine($"{c}: " + $"{Enum.GetName(typeof(OutsideMenu), c).Replace('_', ' ')}");
            }

            OutsideMenu outsideMenu = (OutsideMenu)99;
            int number;
            if (int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out number))
            {
                outsideMenu = (OutsideMenu)number;
                Console.Clear();
            }
            switch (outsideMenu)
            {
                case OutsideMenu.Average_temperature_and_humidity_per_day:
                    OutsideAvrageTempAndHumidity();
                    break;
                case OutsideMenu.Hottest_to_Coldest:
                    HottestToColdestOuside();
                    break;
                case OutsideMenu.Driest_to_moistest:
                    DriestToMoistiestsOutside();
                    break;
                case OutsideMenu.Least_to_greatest_risk_of_mold:

                    break;
                case OutsideMenu.Date_of_meteorological_Autumn:
                    DateAutumn();
                    break;
                case OutsideMenu.Date_of_meteorological_winter:
                    DateWinter();
                    break;
                case OutsideMenu.Go_Back:
                    RunMe();
                    break;
            }
        }
        public static void HottestToColdestOuside()
        {
            Console.Clear();
            string path = "../../../../WeatherData/Files/TextFile1.txt";
            List<WeatherData> weatherDataList = new List<WeatherData>();

            using (StreamReader sr = new StreamReader(path))
            {
                List<string> dataList = new List<string>();
                dataList = File.ReadAllLines(path).ToList();

                string[] parts;

                foreach (string data in dataList)
                {
                    parts = data.Split(' ', ',');

                    string dateTime = parts[0];
                    string time = parts[1];
                    string location = parts[2];
                    double temp = Convert.ToDouble(parts[3].Replace('.', ','));
                    int moist = Convert.ToInt32(parts[4]);

                    WeatherData weatherData = new WeatherData
                    {
                        Datetime = dateTime,
                        Time = time,
                        Location = location,
                        Temprature = temp,
                        Moist = moist
                    };
                    weatherDataList.Add(weatherData);
                }

                var sortedList = (from t in weatherDataList
                                  where t.Location.Contains("Ute")
                                  group t by t.Datetime into g
                                  orderby g.Average(a => a.Temprature) descending
                                  select new { Date = g.Key, AverageTemp = Math.Round(g.Average(a => a.Temprature), 2) });

                Console.WriteLine("Varmaste dagarna i ordning:");

                foreach (var sorted in sortedList)
                {
                    Console.WriteLine(sorted.Date + " har en medeltemperatur på: " + sorted.AverageTemp + " grader celsius");
                }
                Console.WriteLine("Please enter X to go back to the menu");
                string UserInput = Console.ReadLine();
                if (UserInput == "X" || UserInput == "x")
                {
                    Outside();
                }
                //using (StreamWriter writer = new StreamWriter("../../../../WeatherData/Files/newfile.txt"))
                //{
                //    writer.WriteLine("Medeltemperatur utomhus: \n");
                //    foreach (var sorted in sortedList)
                //    {
                //        writer.WriteLine(sorted.Date + " har en medeltemperatur på: " + sorted.AverageTemp + " grader celsius");
                //    }
                //    writer.WriteLine("---------------------------------------------------------------------");
                //}
            }
        }
        public static void DriestToMoistiestsOutside()
        {
            Console.Clear();
            string path = "../../../../WeatherData/Files/TextFile1.txt";
            List<WeatherData> weatherDataList = new List<WeatherData>();

            using (StreamReader sr = new StreamReader(path))
            {
                List<string> dataList = new List<string>();
                dataList = File.ReadAllLines(path).ToList();

                string[] parts;

                foreach (string data in dataList)
                {
                    parts = data.Split(' ', ',');

                    string dateTime = parts[0];
                    string time = parts[1];
                    string location = parts[2];
                    double temp = Convert.ToDouble(parts[3].Replace('.', ','));
                    int moist = Convert.ToInt32(parts[4]);

                    WeatherData weatherData = new WeatherData
                    {
                        Datetime = dateTime,
                        Time = time,
                        Location = location,
                        Temprature = temp,
                        Moist = moist
                    };
                    weatherDataList.Add(weatherData);
                }

                var sortedList = (from t in weatherDataList
                                  where t.Location.Contains("Ute")
                                  group t by t.Datetime into g
                                  orderby g.Average(a => a.Moist) descending
                                  select new { Date = g.Key, AverageMoist = Math.Round(g.Average(a => a.Moist), 2) });

                Console.WriteLine("Fuktigaste dagarna i ordning:");

                foreach (var sorted in sortedList)
                {
                    Console.WriteLine(sorted.Date + " har en medelluftfuktighet " + sorted.AverageMoist + " %");
                }
                Console.WriteLine("Please enter X to go back to the menu");
                string UserInput = Console.ReadLine();
                if (UserInput == "X" || UserInput == "x")
                {
                    Outside();
                }
                //using (StreamWriter streamWriter = new StreamWriter("../../../../WeatherData/Files/newfile.txt", true))
                //{
                //    streamWriter.WriteLine("Medelluftfuktigheten utomhus: \n");
                //    foreach (var s in sortedList)
                //    {
                //        streamWriter.WriteLine(s.Date + " har en medelluftfuktighet " + s.AverageMoist + " %");
                //    }
                //    streamWriter.WriteLine("---------------------------------------------------------------------");
                //}
            }
        }
        public static void DateAutumn()
        {
            string path = "../../../../WeatherData/Files/TextFile1.txt";
            List<WeatherData> weatherDataList = new List<WeatherData>();

            using (StreamReader sr = new StreamReader(path))
            {
                List<string> dataList = new List<string>();
                dataList = File.ReadAllLines(path).ToList();

                string[] parts;

                foreach (string data in dataList)
                {
                    parts = data.Split(' ', ',');

                    string dateTime = parts[0];
                    string time = parts[1];
                    string location = parts[2];
                    double temp = Convert.ToDouble(parts[3].Replace('.', ','));
                    int moist = Convert.ToInt32(parts[4]);

                    WeatherData weatherData = new WeatherData
                    {
                        Datetime = dateTime,
                        Time = time,
                        Location = location,
                        Temprature = temp,
                        Moist = moist
                    };
                    weatherDataList.Add(weatherData);
                }

                var sortedList = (from t in weatherDataList
                                  where t.Location.Contains("Ute")
                                  group t by t.Datetime into g
                                  orderby g.Average(a => a.Temprature) descending
                                  select new { Date = g.Key, AverageTemp = Math.Round(g.Average(a => a.Temprature), 2) });

                foreach (var s in sortedList)
                {
                    Console.WriteLine(s.Date + " - " + s.AverageTemp);
                }


                int count = 0;
                foreach (var sorted in sortedList)
                {
                    if (sorted.AverageTemp <= 10)
                    {
                        count++;
                    }
                }

                Console.WriteLine("Antal dagar med temperaturer under eller lika med 0 grader: " + count);

                if (count < 5)
                {
                    Console.WriteLine("Det finns inte 5 dagar med temperaturer under eller lika med 0 grader.");
                }
            }
        }
        public static void DateWinter()
        {
            string path = "../../../../WeatherData/Files/TextFile1.txt";
            List<WeatherData> weatherDataList = new List<WeatherData>();

            using (StreamReader sr = new StreamReader(path))
            {
                List<string> dataList = new List<string>();
                dataList = File.ReadAllLines(path).ToList();

                string[] parts;

                foreach (string data in dataList)
                {
                    parts = data.Split(' ', ',');

                    string dateTime = parts[0];
                    string time = parts[1];
                    string location = parts[2];
                    double temp = Convert.ToDouble(parts[3].Replace('.', ','));
                    int moist = Convert.ToInt32(parts[4]);

                    WeatherData weatherData = new WeatherData
                    {
                        Datetime = dateTime,
                        Time = time,
                        Location = location,
                        Temprature = temp,
                        Moist = moist
                    };
                    weatherDataList.Add(weatherData);
                }

                var sortedList = (from t in weatherDataList
                                  where t.Location.Contains("Ute")
                                  group t by t.Datetime into g
                                  orderby g.Average(a => a.Temprature) descending
                                  select new { Date = g.Key, AverageTemp = Math.Round(g.Average(a => a.Temprature), 2) });

                foreach (var s in sortedList)
                {
                    Console.WriteLine(s.Date + " - " + s.AverageTemp);
                }


                int count = 0;
                foreach (var sorted in sortedList)
                {
                    if (sorted.AverageTemp <= 0)
                    {
                        count++;
                    }
                }

                Console.WriteLine("Antal dagar med temperaturer under eller lika med 0 grader: " + count);

                if (count < 5)
                {
                    Console.WriteLine("Det finns inte 5 dagar med temperaturer under eller lika med 0 grader.");
                }
            }
        }
        public static void RiskForMold()
        {

        }
    }
}
