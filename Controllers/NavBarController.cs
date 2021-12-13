using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using api.Business;
using api.Models;
using System;
namespace api.Controllers
{
    
    [Route("v1/nv")]
    public class NavBarController: Controller
    {
        private NavBarService _service;
     
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
            var espec = _service.Obter(id);
            if (espec != null)
                return new ObjectResult(espec);
            else
                return NotFound();
        }

        [HttpPost]
        public Resultado Post([FromBody] NavBar espec)
        {
            return _service.Incluir(espec);
        }

        [HttpPut]
        public Resultado Put([FromBody] NavBar espec)
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