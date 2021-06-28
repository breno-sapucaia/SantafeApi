using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SantafeApi.Infraestrucutre.Data;

namespace SantafeApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualificationController : ControllerBase
    {
        private readonly SantafeApiContext _dbContext;
        public QualificationController(SantafeApiContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetErick()
        {
            var list = _dbContext.Vistoria.Where(e => e.NomeCliente == "Erick Bessa De Souza").ToList();
            return Ok(list);
        }
    }
}