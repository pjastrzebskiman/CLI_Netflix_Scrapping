using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Functions
    {
        public delegate void getSave(List<Film> a, string sciezka);

        public delegate Task<List<Film>> getHtml(List<Film> a);

        private static void Save(List<Film> a, string sciezka)
        {
            using (StreamWriter sciezkadoksiazki = new StreamWriter(sciezka, false))
            {
                string listai = "";
                for (int i = 0; i < a.Count; i++)
                {
                    listai += a[i].Name + "\r\n";
                }
                sciezkadoksiazki.Write(listai);

            }
        }

        private static async Task<List<Film>> GetHtmlAsyncNetflix(List<Film> Title)
        {
           
            string path = Path.Combine(Environment.CurrentDirectory, @"baza\", "netflix.txt");
            //find element  contain number of pages
            var url = "https://upflix.pl/netflix/film";
            var httpklient = new HttpClient();
            var html = await httpklient.GetStringAsync(url);

            var html_file = new HtmlDocument();
            html_file.LoadHtml(html);


            string s_counter = html_file.DocumentNode.Descendants("li")
           .Where(node => node.GetAttributeValue("class", "")
           .Equals("last")).FirstOrDefault().InnerHtml.ToString().Substring(27, 3); // Substring(40,3);

            //temp problem solved: divide elements(films) count by numbers element on page
            int counter = Int32.Parse(s_counter) + 1;
            //Title = new List<Film>();

            //itteration page from 1 to the last one
            for (int i = 1; i < 3; i++)//zmien 3 na licznik
            {
                var url1 = "https://upflix.pl/netflix/film/p" + i + "?search=";
                var httpklient1 = new HttpClient();
                var html1 = await httpklient1.GetStringAsync(url1);
                var html_file1 = new HtmlDocument();
                html_file1.LoadHtml(html1);

                var data = html_file1.DocumentNode.Descendants("div")
             .Where(node => node.GetAttributeValue("class", "")
             .Equals("info")).ToList();

                // int aaa = baza.Count * licznik;
                int ab = data.Count;
                string[] a = new string[ab];

                // itteration all elements(films) on current page
                for (int j = 0; j < data.Count; j++)
                {
                    a[j] = data[j].InnerText;
                    a[j] = a[j].Substring(0, a[j].Length - 8);
                    Console.WriteLine(a[j]);
                    Title.Add(new Film() { Name = a[j] });

                }

            }
            return Title;
        }


        public getSave MySave
        {
            get
            {
                return new getSave((Save));
            }
        }

        public getHtml MyHtml
        {
            get
            {
                return new getHtml(GetHtmlAsyncNetflix);
            }
        }
    }
}
