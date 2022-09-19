using HotelListing.API.Data;
using System.Threading.Tasks;

namespace HotelListing.API.Contracts
{
    public interface IHotelsRepository : IGenericRepository<Hotel>
    {
        // Task<Hotel> GetDetails(int id);
    }
}
