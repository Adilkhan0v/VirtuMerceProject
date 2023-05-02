using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using VirtuMerce.Api;
using VirtuMerce.Contracts.Options;
using VirtuMerce.Dal;
using VirtuMerce.Dal.Providers.Abstract;
using VirtuMerce.Dal.Providers.EntityFramework;
using VitruMerce.Bll;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserProvider, UserProvider>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryProvider, CategoryProvider>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductProvider, ProductProvider>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.Configure<SecretOptions>(builder.Configuration.GetSection("SecretOptions"));

/*builder.Services.AddDbContext<ApplicationContext>(x =>
    x.UseInMemoryDatabase(builder.Configuration.GetConnectionString("InMemory")!));
*/
builder.Services.AddDbContext<ApplicationContext>(x =>
    x.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

#region Jwt Configuration

var secrets = builder.Configuration.GetSection("SecretOptions");

var key = Encoding.ASCII.GetBytes(secrets.GetValue<string>("JWTSecret")!);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});

#endregion

ConfigureServicesSwagger.ConfigureServices(builder.Services);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();