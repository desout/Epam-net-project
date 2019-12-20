using System;
using Quartz;

namespace EpamNetProject.PLL.Jobs
{
    public class BasketScheduler
    {
        private readonly IScheduler _scheduler;

        private readonly int _reserveTime;
        public BasketScheduler(IScheduler scheduler, int reserveTime)
        {
            _scheduler = scheduler;
            _reserveTime = reserveTime;
        }

        public void Start(string userId)
        {
            _scheduler.Start();

            var job = JobBuilder.Create<BasketJob>()
                .WithIdentity($"job-{userId}", "admin")
                .WithDescription(userId)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"trigger-{userId}", "admin")
                .StartAt(DateTimeOffset.Now.AddMinutes(_reserveTime))
                .Build();

            _scheduler.ScheduleJob(job, trigger);
        }
    }
}
