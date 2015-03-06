using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
        public Stack myS = new Stack();
        //public Queue listURL = new Queue();
        public List<string> listURL = new List<string>();
        public List<string> blockedListURL = new List<string>();
        WebClient webCrawler = new WebClient();

        public void CrawlerURL(string inputURL)
        {
            webCrawler.Proxy = new WebProxy("http://waytoheaven.bangsatya.com:5050");
            webCrawler.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            if (isValidURL(inputURL))
            {
                listURL.Add(inputURL);
                
                try
                {
                    Console.WriteLine("Telusur dalam try: " + inputURL);
                    byte[] tempResponse = webCrawler.DownloadData(inputURL);
                    
                }
                catch (Exception XX)
                {
                    Console.WriteLine("Telusur dalam exception:" + inputURL);
                    blockedListURL.Add(inputURL);
                    this.CrawlerURL(ambilMasukan());
                }
                Console.WriteLine("Telusur dalam kelas: " + inputURL);
                
                string s = webCrawler.DownloadString(inputURL);
                string temp2;

                foreach (Object i in LinkFinder.Find(s))
                {
                    if (!string.IsNullOrWhiteSpace(i.ToString()) && !string.IsNullOrWhiteSpace(inputURL))
                    {
                        if (this.isValidURL(i.ToString()))
                        {
                            temp2 = i.ToString();
                            if (!myQ.ToArray().Contains(temp2))
                            {
                                myQ.Enqueue(temp2);
                                File.AppendAllText("candidatedBrowsedLink.txt", temp2+Environment.NewLine);

                                Console.WriteLine(temp2);
                            }
                        }
                        else
                        {
                            if (i.ToString().Contains("javascript:") || i.ToString().Contains("mailto:"))
                            {
                            }
                            else
                            {
                                //Apakah input masukan mengandung / di akhir url
                                if (inputURL.EndsWith("/"))
                                {
                                    if (i.ToString().StartsWith("/"))
                                    {
                                        temp2 = inputURL + i.ToString().Substring(1);
                                        if (!myQ.ToArray().Contains(temp2))
                                        {
                                            myQ.Enqueue(temp2);
                                            File.AppendAllText("candidatedBrowsedLink.txt", temp2 + Environment.NewLine);

                                            Console.WriteLine(temp2);
                                        }
                                    }
                                    else
                                    {
                                        temp2 = inputURL + i.ToString();
                                        if (!myQ.ToArray().Contains(temp2))
                                        {
                                            myQ.Enqueue(temp2);
                                            File.AppendAllText("candidatedBrowsedLink.txt", temp2 + Environment.NewLine);

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
                                            File.AppendAllText("candidatedBrowsedLink.txt", temp2 + Environment.NewLine);

                                            Console.WriteLine(temp2);
                                        }
                                    }
                                    else
                                    {
                                        temp2 = inputURL + "/" + i.ToString();
                                        if (!myQ.ToString().Contains(temp2))
                                        {
                                            myQ.Enqueue(temp2);
                                            File.AppendAllText("candidatedBrowsedLink.txt", temp2 + Environment.NewLine);

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
                                    if (isValidURL(obj.ToString()))
                                    {
                                        Console.WriteLine("Sedang menelusuri: " + tempURL);
                                        File.AppendAllText("candidatedBrowsedLink.txt", tempURL + Environment.NewLine);
                                        
                                        this.CrawlerURL(tempURL);

                                    }
                                    else
                                    {
                                        Console.WriteLine("Sedang menelusuri #2: " + tempURL);
                                        File.AppendAllText("candidatedBrowsedLink.txt", tempURL + Environment.NewLine);

                                        this.CrawlerURL(ambilMasukan());
                                    }
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
        public void CrawlerURLDFS(string inputURL)
        {
            webCrawler.Proxy = new WebProxy("http://waytoheaven.bangsatya.com:5050");
            webCrawler.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
            if (isValidURL(inputURL))
            {
                listURL.Add(inputURL);

                try
                {
                    Console.WriteLine("Telusur dalam try: " + inputURL);
                    byte[] tempResponse = webCrawler.DownloadData(inputURL);

                }
                catch (Exception XX)
                {
                    Console.WriteLine("Telusur dalam exception:" + inputURL);
                    blockedListURL.Add(inputURL);
                    this.CrawlerURLDFS(ambilMasukanDFS());
                }
                Console.WriteLine("Telusur dalam kelas: " + inputURL);

                string s = webCrawler.DownloadString(inputURL);
                string temp2;

                foreach (Object i in LinkFinder.Find(s))
                {
                    if (!string.IsNullOrWhiteSpace(i.ToString()) && !string.IsNullOrWhiteSpace(inputURL))
                    {
                        if (this.isValidURL(i.ToString()))
                        {
                            temp2 = i.ToString();
                            if (!myS.ToArray().Contains(temp2))
                            {
                                myS.Push(temp2);
                                File.AppendAllText("candidatedBrowsedLink.txt", temp2 + Environment.NewLine);

                                Console.WriteLine(temp2);
                            }
                        }
                        else
                        {
                            if (i.ToString().Contains("javascript:") || i.ToString().Contains("mailto:"))
                            {
                            }
                            else
                            {
                                //Apakah input masukan mengandung / di akhir url
                                if (inputURL.EndsWith("/"))
                                {
                                    if (i.ToString().StartsWith("/"))
                                    {
                                        temp2 = inputURL + i.ToString().Substring(1);
                                        if (!myS.ToArray().Contains(temp2))
                                        {
                                            myS.Push(temp2);
                                            File.AppendAllText("candidatedBrowsedLink.txt", temp2 + Environment.NewLine);

                                            Console.WriteLine(temp2);
                                        }
                                    }
                                    else
                                    {
                                        temp2 = inputURL + i.ToString();
                                        if (!myS.ToArray().Contains(temp2))
                                        {
                                            myS.Push(temp2);
                                            File.AppendAllText("candidatedBrowsedLink.txt", temp2 + Environment.NewLine);

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
                                        if (!myS.ToString().Contains(temp2))
                                        {
                                            myS.Push(temp2);
                                            File.AppendAllText("candidatedBrowsedLink.txt", temp2 + Environment.NewLine);

                                            Console.WriteLine(temp2);
                                        }
                                    }
                                    else
                                    {
                                        temp2 = inputURL + "/" + i.ToString();
                                        if (!myS.ToString().Contains(temp2))
                                        {
                                            myS.Push(temp2);
                                            File.AppendAllText("candidatedBrowsedLink.txt", temp2 + Environment.NewLine);

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
                Console.WriteLine("Jumlah URL total: " + myS.Count);
                string tempURL;
                try
                {
                    //Console.WriteLine("dsadsadsa");
                    foreach (Object obj in myS)
                    {
                        //Console.WriteLine("dsadsadsa");
                        try
                        {
                            //Console.WriteLine("dsadsadsakahsoiajwniaiwjdnalwjndo");
                            tempURL = ambilMasukanDFS();
                            if (isValidURL(obj.ToString()))
                            {
                                Console.WriteLine("Sedang meneluuri: " + tempURL);
                                File.AppendAllText("DbrowsedLink.txt", tempURL + Environment.NewLine);

                                this.CrawlerURLDFS(tempURL);

                            }
                            else
                            {
                                Console.WriteLine("Sedang menelusuri #2: " + tempURL);
                                File.AppendAllText("DbrowsedLink.txt", tempURL + Environment.NewLine);
                                this.CrawlerURLDFS(ambilMasukan());
                            }
                        }
                        catch (Exception e)
                        {
                            tempURL = ambilMasukanDFS();
                            File.AppendAllText("DbrowsedLink.txt", tempURL + Environment.NewLine);
                            this.CrawlerURLDFS(ambilMasukanDFS());
                        }
                    }
                }
                catch (Exception E)
                {
                    this.CrawlerURLDFS(ambilMasukanDFS());
                }

            }
            else
            {
                this.CrawlerURLDFS(ambilMasukanDFS());
            }
        } 
        public bool isValidURL(string sURL)
        {
            return (!blockedListURL.ToString().Contains(sURL) &&!sURL.ToString().Contains("javascript:") && !sURL.ToString().Contains("mailto:") && !listURL.ToString().Contains(sURL) && ekstensiValid(sURL) && (sURL.ToString().Contains("http:") || 
                sURL.ToString().Contains("ftp:") || sURL.ToString().Contains("https:")));
        }
        public bool ekstensiValid(string eURL)
        {
            bool statusURL = true;
            
            string[] ekstensi = {"ppt", "ptx", "pps", "wmv", "mp4","3GP","AAC","ACE","AIF","ARJ","ASF","AVI","BIN","BZ2","EXE","GZ","GZIP","IMG","ISO","LZH","M4A","M4V","MKV","MOV","MP3","MP4","MPA","MPE","MPEG","MPG","MSI","MSU","OGG","OGV","PDF","PLJ","PPS","PPT","QT","R0*","R1*","RA","RAR","RM","SEA","SIT","SITX","TAR","TIF","TIFF","WAV","WMA","WMV","Z","ZIP","DOCX","XLSX","PPTX","SQL","GZIP","TAR","APK","PDF","PPTX","XLSX","DOCX","","PPT","PDF","DOC","PPT","XLS","ZIP","RAR","CHM","SQL","FLV"};
            
            return !ekstensi.Contains(eURL.Substring(eURL.Length-3));
        }
        public string ambilMasukan()
        {
            return myQ.Dequeue().ToString();
        }
        public string ambilMasukanDFS()
        {
            return myS.Pop().ToString();
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
        public static Stack FindDFS(string file)
        {
            //List<LinkItem> list = new List<LinkItem>();
            Stack list = new Stack();
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
                Match m2 = Regex.Match(value, @"href=\""(.*?)\""", RegexOptions.Singleline);
                if (m2.Success)
                {
                    i.Href = m2.Groups[1].Value;
                }

                //list.Add(i);
                if (!string.IsNullOrWhiteSpace(i.ToString()))
                {
                    list.Push(i);
                }
            }
            return list;
        }
    }
}
