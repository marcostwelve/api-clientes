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

        [HttpGet("{id}", Name = "ObterCliente")]
        public async Task<ActionResult<ClienteDTO>> ListarClientePorId(int id) 
        {
            try
            {
                var cliente = await _uof.ClienteRepository.GetbyId(c => c.Id == id);
                if(cliente is null)
                {
                    return BadRequest("Cliente não encontrado");
                }

                var clienteDTO = _mapper.Map<ClienteDTO>(cliente);

                return Ok(clienteDTO);
            }

            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao tratar a sua solicitação {ex.Message}");
            }
        }

        [HttpGet("logradouros")]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClientesLogradouros()
        {
            try
            {
                var clientesLogradouros = await _uof.ClienteRepository.GetClientesLogradouros();
                if(clientesLogradouros is null)
                {
                    return NotFound();
                }
                var clientesLogradourosDTO = _mapper.Map<List<ClienteDTO>>(clientesLogradouros);
                return Ok(clientesLogradourosDTO);
            }
            catch(Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao tratar a sua solicitação {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarCliente(ClienteDTO clienteDto)
        {
            try
            {
                if(clienteDto is null)
                {
                    return BadRequest();
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
