using Microsoft.EntityFrameworkCore;
using VisitorAPI.DAL;
using VisitorAPI.Hubs;
using VisitorAPI.Model;

var builder = WebApplication.CreateBuilder(args);
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SignalRContext>(opts => { opts.UseSqlServer(builder.Configuration["DefaultConnection"]); });
builder.Services.AddCors(opts => opts.AddPolicy("CorsPolicy", optsBuilder =>
{
    optsBuilder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(host => true);

}));
builder.Services.AddScoped<VisitorService>();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("CorsPolicy");
app.MapHub<VisitorHub>("/VisitorHub");
app.UseAuthorization();

app.MapControllers();

app.Run();
