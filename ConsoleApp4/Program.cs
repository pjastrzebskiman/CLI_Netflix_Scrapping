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
            string path = Path.Combine(Environment.CurrentDirectory, @"baza\","netflix1.txt");
            List<Film> Film_z_netflix_test = new List<Film>();
            Functions.getHtml gethtml = null;
            Functions function = new Functions();
            gethtml = new Functions.getHtml(function.MyHtml);
            await gethtml(Film_z_netflix_test);
            Functions.getSave getsave = null;
            getsave = new Functions.getSave(function.MySave);
            getsave(Film_z_netflix_test, path);


        }

       
    }
}
