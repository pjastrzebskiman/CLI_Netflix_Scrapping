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
    class Program
    {

        static async Task Main(string[] args)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"baza\","netflix.txt");
            List<Film> Film_z_netflix_test = new List<Film>();
            await GetHtmlAsync(Film_z_netflix_test);
            Functions.getSave getsave = null;
            Functions functions = new Functions();
            getsave = new Functions.getSave(functions.MySave);
            getsave(Film_z_netflix_test, path);
           // Console.ReadLine();
        }


        private static async Task<List<Film>> GetHtmlAsync(List<Film> Title)
        {
            /*
            var url = "https://www.filmweb.pl/user/Piotrsowa96/films"; //podmien na link swojego konta
            var httpklient = new HttpClient();
            var html = await httpklient.GetStringAsync(url);

            var html_plik = new HtmlDocument();
            html_plik.LoadHtml(html);

            // var i = html_plik.GetElementbyId("userVotesPage__header blockHeader");


            var test2 = html_plik.DocumentNode.Descendants("a")
                .Where(node => node.GetAttributeValue("title", "")
                .Equals("następna")).ToList();

            var test1 = html_plik.DocumentNode.Descendants("h2")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("filmPreview__title")).ToList();

            var url1 = "https://www.filmweb.pl/films/search";
            var httpklient1 = new HttpClient();
            var html1 = await httpklient.GetStringAsync(url1);

            var html_plik1 = new HtmlDocument();
            html_plik1.LoadHtml(html1);

            var test3 = html_plik1.DocumentNode.Descendants("a")
             .Where(node => node.GetAttributeValue("title", "")
             .Equals("następna")).ToList();

            var test4 = html_plik1.DocumentNode.Descendants("h2")
                .Where(node => node.GetAttributeValue("class", "")
                .Equals("filmPreview__title")).ToList();
                */
            string path = Path.Combine(Environment.CurrentDirectory, @"baza\", "netflix.txt");
            //find element  contain number of pages
            var url = "https://upflix.pl/netflix/film";
            var httpklient = new HttpClient();
            var html= await httpklient.GetStringAsync(url);

            var html_file = new HtmlDocument();
            html_file.LoadHtml(html);


            string s_counter = html_file.DocumentNode.Descendants("li")
           .Where(node => node.GetAttributeValue("class", "")
           .Equals("last")).FirstOrDefault().InnerHtml.ToString().Substring(27,3);

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
                   Title.Add(new Film() { Name = a[j]});
                        
                    }

            }
            // Save(Tytuly, path);

            // Console.ReadLine();
            return Title;
        }
    }
}
