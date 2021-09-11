using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai.inlock.webApi.Domains;
using senai.inlock.webApi.Interfaces;
using senai.inlock.webApi.Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace senai.inlock.webApi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]
    
    public class UsuariosController : ControllerBase
    {
        private IUsuariosRepository _UsuariosRepository { get; set; }

        public UsuariosController()
        {
            _UsuariosRepository = new UsuariosRepository();
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            UsuariosDomain UsuarioEncontrado = _UsuariosRepository.ListarPorId(id);

            if (UsuarioEncontrado == null)
            {
                return NotFound("Nenhum usuário encontrado!");
            }

            return Ok(UsuarioEncontrado);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            List<UsuariosDomain> listaUsuarios = _UsuariosRepository.ListarTodos();
            return Ok(listaUsuarios);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Post(UsuariosDomain novoUsuario)
        {
            _UsuariosRepository.Cadastrar(novoUsuario);
            return StatusCode(201);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _UsuariosRepository.Deletar(id);
            return StatusCode(204);
        }

        [Authorize]
        [HttpPut]
        public IActionResult PutBody(UsuariosDomain Usuario)
        {
            if (Usuario.IdTipoUsuario == 0 || Usuario.NomeUsuario == null || Usuario.IdUsuario == 0 || Usuario.email == null || Usuario.senha == null)
            {
                return BadRequest(
                    new
                    {
                        mensagemErro = "Algumas informações não foram informadas!"
                    }
                );
            }
            UsuariosDomain UsuarioVerificacao = _UsuariosRepository.ListarPorId(Usuario.IdUsuario);

            if (UsuarioVerificacao != null)
            {
                try
                {
                    _UsuariosRepository.Atualizar(Usuario);
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
                        mensagemErro = "Usuário não encontrado!",
                        codErro = true
                    }
                );
        }

        [HttpPost("login")]
        public IActionResult Logar(UsuariosDomain usuarioLogin)
        {
            UsuariosDomain usuarioEncontrado = _UsuariosRepository.Autenticar(usuarioLogin.email, usuarioLogin.senha);

            if (usuarioEncontrado != null)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioEncontrado.email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioEncontrado.IdUsuario.ToString()),
                    new Claim(JwtRegisteredClaimNames.Name, usuarioEncontrado.NomeUsuario),
                    new Claim(ClaimTypes.Role, usuarioEncontrado.IdTipoUsuario.ToString()),
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("InLock-chave-autenticacao"));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var myToken = new JwtSecurityToken(
                    issuer: "senai.inlock.webAPI",  
                    audience: "senai.inlock.webAPI", 
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: creds
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(myToken)
                });
            }
            return NotFound("usuário não encontrado. Tente novamente.");
        }
    }
}
