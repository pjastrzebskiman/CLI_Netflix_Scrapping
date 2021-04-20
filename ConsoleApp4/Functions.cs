using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Functions
    {
        public delegate void getSave(List<Film> a, string sciezka);
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
        public getSave MySave
        {
            get
            {
                return new getSave((Save));
            }
        }
    }
}
