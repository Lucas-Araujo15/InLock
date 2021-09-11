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
    public class TiposUsuariosController : ControllerBase
    {
        private ITiposUsuariosRepository _TiposUsuariosRepository { get; set; }

        public TiposUsuariosController()
        {
            _TiposUsuariosRepository = new TiposUsuariosRepository();
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            TiposUsuariosDomain tipoUsuarioEncontrado = _TiposUsuariosRepository.ListarPorId(id);

            if (tipoUsuarioEncontrado == null)
            {
                return NotFound("Nenhum tipo de usuário encontrado!");
            }

            return Ok(tipoUsuarioEncontrado);
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<TiposUsuariosDomain> listaTiposUsuarios = _TiposUsuariosRepository.ListarTodos();
            return Ok(listaTiposUsuarios);
        }

        [HttpPost]
        public IActionResult Post(TiposUsuariosDomain novoTipoUsuario)
        {
            _TiposUsuariosRepository.Cadastrar(novoTipoUsuario);
            return StatusCode(201);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _TiposUsuariosRepository.Deletar(id);
            return StatusCode(204);
        }

        [HttpPut]
        public IActionResult PutBody(TiposUsuariosDomain tipoUsuario)
        {
            if (tipoUsuario.IdTipoUsuario == 0 || tipoUsuario.NomeTipoUsuario == null)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "Algumas informações não foram informadas!"
                    }
                );
            }
            TiposUsuariosDomain tipoUsuarioVerificacao = _TiposUsuariosRepository.ListarPorId(tipoUsuario.IdTipoUsuario);

            if (tipoUsuarioVerificacao != null)
            {
                try
                {
                    _TiposUsuariosRepository.Atualizar(tipoUsuario);
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
                        mensagemErro = "Tipo de usuário não encontrado!",
                        codErro = true
                    }
                );
        }
    }
}
