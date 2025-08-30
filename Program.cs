using Artist_api1.Models;
using System;
using static Artist_api1.Controllers.UserController;

var builder = WebApplication.CreateBuilder(args);

/// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ArtistContext>();
builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}



app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseMiddleware<AdminAuthMiddleware>();
app.MapControllers();
app.Run();
