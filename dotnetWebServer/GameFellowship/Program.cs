using GameFellowship.Data.Database;
using GameFellowship.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<ILoginService, LoginService>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddSingleton<IGameService, GameService>();
builder.Services.AddSingleton<IPostService, PostService>();
builder.Services.AddTransient<IIconUploadService, IconUploadService>();

// Database
// builder.Services.AddDbContext<GameFellowshipDb>(options =>
// 	options.UseSqlite(builder.Configuration.GetConnectionString("GameFellowship")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
