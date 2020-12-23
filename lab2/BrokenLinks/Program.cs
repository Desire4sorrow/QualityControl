using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HtmlAgilityPack;
using System.Net.Http;
using System.Text;

namespace BrokenLinks
{
    class CheckBrokenLinks
    {
        private static List<string> m_validLinks = new List<string>();
        private static List<string> m_invalidLinks = new List<string>(); 
        private static HttpClient client = new HttpClient();
        private static string m_fullLink;
        private static List<string> m_links = new List<string>();


        public CheckBrokenLinks(string link)
        {
            m_fullLink = link;  
        }
        private static bool CheckStatusCode(HttpResponseMessage res) //valid
        {
            int statusCode = (int)res.StatusCode;
            if (statusCode < 400 && 
                statusCode >= 200)
            {
                return true;
            }
            else
            {
                return false;
            }
        
        }

        public List<string> GetValidLinks() 
        {
            return m_validLinks;
        }

        public List<string> GetInvalidLinks()
        {
            return m_invalidLinks;
        }

        private static void CheckGetLinks(string thisLinks)
        {
            try
            {
                GetLinks(thisLinks);
            }
            catch (System.ArgumentNullException)
            {
                return;
            }
        }

        private static void GetLinks(string currentLink)
        {
            HtmlWeb html = new HtmlWeb();
            HtmlDocument doc = html.Load(currentLink);

            HtmlNode[] nodes = doc.DocumentNode.SelectNodes("//a").ToArray();

            foreach (HtmlNode item in nodes)
            {
                string url = item.GetAttributeValue("href", null);
                if (!m_links.Contains(url) && Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute)) //check needed value
                {
                    m_links.Add(url);
                    CheckGetLinks(m_fullLink + url);
                }
            }
        }


        public void CheckAllLinks()
        {
            CheckGetLinks(m_fullLink);

            string url;
            string message;
            foreach (var refer in m_links)
            {
                url = refer;
                if (!refer.StartsWith("http://") && !refer.StartsWith("https://"))
                {
                    url = m_fullLink + refer;
                }

                using (var response = client.GetAsync(url).Result)
                {
                    int code = (int)response.StatusCode;
                    message = $"{url} {code.ToString()} {response.StatusCode}";        //distribution val, inval with link's params
                    if (CheckStatusCode(response))
                    {
                        m_validLinks.Add(message);                             
                    }
                    else
                    {
                        m_invalidLinks.Add(message);
                    }

                }
            }
        }

    }
    class Program
    {
        private static string CheckAvailableArguments(string[] args)
        {
            if (args.Length != COUNTER_ARGS)
            {
                throw new Exception("Incorrect number of args");
            }

            return args[0];
        }
        static void Main(string[] args)
        {
            try
            {
                string link = CheckAvailableArguments(args);           //check on correct args
                CheckBrokenLinks testing = new CheckBrokenLinks(link);
                testing.CheckAllLinks();

                TakeLinksInFiles(m_validOut, testing.GetValidLinks());
                TakeLinksInFiles(m_invalidOut, testing.GetInvalidLinks()); //take 2 files
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            m_validOut.Close();
            m_invalidOut.Close();
        }

        private static void TakeLinksInFiles(StreamWriter stream, List<string> links)
        {
            foreach (var link in links)
            {
                stream.WriteLine(link);
            }

            stream.WriteLine("Links count " + links.Count());
            stream.WriteLine("Date " + DateTime.Now);
        }

        private static readonly int COUNTER_ARGS = 1;
        private static StreamWriter m_validOut = new StreamWriter("../../../valid.txt", false, Encoding.UTF8);
        private static StreamWriter m_invalidOut = new StreamWriter("../../../invalid.txt", false, Encoding.UTF8);
    }
}
