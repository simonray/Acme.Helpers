
namespace Acme.Helpers.Website.Models
{
    public class PersonAddress
    {
        public override string ToString()
        {
            return AddressLine1?.ToString();
        }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public string AddressLines { get { return AddressLine1; } }
    }
}
