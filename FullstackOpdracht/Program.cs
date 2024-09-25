using FullstackOpdracht.Data;
using FullstackOpdracht.Domains.Data;
using FullstackOpdracht.Domains.Entities;
using FullstackOpdracht.Repositories;
using FullstackOpdracht.Repositories.IDAO;
using FullstackOpdracht.Repositories.Interfaces;
using FullstackOpdracht.Services;
using FullstackOpdracht.Services.Interfaces;
using FullstackOpdracht.Services.IService;
using FullstackOpdracht.Util.Interfaces;
using FullstackOpdracht.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using FullstackOpdracht.Util.PDF.Interfaces;
using FullstackOpdracht.Util.PDF;
using FullstackOpdracht.Areas.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>() // voor roles
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My Proleague API",
        Version = "version 1",
        Description = "An API to get information",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "NVS",
            Email = "noah.vansteenlandt@student.vives.be",
            Url = new Uri("https://vives.be"),
        },
        License = new OpenApiLicense
        {
            Name = "Football API LICX",
            Url = new Uri("https://example.com/license"),
        }
    });
});
// Automapper
builder.Services.AddAutoMapper(typeof(Program));

// DI----------
builder.Services.AddDbContext<VoetbalDbContext>(options =>
    options.UseSqlServer(connectionString));

//stadium
builder.Services.AddTransient<IService<Stadium>, StadiumService>();
builder.Services.AddTransient<IDAO<Stadium>, StadiumDAO>();

//Match
builder.Services.AddTransient<IService<Match>, MatchService>();
builder.Services.AddTransient<IDAO<Match>, MatchDAO>();

//ExtendedMatch
builder.Services.AddTransient<ExtendedMatchDAO>();
builder.Services.AddTransient<ExtendedMatchService>();

//Team
builder.Services.AddTransient<IService<Team>, TeamService>();
builder.Services.AddTransient<IDAO<Team>, TeamDAO>();

//Section
builder.Services.AddTransient<IService<Section>, SectionService>();
builder.Services.AddTransient<IDAO<Section>, SectionDAO>();

//Booking
builder.Services.AddTransient<IService<Booking>, BookingService>();
builder.Services.AddTransient<IDAO<Booking>, BookingDAO>();

//Ticket
builder.Services.AddTransient<IService<Ticket>, TicketService>();
builder.Services.AddTransient<IDAO<Ticket>, TicketDAO>();

//Membership
builder.Services.AddTransient<IService<Membership>, MembershipService>();
builder.Services.AddTransient<IDAO<Membership>, MembershipDAO>();
builder.Services.AddTransient<ExtendedMembershipDAO>();
builder.Services.AddTransient<ExtendedMembershipService>();

//Ring
builder.Services.AddTransient<RingService>();
builder.Services.AddTransient<RingDAO>();

//ExtendedSection
builder.Services.AddTransient<ExtendedSectionDAO>();
builder.Services.AddTransient<ExtendedSectionService>();

//ExtendedSeat
builder.Services.AddTransient<ExtendedSeatDAO>();
builder.Services.AddTransient<ExtendedSeatService>();

//ExtendedTicket
builder.Services.AddTransient<ExtendedTicketDAO>();
builder.Services.AddTransient<ExtendedTicketService>();

//ExtendedBooking
builder.Services.AddTransient<ExtendedBookingDAO>();
builder.Services.AddTransient<ExtendedBookingService>();

//BookingTicket
builder.Services.AddTransient<IService<BookingTicket>, BookingTicketService>();
builder.Services.AddTransient<IDAO<BookingTicket>, BookingTicketDAO>();

//BookingMembership
builder.Services.AddTransient<IService<BookingMembership>, BookingMembershipService>();
builder.Services.AddTransient<IDAO<BookingMembership>, BookingMembershipDAO>();

//ExtendedBookingTicket
builder.Services.AddTransient<ExtendedBookingTicketDAO>();
builder.Services.AddTransient<ExtendedBookingTicketService>();

//user
builder.Services.AddTransient<IService<AspNetUser>, UserService>();
builder.Services.AddTransient<IDAO<AspNetUser>, UserDAO>();
builder.Services.AddTransient<ExtendedUserDAO>();
builder.Services.AddTransient<ExtendedUserService>();




builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
// Configuration.GetSection("EmailSettings")) zal de instellingen opvragen uit de
//AppSettings.json file en vervolgens wordt er een emailsettings - object
//aangemaakt en de waarden worden geïnjecteerd in het object
builder.Services.AddSingleton<IEmailSend, EmailSend>();
//Als in een Constructor een IEmailSender-parameter wordt gevonden, zal een
//emailSender - object worden aangemaakt. 

//mail Sender
builder.Services.AddTransient<IEmailSend, EmailSend>();
builder.Services.AddTransient<ICreatePDF, CreatePDF>();

// localisatie toevoegen
builder.Services.AddLocalization(
    options => options.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.SubFolder); // vertaling op View

var supportedCultures = new[] { "nl", "en", "fr" };

builder.Services.Configure<RequestLocalizationOptions>(options => {
    options.SetDefaultCulture(supportedCultures[0]) // default taal zal nl zijn, verwijst naar array op lijn 27
      .AddSupportedCultures(supportedCultures)  //Culture is used when formatting or parsing culture dependent data like dates, numbers, currencies, etc
      .AddSupportedUICultures(supportedCultures);  //UICulture is used when localizing strings, for example when using resource files.
});

// Culture from the HttpRequest
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

// Session
builder.Services.AddSession(options =>
{
    options.Cookie.Name = "Football.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(20);
});

//user

var app = builder.Build();

// culture localizatie
app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Profliga/Index");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var swaggerOptions = new FullstackOpdracht.Options.OptionsSwagger();
builder.Configuration.GetSection(nameof(FullstackOpdracht.Options.OptionsSwagger)).Bind(swaggerOptions);
// Enable middleware to serve generated Swagger as a JSON endpoint.
//RouteTemplate legt het path vast waar de JSON‐file wordt aangemaakt
app.UseSwagger(option => { option.RouteTemplate = swaggerOptions.JsonRoute; });
//// By default, your Swagger UI loads up under / swagger /.If you want to change this, it's thankfully very straight‐forward.
//Simply set the RoutePrefix option in your call to app.UseSwaggerUI in Program.cs:
app.UseSwaggerUI(option =>
{
    option.SwaggerEndpoint(swaggerOptions.UiEndpoint, swaggerOptions.Description);
});
app.UseSwagger();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Profliga}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
