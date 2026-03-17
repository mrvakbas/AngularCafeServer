using AngularCafeServer.Context;
using AngularCafeServer.DTOs.MenuDtos;
using AngularCafeServer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularCafeServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly AppDbContext _context;
        public MenusController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Menus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenus()
        {
            return await _context.Menus.AsNoTracking().Include(x => x.Category).ToListAsync();
        }


        [HttpGet("latestFour")]
        public async Task<IActionResult> GetLatestFour()
        {
            var values = await _context.Menus.Include(x => x.Category).OrderByDescending(x => x.Id).Take(4).ToListAsync();
            return Ok(values);
        }


        // GET: api/Menus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenu(int id)
        {
            var menu = await _context.Menus.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);

            if (menu == null)
            {
                return NotFound();
            }

            return menu;
        }

        // PUT: api/Menus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(int id, UpdateMenuDto menuDto)
        {
            if (id != menuDto.Id)
            {
                return BadRequest();
            }

            var menu = new Menu
            {
                Id = menuDto.Id,
                Name = menuDto.Name,
                ImageUrl = menuDto.ImageUrl,
                Price = menuDto.Price,
                CategoryId = menuDto.CategoryId,
            };

            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
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

        // POST: api/Menus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(CreateMenuDto menuDto)
        {
            var menu = new Menu
            {
                Name = menuDto.Name,
                ImageUrl = menuDto.ImageUrl,
                Price = menuDto.Price,
                CategoryId = menuDto.CategoryId,
            };

            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenu", new { id = menu.Id }, menu);
        }

        // DELETE: api/Menus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(int id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }
    }
}