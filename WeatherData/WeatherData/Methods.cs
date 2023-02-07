using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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

        }
        public static void Outside()
        {

        }
        public static void Pattern()
        {
            //  ([12]\d{ 3}-(0[6 - 9] | 1[0 - 2]) - (0[1 - 9] |[12]\d | 3[01]))     -TAR BORT MAJ
            // (2016-05|2017-01)    -matcher det vi inte villmatcha
            //      ^(2017-(0[2-9]|[2-9][0-9])|2016-(0[0-4]|0[6-9]|[1-9][0-9])).* - matchar det vi vill.
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
    }
}
