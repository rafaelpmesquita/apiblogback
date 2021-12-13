using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using api.Business;
using api.Models;
using System;
namespace api.Controllers
{
    
    [Route("v1/")]
    public class EspecialidadeController: Controller
    {
        private EspecialidadeService _serviceEspecialidade;
     
        public EspecialidadeController(EspecialidadeService service)
        {
            _serviceEspecialidade = service;
        }
        [HttpGet]
        public IEnumerable<Especialidades> Get()
        {
            return _serviceEspecialidade.ListarTodos();
        }
    
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var espec = _serviceEspecialidade.Obter(id);
            if (espec != null)
                return new ObjectResult(espec);
            else
                return NotFound();
        }

        [HttpPost]
        public Resultado Post([FromBody] Especialidades espec)
        {
            return _serviceEspecialidade.Incluir(espec);
        }

        [HttpPut]
        public Resultado Put([FromBody] Especialidades espec)
        {
            return _serviceEspecialidade.Atualizar(espec);
        }

        [HttpDelete("{id}")]
        public Resultado Delete(Guid id)
        {
            return _serviceEspecialidade.Excluir(id);
        }

    }
}