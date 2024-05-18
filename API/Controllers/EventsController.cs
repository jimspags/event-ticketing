using API.Database;
using API.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AutomationDbContext _dbContext;

        public EventsController(AutomationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Event>>> GetAll()
        {
            return await _dbContext.Events.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var getEvent = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == id);

            if (getEvent is null)
            {
                return NotFound();
            }

            return Ok(getEvent);
        }
    }
}
