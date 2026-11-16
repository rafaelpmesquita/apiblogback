using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using api.Business;
using api.Models;

namespace api.Controllers
{
    // The public portfolio API is read-only. Administrative writes must only
    // be reintroduced together with a working authentication/authorization flow.
    [Route("v1/tl")]
    public class TimeLineController : Controller
    {
        private readonly TimeLineService _service;

        public TimeLineController(TimeLineService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<TimeLine> Get()
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
