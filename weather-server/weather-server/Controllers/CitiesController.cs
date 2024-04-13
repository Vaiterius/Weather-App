using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CountryModel;
using weather_server.DTO;
using Microsoft.AspNetCore.Authorization;

namespace weather_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController(CountriesSourceContext context) : ControllerBase
    {
        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            return await context.Cities.ToListAsync();
        }

        [Authorize]
        [HttpGet("GetPopulation")]
        public async Task<ActionResult<IEnumerable<CountryPopulation>>> GetPopulation()
        {
            // Select statement returns string query, then convert into enumerable list (the results).
            IQueryable<CountryPopulation> query = from c in context.Countries
                select new CountryPopulation
                {
                    Name = c.Name,
                    CountryId = c.CountryId,
                    Population = c.Cities.Sum(t => t.Population)
                };
            return await query.ToListAsync();
        }

        // Other way to write the same thing as the method above.
        [HttpGet("GetPopulation2")]
        public async Task<ActionResult<IEnumerable<CountryPopulation>>> GetPopulation2()
        {
            IQueryable<CountryPopulation> query = context.Countries.Select(c =>
                new CountryPopulation
                {
                    Name = c.Name,
                    CountryId = c.CountryId,
                    Population = c.Cities.Sum(t => t.Population)
                });
            return await query.ToListAsync();
        }
    }
}
