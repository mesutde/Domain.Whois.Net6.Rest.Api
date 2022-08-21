using Domain.Whois.Net6.Rest.Api.Model;
using Newtonsoft.Json;

namespace Domain.Whois.Net6.Rest.Api.Utility
{
    public class DomainWhoisHelper
    {
        public static DomainWhoisModel GetResultDomainWhois(string WhoisDomain)
        {
            DomainWhoisModel VresultDwh = new DomainWhoisModel();

            VresultDwh = GetStatScropDomainWhois(WhoisDomain);

            //if (Dwh.DomainName.IndexOf(" Rate Limit") >= 0)
            //    GetWhois();

            if (VresultDwh == null || VresultDwh.DomainName != null)
            {
                if (VresultDwh == null || VresultDwh.DomainName.IndexOf(" Rate Limit") >= 0)
                    VresultDwh = GetWhois(WhoisDomain);
            }

            if (VresultDwh == null || VresultDwh.DomainName != null)
            {
                if (VresultDwh == null || VresultDwh.DomainName.IndexOf(" Rate Limit") >= 0)
                    VresultDwh = GetWhoisSsoft(WhoisDomain);
            }

            return VresultDwh;
        }

        public static DomainWhoisModel GetWhoisSsoft(string url)
        {
            DomainWhoisModel Dwh = new DomainWhoisModel();
            string HtmlCode = Helper.getHtmlCode("http://whoissoft.com/" + url);
            if (HtmlCode == null) return null;

            string strIndexof = "<h3>WHOIS Lookup For Domain Name";

            HtmlCode = HtmlCode.Substring(HtmlCode.IndexOf(strIndexof), HtmlCode.Length - HtmlCode.IndexOf(strIndexof));

            if (HtmlCode == null) return null;

            Dwh.DomainName = Helper.FindStringBetween(HtmlCode, "Domain Name: ", "<br />");
            Dwh.RegistrantName = Helper.FindStringBetween(HtmlCode, "Registrant Name: ", "<br />");
            Dwh.RegistrantOrganization = Helper.FindStringBetween(HtmlCode, "Registrant Organization: ", "<br />");
            Dwh.RegistrantStreet = Helper.FindStringBetween(HtmlCode, "Registrant Street: ", "<br />");
            Dwh.RegistrantCity = Helper.FindStringBetween(HtmlCode, "Registrant City: ", "<br />");
            Dwh.RegistrantPostalCode = Helper.FindStringBetween(HtmlCode, "Registrant Postal Code: ", "<br />");
            Dwh.RegistrantCountry = Helper.FindStringBetween(HtmlCode, "Registrant Country: ", "<br />");
            Dwh.RegistrantPhone = Helper.FindStringBetween(HtmlCode, "Registrant Phone: ", "<br />");
            Dwh.RegistrantEmail = Helper.FindStringBetween(HtmlCode, "Registrant Email: ", "<br />");
            Dwh.NameServer = Helper.FindStringBetween(HtmlCode, "Name Server: ", "<br />");
            Dwh.CreationDate = Helper.FindStringBetween(HtmlCode, "Creation Date: ", "<br />");
            Dwh.UpdatedDate = Helper.FindStringBetween(HtmlCode, "Updated Date: ", "<br />");
            Dwh.ExpirationDate = Helper.FindStringBetween(HtmlCode, "Registrar Registration Expiration Date: ", "<br />");
            Dwh.DomainRegistrar = Helper.FindStringBetween(HtmlCode, "Registrar: ", "<br />");

            return Dwh;
        }

