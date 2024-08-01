using EmployeeDirectory.Dal.Interfaces;
using EmployeeDirectory.Dal;
using EmployeeDirectory.Services;
using EmployeeDirectory.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{

    options.AddPolicy("ApiCorsPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddTransient<IEmployeeService, EmployeeService>();
//builder.Services.AddTransient<IDataAccess, DataAccess>();
builder.Services.AddTransient<IDataAccessUsingSp, DataAccessUsingSp>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseCors("ApiCorsPolicy");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DefaultModelExpandDepth(0);
    });
}

app.UseAuthorization();

app.MapControllers();

app.Run();
