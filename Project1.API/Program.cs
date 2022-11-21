using Microsoft.Extensions.DependencyInjection;
using Project1.Data;
using Project1.Logic;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = File.ReadAllText("/Revature/ConnectionStrings/PokeAppConnectionString.txt");

builder.Services.AddTransient<SqlRepository>();
//IRepository repo = SqlRepository(connectionString);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGet("/Tickets/Pending", (SqlRepository repo) => {
    repo.connectionString = connectionString;
    return repo.getReimbursementsPending();
});
app.MapPost("/Tickets/UserAll", (Tickets category, SqlRepository repo) =>
{
    repo.connectionString = connectionString;
    List<Tickets> d = repo.getReimbursementsUserAll(category.username);//(connValue, category);
    return d;//Results.Created($"/categories/{category.amount}", d);
});
app.MapPost("/Tickets/UserSpecific", (Tickets t, SqlRepository repo) => {
    repo.connectionString = connectionString;
    return repo.getReimbursementsUserSpecific(t.username,t.stat);
   
});
app.MapPost("/checkExists/Log", (User s, SqlRepository repo) => {
    repo.connectionString = connectionString;
    return repo.checkLogExists(s.username,s.password,s.managerStatus);

});

app.MapPost("/checkExists/User", (User name, SqlRepository repo) =>
{
    repo.connectionString = connectionString;
    return repo.checkUserExists(name.username);

});
app.MapPost("/Tickets",(Tickets t, SqlRepository repo) => {
    repo.connectionString= connectionString;
    repo.addReimbursement(t.username,t.amount,t.descript, t.type);
    //return Results.Created($"/categories/{repo.}", category);

});
app.MapPost("/users",(User u, SqlRepository repo) => {
    repo.connectionString = connectionString;
    repo.createUser(u.username,u.password);
});

app.MapPut("/Tickets/Update",(Tickets t, SqlRepository repo) => {
    repo.connectionString = connectionString;
    return repo.updateStatus(t.username, t.amount, t.stat);
    //return Results.NoContent();
});
app.MapGet("/users/all" ,(SqlRepository repo) => {
    repo.connectionString = connectionString;
    return repo.getAllUsers();
});
app.MapPost("/user/Position", (User u,SqlRepository repo) => {
    repo.connectionString = connectionString;
    return repo.UserPostion(u.username,u.managerStatus);
});
app.MapPut("/user/UpdatePosition", (User u, SqlRepository repo) => {
    repo.connectionString = connectionString;
    repo.UpdateManagerStatus(u.username,u.managerStatus);
    return Results.NoContent();
});
app.MapPost("/user/Type",(Tickets t, SqlRepository repo) => {
    repo.connectionString = connectionString;
    return repo.getReimbursementsByType(t.username, t.type);
});
app.MapPost("/Ticket/Image",(Tickets t, SqlRepository repo) => {
    repo.connectionString = connectionString;
    repo.UploadReciptImage(t.username, t.amount, t.file);
});
app.MapPost("/user/UpdateName",(User u, SqlRepository repo) => {
    repo.connectionString = connectionString;
    repo.UpdateName(u.username,u.name);
});
app.MapPost("/user/UpdateAddress", (User u, SqlRepository repo) => {
    repo.connectionString = connectionString;
    repo.UpdateAddress(u.username, u.address);
});
app.MapPost("/user/Image", (Tickets t, SqlRepository repo) => {
    repo.connectionString = connectionString;
    repo.UpdateProfile(t.username, t.file);
});
app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}