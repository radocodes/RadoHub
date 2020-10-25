namespace RadoHub.Data.Models
{
    public class Culture
    {
        public Culture(string abreviation, string IsoCode)
        {
            this.Abbreviation = abreviation;
            this.IsoCode = IsoCode;
        }
        public string Abbreviation { get; }

        public string IsoCode { get; }
    }
}
