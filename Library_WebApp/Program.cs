using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using BlazorStrap;
using Library_WebApp.Model;
using Library_WebApp.Services;
using Library_WebApp.Services.HttpServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorStrap();

builder.Services.AddScoped<IStudentCRUD, StudentCRUD>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddSingleton<UserStateManagementService>();
builder.Services.AddHttpClient();
builder.Services.Configure<LibraryDataConfiguration>(builder.Configuration.GetSection("LibraryDataApi"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
