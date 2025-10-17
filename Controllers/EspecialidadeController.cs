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
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Especialidades> Get()
        {
            var tenant = Request.Headers["X-Tenant"].ToString();
            return _serviceEspecialidade.ListarTodos(tenant);
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
            var tenant = Request.Headers["X-Tenant"].ToString();
            return _serviceEspecialidade.Incluir(espec, tenant);
        }

        [HttpPut]
        public Resultado Put([FromBody] Especialidades espec)
        {
            var tenant = Request.Headers["X-Tenant"].ToString();
            return _serviceEspecialidade.Atualizar(espec, tenant);
        }

        [HttpDelete("{id}")]
        public Resultado Delete(Guid id)
        {
            return _serviceEspecialidade.Excluir(id);
        }

    }
}