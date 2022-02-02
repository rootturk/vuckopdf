using System.Data.SQLite;
using Dapper;
var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3002");
                          builder.WithOrigins("http://pdffactory:5001");
                      });
});


var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

DataHelper.CreateData();

app.MapGet("/GetSpecificContent", async (int id) =>
{
    string cs = @"URI=file:storage.db";

    var dynamicParameters = new DynamicParameters();
    dynamicParameters.Add("Id", id);

    using var con = new SQLiteConnection(cs);
    con.Open();  

    var res = con.QueryFirst<CityInfo>("SELECT * FROM CustomerInfo WHERE Id = @Id", dynamicParameters);
    
    if(res==null)
        return Results.NoContent();

    return Results.Ok(res);
});

app.MapGet("/GetDocuments", async () =>
{
    string cs = @"URI=file:storage.db";

    using var con = new SQLiteConnection(cs);
    await con.OpenAsync();    
    var res = con.Query<CityInfo>("SELECT * FROM CustomerInfo");

    return Results.Ok(res);
});


app.UseHttpsRedirection();

app.Run();
