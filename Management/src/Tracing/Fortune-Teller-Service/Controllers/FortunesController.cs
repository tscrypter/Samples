﻿using FortuneTellerService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;

namespace FortuneTellerService.Controllers
{
    [Route("api/[controller]")]
    public class FortunesController : Controller
    {
        private Random SleepInterval { get; } = new Random();
        private IFortuneRepository _fortunes;
        private ILogger<FortunesController> _logger;
        public FortunesController(IFortuneRepository fortunes, ILogger<FortunesController> logger)
        {
            _fortunes = fortunes;
            _logger = logger;
        }

        // GET: api/fortunes
        [HttpGet]
        public IEnumerable<Fortune> Get()
        {
            _logger?.LogInformation("GET api/fortunes");
            return _fortunes.GetAll();
        }

        // GET api/fortunes/random
        [HttpGet("random")]
        public Fortune Random()
        {
            _logger?.LogInformation("GET api/fortunes/random");
            Thread.Sleep(SleepInterval.Next(0, 10000));
            return _fortunes.RandomFortune();
        }
    }
}
