using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalTrafficDataGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var csvData = CsvSerializer.SerializeToString<List<Traffic>>(GetAllData());
            File.WriteAllText("c:\\code\\data\\traffic.csv", csvData);
        }

        public static List<Traffic> GetAllData()
        {
            var allData = new List<Traffic>();


            foreach (DateTime date in GetAllDaysForYearRange(2010, 2011))
            {
                foreach (string time in GetTimeOfDay())
                {
                    foreach (string city in GetCities())
                    {
                        allData.Add(new Traffic
                        {
                            Date = date.ToString("yyyy-mm-dd"),
                            Time = time,
                            City = city,
                            Speed = GetSpeedForYear(date.Year),
                            People = GetPersonForYear(date.Year)
                        });
                    }
                    Console.WriteLine("Completed time" + time);
                }
            }
            return allData;
        }

        public static IEnumerable<string> GetTimeOfDay()
        {
            return new List<string>
            {
                //"07:30:00",
                "08:00:00",
                //"08:30:00",
                "09:30:00",
                //"10:30:00",
                "11:30:00",
                "18:30:00",
                //"19:00:00",
                "19:30:00",
                "20:00:00",
            };
        }

        public static IEnumerable<DateTime> GetAllDaysForYearRange(int start, int end)
        {
            return Enumerable.Range(start, end)
                .SelectMany(year => GetDaysForYear(year));
        }

        public static IEnumerable<DateTime> GetDaysForYear(int year)
        {
            return new int[] {1, 3, 8, 12} 
                //Enumerable.Range(1, 12)
                .SelectMany(month => GetDatesForMonthYear(year, month));
        }

        public static IEnumerable<DateTime> GetDatesForMonthYear(int year, int month)
        {
            return  new int[] { 1, 3, 10, 15, 20, 25} 
                //Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                    .Select(day => new DateTime(year, month, day)); // Map each day to a 
        }

        public static List<string> GetCities()
        {
            return new List<string>
            {
                "New Delhi",
                "Mumbai",
                "Kolkata",
                "Bangalore",
                "Pune",
                "Hyderabad",
                "Chennai",
                "Ahmedabad",
                "Visakhapatnam",
                "Surat",
                "Jaipur"
            };
        }

        static Random random = new Random();

        public static int GetSpeedForYear(int year)
        {
            switch (year)
            {
                case 2010:
                    return RandomNumber(20,30);
                case 2011:
                    return RandomNumber(22, 30);
                case 2012:
                    return RandomNumber(24, 32);
                case 2013:
                    return RandomNumber(24, 34);
                case 2014:
                    return RandomNumber(26, 36);
                case 2015:
                    return RandomNumber(28, 36);
            }
            return 0;    
            
        }

        public static int GetPersonForYear(int year)
        {
            switch (year)
            {
                case 2010:
                    return RandomNumber(1, 10);
                case 2011:
                    return RandomNumber(3, 10);
                case 2012:
                    return RandomNumber(4, 10);
                case 2013:
                    return RandomNumber(4, 10);
                case 2014:
                    return RandomNumber(5, 10);
                case 2015:
                    return RandomNumber(5, 10);
            }
            return 0;

        }

        public static int RandomNumber(int min, int max)
        {            
            return random.Next(min, max);
        }

        public class Traffic
        {
            public string Date{ get; set; }
            public string Time{ get; set; }
            public string City{ get; set; }
            public int Speed{ get; set; }
            public int People{ get; set; }

        }
    }
}
