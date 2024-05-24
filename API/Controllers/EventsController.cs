using API.Database;
using API.Helpers;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using System.Net;
using System.Net.Mail;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly AutomationDbContext _dbContext;
        private readonly StripeSettings _stripeSettings;
        private readonly IEmailSenderService _emailSenderService;
        public EventsController(AutomationDbContext dbContext, IOptions<StripeSettings> stripeSettings, IEmailSenderService emailSenderService)
        {
            _dbContext = dbContext;
            _stripeSettings = stripeSettings.Value;
            _emailSenderService = emailSenderService;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
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
            var successUrl = "https://localhost:7220/api/events/paymentsuccess?session_id={CHECKOUT_SESSION_ID}";
            var cancelUrl = $"http://localhost:4200/event/{eventId}";
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


        [HttpGet("paymentsuccess")]
        public async Task<IActionResult> PaymentSuccess([FromQuery] string? session_id = null)
        {
            if (session_id is null) return BadRequest();

            var result = new Stripe.Checkout.SessionService();
            var customer = await result.GetAsync(session_id);

            var customerEmail = customer.CustomerDetails.Email;
            var lineItem = result.ListLineItems(session_id);

            var emailBody = Helpers.Helpers.ConstructEmailBody(lineItem.First().Description, (int)lineItem.First().Quantity, lineItem.First().AmountTotal);

            // Send Email
            await _emailSenderService.SendEmailAsync(customerEmail, $"{lineItem.First().Description} Ticket Payment Success", emailBody);

            return Redirect($"http://localhost:4200/payment-success/{session_id}");
        }

        [HttpGet("order-details/{sessionId}")]
        public async Task<ActionResult<PaymentSuccessOrderDetails>> GetOrderDetails(string sessionId)
        {
            var result = new Stripe.Checkout.SessionService();
            var lineItem = result.ListLineItems(sessionId);

            return new PaymentSuccessOrderDetails() { Title = lineItem.First().Description, Quantity = (int)lineItem.First().Quantity, TotalAmout = lineItem.First().AmountTotal };
        }
    }
}
