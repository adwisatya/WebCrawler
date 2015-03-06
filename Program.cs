
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace WebCrawlerStima
{
    class Program
    {
        static void Main(string[] args)
        {

            //string[] lines = { "First line", "Second line", "Third line" };
            // WriteAllLines creates a file, writes a collection of strings to the file, 
            // and then closes the file.


            int a;
            Console.WriteLine("11");
            Crawler crawler = new Crawler();
            Console.WriteLine("1");
            crawler.CrawlerURL("http://itb.ac.id/");
            Console.WriteLine("2");
            string s = "http://itb.ac.id/about/";
            Console.Write(s.Substring(0,12));
            
            //Console.Read();
            
        }
    }

}
