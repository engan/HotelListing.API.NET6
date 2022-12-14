using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Core.Models.Country;
using AutoMapper;
using HotelListing.API.Contracts;
using Microsoft.AspNetCore.Authorization;
using HotelListing.API.Core.Exceptions;
using HotelListing.API.Core.Models;
using static HotelListing.API.Core.Models.QueryParameters;
using Microsoft.OData.ModelBuilder;
using System.Diagnostics.Metrics;

namespace HotelListing.API.Controllers
{
    [Route("api/v{version:apiVersion}/countries")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    public class CountriesController : ControllerBase
    {
        // private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICountriesRepository _countriesRepository;
        private readonly ILogger<CountriesController> _logger;

        public CountriesController(IMapper mapper, ICountriesRepository countriesRepository,
            ILogger<CountriesController> logger)
        {
            // _context = context;
            this._mapper = mapper;
            this._countriesRepository = countriesRepository;
            this._logger = logger;
        }

        // GET: api/Countries/GetAll
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            // var countries = await _context.Countries.ToListAsync();

            //var countries = await _countriesRepository.GetAllAsync();
            //var records = _mapper.Map<List<GetCountryDto>>(countries);
            //return Ok(records);

            var countries = await _countriesRepository.GetAllAsync<GetCountryDto>();
            return Ok(countries);
        }

        // GET: api/Countries/?StartIndex=0&pagesize=25&PageNumber=1
        [HttpGet]
        public async Task<ActionResult<PagedResult<GetCountryDto>>> GetPagedCountries([FromQuery] QueryParameters queryParameters)
        {
            var pagedCountriesResult = await _countriesRepository.GetAllAsync<GetCountryDto>(queryParameters);
            return Ok(pagedCountriesResult);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            //var country = await _context.Countries.Include(q => q.Hotels)
            //    .FirstOrDefaultAsync(q => q.Id == id);

            var country = await _countriesRepository.GetDetails(id);
            //if (country == null)
            //{
            //    throw new NotFoundException(nameof(GetCountry), id);
            //}
            //var countryDto = _mapper.Map<CountryDto>(country);
            //return Ok(countryDto);

            return Ok(country);
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto updateCountryDto)
        {
            if (id != updateCountryDto.Id)
            {
                return BadRequest("Invalid Record Id");
            }

            // var country = await _context.Countries.FindAsync(id);

            //var country = await _countriesRepository.GetAsync(id);
            //if (country == null)
            //{
            //    throw new NotFoundException(nameof(GetCountry), id);
            //}
            //_mapper.Map(updateCountryDto, country);

            try
            {
                //await _context.SaveChangesAsync();

                //await _countriesRepository.UpdateAsync(country);

                await _countriesRepository.UpdateAsync(id, updateCountryDto);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto createCountryDto)
        {

            //await _context.Countries.AddAsync(country);

            //await _context.SaveChangesAsync();

            //var country = _mapper.Map<Country>(createCountryDto);
            //await _countriesRepository.AddAsync(country);
            //return CreatedAtAction("GetCountry", new { id = country.Id }, country);

            var country = await _countriesRepository.AddAsync<CreateCountryDto, GetCountryDto>(createCountryDto);
            return CreatedAtAction(nameof(GetCountry), new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            //var country = await _context.Countries.FindAsync(id);

            //var country = await _countriesRepository.GetAsync(id);
            //if (country == null)
            //{
            //    throw new NotFoundException(nameof(GetCountry), id);
            //}

            //_context.Countries.Remove(country);
            //await _context.SaveChangesAsync();

            await _countriesRepository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> CountryExists(int id)
        {
            //return _context.Countries.Any(e => e.Id == id);
            return await _countriesRepository.Exists(id);
        }
    }
}
