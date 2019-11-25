using System;
using System.Diagnostics;
using System.Threading.Tasks;
using EpamNetProject.BLL.Interfaces;
using EpamNetProject.PLL.Managers;
using Microsoft.Owin.Logging;
using Quartz;

namespace EpamNetProject.PLL.Jobs
{
    public class BasketJob: IJob
    {
        private readonly IEventService _eventService;
        public BasketJob(IEventService eventService)
        {
            _eventService = eventService;
        }
        
        async Task IJob.Execute(IJobExecutionContext context)
        {
            _eventService.CheckReservation();
        }
    }
}
