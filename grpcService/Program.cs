using grpcService.Models;
using Microsoft.EntityFrameworkCore;
using grpcService.Services.OrderServices;
using grpcService.Services.OrderServiceGrpc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Get ConnectionString
var conn = builder.Configuration.GetConnectionString("DbConnection");

// Register AppDbContext
builder.Services.AddDbContext<AppDbContext>(db => db.UseSqlServer(conn));

// Register ORder Service
builder.Services.AddScoped<IOrderService, OrderService>();

// Register Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddGrpc().AddJsonTranscoding();

builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "gRPC Transcoding",
        Version = "v1"
    });

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "grpcService.xml");
    options.IncludeXmlComments(filePath);
    options.IncludeGrpcXmlComments(filePath, includeControllerXmlComments: true);
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
app.MapGrpcService<OrderGrpcService>();

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
