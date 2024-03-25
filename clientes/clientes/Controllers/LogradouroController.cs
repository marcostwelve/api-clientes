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
    public class LogradouroController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public LogradouroController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogradouroDTO>>> PegarTodosOsLogradouros([FromQuery] LogradouroParametros logradouroParametros)
        {
            try
            {
                var logradouros = await _uof.LogradouroRepository.GetLogradouros(logradouroParametros);

                var metadata = new
                {
                    logradouros.ContagemTotal,
                    logradouros.TamanhoPagina,
                    logradouros.PaginaAtual,
                    logradouros.TotalPagina,
                    logradouros.ProximaPagina,
                    logradouros.PaginaAnterior,
                };

                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                var logradourosDTO = _mapper.Map<List<LogradouroDTO>>(logradouros);
                return Ok(logradourosDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao tratar a sua solicitação {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LogradouroDTO>> PegarLogradouroPorId(int id) 
        {
            try
            {
                var logradouro = await _uof.LogradouroRepository.GetbyId(l => l.Id == id);
                if (logradouro is null)
                {
                    return NotFound("Logradouro não encontrado!");
                }
                var logradouroDTO = _mapper.Map<LogradouroDTO>(logradouro);
                return Ok(logradouroDTO);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao tratar a sua solicitação {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<LogradouroDTO>> CadastrarLogradouro(LogradouroDTO logradouroDto)
        {
            try
            {
                var logradouro = _mapper.Map<Logradouro>(logradouroDto);
                _uof.LogradouroRepository.Add(logradouro);
                await _uof.Commit();

                var logradouroDTO = _mapper.Map<LogradouroDTO>(logradouro);
                return Ok(logradouroDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao tratar a sua solicitação {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarLogradouro(int id, LogradouroDTO logradouroDto)
        {
            try
            {
                if (id != logradouroDto.Id)
                {
                    return BadRequest("Id's não conferem");
                }

                var logradouro = _mapper.Map<Logradouro>(logradouroDto);
                _uof.LogradouroRepository.Update(logradouro);
                await _uof.Commit();

                var logradouroDTO = _mapper.Map<LogradouroDTO>(logradouro);
                return Ok(logradouroDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Ocorreu um erro ao tratar a sua solicitação {ex.Message}");
            }

        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<LogradouroDTO>> DeletarLogradouro(int id)
        {
            try
            {
                var logradouro = await _uof.LogradouroRepository.GetbyId(l => l.Id == id);
                if(logradouro is null)
                {
                    return NotFound("Logradouro não encontrado");
                }

                _uof.LogradouroRepository.Delete(logradouro);
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
