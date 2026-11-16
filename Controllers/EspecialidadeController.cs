using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using api.Business;
using api.Models;

namespace api.Controllers
{
    // The public portfolio API is read-only. Administrative writes must only
    // be reintroduced together with a working authentication/authorization flow.
    [Route("v1/")]
    public class EspecialidadeController : Controller
    {
        private readonly EspecialidadeService _service;

        public EspecialidadeController(EspecialidadeService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<Especialidades> Get()
        {
            return _service.ListarTodos();
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var item = _service.Obter(id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }
    }
}
