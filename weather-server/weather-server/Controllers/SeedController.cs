using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CountryModel;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper;
using weather_server.Data;
using Microsoft.Extensions.Hosting;

namespace weather_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController(CountriesSourceContext db, IHostEnvironment environment) : ControllerBase
    {
        private readonly string _pathName = Path.Combine(environment.ContentRootPath, "Data/worldcities.csv");

        [HttpPost("City")]
        public async Task<ActionResult<City>> SeedCity()
        {
            Dictionary<string, Country> countries = await db.Countries//.AsNoTracking()
            .ToDictionaryAsync(c => c.Name);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };
            int cityCount = 0;
            using (StreamReader reader = new(_pathName))
            using (CsvReader csv = new(reader, config))
            {
                IEnumerable<WorldCitiesCSV>? records = csv.GetRecords<WorldCitiesCSV>();
                foreach (WorldCitiesCSV record in records)
                {
                    if (!countries.TryGetValue(record.country, out Country? value))
                    {
                        Console.WriteLine($"Not found country for {record.city}");
                        return NotFound(record);
                    }

                    if (!record.population.HasValue || string.IsNullOrEmpty(record.city_ascii))
                    {
                        Console.WriteLine($"Skipping {record.city}");
                        continue;
                    }
                    City city = new()
                    {
                        Name = record.city,
                        Latitude = record.lat,
                        Longitude = record.lng,
                        Population = (int)record.population.Value,
                        CountryId = value.CountryId
                    };
                    db.Cities.Add(city);
                    cityCount++;
                }
                await db.SaveChangesAsync();
            }
            return new JsonResult(cityCount);
        }

        [HttpPost("Country")]
        public async Task<ActionResult<City>> SeedCountry()
        {
            // create a lookup dictionary containing all the countries already existing 
            // into the Database (it will be empty on first run).
            Dictionary<string, Country> countriesByName = db.Countries
                .AsNoTracking().ToDictionary(x => x.Name, StringComparer.OrdinalIgnoreCase);

            CsvConfiguration config = new(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                HeaderValidated = null
            };

            using StreamReader reader = new(_pathName);
            using CsvReader csv = new(reader, config);

            List<WorldCitiesCSV> records = csv.GetRecords<WorldCitiesCSV>().ToList();
            foreach (WorldCitiesCSV record in records)
            {
                if (countriesByName.ContainsKey(record.country))
                {
                    continue;
                }

                Country country = new()
                {
                    Name = record.country,
                    Iso2 = record.iso2,
                    Iso3 = record.iso3
                };
                await db.Countries.AddAsync(country);
                countriesByName.Add(record.country, country);
            }

            await db.SaveChangesAsync();

            return new JsonResult(countriesByName.Count);
        }
    }
}
