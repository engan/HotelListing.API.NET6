using HotelListing.API.Core.Models.Hotels;

namespace HotelListing.API.Core.Models.Country
{
    public class CountryDto : BaseCountryDto
    {
        public int Id { get; set; }

        public List<HotelDto> Hotels { get; set; }
    }
}
