using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using api.Business;
using api.Models;
using System;
namespace api.Controllers
{
    
    [Route("v1/tl")]
    public class TimeLineController : Controller
    {
        private TimeLineService _service;

        public TimeLineController (TimeLineService service)
        {
            _service = service;
        }
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<TimeLine> Get()
        {
            return _service.ListarTodos();
        }
    
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var espec = _service.Obter(id);
            if (espec != null)
                return new ObjectResult(espec);
            else
                return NotFound();
        }

        [HttpPost]
        public Resultado Post([FromBody] TimeLine espec)
        {
            return _service.Incluir(espec);
        }

        [HttpPut]
        public Resultado Put([FromBody] TimeLine espec)
        {
            return _service.Atualizar(espec);
        }

        [HttpDelete("{id}")]
        public Resultado Delete(Guid id)
        {
            return _service.Excluir(id);
        }
    }
}