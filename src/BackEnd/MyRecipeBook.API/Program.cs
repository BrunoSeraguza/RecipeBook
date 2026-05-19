using MyRecipeBook.API.Filters;
using MyRecipeBook.API.Middlewares;
using MyRecipeBook.Application;
using MyRecipeBook.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddMvc(options => options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection")!);

var app = builder.Build();

// Configure the HTTP request pipeline.u
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Ao declarar o middleware ele sera executado sempre quando alguma requisicao bater na aplicacao, se há mais de um middleware seguira a ordem de execução
app.UseMiddleware<CultureMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
