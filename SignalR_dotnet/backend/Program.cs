var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:5162") // Change this to match your frontend URL
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()); // Keep this for WebSockets
});

builder.Services.AddSignalR();

var app = builder.Build();
app.UseCors("AllowSpecificOrigin");
app.MapHub<ChatHub>("/chathub");
app.Run();