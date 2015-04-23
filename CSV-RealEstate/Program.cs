using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Globalization;

namespace CSV_RealEstate
{
    // WHERE TO START?
    // 1. Complete the RealEstateType enumeration
    // 2. Complete the RealEstateSale object.  Fill in all properties, then create the constructor.
    // 3. Complete the GetRealEstateSaleList() function.  This is the function that actually reads in the .csv document and extracts a single row from the document and passes it into the RealEstateSale constructor to create a list of RealEstateSale Objects.
    // 4. Start by displaying the the information in the Main() function by creating lambda expressions.  After you have acheived your desired output, then translate your logic into the function for testing.
    class Program
    {
        static void Main(string[] args)
        {
            List<RealEstateSale> realEstateSaleList = GetRealEstateSaleList();

            Console.WriteLine(GetAveragePricePerSquareFootByRealEstateTypeAndCity(realEstateSaleList, RealEstateType.Condo, "sacramento"));
            //Display the average square footage of a Condo sold in the city of Sacramento, 
            Console.WriteLine(GetAveragePricePerSquareFootByRealEstateTypeAndCity(realEstateSaleList, RealEstateType.Condo, "sacramento"));
            //Use the GetAverageSquareFootageByRealEstateTypeAndCity() function.
            Console.WriteLine(GetAverageSquareFootageByRealEstateTypeAndCity(realEstateSaleList, RealEstateType.Condo, "sacramento"));

            //Display the total sales of all residential homes in Elk Grove.  Use the GetTotalSalesByRealEstateTypeAndCity() function for testing.
            Console.WriteLine(GetTotalSalesByRealEstateTypeAndCity(realEstateSaleList, RealEstateType.Residential, "elk grove"));

            //Display the total number of residential homes sold in the zip code 95842.  Use the GetNumberOfSalesByRealEstateTypeAndZip() function for testing.
            Console.WriteLine(GetNumberOfSalesByRealEstateTypeAndZip(realEstateSaleList, RealEstateType.Residential, "95842"));
            //Display the average sale price of a lot in Sacramento.  Use the GetAverageSalePriceByRealEstateTypeAndCity() function for testing.
            Console.WriteLine(GetAverageSalePriceByRealEstateTypeAndCity(realEstateSaleList, RealEstateType.Lot, "sacramento"));
            //Display the average price per square foot for a condo in Sacramento. Round to 2 decimal places. Use the GetAveragePricePerSquareFootByRealEstateTypeAndCity() function for testing.
            Console.WriteLine(GetAveragePricePerSquareFootByRealEstateTypeAndCity(realEstateSaleList, RealEstateType.Condo, "sacramento"));
            //Display the number of all sales that were completed on a Wednesday.  Use the GetNumberOfSalesByDayOfWeek() function for testing.
            Console.WriteLine(GetNumberOfSalesByDayOfWeek(realEstateSaleList, DayOfWeek.Wednesday));

            //Display the average number of bedrooms for a residential home in Sacramento when the 
            // price is greater than 300000.  Round to 2 decimal places.  Use the GetAverageBedsByRealEstateTypeAndCityHigherThanPrice() function for testing.
            Console.WriteLine(GetAverageBedsByRealEstateTypeAndCityHigherThanPrice(realEstateSaleList, RealEstateType.Residential, "sacramento", 300000));
            //Extra Credit:
            //Display top 5 cities by the number of homes sold (using the GroupBy extension)
            // Use the GetTop5CitiesByNumberOfHomesSold() function for testing.
            List<string> cities = GetTop5CitiesByNumberOfHomesSold(realEstateSaleList);
            foreach (object topFive in cities)
            {
                Console.WriteLine(topFive);
            }
            Console.ReadKey();
        }



        public static List<RealEstateSale> GetRealEstateSaleList()
        {
            List<RealEstateSale> estateList = new List<RealEstateSale>();
            //read in the realestatedata.csv file.  As you process each row, you'll add a new 
            // RealEstateData object to the list for each row of the document, excluding the first.  bool skipFirstLine = true;
            using (StreamReader reader = new StreamReader("realestatedata.csv"))
            {
                //stolen from other file
                // Get and don't use the first line
                string firstline = reader.ReadLine();
                // Loop through the rest of the lines
                while (!reader.EndOfStream)
                {
                    //adding stuff to list
                    estateList.Add(new RealEstateSale(reader.ReadLine()));
                }
            }
            return estateList;
        }
        /// <summary>
        /// .Average
        /// </summary>
        /// <param name="realEstateDataList"></param>
        /// <param name="realEstateType"></param>
        /// <param name="city"></param>
        /// <returns></returns>
        public static double GetAverageSquareFootageByRealEstateTypeAndCity(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city)
        {
            //pare by type
            //pare by city
            //left with list of objects in city with type of property
            //select square feet
            //left with list of double
            double average = realEstateDataList.Where(x => x.Type == realEstateType).Where(y => y.City.ToUpper() == city.ToUpper()).Average(z => z.SquareFeet);
            return average;
        }

