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
            switch(outsideMenu) 
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
            string path = "../../../../WeatherData/Files/TextFile1.txt";
            Console.WriteLine("Please enter a date you would like to see (yyyy-mm-dd)");
            string userInput = Console.ReadLine();

            Regex regex = new Regex(@"^" + userInput + ".*(?<=Ute,).*");
            
            string[] lines = File.ReadAllLines(path);
            bool dateFound = false;

            string[] splitArray = lines.ToString().Split(',');
            Console.WriteLine(splitArray[0]);

            foreach (string line in lines)
            {
                if (regex.IsMatch(line))
                {
                    Console.WriteLine(line);
                    dateFound = true;

                }
            }
            if (!dateFound)
            {
                Console.WriteLine("No data found for the specified date.");
                OutsideAvrageTempAndHumidity();
            }
        }
        public static void Testing()
        {
            //string path = "../../../../WeatherData/Files/TextFile1.txt";
            //Console.WriteLine("Please enter a date you would like to see (yyyy-mm-dd)");
            //string userInput = Console.ReadLine();

            //Regex regex = new Regex(@"^" + userInput + ".*(?<=Ute,).*");

            //string[] lines = File.ReadAllLines(path);
            //bool dateFound = false;

            //string[] splitArray = lines.ToString().Split(',');

            //Console.WriteLine("Test: " + splitArray[0]);


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



            // -------------------------------------------------
        //    string path = "../../../../WeatherData/Files/TextFile1.txt";
        //    Console.WriteLine("Please enter a date you would like to see (yyyy-mm-dd)");
        //    string userInput = Console.ReadLine();

        //    Regex regex = new Regex(@"^" + userInput + ".*(?<=Ute,).*");

        //    string[] lines = File.ReadAllLines(path);
        //    bool dateFound = false;

        //    double Temp = 0;
        //    double Moist = 0; 

        //    foreach (string line in lines)
        //    {
        //        if (regex.IsMatch(line))
        //        {
        //            string[] splitArray = line.Split(',');
        //            Console.WriteLine("Temp: " + splitArray[2]);
        //            Console.WriteLine("Moist: " + splitArray[3]);

        //            //Temp = Temp + splitArray[2];


        //            dateFound = true;
        //            break;
        //        }
        //    }

        //    if (!dateFound)
        //    {
        //        Console.WriteLine("Date not found.");
        //        Testing();
        //    }
        }
        public static void NewTesting()
        {
            //List<WeatherData> WeatherDataList = new List<WeatherData>();

            //string path = "../../../../WeatherData/Files/TextFile1.txt";
            //Console.WriteLine("Please enter a date you would like to see (yyyy-mm-dd)");
            //string userInput = Console.ReadLine();

            //Regex regex = new Regex(@"^" + userInput + ".*(?<=Ute,).*");

            //string[] lines = File.ReadAllLines(path);
            //bool dateFound = false;

            //foreach (string line in lines)
            //{
            //    if (regex.IsMatch(line))
            //    {
            //        Console.WriteLine(line);
            //        WeatherDataList.Add(line);
            //        dateFound = true;
            //    }
            //}
            //if (!dateFound)
            //{
            //    Console.WriteLine("No data found for the specified date.");
            //    OutsideAvrageTempAndHumidity();
            //}


            //List<WeatherData> WeatherDataList = new List<WeatherData>();

            //string path = "../../../../WeatherData/Files/TextFile1.txt";
            //Console.WriteLine("Please enter a date you would like to see (yyyy-mm-dd)");
            //string userInput = Console.ReadLine();

            //Regex regex = new Regex(@"^" + userInput + "*");

            //string[] lines = File.ReadAllLines(path);
            //bool dateFound = false;

            //foreach (string line in lines)
            //{
            //    if (regex.IsMatch(line))
            //    {
            //        Console.WriteLine(line);
            //        string[] splitArray = new string[4];
            //        splitArray = line.Split(',', ' ');
            //        WeatherData weatherData = new WeatherData
            //        {
            //            Datetime = int.Parse(splitArray[0]),
            //            Time = int.Parse(splitArray[1]),
            //            Location = splitArray[2],
            //            Temprature = double.Parse(splitArray[3]),
            //            Moist = int.Parse(splitArray[4])
            //        };
            //        WeatherDataList.Add(weatherData);
            //        dateFound = true;
            //    }
            //}

            //if (!dateFound)
            //{
            //    Console.WriteLine("No data found for the specified date.");
            //    OutsideAvrageTempAndHumidity();
            //}

            List<WeatherData> WeatherDataList = new List<WeatherData>();



        }
        public static void Testing123()
        {
            string path = "../../../../WeatherData/Files/TextFile1.txt";

            //string dateTime;
            //string time;
            //string location;
            //double temp;
            //int moist;

            using (StreamReader sr = new StreamReader(path))
            {
                List<string> dataList = new List<string>();
                dataList = File.ReadAllLines(path).ToList();

                List<WeatherData> weatherDataList = new List<WeatherData>();

                string[] parts;

                foreach (string data in dataList)
                {
                     parts = data.Split(' ', ',');

                    //foreach(string line in parts)
                    //{
                    //    Console.WriteLine(line);
                    //}
                    string dateTime;
                    string time;
                    string location;
                    double temp;
                    int moist;

                    dateTime = parts[0];
                    time = parts[1];
                    location = parts[2];
                    temp = Convert.ToDouble(parts[3]);
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
                    foreach (var w in weatherDataList)
                    {
                        Console.WriteLine(w.Temprature);
                    }







                    //try
                    //{
                    //    WeatherData weatherData = new WeatherData
                    //    {
                    //        Datetime = dateTime,
                    //        Time = time,
                    //        Location = location,
                    //        Temprature = temp,
                    //        Moist = moist,
                    //    };
                    //}
                    //catch (FormatException e)
                    //{
                    //    Console.WriteLine(e);
                    //}


                }
            }
                



            //string path = "../../../../WeatherData/Files/TextFile1.txt";
            //List<WeatherData> WeatherDataList = new List<WeatherData>();

            //using (StreamReader sr = new StreamReader(path))
            //{
            //    List<string> DataParts = new List<string>();

            //    DataParts = File.ReadAllLines(path).ToList();

            //    foreach (string data in DataParts)
            //    {
            //        string[] parts = data.Split(' ',',');

            //        foreach (string part in parts)
            //        {
            //           DataParts.Add(part);
            //        }

            //    }
            //for(int i = 0; i < DataParts.Count(); i++)
            //{
            //    WeatherData weatherData = new WeatherData()
            //    {
            //        Datetime = Convert.ToString(DataParts[0]),
            //        Time = Convert.ToString(DataParts[1]),
            //        Location = Convert.ToString(DataParts[2]),
            //        Temprature = Convert.ToDouble(DataParts[3]),
            //        Moist = Convert.ToInt32(DataParts[4]),
            //    };
            //}
            //foreach (string line in lines)
            //{
            //    splitString = line.Replace(",", " ");
            //    //Console.WriteLine(splitString);


            //}
            //}
        }
    }
    
}
