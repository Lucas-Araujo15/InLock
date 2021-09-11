using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EstudiosController : ControllerBase
    {
        private IEstudiosRepository _EstudiosRepository { get; set; }

        public EstudiosController()
        {
            _EstudiosRepository = new EstudiosRepository();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            EstudiosDomain estudioEncontrado = _EstudiosRepository.ListarPorId(id);

            if (estudioEncontrado == null)
            {
                return NotFound("Nenhum estúdio encontrado!");
            }

            return Ok(estudioEncontrado);
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<EstudiosDomain> listaEstudios = _EstudiosRepository.ListarTodos();
            return Ok(listaEstudios);
        }

        [HttpGet("listajogos")]
        public IActionResult GetWithGames()
        {
            List<EstudiosDomain> listaEstudios = _EstudiosRepository.ListarComJogos();
            return Ok(listaEstudios);
        }

        [HttpPost]
        public IActionResult Post(EstudiosDomain novoEstudio)
        {
            _EstudiosRepository.Cadastrar(novoEstudio);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _EstudiosRepository.Deletar(id);
            return StatusCode(204);
        }

        [HttpPut]
        public IActionResult PutBody(EstudiosDomain estudio)
        {
            if (estudio.IdEstudio == 0 || estudio.NomeEstudio == null)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "Algumas informações não foram informadas!"
                    }
                );
            }
            EstudiosDomain estudioVerificacao = _EstudiosRepository.ListarPorId(estudio.IdEstudio);

            if (estudioVerificacao != null)
            {
                try
                {
                    _EstudiosRepository.Atualizar(estudio);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }

            return NotFound(
                    new
                    {
                        mensagemErro = "Estúdio não encontrado!",
                        codErro = true
                    }
                );
        }
    }
}
