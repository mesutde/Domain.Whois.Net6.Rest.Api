using HtmlAgilityPack;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain.Whois.Net6.Rest.Api.Utility
{
    public static class Helper
    {
        public static string FindStringBetween(string str, string from, string to)
        {
            int index = str.IndexOf(from);
            if (index == -1)
            {
                return null;
            }
            int num2 = str.IndexOf(to, (int)(index + from.Length));
            return ((num2 == -1) ? str.Substring(index + from.Length) : str.Substring(index + from.Length, (num2 - from.Length) - index));
        }

        public static string GetDomainFromUrl(string url)
        {
            url = url.Replace("https://", "").Replace("http://", "").Replace("www.", ""); //Remove the prefix
            string[] fragments = url.Split('/');
            return fragments[0];
        }

        public static bool IsValidUrl(string url)
        {
            Uri uri;
            if (Uri.TryCreate(url, UriKind.Absolute, out uri) == false)
            {
                return false;
            }
            return uri.Scheme == Uri.UriSchemeHttp ||
                    uri.Scheme == Uri.UriSchemeHttps ||
                    uri.Scheme == Uri.UriSchemeFtp;
        }

        public static bool IsUrlValid(string webUrl)
        {
            if (webUrl == null) return false;
            return Regex.IsMatch(webUrl, @"(http|https)://(([www\.])?|([\da-z-\.]+))\.([a-z\.]{2,3})$");
        }

        public static bool CheckURLValid(this string source)
        {
            Uri uriResult;
            return Uri.TryCreate(source, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
        }

        public static string GetFormatDateTRFormatter(string dt)
        {
            DateTime oDate = new DateTime();
            if (dt != null && !dt.Equals(DBNull.Value))
            {
                oDate = DateTime.Parse(dt);
            }

            string _MonthValue = string.Empty;

            if (oDate.Month.ToString().Length == 1)
                _MonthValue = "0" + oDate.Month;
            else
                _MonthValue = oDate.Month.ToString();

            string _DayValue = string.Empty;

            if (oDate.Day.ToString().Length == 1)
                _DayValue = "0" + oDate.Day;
            else
                _DayValue = oDate.Day.ToString();

            string rValue = _DayValue + "." + _MonthValue + "." + oDate.Year;

            return rValue;
        }

        public static bool IsValidURL(string URL)
        {
            string Pattern = @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$";
            Regex Rgx = new Regex(Pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
            return Rgx.IsMatch(URL);
        }

        public static string getHtmlSourceAgility(string link)
        {
            HtmlWeb webSite = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc;

            try
            {
                doc = webSite.Load(link);
            }
            catch (Exception)
            {
                return null;
            }

            return doc.DocumentNode.OuterHtml;
        }

        public static string getHtmlCode(string url)
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                client.Encoding = UTF8Encoding.UTF8;
                string htmlCode = "";

                try
                {
                    htmlCode = client.DownloadString(url);
                }
                catch (Exception)
                {
                }
                return System.Net.WebUtility.HtmlDecode(htmlCode);
            }
        }

        public static HtmlNodeCollection GetNodesToResult(string Url, string Nodes)
        {
            //.SelectNodes(@"div[@id='frmPnlProductGallery']/ul/li/a")
            //.SelectNodes("//div[@id='myID']")
            //.SelectNodes("//table[3]");
            //SelectNodes(".//span[@class='nobr']");
            //"//div[@class='link_row']//a[@href]");   var Link = getiframe[0].Attributes["href"].Value;

            string htmlCode = getHtmlCode(Url);

            if (String.IsNullOrEmpty(htmlCode))
            {
                htmlCode = getHtmlSourceAgility(Url);
            }

            if (htmlCode == null) return null;

            HtmlAgilityPack.HtmlDocument dokuman = new HtmlAgilityPack.HtmlDocument();
            dokuman.LoadHtml(htmlCode);
            HtmlNodeCollection basliklar = dokuman.DocumentNode.SelectNodes(Nodes);
            return basliklar;
        }
    }
}