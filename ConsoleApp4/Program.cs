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
           /* string path = Path.Combine(Environment.CurrentDirectory, @"baza\","netflix.txt");
            List<Film> Film_z_netflix_test = new List<Film>();
            Functions.getHtml gethtml = null;
            Functions function = new Functions();
            gethtml = new Functions.getHtml(function.MyHtml);
            await gethtml(Film_z_netflix_test);
            Functions.getSave getsave = null;
            getsave = new Functions.getSave(function.MySave);
            getsave(Film_z_netflix_test, path);*/


        }


        private static async Task<List<Film>> GetHtmlAsyncFilmweb(List<Film> Title)
        {
            string profile = "Piotrsowa96";
            var url = "https://www.filmweb.pl/user/"+profile+"/films";
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

            return Title;
           
        }
    }
}
