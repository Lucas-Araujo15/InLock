using senai.inlock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{
    interface IUsuariosRepository
    {
        /// <summary>
        /// Cadastra um novo usuário 
        /// </summary>
        /// <param name="Usuario">Objeto UsuariosDomain a ser cadastrado</param>
        void Cadastrar(UsuariosDomain Usuario);

        /// <summary>
        /// Deleta um usuário em específico
        /// </summary>
        /// <param name="id">ID do usuário a ser deletado</param>
        void Deletar(int id);

        /// <summary>
        /// Atualiza as informações de um usuário específico
        /// </summary>
        /// <param name="novoUsuario">Objeto UsuariosDomain com as novas informações</param>
        void Atualizar(UsuariosDomain novoUsuario);

        /// <summary>
        /// Lista todos os usuários cadastrados
        /// </summary>
        /// <returns>Retorna uma lista de usuários</returns>
        List<UsuariosDomain> ListarTodos();

        /// <summary>
        /// Lista um usuário específico
        /// </summary>
        /// <param name="id">ID do usuário a ser listado</param>
        /// <returns>Objeto UsuariosDomain que foi procurado</returns>
        UsuariosDomain ListarPorId(int id);

        /// <summary>
        /// Realiza a validação do usuário
        /// </summary>
        /// <param name="email">Endereço de email do usuário</param>
        /// <param name="senha">Senha do usuário</param>
        /// <returns>Usuário correspondente ao login</returns>
        UsuariosDomain Autenticar(string email, string senha);
    }
}
