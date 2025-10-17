using System;
using System.Collections.Generic;
using System.Linq;
using api.Data;
using api.Models;

namespace api.Business
{
    public class EspecialidadeService
    {
        private ApplicationDbContext _context;

        public EspecialidadeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Especialidades Obter(Guid Id)
        {
            Guid? id = Id;
            if (id!=null)
            {
                return _context.Especialidades.Where(
                    p => p.Id == id).FirstOrDefault();
            }
            else
                return null;
        }

        public IEnumerable<Especialidades> ListarTodos(string tenant = null)
        {
            var query = _context.Especialidades.AsQueryable();
            if (!string.IsNullOrWhiteSpace(tenant))
            {
                var t = tenant.Trim().ToLower();
                query = query.Where(p => p.Tenant == t);
            }
            return query.OrderBy(p => p.Lang).ToList();
        }

        public Resultado Incluir(Especialidades dadosEspecialidades, string tenant = null)
        {
            Resultado resultado = DadosValidos(dadosEspecialidades);
            resultado.Acao = "Inclusão de Especialidades";

            if (resultado.Inconsistencias.Count == 0 &&
                _context.Especialidades.Where(
                p => p.Id == dadosEspecialidades.Id).Count() > 0)
            {
                resultado.Inconsistencias.Add(
                    "Id já cadastrado");
            }

            if (resultado.Inconsistencias.Count == 0)
            {
                if (!string.IsNullOrWhiteSpace(tenant))
                {
                    dadosEspecialidades.Tenant = tenant.Trim().ToLower();
                }
                _context.Especialidades.Add(dadosEspecialidades);
                _context.SaveChanges();
            }

            return resultado;
        }

        public Resultado Atualizar(Especialidades dadosEspecialidades, string tenant = null)
        {
            Resultado resultado = DadosValidos(dadosEspecialidades);
            resultado.Acao = "Atualização de Especialidades";

            if (resultado.Inconsistencias.Count == 0)
            {
                Especialidades especialidade = _context.Especialidades.Where(
                    p => p.Id == dadosEspecialidades.Id).FirstOrDefault();

                if (especialidade == null)
                {
                    resultado.Inconsistencias.Add(
                        "Especialidades não encontrado");
                             
                }
                else
                {
                    especialidade.Lang = dadosEspecialidades.Lang;
                    especialidade.Img = dadosEspecialidades.Img;
                    especialidade.Desc = dadosEspecialidades.Desc;
                    if (!string.IsNullOrWhiteSpace(tenant))
                    {
                        especialidade.Tenant = tenant.Trim().ToLower();
                    }
                    _context.SaveChanges();
                }
            }

            return resultado;
        }

        public Resultado Excluir(Guid id)
        {
            Resultado resultado = new Resultado();
            resultado.Acao = "Exclusão de Especialidades";

            Especialidades especialidade = Obter(id);
            if (especialidade == null)
            {
                resultado.Inconsistencias.Add(
                    "Especialidades não encontrado");
            }
            else
            {
                _context.Especialidades.Remove(especialidade);
                _context.SaveChanges();
            }
                
            return resultado;
        }

        private Resultado DadosValidos(Especialidades especialidade)
        {
            var resultado = new Resultado();
            if (especialidade == null)
            {
                resultado.Inconsistencias.Add(
                    "Preencha os Dados do Especialidades");
            }
            else
            {
                
                if (String.IsNullOrWhiteSpace(especialidade.Lang))
                {
                    resultado.Inconsistencias.Add(
                        "Preencha a Linguagem do Especialidades");
                }
                if (String.IsNullOrWhiteSpace(especialidade.Img))
                {
                    resultado.Inconsistencias.Add(
                        "Preencha o url da Imagem do Especialidades");
                }
                if (String.IsNullOrWhiteSpace(especialidade.Desc))
                {
                    resultado.Inconsistencias.Add(
                        "Preencha a descricao do Especialidades");
                }
               
            }

            return resultado;
        }
    }
}