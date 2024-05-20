using ControlBS.WebApi.Utils.Auth;
using ControlBS.WebApi.Utils.Helpers;
using Quartz;
using QuartzJob;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

//dependecy injection
builder.Services.AddScoped<IJwtUtils, JwtUtils>();


//Quartz => To implements Schedulers Jobs. 
builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("sendNotificationJob");
    q.AddJob<SendNotificationJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts.ForJob(jobKey).WithIdentity("sendNotificationJob-trigger").WithCronSchedule("0 0 9,13,14,18 ? * MON-FRI *"));
    //q.AddTrigger(opts => opts.ForJob(jobKey).WithIdentity("sendNotificationJob-trigger").WithCronSchedule("0 17 17 ? * MON-FRI"));
});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);


var app = builder.Build();

//Logger
Log.Logger = new LoggerConfiguration().MinimumLevel.Override("Quartz", LogEventLevel.Warning).
        WriteTo.File("Logs/log.log", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, shared: true).
        CreateLogger();

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // custom jwt auth middleware
    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();
}

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
