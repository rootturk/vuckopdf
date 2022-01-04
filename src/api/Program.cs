using System.Data.SQLite;
using Dapper;
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000");
                      });
});


var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);
app.MapGet("/GetHtmlContent", async (string token) =>
{
    DataHelper.CreateData();

    var file = await File.ReadAllLinesAsync("template.html");
    string cs = @"URI=file:storage.db";

    using var con = new SQLiteConnection(cs);
    con.Open();    
    var res = con.Query<CityInfo>("SELECT * FROM CustomerInfo");

    foreach(var item in res){
        Console.WriteLine(item.City);
    }
    
    return Results.Ok(file);
});

app.MapGet("/GetDocuments", async () =>
{
    DataHelper.CreateData();

    var file = await File.ReadAllLinesAsync("template.html");
    string cs = @"URI=file:storage.db";

    using var con = new SQLiteConnection(cs);
    con.Open();    
    var res = con.Query<CityInfo>("SELECT * FROM CustomerInfo");

    return Results.Ok(res);
});


app.UseHttpsRedirection();

app.Run();
