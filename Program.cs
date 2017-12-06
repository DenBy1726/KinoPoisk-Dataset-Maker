using CsQuery;
using Parser.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KinopoiskParser
{
    //https://www.kinopoisk.ru/reviews/type/comment/status/bad/period/year/sort/rating/perpage/100/
    class Program
    {
        private static ArgParser Input;
        private static ConsoleProgressBar Bar;
        private static string status = "good";
        private static string period = "month";
        private static int count = 5000;
        private static string sort = "date";
        private static string name = "review_"+DateTime.Now.Ticks.ToString();
        private static int perpage = 100;
        private static string url;
        static void Main(string[] args)
        {
          
            Input = new ArgParser(args);
            if (Input.Key.Contains("help"))
            {
                Help();
                return;
            }
            try
            {
                if (Input.Arg.ContainsKey("status"))
                {
                    status = Input.Arg["status"];
                }
                if (Input.Arg.ContainsKey("period"))
                {
                    period = Input.Arg["period"];
                }
                if (Input.Arg.ContainsKey("count"))
                {
                    count = Int32.Parse(Input.Arg["count"]);
                }
                if (Input.Arg.ContainsKey("sort"))
                {
                    sort = Input.Arg["sort"];
                }
                if (Input.Arg.ContainsKey("name"))
                {
                    name = Input.Arg["name"];
                }
                if (Input.Arg.ContainsKey("perpage"))
                {
                    perpage = Int32.Parse(Input.Arg["perpage"]);
                }

                url = $"https://www.kinopoisk.ru/reviews/type/comment/status/{status}/period/{period}/sort/{sort}/perpage/{perpage}/";
                Parse();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }

        }

        private static void Parse()
        {
            int part = count / perpage;
            Bar = new ConsoleProgressBar(part);
            var ci = CultureInfo.CreateSpecificCulture("ru-RU");
            Func<string,string> tr = WebUtility.HtmlDecode;
            List<FilmEntity> dataset = new List<FilmEntity>();
            for(int i = 0; i < part; i++)
            {
                //Если ресурс не найден, получаем исключение
                //переходим на сл. итерацию
                try
                {
                    CQ cq = CQ.CreateFromUrl(url + "/page/" + i);
                    var reviews = cq.Find(".response");

                    foreach (var it in reviews)
                    {
                        var response = it.Cq();
                        string author = tr(response.Find(Selectors.Author)[0].InnerHTML);
                        string name = tr(response.Find(Selectors.Film)[0].InnerHTML);
                        string dateRepr = tr(response.Find(Selectors.Date)[0].InnerHTML);
                        var date = DateTime.ParseExact(dateRepr, "d MMMM yyyy | HH:mm", ci);
                        var title = tr(response.Find(Selectors.Title)[0].InnerHTML);
                        var text = tr(response.Find(Selectors.Text)[0].ChildNodes[1].ChildNodes[0].InnerText);

                        var entity = new FilmEntity(name, author, date, title, text, status);
                        dataset.Add(entity);
                    }
                }
                catch
                {
                    
                }

                Bar.Update(i);

            }

            File.WriteAllText(name,JSonParser.Save(dataset, dataset.GetType()));
        }

        private static void Help()
        {
            Console.WriteLine("Support args:");
            Console.WriteLine("___________________________________________");
            Console.WriteLine("|name     | values                         |");
            Console.WriteLine("|_________|________________________________|");
            Console.WriteLine("|status   | good*,bad                      |");
            Console.WriteLine("|period   | week,month*                    |");
            Console.WriteLine("|count    | number default 500             |");
            Console.WriteLine("|sort     | rating,film,user,date*         |");
            Console.WriteLine("|name     | string default review_{time}   |");
            Console.WriteLine("|perpage  | number default 100             |");
            Console.WriteLine("|_________|________________________________|");

        }
    }
}
