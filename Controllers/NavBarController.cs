using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using api.Business;
using api.Models;

namespace api.Controllers
{
    // The public portfolio API is read-only. Administrative writes must only
    // be reintroduced together with a working authentication/authorization flow.
    [Route("v1/nv")]
    public class NavBarController : Controller
    {
        private readonly NavBarService _service;

        public NavBarController(NavBarService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<NavBar> Get()
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