        public static decimal GetTotalSalesByRealEstateTypeAndCity(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city)
        {
            //pare by type
            //pare by city
            //add sales
            decimal sales = realEstateDataList.Where(x => x.Type == realEstateType).Where(x => x.City.ToUpper() == city.ToUpper()).Sum(x => x.Price);
            return sales;
        }


        public static int GetNumberOfSalesByRealEstateTypeAndZip(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string zipcode)
        {
            //pare by type
            //convert string zip to int
            //pare by zip

            int sales = realEstateDataList.Where(x => x.Type == realEstateType).Where(x => x.ZipCode == int.Parse(zipcode)).Count();
            return sales;
        }


        public static decimal GetAverageSalePriceByRealEstateTypeAndCity(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city)
        {
            //Must round to 2 decimal points
            //pare by type
            //pare by city
            //average the price
            decimal avgSale = Math.Round(Convert.ToDecimal(realEstateDataList.Where(x => x.Type == realEstateType).Where(x => x.City.ToUpper() == city.ToUpper()).Average(x => x.Price).ToString()), 2);
            return avgSale;
        }
        public static decimal GetAveragePricePerSquareFootByRealEstateTypeAndCity(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city)
        {

            //Must round to 2 decimal points
            //math.round to nearest 2 decimals
            //pare by type
            //pare by city
            //average the quotient of price over sqft
            decimal squareFt = Math.Round(realEstateDataList.Where(x => x.Type == realEstateType).Where(x => x.City.ToUpper() == city.ToUpper()).Average(x => ((decimal)x.Price / (decimal)x.SquareFeet)), 2);
            return squareFt;
        }

        public static int GetNumberOfSalesByDayOfWeek(List<RealEstateSale> realEstateDataList, DayOfWeek dayOfWeek)
        {
            //pare by day of week
            //count
            int numOfSales = realEstateDataList.Where(x => x.SaleDate.DayOfWeek == dayOfWeek).Count();
            return numOfSales;
        }

        public static double GetAverageBedsByRealEstateTypeAndCityHigherThanPrice(List<RealEstateSale> realEstateDataList, RealEstateType realEstateType, string city, decimal price)
        {
            //Must round to 2 decimal points
            //pare by type
            //pare by city
            //eliminate lower cost houses
            //average no. of beds
            double beds = Math.Round(realEstateDataList.Where(x => x.Type == realEstateType).Where(x => x.City.ToLower() == city.ToLower()).Where(x => (decimal)x.Price > price).Average(x => x.Beds), 2);
            return beds;
        }

        public static List<string> GetTop5CitiesByNumberOfHomesSold(List<RealEstateSale> realEstateDataList)
        {

            //group by city
            //order by count
            //select many takes from each object the select city
            //distinct only accepts the first time a city is encountered
            //take first 5
            //to list
            List<string> topFive = realEstateDataList.GroupBy(x => x.City).OrderByDescending(x => x.Count()).SelectMany(x => x.Select(y => y.City).Distinct()).Take(5).ToList();
            return topFive;
        }
    }

    public enum RealEstateType
    {
        //fill in with enum types: Residential, MultiFamily, Condo, Lot
        Residential,
        MultiFamily,
        Condo,
        Lot
    }
    class RealEstateSale
    {
        //Create properties, using the correct data types (not all are strings) for all columns of the CSV
        //street,city,zip,state,beds,baths,sq__ft,type,sale_date,price,latitude,longitude
        public string Street { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string State { get; set; }
        public int Beds { get; set; }
        public int Baths { get; set; }
        public int SquareFeet { get; set; }
        public DateTime SaleDate { get; set; }
        public int Price { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public RealEstateType Type { get; set; }



        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="lineInput"></param>
        public RealEstateSale(string lineInput)
        {
            //The constructor will take a single string arguement.  This string will be one line of the real estate data.
            // Inside the constructor, you will seperate the values into their corrosponding properties, and do the necessary conversions

            //When computing the RealEstateType, if the square footage is 0, then it is of the Lot type, otherwise, use the string
            // value of the "Type" column to determine its corresponding enumeration type.

            string[] realEstateData = lineInput.Split(',');
            this.Street = realEstateData[0];
            this.City = realEstateData[1];
            this.ZipCode = int.Parse(realEstateData[2]);
            this.State = realEstateData[3];
            this.Beds = int.Parse(realEstateData[4]);
            this.Baths = int.Parse(realEstateData[5]);


            this.SquareFeet = int.Parse(realEstateData[6]);
            //establish enum 
            switch (realEstateData[7].ToUpper())
            {
                case "RESIDENTIAL":
                    this.Type = RealEstateType.Residential;
                    break;
                case "CONDO":
                    this.Type = RealEstateType.Condo;
                    break;
                case "MULTI-FAMILY":
                    this.Type = RealEstateType.MultiFamily;
                    break;
                default:
                    break;

            }
            //Wed May 21 2008
            this.SaleDate = Convert.ToDateTime(realEstateData[8]);
            this.Price = int.Parse(realEstateData[9]);
            this.Latitude = double.Parse(realEstateData[10]);
            this.Longitude = double.Parse(realEstateData[11]);

            if (SquareFeet == 0)
            {
                this.Type = RealEstateType.Lot;
            }


        }
    }
}
