using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Repositories
{
    public class UsuariosRepository : IUsuariosRepository
    {
        public void Atualizar(UsuariosDomain novoUsuario)
        {
            throw new NotImplementedException();
        }

        public UsuariosDomain Autenticar(string email, string senha)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(UsuariosDomain Usuario)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int id)
        {
            throw new NotImplementedException();
        }

        public UsuariosDomain ListarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public List<UsuariosDomain> ListarTodos()
        {
            throw new NotImplementedException();
        }
    }
}
