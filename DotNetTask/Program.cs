using DotNetTask.Services;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Retrieve the Cosmos DB connection string from configuration
var cosmosDbConnectionString = builder.Configuration.GetConnectionString("applicationDB");

// Retrieve other configuration settings
var cosmosDbOptions = builder.Configuration.GetSection("CosmosDb");
var applicationDb = cosmosDbOptions["ApplicationDb"];
var employeesContainer = cosmosDbOptions["Employees"];
var questionContainer = cosmosDbOptions["Questions"];

// Register CosmosClient as a singleton service
builder.Services.AddSingleton<CosmosClient>(sp =>
{
    return new CosmosClient(cosmosDbConnectionString);
});

// Register ICosmosDbService with CosmosDbService implementation
builder.Services.AddSingleton<ICosmosDbServices>(sp =>
{
    var cosmosClient = sp.GetRequiredService<CosmosClient>();
    var employeesContainer = cosmosClient.GetContainer("ApplicationDb", "Employees");
    var questionContainer = cosmosClient.GetContainer("ApplicationDb", "Questions");
    return new CosmosDbService(employeesContainer, questionContainer);
});

builder.Services.AddSingleton<IProgramService, ProgramService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
