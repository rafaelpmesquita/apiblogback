using System;
using System.Collections.Generic;
using System.Linq;
using api.Data;
using api.Models;

namespace api.Business
{
    public class UsuarioService
    {
        private ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Usuario Obter(Guid Id)
        {
            Guid? id = Id;
            if (id!=null)
            {
                return _context.Usuario.Where(
                    p => p.Id == id).FirstOrDefault();
            }
            else
                return null;
        }

        public IEnumerable<Usuario> ListarTodos()
        {
            return _context.Usuario.ToList();
        }

        public Resultado Incluir(Usuario dadosUsuario)
        {
            Resultado resultado = DadosValidos(dadosUsuario);
            resultado.Acao = "Inclusão de Usuario";

            if (resultado.Inconsistencias.Count == 0 &&
                _context.Usuario.Where(
                p => p.Id == dadosUsuario.Id).Count() > 0)
            {
                resultado.Inconsistencias.Add(
                    "Id já cadastrado");
            }

            if (resultado.Inconsistencias.Count == 0)
            {
                _context.Usuario.Add(dadosUsuario);
                _context.SaveChanges();
            }

            return resultado;
        }

        public Resultado Atualizar(Usuario dadosUsuario)
        {
            Resultado resultado = DadosValidos(dadosUsuario);
            resultado.Acao = "Atualização de Usuario";

            if (resultado.Inconsistencias.Count == 0)
            {
                Usuario Usuario = _context.Usuario.Where(
                    p => p.Id == dadosUsuario.Id).FirstOrDefault();

                if (Usuario == null)
                {
                    resultado.Inconsistencias.Add(
                        "Usuario não encontrado");
                             
                }
                else
                {
                    Usuario.Email = dadosUsuario.Email;
                    Usuario.senha = dadosUsuario.senha;
                    _context.SaveChanges();
                }
            }

            return resultado;
        }

        public Resultado Excluir(Guid id)
        {
            Resultado resultado = new Resultado();
            resultado.Acao = "Exclusão de Usuario";

            Usuario Usuario = Obter(id);
            if (Usuario == null)
            {
                resultado.Inconsistencias.Add(
                    "Usuario não encontrado");
            }
            else
            {
                _context.Usuario.Remove(Usuario);
                _context.SaveChanges();
            }
                
            return resultado;
        }

        private Resultado DadosValidos(Usuario Usuario)
        {
            var resultado = new Resultado();
            if (Usuario == null)
            {
                resultado.Inconsistencias.Add(
                    "Preencha os Dados do Usuario");
            }
            else
            {
                
                if (String.IsNullOrWhiteSpace(Usuario.Email))
                {
                    resultado.Inconsistencias.Add(
                        "Preencha a Lado do Usuario");
                }
                if (String.IsNullOrWhiteSpace(Usuario.senha))
                {
                    resultado.Inconsistencias.Add(
                        "Preencha a descricao do Usuario");
                }
               
            }

            return resultado;
        }
    }
}