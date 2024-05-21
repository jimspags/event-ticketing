using API.Database;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AutomationDbContext _dbContext;
        private readonly StripeSettings _stripeSettings;

        public EventsController(AutomationDbContext dbContext, IOptions<StripeSettings> stripeSettings)
        {
            _dbContext = dbContext;
            _stripeSettings = stripeSettings.Value;
        }

        [HttpGet]
        public async Task<ActionResult<List<Database.Entities.Event>>> GetAll()
        {
            return await _dbContext.Events.AsNoTracking().ToListAsync();
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

        [HttpGet("checkout/{eventId}/{quantity}")]
        public async Task<IActionResult> CreateCheckoutSession(Guid eventId, int quantity)
        {
            var getEvent = await _dbContext.Events.FirstOrDefaultAsync(x => x.Id == eventId);
            if (getEvent is null) return NotFound();
  
            var currency = "usd";
            var successUrl = "http://localhost:4200";
            var cancelUrl = "http://localhost:4200/event/" + eventId;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card"
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = currency,
                            UnitAmount = Convert.ToInt32(getEvent.Price) * 100,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = getEvent.Title,
                                Description = getEvent.Description
                            },
                        },
                        Quantity = quantity
                    }
                },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl
            };

            var service = new SessionService();
            var session = service.Create(options);
            //SessionId = session.Id;

            return Ok(new { Url = session.Url });
        }

        [HttpGet("filter/{category}")]
        public async Task<ActionResult<List<Database.Entities.Event>>> Filter(string category, [FromQuery] string? search = null)
        {
            search = search ?? string.Empty;

            var events = _dbContext.Events
                .AsNoTracking()
                .Where(x =>
                    x.Title.ToLower().Contains(search.ToLower()) ||
                    x.Description.ToLower().Contains(search.ToLower())
                )
                .AsQueryable();

            var dateTimeNow = DateTimeOffset.Now;

            switch (category)
            {
                case "all":
                    break;
                case "upcoming":
                    events = events.Where(x => x.Date > dateTimeNow);
                    break;
                case "recent":
                    events = events.Where(x => x.Date <= dateTimeNow && x.Date >= dateTimeNow.AddDays(-30));
                    break;
                default:
                    return BadRequest();
            }

            return await events.ToListAsync();
        }

    }
}
