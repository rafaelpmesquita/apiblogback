using System;
using System.Collections.Generic;
using System.Linq;
using api.Data;
using api.Models;

namespace api.Business
{
    public class NavBarService
    {
        private ApplicationDbContext _context;

        public NavBarService(ApplicationDbContext context)
        {
            _context = context;
        }

        public NavBar Obter(Guid Id)
        {
            Guid? id = Id;
            if (id!=null)
            {
                return _context.NavBar.Where(
                    p => p.Id == id).FirstOrDefault();
            }
            else
                return null;
        }

        public IEnumerable<NavBar> ListarTodos()
        {
            return _context.NavBar
                .OrderBy(p => p.Id).ToList();
        }

        public Resultado Incluir(NavBar dadosNavBar)
        {
            Resultado resultado = DadosValidos(dadosNavBar);
            resultado.Acao = "Inclusão de NavBar";

           

            if (resultado.Inconsistencias.Count == 0)
            {
                _context.NavBar.Add(dadosNavBar);
                _context.SaveChanges();
            }

            return resultado;
        }

        public Resultado Atualizar(NavBar dadosNavBar)
        {
            Resultado resultado = DadosValidos(dadosNavBar);
            resultado.Acao = "Atualização de NavBar";

            if (resultado.Inconsistencias.Count == 0)
            {
                NavBar NavBar = _context.NavBar.Where(
                    p => p.Id == dadosNavBar.Id).FirstOrDefault();

                if (NavBar == null)
                {
                    resultado.Inconsistencias.Add(
                        "NavBar não encontrado");
                             
                }
                else
                {
                    NavBar.Texto = dadosNavBar.Texto;
                    NavBar.Rota = dadosNavBar.Rota;
                    _context.SaveChanges();
                }
            }

            return resultado;
        }

        public Resultado Excluir(Guid id)
        {
            Resultado resultado = new Resultado();
            resultado.Acao = "Exclusão de NavBar";

            NavBar NavBar = Obter(id);
            if (NavBar == null)
            {
                resultado.Inconsistencias.Add(
                    "NavBar não encontrado");
            }
            else
            {
                _context.NavBar.Remove(NavBar);
                _context.SaveChanges();
            }
                
            return resultado;
        }

        private Resultado DadosValidos(NavBar NavBar)
        {
            var resultado = new Resultado();
            if (NavBar == null)
            {
                resultado.Inconsistencias.Add(
                    "Preencha os Dados do NavBar");
            }
            else
            {
                
                if (String.IsNullOrWhiteSpace(NavBar.Rota))
                {
                    resultado.Inconsistencias.Add(
                        "Preencha a Rota do NavBar");
                }
                if (String.IsNullOrWhiteSpace(NavBar.Texto))
                {
                    resultado.Inconsistencias.Add(
                        "Preencha o Texto do NavBar");
                }
               
            }

            return resultado;
        }
    }
}