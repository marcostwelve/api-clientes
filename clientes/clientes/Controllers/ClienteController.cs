using AutoMapper;
using clientes.DTO_s;
using clientes.Models;
using clientes.Paginacao;
using clientes.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace clientes.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ClienteController : ControllerBase
    {

        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public ClienteController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todos os clientes
        /// </summary>
        /// <param name="clienteParametros"></param>
        /// <returns>Lista de Clientes</returns>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> ListarTodosClientes([FromQuery] ClienteParametros clienteParametros)
        {
            try
            {
                var clientes = await _uof.ClienteRepository.GetClientes(clienteParametros);

                var metadata = new
                {
                    clientes.ContagemTotal,
                    clientes.TamanhoPagina,
                    clientes.PaginaAtual,
                    clientes.TotalPagina,
                    clientes.ProximaPagina,
                    clientes.PaginaAnterior,
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
                var clientesDTO = _mapper.Map<List<ClienteDTO>>(clientes);
                return Ok(clientesDTO);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao tratar a sua solicita~ção {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna um cliente através do ID declarado
        /// </summary>
        /// <param name="id">id do Cliente</param>
        /// <returns>Cliente</returns>

        [HttpGet("{id}", Name = "ObterCliente")]
        public async Task<ActionResult<ClienteDTO>> ListarClientePorId(int id) 
        {
            try
            {
                var cliente = await _uof.ClienteRepository.GetbyId(c => c.Id == id);
                if(cliente is null)
                {
                    return NotFound("Cliente não encontrado");
                }

                var clienteDTO = _mapper.Map<ClienteDTO>(cliente);

                return Ok(clienteDTO);
            }

            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao tratar a sua solicitação {ex.Message}");
            }
        }

        /// <summary>
        /// Retorna todos os clientes com os logradouros cadastrados
        /// </summary>
        /// <returns>Clientes e Logradouros</returns>

        [HttpGet("logradouros")]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClientesLogradouros()
        {
            try
            {
                var clientesLogradouros = await _uof.ClienteRepository.GetClientesLogradouros();
                if (clientesLogradouros is null)
                {
                    return NotFound();
                }
                var clientesLogradourosDTO = _mapper.Map<List<ClienteDTO>>(clientesLogradouros);
                return Ok(clientesLogradourosDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao tratar a sua solicitação {ex.Message}");
            }
        }

        /// <summary>
        /// Cadastra um cliente
        /// </summary>
        /// <param name="clienteDto">Dados do Cliente</param>
        /// <returns>Cliente cadastrado</returns>

        [HttpPost]
        public async Task<ActionResult> CadastrarCliente(ClienteDTO clienteDto)
        {
            try
            {
                if(clienteDto is null)
                {
                    return BadRequest("Necessário preencher todos os campos");
                }

                var cliente = _mapper.Map<Cliente>(clienteDto);

                if(await _uof.ClienteRepository.GetClientesEmail(cliente.Email))
                {
                    return BadRequest("Email já está em uso");
                }

                _uof.ClienteRepository.Add(cliente);
                await _uof.Commit();

                var clienteDTO = _mapper.Map<ClienteDTO>(cliente);

                return new CreatedAtRouteResult("ObterCliente", new  { id = cliente.Id }, clienteDTO);
            }

            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao tratar a sua solicitação {ex.Message}");
            }
        }

        /// <summary>
        /// Atualiza um cliente cadastrado, através do seu ID
        /// </summary>
        /// <param name="id">Id Cliente</param>
        /// <param name="clienteDto">Dados do cliente a serem atualizadod</param>
        /// <returns>Cliente com dados atualizados</returns>

        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarCliente(int id, ClienteDTO clienteDto)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteDto);
                if(id != cliente.Id)
                {
                    return BadRequest("Id diferente do cadastrado");
                }

                _uof.ClienteRepository.Update(cliente);
                await _uof.Commit();

                var clienteDTO = _mapper.Map<ClienteDTO>(cliente);
                return Ok(clienteDTO);
            }
            catch(Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao tratar a sua solicitação {ex.Message}");
            }
        }

        /// <summary>
        /// Deleta um cliente da base de dados, através do seu ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        public async Task<ActionResult<ClienteDTO>> DeletarCliente(int id)
        {
            try
            {
                var cliente = await _uof.ClienteRepository.GetbyId(c => c.Id == id);
                if(cliente is null)
                {
                    return NotFound("Cliente não encontrado");
                }

                _uof.ClienteRepository.Delete(cliente);
                await _uof.Commit();
                return NoContent();
            }

            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao tratar a sua solicitação {ex.Message}");
            }
        }
    }
}
