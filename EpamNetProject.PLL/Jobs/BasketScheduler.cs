using Quartz;

namespace EpamNetProject.PLL.Jobs
{
    public class BasketScheduler
    {
        private readonly IScheduler _scheduler;

        public BasketScheduler(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public void Start()
        {
            _scheduler.Start();

            var job = JobBuilder.Create<BasketJob>()
                .WithIdentity("job", "admin")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("trigger", "admin")
                .WithSimpleSchedule(x => x
                    .RepeatForever()
                    .WithIntervalInSeconds(30)
                )
                .StartNow()
                .Build();

            _scheduler.ScheduleJob(job, trigger);
        }
    }
}