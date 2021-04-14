﻿using HtmlAgilityPack;
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

        static void Main(string[] args)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"baza\","netflix.txt");
            List<Film> Film_z_netflix_test = new List<Film>();
            GetHtmlAsync(Film_z_netflix_test);
            Save(Film_z_netflix_test, path);
            Console.ReadLine();
        }

           private static void Save(List<Film> Tytuly1,string sciezka)
        {
            using (StreamWriter sciezkadoksiazki = new StreamWriter(sciezka, false))
            {
                string listai = "";
                for (int i = 0; i < Tytuly1.Count; i++)
                {
                    listai += Tytuly1[i].nazwa + "\r\n";
                }
                sciezkadoksiazki.Write(listai);

            }
        }

        private static async void GetHtmlAsync(List<Film> Tytuly)
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
            int x = 1;
            var url = "https://upflix.pl/netflix/film";
            var httpklient = new HttpClient();
            var html= await httpklient.GetStringAsync(url);

            var html_plik = new HtmlDocument();
            html_plik.LoadHtml(html);


            string s_licznik = html_plik.DocumentNode.Descendants("li")
           .Where(node => node.GetAttributeValue("class", "")
           .Equals("last")).FirstOrDefault().InnerHtml.ToString().Substring(40,3);

            int licznik = Int32.Parse(s_licznik) + 1;
            Tytuly = new List<Film>();
            for (int i = 1; i < 3; i++)//zmien 3 na licznik
            {
                var url1 = "https://upflix.pl/netflix/film/p" + i + "?search=";
                var httpklient1 = new HttpClient();
                var html1 = await httpklient1.GetStringAsync(url1);
                var html_plik1 = new HtmlDocument();
                html_plik1.LoadHtml(html1);

                var baza = html_plik1.DocumentNode.Descendants("div")
             .Where(node => node.GetAttributeValue("class", "")
             .Equals("info")).ToList();

               // int aaa = baza.Count * licznik;
                int ab = baza.Count;
                string[] a = new string[ab];
                

                for (int j = 0; j < baza.Count; j++)
                {
                    a[j] = baza[j].InnerText;
                    a[j] = a[j].Substring(0, a[j].Length - 9);
                   Console.WriteLine(a[j]);
                    Tytuly.Add(new Film() { nazwa = a[j] });
                        
                    }

            }
           // Save(Tytuly, path);

           // Console.ReadLine();

        }
    }
}
