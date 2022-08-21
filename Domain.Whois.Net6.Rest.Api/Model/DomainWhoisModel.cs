namespace Domain.Whois.Net6.Rest.Api.Model
{
    public class DomainWhoisModel
    {
        public string DomainName { get; set; }
        public string TimeLeft { get; set; }
        public string RegistrantName { get; set; }
        public string RegistrantOrganization { get; set; }
        public string RegistrantStreet { get; set; }
        public string RegistrantCity { get; set; }
        public string RegistrantPostalCode { get; set; }
        public string RegistrantCountry { get; set; }
        public string RegistrantPhone { get; set; }
        public string RegistrantEmail { get; set; }
        public string NameServer { get; set; }
        public string UpdatedDate { get; set; }
        public string CreationDate { get; set; }
        public string ExpirationDate { get; set; }
        public string DomainRegistrar { get; set; }
        public string DomainServer { get; set; }
        public string SiteAge { get; set; }
        public string GlobalRank { get; set; }
        public string SiteStatus { get; set; }
        public string Rating { get; set; }
        public string SeoScore { get; set; }
        public string ServerLocation { get; set; }
        public string ServerIp { get; set; }
    }
}