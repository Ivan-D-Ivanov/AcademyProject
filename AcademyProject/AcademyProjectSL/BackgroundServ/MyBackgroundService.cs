using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AcademyProjectSL.BackgroundServ
{
    public class MyBackgroundService : IHostedService
    {
        private readonly ILogger<MyBackgroundService> _logger;
        private Timer? _timer;

        public MyBackgroundService(ILogger<MyBackgroundService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, 0, 2000);
            _logger.LogInformation($"Hello from {nameof(MyBackgroundService)} {DateTime.UtcNow}");
            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            _logger.LogInformation($"Hello from {nameof(DoWork)} : {DateTime.UtcNow}");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
