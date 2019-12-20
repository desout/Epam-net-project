using System.Diagnostics;
using System.Threading.Tasks;
using EpamNetProject.BLL.Interfaces;
using Quartz;

namespace EpamNetProject.PLL.Jobs
{
    public class BasketJob : IJob
    {
        private readonly IEventService _eventService;

        public BasketJob(IEventService eventService)
        {
            _eventService = eventService;
        }

        async Task IJob.Execute(IJobExecutionContext context)
        {
            Debug.WriteLine($"Job - {context.JobDetail.Key.Name} now", "Action Filter Log");
            _eventService.CheckReservation(context.JobDetail.Description);
        }
    }
}
