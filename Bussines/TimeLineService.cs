using System;
using System.Collections.Generic;
using System.Linq;
using api.Data;
using api.Models;

namespace api.Business
{
    public class TimeLineService
    {
        private ApplicationDbContext _context;

        public TimeLineService(ApplicationDbContext context)
        {
            _context = context;
        }

        public TimeLine Obter(Guid Id)
        {
            Guid? id = Id;
            if (id!=null)
            {
                return _context.TimeLine.Where(
                    p => p.Id == id).FirstOrDefault();
            }
            else
                return null;
        }

        public IEnumerable<TimeLine> ListarTodos()
        {
            return _context.TimeLine.ToList();
        }

        public Resultado Incluir(TimeLine dadosTimeLine)
        {
            Resultado resultado = DadosValidos(dadosTimeLine);
            resultado.Acao = "Inclusão de TimeLine";

            if (resultado.Inconsistencias.Count == 0 &&
                _context.TimeLine.Where(
                p => p.Id == dadosTimeLine.Id).Count() > 0)
            {
                resultado.Inconsistencias.Add(
                    "Id já cadastrado");
            }

            if (resultado.Inconsistencias.Count == 0)
            {
                _context.TimeLine.Add(dadosTimeLine);
                _context.SaveChanges();
            }

            return resultado;
        }

        public Resultado Atualizar(TimeLine dadosTimeLine)
        {
            Resultado resultado = DadosValidos(dadosTimeLine);
            resultado.Acao = "Atualização de TimeLine";

            if (resultado.Inconsistencias.Count == 0)
            {
                TimeLine timeline = _context.TimeLine.Where(
                    p => p.Id == dadosTimeLine.Id).FirstOrDefault();

                if (timeline == null)
                {
                    resultado.Inconsistencias.Add(
                        "TimeLine não encontrado");
                             
                }
                else
                {
                    timeline.Desc = dadosTimeLine.Desc;
                    timeline.lado = dadosTimeLine.lado;
                    _context.SaveChanges();
                }
            }

            return resultado;
        }

        public Resultado Excluir(Guid id)
        {
            Resultado resultado = new Resultado();
            resultado.Acao = "Exclusão de TimeLine";

            TimeLine timeline = Obter(id);
            if (timeline == null)
            {
                resultado.Inconsistencias.Add(
                    "TimeLine não encontrado");
            }
            else
            {
                _context.TimeLine.Remove(timeline);
                _context.SaveChanges();
            }
                
            return resultado;
        }

        private Resultado DadosValidos(TimeLine timeline)
        {
            var resultado = new Resultado();
            if (timeline == null)
            {
                resultado.Inconsistencias.Add(
                    "Preencha os Dados do TimeLine");
            }
            else
            {
                
                if (String.IsNullOrWhiteSpace(timeline.lado))
                {
                    resultado.Inconsistencias.Add(
                        "Preencha a Lado do TimeLine");
                }
                if (String.IsNullOrWhiteSpace(timeline.Desc))
                {
                    resultado.Inconsistencias.Add(
                        "Preencha a descricao do TimeLine");
                }
               
            }

            return resultado;
        }
    }
}