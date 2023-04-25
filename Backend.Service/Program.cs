using Backend.Application.Extensions;
using Backend.Application.Filters;
using Backend.Domain.EntityModels;
using Backend.Infuructures.Concrete;
using Backend.Infuructures.Extensions;
using Backend.Persistence.axtr;
using Backend.Persistence.Database;
using Backend.Service.Configurations.ColumnWriters;
using Backend.Service.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<CustomFilter>();
    options.Filters.Add<ExceptionFilter>();
}).AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Katmans sdfjjkdshf
builder.Services.AddApplication();
builder.Services.AddInfuructers<LocalStorage>();
builder.Services.AddPresentation();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlite("DataSource=./Database.db"));
//Identity Configuration
builder.Services.AddIdentity<Employe, Role>()
   .AddEntityFrameworkStores<DataContext>()
   .AddDefaultTokenProviders();

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("./logs/log.txt")
    //
    //.WriteTo.SQLite(Environment.CurrentDirectory + @"\logs\log.db")
    .WriteTo.PostgreSQL("User ID=postgres;Password=12345678;Host=localhost;Port=5433;Database=ApiApp;", "Logs"
        ,needAutoCreateTable:true
        ,columnOptions:new Dictionary<string, ColumnWriterBase>
        {
            {"message",new RenderedMessageColumnWriter() },
            {"message_template",new MessageTemplateColumnWriter() },
            {"level",new LevelColumnWriter() },
            {"time_stamp",new TimestampColumnWriter() },
            {"exception",new ExceptionColumnWriter() },
            {"log_event",new LogEventSerializedColumnWriter() },
            {"username",new  UserColumnWriter()}
        })
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog(log);

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;

    // Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings.
    options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"])),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false
    };
});
var app = builder.Build();

app.UseSerilogRequestLogging();

app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
           .SetIsOriginAllowed(origin => true)
           .AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader());

app.UseAuthentication();

app.UseAuthorization();

app.Use(async(context, next) => {await app.AddUserLog(context, next); });

app.MapControllers();

app.Run();
