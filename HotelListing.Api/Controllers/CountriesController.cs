using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.Api.Data;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly HoteListingDbContext _context;
    public CountriesController(HoteListingDbContext context)
    {
        _context = context;
    }

    // GET: api/Country
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetCountry()
    {
        var countries = await _context.Countries.ToListAsync();
        return countries;
    }

    // GET: api/Country/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Country>> GetCountry(int id)
    {
        var country = await _context.Countries
            .Include(h => h.Hotels)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (country == null)
        {
            return NotFound("Couldn't find the country, sorry");
        }

        return country;
    }

    // PUT: api/Country/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int? id, Country country)
    {
        if (id != country.Id)
        {
            return BadRequest();
        }

        _context.Entry(country).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (! await CountryExistsAsync(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        // CHANGED IT MYSELF
        // return NoContent();
        return Ok(country);
    }

    // POST: api/Country
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(Country country)
    {
        _context.Countries.Add(country);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCountry", new { id = country.Id }, country);
    }

    // DELETE: api/Country/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int? id)
    {
        var country = await _context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        _context.Countries.Remove(country);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> CountryExistsAsync(int? id)
    {
        var country = await _context.Countries.AnyAsync(q => q.Id == id);
        return country;
    }
}
