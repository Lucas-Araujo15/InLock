using senai.inlock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{
    interface ITiposUsuariosRepository
    {
        /// <summary>
        /// Cadastra um novo tipo de usuário
        /// </summary>
        /// <param name="TipoUsuario">Objeto TiposUsuariosDomain que será cadastrado</param>
        void Cadastrar(TiposUsuariosDomain TipoUsuario);

        /// <summary>
        /// Deleta um tipo de usuário em específico
        /// </summary>
        /// <param name="id">ID do tipo de usuário que será deletado</param>
        void Deletar(int id);

        /// <summary>
        /// Atualiza as informações de um tipo de usuário específico
        /// </summary>
        /// <param name="novoTipoUsuario"></param>
        void Atualizar(TiposUsuariosDomain novoTipoUsuario);

        /// <summary>
        /// Lista todos os tipos de usuários cadastrados
        /// </summary>
        /// <returns>Lista de tipos de usuários</returns>
        List<TiposUsuariosDomain> ListarTodos();

        /// <summary>
        /// Lista um tipo de usuário em específico
        /// </summary>
        /// <param name="id">ID do tipo de usuário a ser listado</param>
        /// <returns>Objeto TiposUsuariosDomain que foi encontrado</returns>
        TiposUsuariosDomain ListarPorId(int id);
    }
}
