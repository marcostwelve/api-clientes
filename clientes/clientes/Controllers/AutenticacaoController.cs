using AutoMapper;
using clientes.Context;
using clientes.DTO_s;
using clientes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace clientes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;

        public AutenticacaoController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        /// <summary>
        /// Realiza o registro de um usuário
        /// </summary>
        /// <param name="usuario">Dados Usuário</param>
        /// <returns></returns>

        [HttpPost("registrar")]

        public async Task<ActionResult> RegistrarUsuario(UsuarioDTO usuario)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            }

            if(usuario.Senha != usuario.ConfirmarSenha)
            {
                return BadRequest("Senhas não conferem");
            }

            var user = new IdentityUser
            {
                UserName = usuario.Email,
                Email = usuario.Email,
                EmailConfirmed = true,
            };

            var resultado = await _userManager.CreateAsync(user, usuario.Senha);


            if(!resultado.Succeeded)
            {
                return BadRequest(resultado.Errors);
            }

            await _signInManager.SignInAsync(user, false);
            return Ok(GerarToken(usuario));
        }

        /// <summary>
        /// Realiza o login para o usuário e gera um token de acesso
        /// </summary>
        /// <param name="dadosUsuario"></param>
        /// <returns>Token de acesso</returns>

        [HttpPost("login")]

        public async Task<ActionResult> Login(UsuarioDTO dadosUsuario)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.SelectMany(e => e.Errors));
            }

            var resultado = await _signInManager.PasswordSignInAsync(dadosUsuario.Email, dadosUsuario.Senha, isPersistent: false, lockoutOnFailure: false);

            if(resultado.Succeeded)
            {
                return Ok(GerarToken(dadosUsuario));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Inválido...");
                return BadRequest(ModelState);
            }
        }

        /// <summary>
        /// Gera token de acesso ao usuário
        /// </summary>
        /// <param name="dadosUsuario">Usuário</param>
        /// <returns>Token de usuário</returns>

        private UsuarioToken GerarToken(UsuarioDTO dadosUsuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, dadosUsuario.Email),
                new Claim("cliente", "cliente"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracao = _config["TokenConfiguration:ExpireHours"];
            var limiteExpirar = DateTime.UtcNow.AddHours(double.Parse(expiracao));

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _config["TokenConfiguration:Issuer"],
                audience: _config["TokenConfiguration:Audience"],
                claims: claims,
                expires: limiteExpirar,
                signingCredentials: credenciais
                );

            return new UsuarioToken()
            {
                Autenticado = true,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                DataExpirar = limiteExpirar,
                Mensagem = "Token JWT OK"
            };
        }
    }
}
