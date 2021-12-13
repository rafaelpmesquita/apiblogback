using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using api.Business;
using api.Models;
using System;
namespace api.Controllers
{
    
    [Route("v1/user")]
    public class UsuarioController: Controller
    {
        private UsuarioService _service;
     
        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }
        [HttpGet]
        public IEnumerable<Usuario> Get()
        {
            return _service.ListarTodos();
        }
    
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var usuario = _service.Obter(id);
            if (usuario != null)
                return new ObjectResult(usuario);
            else
                return NotFound();
        }

        [HttpPost]
        public Resultado Post([FromBody] Usuario usuario)
        {
            return _service.Incluir(usuario);
        }

        [HttpPut]
        public Resultado Put([FromBody] Usuario usuario)
        {
            return _service.Atualizar(usuario);
        }

        [HttpDelete("{id}")]
        public Resultado Delete(Guid id)
        {
            return _service.Excluir(id);
        }

    }
}