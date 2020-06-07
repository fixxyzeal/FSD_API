using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceLB
{
    public class BrushingInformationCronJob : CronJobService
    {
        private readonly ILogger<BrushingInformationCronJob> _logger;
        private readonly IBrushingInformationService _brushingInformationService;

        public BrushingInformationCronJob(
            IScheduleConfig<BrushingInformationCronJob> config,
            ILogger<BrushingInformationCronJob> logger,
            IBrushingInformationService brushingInformationService)
            : base(config.CronExpression, config.TimeZoneInfo)
        {
            _logger = logger;
            _brushingInformationService = brushingInformationService;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brushing Information Cronjob Starts");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} Brushing Information is working.");
            await _brushingInformationService.DoWork().ConfigureAwait(false);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Brushing Information is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}