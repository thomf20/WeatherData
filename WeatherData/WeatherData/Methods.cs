using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics.Metrics;
using System.Globalization;

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
            Least_to_greatest_risk_of_mold

        }
        enum OutsideMenu
        {
            Average_temperature_and_humidity_per_day = 1,
            Hottest_to_Coldest,
            Driest_to_moistest,
            Least_to_greatest_risk_of_mold,
            Date_of_meteorological_Autumn,
            Date_of_meteorological_winter
        }
        public static void RunMe()
        {
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
                    Console.WriteLine("You are inside now");
                    Inside();
                    break;
                case MainMenu.Outside:
                    Console.WriteLine("You are outside now");
                    Outside();
                    break;
            }
        }
        public static void Inside()
        {
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

                    break;
                case InsideMenu.Hottest_to_coldest:

                    break;
                case InsideMenu.Driest_to_moistest:

                    break;
                case InsideMenu.Least_to_greatest_risk_of_mold:

                    break;
            }
        }
        public static void Outside()
        {
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

                    break;
                case OutsideMenu.Driest_to_moistest:

                    break;
                case OutsideMenu.Least_to_greatest_risk_of_mold:

                    break;
                case OutsideMenu.Date_of_meteorological_Autumn:

                    break;
                case OutsideMenu.Date_of_meteorological_winter:

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
        public static void OutsideAvrageTempAndHumidity()
        {

            Console.WriteLine("Please enter a date you would like to see (yyyy-mm-dd)");
            string userInput = Console.ReadLine();

            Regex regex = new Regex(@"^" + userInput + ".*(?<=Ute,).*");

            CreateCorrectData(userInput);











            //string path = "../../../../WeatherData/Files/TextFile1.txt";
            //Regex regex = new Regex(@"^" + userInput + ".*(?<=Ute,).*");

            //string[] lines = File.ReadAllLines(path);
            //bool dateFound = false;

            //string[] splitArray = lines.ToString().Split(',');
            //Console.WriteLine(splitArray[0]);

            //foreach (string line in lines)
            //{
            //    if (regex.IsMatch(line))
            //    {
            //        Console.WriteLine(line);
            //        dateFound = true;

            //    }
            //}
            //if (!dateFound)
            //{
            //    Console.WriteLine("No data found for the specified date.");
            //    OutsideAvrageTempAndHumidity();
            //}
        }
        public static List<WeatherData> CreateCorrectData(string userinput)
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
    }
}
