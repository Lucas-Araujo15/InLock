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
    public class JogosController : ControllerBase
    {
        private IJogosRepository _JogosRepository { get; set; }

        public JogosController()
        {
            _JogosRepository = new JogosRepository();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            JogosDomain jogoEncontrado = _JogosRepository.ListarPorId(id);

            if (jogoEncontrado == null)
            {
                return NotFound("Nenhum jogo encontrado!");
            }

            return Ok(jogoEncontrado);
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<JogosDomain> listaJogos = _JogosRepository.ListarTodos();
            return Ok(listaJogos);
        }

        [HttpPost]
        [Authorize(Roles = "1")] // Administrador
        public IActionResult Post(JogosDomain novoJogo)
        {
            _JogosRepository.Cadastrar(novoJogo);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _JogosRepository.Deletar(id);
            return StatusCode(204);
        }

        [HttpPut]
        public IActionResult PutBody(JogosDomain jogo)
        {
            if (jogo.IdJogo == 0 || jogo.NomeJogo == null || jogo.DescricaoJogo == null || jogo.ValorJogo == 0 || jogo.IdEstudio == 0)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "Algumas informações não foram informadas!"
                    }
                );
            }
            JogosDomain jogoVerificacao = _JogosRepository.ListarPorId(jogo.IdJogo);

            if (jogoVerificacao != null)
            {
                try
                {
                    _JogosRepository.Atualizar(jogo);
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
                        mensagemErro = "Jogo não encontrado!",
                        codErro = true
                    }
                );
        }
    }
}
