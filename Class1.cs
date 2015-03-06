using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using System.Collections;

namespace WebCrawlerStima
{
    class Crawler
    {

        public Queue myQ = new Queue();
        //public Queue listURL = new Queue();
        public List<string> listURL = new List<string>();
        WebClient webCrawler = new WebClient();
        
        public void CrawlerURL(string inputURL)
        {

            if (isValidURL(inputURL))
            {
                listURL.Add(inputURL);
                string s = webCrawler.DownloadString(inputURL);
                if (s.Length < 10)
                {
                    this.CrawlerURL(ambilMasukan());
                }
                
                string temp2;

                foreach (Object i in LinkFinder.Find(s))
                {
                    if (!string.IsNullOrWhiteSpace(i.ToString()) && !string.IsNullOrWhiteSpace(inputURL))
                    {
                        if (this.isValidURL(i.ToString()))
                        {
                            temp2 = i.ToString();
                            if (!myQ.ToString().Contains(temp2))
                            {
                                myQ.Enqueue(temp2);
                                Console.WriteLine(temp2);
                            }
                        }
                        else
                        {
                            if (i.ToString().Contains("javascript:") || i.ToString().Contains("mailto:"))
                            {
                                //do nothing
                            }
                            else
                            {
                                //Apakah input masukan mengandung / di akhir url
                                if (inputURL.EndsWith("/"))
                                {
                                    if (i.ToString().StartsWith("/"))
                                    {
                                        temp2 = inputURL + i.ToString().Substring(1);
                                        if (!myQ.ToString().Contains(temp2))
                                        {
                                            myQ.Enqueue(temp2);
                                            Console.WriteLine(temp2);
                                        }
                                    }
                                    else
                                    {
                                        temp2 = inputURL + i.ToString();
                                        if (!myQ.ToString().Contains(temp2))
                                        {
                                            myQ.Enqueue(temp2);
                                            Console.WriteLine(temp2);
                                        }
                                    }
                                    //jika tidak maka cek url dalam queue
                                }
                                else
                                {
                                    inputURL = inputURL.ToString().Substring(0, inputURL.ToString().LastIndexOf("/") + 1);
                                    if (i.ToString().StartsWith("/"))
                                    {
                                        temp2 = inputURL + i.ToString();
                                        if (!myQ.ToString().Contains(temp2))
                                        {
                                            myQ.Enqueue(temp2);
                                            Console.WriteLine(temp2);
                                        }
                                    }
                                    else
                                    {
                                        temp2 = inputURL + "/" + i.ToString();
                                        if (!myQ.ToString().Contains(temp2))
                                        {
                                            myQ.Enqueue(temp2);
                                            Console.WriteLine(temp2);
                                        }
                                    }
                                }
                            }

                        }


                    }
                    else
                    {
                        Console.WriteLine("masukan null");
                    }

                    }
                    Console.WriteLine("Jumlah URL total: " + myQ.Count);
                    string tempURL;
                    try
                    {
                        foreach (Object obj in myQ)
                        {

                            try
                            {
                                tempURL = ambilMasukan();
                                Console.WriteLine("Sedang menelusuri: " + tempURL);
                                this.CrawlerURL(tempURL);
                            }
                            catch (Exception e)
                            {
                                this.CrawlerURL(ambilMasukan());
                            }
                        }
                    }
                    catch (Exception E)
                    {
                        this.CrawlerURL(ambilMasukan());
                    }

                }
                else
                {
                    this.CrawlerURL(ambilMasukan());
                }
        } 
        public bool isValidURL(string sURL)
        {
            return (!sURL.ToString().Contains("javascript:") && !sURL.ToString().Contains("mailto:") && !listURL.ToString().Contains(sURL) && (sURL.ToString().Contains("http:") || sURL.ToString().Contains("ftp:") || sURL.ToString().Contains("https:")));
        }
        public string ambilMasukan()
        {
            return myQ.Dequeue().ToString();
        }
    }
    public struct LinkItem
    {
        public string Href;

        public override string ToString()
        {
            return Href;
        }
    }
    static class LinkFinder
    {
        //public static List<LinkItem> Find(string file)
        public static Queue Find(string file)
        {
            //List<LinkItem> list = new List<LinkItem>();
            Queue list = new Queue();
            // 1.
            // Find all matches in file.
            MatchCollection m1 = Regex.Matches(file, @"(<a.*?>.*?</a>)",
                RegexOptions.Singleline);

            // 2.
            // Loop over each match.
            foreach (Match m in m1)
            {
                string value = m.Groups[1].Value;
                LinkItem i = new LinkItem();

                // 3.
                // Get href attribute.
                Match m2 = Regex.Match(value, @"href=\""(.*?)\""",RegexOptions.Singleline);
                if (m2.Success)
                {
                    i.Href = m2.Groups[1].Value;
                }

                //list.Add(i);
                if (!string.IsNullOrWhiteSpace(i.ToString()))
                {
                    list.Enqueue(i);
                }
            }
            return list;
        }
    }
}