        public static DomainWhoisModel GetWhois(string url)
        {
            DomainWhoisModel Dwh = new DomainWhoisModel();

            string HtmlCode = Helper.getHtmlSourceAgility("https://www.whois.com/whois/" + url);
            if (HtmlCode == null) return null;
            Dwh.DomainName = Helper.FindStringBetween(HtmlCode, "Domain Name: ", "\n");
            Dwh.RegistrantName = Helper.FindStringBetween(HtmlCode, "Registrant Name: ", "\n");
            Dwh.RegistrantOrganization = Helper.FindStringBetween(HtmlCode, "Registrant Organization: ", "\n");
            Dwh.RegistrantStreet = Helper.FindStringBetween(HtmlCode, "Registrant Street: ", "\n");
            Dwh.RegistrantCity = Helper.FindStringBetween(HtmlCode, "Registrant City: ", "\n");
            Dwh.RegistrantPostalCode = Helper.FindStringBetween(HtmlCode, "Registrant Postal Code: ", "\n");
            Dwh.RegistrantCountry = Helper.FindStringBetween(HtmlCode, "Registrant Country: ", "\n");
            Dwh.RegistrantPhone = Helper.FindStringBetween(HtmlCode, "Registrant Phone: ", "\n");
            //  Dwh.RegistrantEmail = Helper.FindStringBetween(HtmlCode, "Registrant Email: ", ".com");
            Dwh.NameServer = Helper.FindStringBetween(HtmlCode, "Name Server: ", "\n");
            Dwh.CreationDate = Helper.FindStringBetween(HtmlCode, "Creation Date: ", "\n");
            Dwh.UpdatedDate = Helper.FindStringBetween(HtmlCode, "Updated Date: ", "\n");
            Dwh.ExpirationDate = Helper.FindStringBetween(HtmlCode, "Registrar Registration Expiration Date: ", "\n");
            Dwh.DomainRegistrar = Helper.FindStringBetween(HtmlCode, "Registrar: ", "\n");

            return Dwh;
        }

        public static DomainWhoisModel GetStatScropDomainWhois(string url)
        {
            DomainWhoisModel Dwh = new DomainWhoisModel();

            string HtmlCode = Helper.getHtmlSourceAgility("https://www.statscrop.com/" + url);
            if (HtmlCode == null) return null;

            string h1hash = Helper.FindStringBetween(HtmlCode, "<script>var hash='", "', hash2");
            if (h1hash == null) return null;

            string updatetime = Helper.FindStringBetween(HtmlCode, "update_time=", ",is_updating");

            string getStatsJson = Helper.getHtmlSourceAgility(
                "https://www.statscrop.com/data/www-domain/?ac=whois&domain=" + url + "&hash=" + h1hash + "&ut=" + updatetime + "&__source_origin=https%3A%2F%2Fwww.statscrop.com");

            StatscropModel myDeserializedClass = JsonConvert.DeserializeObject<StatscropModel>(getStatsJson);

            string[] lstInterest = myDeserializedClass.whois.Split("\n");

            if (lstInterest.Count() < 5) return null;

            Dwh.DomainName = lstInterest[0].Replace("Domain Name: ", "").Trim();
            Dwh.RegistrantName = lstInterest[11].Replace("Registrant Name: ", "").Trim();
            Dwh.RegistrantOrganization = lstInterest[12].Replace("Registrant Organization: ", "").Trim();
            Dwh.RegistrantStreet = lstInterest[13].Replace("Registrant Street: ", "").Trim();
            Dwh.RegistrantCity = lstInterest[14].Replace("Registrant City: ", "").Trim();
            Dwh.RegistrantPostalCode = lstInterest[16].Replace("Registrant Postal Code: ", "").Trim();
            Dwh.RegistrantCountry = lstInterest[17].Replace("Registrant Country: ", "").Trim();
            Dwh.RegistrantPhone = lstInterest[18].Replace("Registrant Phone: ", "").Trim();
            Dwh.RegistrantEmail = lstInterest[22].Replace("Registrant Email: ", "").Trim();
            Dwh.NameServer = lstInterest[49].Replace("Name Server: ", "").Trim();
            Dwh.CreationDate = lstInterest[5].Replace("Creation Date: ", "").Trim();
            Dwh.UpdatedDate = lstInterest[4].Replace("Updated Date: ", "").Trim();
            Dwh.ExpirationDate = lstInterest[6].Replace("Registrar Registration Expiration Date: ", "").Trim();
            Dwh.DomainRegistrar = lstInterest[7].Replace("Registrar: ", "").Trim();

            return Dwh;
        }
    }
}