using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using VirtuMerce.Contracts.Options;
using VirtuMerce.Dal;
using VirtuMerce.Dal.Providers.Abstract;
using VirtuMerce.Dal.Providers.EntityFramework;
using VitruMerce.Bll;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserProvider, UserProvider>();
builder.Services.AddDbContext<ApplicationContext>(x =>
    x.UseInMemoryDatabase(builder.Configuration.GetConnectionString("InMemory")!));


builder.Services.Configure<SecretOptions>(builder.Configuration.GetSection("SecretOptions"));
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