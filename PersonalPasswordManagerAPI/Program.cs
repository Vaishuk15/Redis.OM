using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PersonalPasswordManager.Repository;
using PersonalPasswordManager.Repository.Implementation;
using PersonalPasswordManager.Repository.Interface;
using PersonalPasswordManager.Services;
using PersonalPasswordManager.Services.Implementation;
using PersonalPasswordManager.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Allows any origin (frontend)
              .AllowAnyHeader()  // Allows any headers
              .AllowAnyMethod(); // Allows any methods (GET, POST, etc.)
    });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(AutoMapping));
builder.Services.AddDbContext<PasswordManagerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//builder.Services.AddTransient<IPasswordManagerRepository, PasswordManagerRepository>();
//builder.Services.AddTransient<IPasswordManagerService, PasswordManagerService>();
//module registration
new PasswordManagerRepositoryModule(builder.Services);
new PasswordManagerServiceModule(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Use CORS policy
app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
