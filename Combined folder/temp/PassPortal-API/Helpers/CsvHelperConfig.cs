using CsvHelper.Configuration;
using PassPortal_API.DTOs;

namespace PassPortal_API.Helpers
{
    public class CsvHelperConfig : ClassMap<PassportOfficeDTO>
    {
        public CsvHelperConfig()
        {
            Map(m => m.OfficeName).Name("OfficeName");
            Map(m => m.City).Name("City");
            Map(m => m.State).Name("State");
            Map(m => m.Country).Name("Country");
            Map(m => m.ContactNumber).Name("ContactNumber");
            Map(m => m.Email).Name("Email");
        }
    }
}
