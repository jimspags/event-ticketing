using API.Configuration;
using API.Database;
using API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AutomationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Add environment variables to configuration
builder.Configuration.AddEnvironmentVariables();

var stripeSettings = new StripeSettings()
{
    PublicKey = Environment.GetEnvironmentVariable("StripeSettings__PublishableKey"),
    SecretKey = Environment.GetEnvironmentVariable("StripeSettings__SecretKey")
};

var emailCreds = new EmailCredential()
{
    Email = Environment.GetEnvironmentVariable("EventTicketing__Email"),
    Password = Environment.GetEnvironmentVariable("EventTicketing__Password")
};

builder.Services.AddSingleton(emailCreds);
builder.Services.AddSingleton(stripeSettings);
builder.Services.AddSingleton<IEmailSenderService, EmailSenderService>();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
