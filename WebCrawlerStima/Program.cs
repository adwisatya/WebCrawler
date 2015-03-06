
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
            int a;
            Crawler crawler = new Crawler();
            crawler.CrawlerURLDFS("http://informatika.stei.itb.ac.id/~rinaldi.munir/");
            Console.Read();
            
        }

    }

}
