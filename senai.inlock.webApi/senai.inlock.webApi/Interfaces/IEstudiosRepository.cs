using senai.inlock.webApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Interfaces
{
    interface IEstudiosRepository
    {
        /// <summary>
        /// Cadastra um novo estúdio
        /// </summary>
        /// <param name="estudio">Objeto EstudiosDomain que será cadastrado</param>
        void Cadastrar(EstudiosDomain estudio);

        /// <summary>
        /// Deleta um estúdio em específico
        /// </summary>
        /// <param name="id">ID do estúdio a ser deletado</param>
        void Deletar(int id);

        /// <summary>
        /// Atualiza as informações de um estúdio já existente
        /// </summary>
        /// <param name="novoEstudio">Objeto EstudiosDomain com as novas informações</param>
        void Atualizar(EstudiosDomain novoEstudio);

        /// <summary>
        /// Lista todos os estúdios cadastrados
        /// </summary>
        /// <returns>Retorna uma lista de estúdios</returns>
        List<EstudiosDomain> ListarTodos();

        /// <summary>
        /// Lista um estúdio em específico
        /// </summary>
        /// <param name="id">ID do estúdio que será listado</param>
        /// <returns>Objeto EstudiosDomain que foi encontrado</returns>
        EstudiosDomain ListarPorId(int id);

        /// <summary>
        /// Lista todos os estúdios e inclui a lista de jogos daquele determinado estúdio
        /// </summary>
        /// <returns>Lista de estúdios</returns>
        List<EstudiosDomain> ListarComJogos();
    }
}
