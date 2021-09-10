using senai.inlock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{
    interface IJogosRepository
    {
        /// <summary>
        /// Cadastra um novo jogo
        /// </summary>
        /// <param name="jogo"></param>
        void Cadastrar(JogosDomain jogo);

        /// <summary>
        /// Deleta um jogo específico
        /// </summary>
        /// <param name="id">ID do jogo a ser deletado</param>
        void Deletar(int id);

        /// <summary>
        /// Atualiza as informações de um jogo específico
        /// </summary>
        /// <param name="novoJogo">Objeto JogosDomain com as novas informações</param>
        void Atualizar(JogosDomain novoJogo);

        /// <summary>
        /// Lista todos os jogos cadastrados
        /// </summary>
        /// <returns>Uma lista de jogos</returns>
        List<JogosDomain> ListarTodos();

        /// <summary>
        /// Lista um jogo específico
        /// </summary>
        /// <param name="id">ID do jogo a ser procurado</param>
        /// <returns>Objeto JogosDomain que foi encontrado</returns>
        JogosDomain ListarPorId(int id);
    }
}
