

using So2Baladna.API.Middlewares;
using So2Baladna.infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CROSPolicy", policy =>
    {
        policy
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials()
              .WithOrigins("http://localhost:4200")
              ;
    });
});

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Registering the infrastructure services that include the database context and repositories
builder.Services.AddInfrastructure(builder.Configuration);
// we should register the autoMapper configuration here
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.     
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CROSPolicy");
app.UseMiddleware<XssProtectionMiddleware>();

app.UseMiddleware<RateLimitingMiddleware>();

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();

app.UseStatusCodePagesWithReExecute("/errors/{0}");


app.UseHttpsRedirection();

app.UseStaticFiles();
app.MapControllers();

app.Run();
