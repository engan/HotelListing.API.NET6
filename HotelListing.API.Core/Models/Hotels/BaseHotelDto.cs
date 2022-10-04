using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Core.Models.Hotels
{
    public abstract class BaseHotelDto
    {
        [Required] // Delete if error
        public string Name { get; set; }
        [Required] // Delete if error
        public string Address { get; set; }
        public double? Rating { get; set; }
        [Required] // Delete if error
        [Range(1, int.MaxValue)]
        public int CountryId { get; set; }
    }
}
