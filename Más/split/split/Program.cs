using System;
using System.IO;

namespace split
{
    class Program
    {
        static void Main(string[] args)
        {
            //string[] kodoltAdatok = new string[5];
            //int x = 0;

            Console.WriteLine("1.feladat");
            StreamReader sr = new StreamReader("szoveg.txt");
            //string st = sr.ReadLine();
            int[] adatok = new int[4];

            //cut = st.Split(" ");
            string sor = "";
            int i = 0;
            while (!sr.EndOfStream)
            {
                sor = sr.ReadLine();
                adatok[i] = Convert.ToInt32(sor.Split("\n"));
                i++;
            }

            sr.Close();

            //string[] jeloltek = new string[4];
            //int i = 0;
            //StreamReader sr = new StreamReader("szoveg.txt");
            //string[] cut = new string[4];
            //string st = sr.ReadLine();
            //int x = 0;
            //while (st != null)
            //{
            //    x++;
            //    cut = st.Split(' ');
            //    kodoltAdatok[x] = cut[x];
            //    st = sr.ReadLine();
            //}
            //sr.Close();

            Console.WriteLine("a TXT : " + adatok[0] + adatok[1] + adatok[2] + adatok[3] + adatok[4] + "");
            Console.ReadLine();
        }
    }
}
