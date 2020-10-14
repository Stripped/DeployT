using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeployTracker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DeployTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICounter _counter;
        public HomeController(ILogger<HomeController> logger,ICounter counter)
        {
            _logger = logger;
            _counter = counter;
        }
        public ActionResult Index()
        {
            _logger.LogInformation("Index action called!");
            _logger.LogInformation(_counter.GetValue().ToString());
            _counter.Increment();
            return View();
        }
    }
}
