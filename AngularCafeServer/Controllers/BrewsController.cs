using AngularCafeServer.Context;
using AngularCafeServer.DTOs.BrewDtos;
using AngularCafeServer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularCafeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrewsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BrewsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Brews
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brew>>> GetBrews()
        {
            return await _context.Brews.ToListAsync();
        }

        // GET: api/Brews/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brew>> GetBrew(int id)
        {
            var brew = await _context.Brews.FindAsync(id);

            if (brew == null)
            {
                return NotFound();
            }

            return brew;
        }

        // PUT: api/Brews/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrew(int id, UpdateBrewDto brewDto)
        {

            if (id != brewDto.Id)
            {
                return BadRequest();
            }

            var brew = new Brew
            {
                Id = id,
                Title = brewDto.Title,
                Icon = brewDto.Icon,
                Description = brewDto.Description,  
            };

            _context.Entry(brew).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrewExists(id))
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

        // POST: api/Brews
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Brew>> PostBrew(CreateBrewDto brewDto)
        {
            var brew = new Brew
            {
                Description = brewDto.Description,
                Icon = brewDto.Icon,
                Title = brewDto.Title,  
            };

            _context.Brews.Add(brew);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBrew", new { id = brew.Id }, brew);
        }

        // DELETE: api/Brews/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrew(int id)
        {
            var brew = await _context.Brews.FindAsync(id);
            if (brew == null)
            {
                return NotFound();
            }

            _context.Brews.Remove(brew);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BrewExists(int id)
        {
            return _context.Brews.Any(e => e.Id == id);
        }
    }
}
