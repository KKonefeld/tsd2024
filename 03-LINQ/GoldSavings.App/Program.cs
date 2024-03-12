using GoldSavings.App.Model;
using GoldSavings.App.Client;
using System.Xml.Linq;
namespace GoldSavings.App;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Gold Saver!");

        GoldClient goldClient = new GoldClient();

        GoldPrice currentPrice = goldClient.GetCurrentGoldPrice().GetAwaiter().GetResult();
        Console.WriteLine($"The price for today is {currentPrice.Price}");

        List<GoldPrice> thisMonthPrices = goldClient.GetGoldPrices(new DateTime(2024, 03, 01), new DateTime(2024, 03, 11)).GetAwaiter().GetResult();

        foreach(var goldPrice in thisMonthPrices)
        {
            Console.WriteLine($"The price for {goldPrice.Date} is {goldPrice.Price}");
        }

        // Last year //METHOD METHOD METHOD METHOD
        List<GoldPrice> lastYearPrices = goldClient.GetGoldPrices(new DateTime(2023, 03, 12), new DateTime(2024, 03, 12)).GetAwaiter().GetResult();

        // descending 
        lastYearPrices.Sort((p1, p2) => p2.Price.CompareTo(p1.Price));

        // top3
        var top3Highest = lastYearPrices.Take(3);
        Console.WriteLine("Highest Prices in last year METHOD");
        foreach (var price in top3Highest)
        {
            Console.WriteLine($"\t- {price.Date}: {price.Price}");
        }
        // ascending order low3
        lastYearPrices.Sort((p1, p2) => p1.Price.CompareTo(p2.Price));
        var top3Lowest = lastYearPrices.Take(3);
        Console.WriteLine("Lowest Prices in last year METHOD");
        foreach (var price in top3Highest)
        {
            Console.WriteLine($"\t- {price.Date}: {price.Price}");
        }

        //QUERY QUERY QUERY
        // Get last year's prices
        List<GoldPrice> lastYearPricesQ = goldClient.GetGoldPrices(new DateTime(2023, 03, 12), new DateTime(2024, 03, 12)).GetAwaiter().GetResult();

        // Highest prices 
        var top3HighestQ = (from price in lastYearPricesQ
                        orderby price.Price descending
                        select price).Take(3).ToList(); 

        Console.WriteLine("Highest Prices in last year QUERY");
        foreach (var price in top3HighestQ)
        {
            Console.WriteLine($"\t- {price.Date}: {price.Price}");
        }

        // Lowest prices 
        var top3LowestQ = (from price in lastYearPricesQ
                        orderby price.Price ascending
                        select price).Take(3).ToList(); 

        Console.WriteLine("Lowest Prices in last year QUERY");
        foreach (var price in top3LowestQ)
        {
            Console.WriteLine($"\t- {price.Date}: {price.Price}");
        }

        /////////////////////////////////////////////4444444444444444////////////
        // Get January 2020 prices
        
        List<GoldPrice> january2020Prices = goldClient.GetGoldPrices(new DateTime(2020, 01, 01), new DateTime(2020, 02, 01)).GetAwaiter().GetResult();

        // days with 5% or more
        var profitableDays = january2020Prices.Where(p => ((currentPrice.Price / p.Price )* 100) > 105);

        // are there profitable days?
        if (profitableDays.Any())
        {
            Console.WriteLine("Days with price increase > 5% in January 2020:");
            foreach (var price in profitableDays)
            {
                Console.WriteLine($"\t- {price.Date}: {price.Price}");
            }
        }
        else
        {
            Console.WriteLine("No days found in January 2020 with a price increase > 5%.");
        }
        ///////// 4 QUERY
        List<GoldPrice> january2020PricesQ = goldClient.GetGoldPrices(new DateTime(2020, 01, 01), new DateTime(2020, 02, 01)).GetAwaiter().GetResult();
        var profitableDaysQ = (from price in january2020PricesQ
                          where ((currentPrice.Price / price.Price) * 100) > 105
                          select price).ToList();
         if (profitableDaysQ.Any())
        {
            Console.WriteLine("QQQ Days with price increase > 5% in January 2020:");
            foreach (var price in profitableDaysQ)
            {
                Console.WriteLine($"\t- {price.Date}: {price.Price}");
            }
        }
        else
        {
            Console.WriteLine("No days found in January 2020 with a price increase > 5%.");
        }


        /////////////////////555555555555555555
        var gold2019 = goldClient.GetGoldPrices(new DateTime(2019, 01, 01), new DateTime(2019, 12, 31)).GetAwaiter().GetResult();
        var gold2020 = goldClient.GetGoldPrices(new DateTime(2020, 01, 01), new DateTime(2020, 12, 31)).GetAwaiter().GetResult();
        var gold2021 = goldClient.GetGoldPrices(new DateTime(2021, 01, 01), new DateTime(2021, 12, 31)).GetAwaiter().GetResult();

        var top3FromSecondTen2019 = gold2019
            .OrderByDescending(g => g.Price)
            .Skip(10)
            .Take(3);

        var index = 11;
        foreach (var goldPrice in top3FromSecondTen2019)
        {
            Console.WriteLine($"{index}. {goldPrice.Date} {goldPrice.Price}");
            index++;
        }

        var top3FromSecondTen2020 = gold2020
            .OrderByDescending(g => g.Price)
            .Skip(10)
            .Take(3);

        index = 11;
        foreach (var goldPrice in top3FromSecondTen2020)
        {
            Console.WriteLine($"{index}. {goldPrice.Date} {goldPrice.Price}");
            index++;
        }

        var top3FromSecondTen2021 = gold2021
            .OrderByDescending(g => g.Price)
            .Skip(10)
            .Take(3);

        index = 11;
        foreach (var goldPrice in top3FromSecondTen2021)
        {
            Console.WriteLine($"{index}. {goldPrice.Date} {goldPrice.Price}");
            index++;
        }

        var top3FromSecondTen2019Q = (from g in gold2019
        orderby g.Price descending
        select g).Skip(10).Take(3);

        var top3FromSecondTen2020Q = (from g in gold2020
        orderby g.Price descending
        select g).Skip(10).Take(3);

        var top3FromSecondTen2021Q = (from g in gold2021
        orderby g.Price descending
        select g).Skip(10).Take(3);

        index = 11;
        foreach (var goldPrice in top3FromSecondTen2019Q)
        {
            Console.WriteLine($"{index}. {goldPrice.Date} {goldPrice.Price}");
            index++;
        }

        index = 11;
        foreach (var goldPrice in top3FromSecondTen2020Q)
        {
            Console.WriteLine($"{index}. {goldPrice.Date} {goldPrice.Price}");
            index++;
        }

        index = 11;
        foreach (var goldPrice in top3FromSecondTen2021Q)
        {
            Console.WriteLine($"{index}. {goldPrice.Date} {goldPrice.Price}");
            index++;
        }

       //////////////6666666666666666666666
        var year2021Prices = goldClient.GetGoldPrices(new DateTime(2021, 01, 01), new DateTime(2022, 01, 01)).GetAwaiter().GetResult();
        var year2022Prices = goldClient.GetGoldPrices(new DateTime(2022, 01, 01), new DateTime(2023, 01, 01)).GetAwaiter().GetResult();
        var year2023Prices = goldClient.GetGoldPrices(new DateTime(2023, 01, 01), new DateTime(2024, 01, 01)).GetAwaiter().GetResult();

        var averagePrice2021 = year2021Prices.Average(p => p.Price);
        var averagePrice2022 = year2022Prices.Average(p => p.Price);
        var averagePrice2023 = year2023Prices.Average(p => p.Price);

        Console.WriteLine("\nAverage Gold Prices by Year:");
        Console.WriteLine($"2021: {averagePrice2021}");
        Console.WriteLine($"2022: {averagePrice2022}");
        Console.WriteLine($"2023: {averagePrice2023}");


        //QUERY QUERY

        var averagePrice2021Q = (from price in year2021Prices
                                    select price.Price).Average();

        var averagePrice2022Q = (from price in year2022Prices
                                    select price.Price).Average();

        var averagePrice2023Q = (from price in year2023Prices
                                    select price.Price).Average();

        Console.WriteLine("\nAverage Gold Prices by Year:");
        Console.WriteLine($"2021: {averagePrice2021}");
        Console.WriteLine($"2022: {averagePrice2022}");
        Console.WriteLine($"2023: {averagePrice2023}");


    ///////////777777777777777777777777
    DateTime startDate = new DateTime(2019, 01, 01);
        DateTime endDate = new DateTime(2023, 12, 31);

        for (int year = startDate.Year; year <= endDate.Year; year++)
        {
            DateTime yearStart = new DateTime(year, 1, 1);
            DateTime yearEnd = new DateTime(year + 1, 1, 1);

            List<GoldPrice> prices = goldClient.GetGoldPrices(yearStart, yearEnd).GetAwaiter().GetResult();

            if (prices != null && prices.Any())
            {
                var minPrice = prices.OrderBy(p => p.Price).First();
                var maxPrice = prices.OrderByDescending(p => p.Price).First();

                var profitablePrices = prices.Where(p => p.Date > minPrice.Date && p.Date < maxPrice.Date);

                if (profitablePrices.Any())
                {
                    var buyDate = profitablePrices.OrderBy(p => p.Date).First().Date;
                    var sellDate = profitablePrices.OrderByDescending(p => p.Date).First().Date;

                    Console.WriteLine($"\nBest Time to Buy and Sell Gold in {year}:");
                    Console.WriteLine($"Buy on: {buyDate}, Price: {minPrice.Price}");
                    Console.WriteLine($"Sell on: {sellDate}, Price: {maxPrice.Price}");

                    decimal profit = (decimal)(maxPrice.Price - minPrice.Price);
                    decimal ROI = (profit / (decimal)minPrice.Price) * 100;
                    Console.WriteLine($"Return on Investment: {ROI}%");
                }
                else
                {
                    Console.WriteLine($"No profitable prices found for the year {year}.");
                }
            }
            else
            {
                Console.WriteLine($"No prices available for the year {year}.");
            }
        }

        for (int year = startDate.Year; year <= endDate.Year; year++)
        {
            DateTime yearStart = new DateTime(year, 1, 1);
            DateTime yearEnd = new DateTime(year + 1, 1, 1);

            List<GoldPrice> prices = goldClient.GetGoldPrices(yearStart, yearEnd).GetAwaiter().GetResult();

            if (prices != null && prices.Any())
            {
                var minPrice = (from p in prices
                                orderby p.Price
                                select p).First();
                var maxPrice = (from p in prices
                                orderby p.Price descending
                                select p).First();

                var profitablePrices = from p in prices
                                    where p.Date > minPrice.Date && p.Date < maxPrice.Date
                                    select p;

                if (profitablePrices.Any())
                {
                    var buyDate = (from p in profitablePrices
                                orderby p.Date
                                select p.Date).First();
                    var sellDate = (from p in profitablePrices
                                    orderby p.Date descending
                                    select p.Date).First();

                    Console.WriteLine($"\nBest Time to Buy and Sell Gold in {year}:");
                    Console.WriteLine($"Buy on: {buyDate}, Price: {minPrice.Price}");
                    Console.WriteLine($"Sell on: {sellDate}, Price: {maxPrice.Price}");

                    decimal profit = (decimal)(maxPrice.Price - minPrice.Price);
                    decimal ROI = (profit / (decimal)minPrice.Price) * 100;
                    Console.WriteLine($"Return on Investment: {ROI}%");
                }
                else
                {
                    Console.WriteLine($"No profitable prices found for the year {year}.");
                }
            }
            else
            {
                Console.WriteLine($"No prices available for the year {year}.");
            }
        }

        //CALL 8
        SavePricesToXml(january2020PricesQ, "test");

        // CALL 9
        Console.WriteLine("zad. 9");
        var loadedPrices = LoadPricesFromXml("test");
        foreach (var price in loadedPrices)
        {
            Console.WriteLine($"Date: {price.Date}, Price: {price.Price}");
        }
        
    }

    /// 888888888
        static void SavePricesToXml(List<GoldPrice> prices, string filePath)
            {
                XElement root = new XElement("GoldPrices");

                foreach (var price in prices)
                {
                    XElement price2 = new XElement("GoldPrice");
                    XElement priceElement = new XElement("Price", price.Price);
                    XElement dateElement = new XElement("Date", price.Date);
                    price2.Add(priceElement);
                    price2.Add(dateElement);
                    root.Add(price2);
                }

                XDocument doc = new XDocument(root);
                doc.Save(filePath);
            }
        //99999999999999999999999999999
        static List<GoldPrice> LoadPricesFromXml(string filePath)
            {
            return XElement.Load(filePath).Descendants("GoldPrice")
                .Select(price => new GoldPrice 
                { 
                    Price = double.Parse(price.Element("Price").Value), 
                    Date = DateTime.Parse(price.Element("Date").Value)
                })
                .ToList();
            }
}
