using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SantafeApi.Infraestrucutre.Data;
using SantafeApi.Services;
using SantafeApi.Services.Interfaces;

namespace SantafeApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualificationController : ControllerBase
    {
        private readonly IQualificationService _qualificationService;
        private readonly SantafeApiContext _dbContext;
        public QualificationController(SantafeApiContext dbContext, IQualificationService qualificationService)
        {
            _qualificationService = qualificationService;
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetErick(int codCliente, DateTime start, DateTime end)
        {
            var qualifications = _qualificationService.GetQualificationReport(codCliente, start, end);
            Console.WriteLine(codCliente);
            Console.WriteLine(start);
            Console.WriteLine(end);
            Console.WriteLine(qualifications);
            return Ok(qualifications);
        }
    }
}