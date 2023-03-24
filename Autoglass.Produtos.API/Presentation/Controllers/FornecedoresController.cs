using Microsoft.AspNetCore.Mvc;
using AutoglassAPI.Application.DTOs;
using AutoglassAPI.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;

namespace AutoglassAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedoresController : ControllerBase
    {
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorService fornecedorService, IMapper mapper)
        {
            _fornecedorService = fornecedorService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorDTO>>> ListarFornecedores([FromQuery] FiltroFornecedorDTO filtro)
        { 
            var fornecedores = await _fornecedorService.GetFornecedorAsync(filtro);
            var resposta = new
            {
                Total = fornecedores.Count(),
                Pagina = filtro.Pagina,
                TamanhoPagina = filtro.TamanhoPagina,
                Fornecedores = fornecedores
            };

            return Ok(resposta);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var fornecedor = await _fornecedorService.GetByIdAsync(id);
            if (fornecedor == null) return NotFound();
            return Ok(fornecedor);
        }

        [HttpPost]
        public async Task<ActionResult<FornecedorDTO>> Post([FromBody] FornecedorCreateDTO fornecedorCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdFornecedor = await _fornecedorService.AddAsync(fornecedorCreateDTO);
            return CreatedAtAction(nameof(GetById), new { id = createdFornecedor.Codigo }, createdFornecedor);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(int id, FornecedorUpdateDTO fornecedorUpdateDto)
        {   
            try
            {
                await _fornecedorService.UpdateByIdAsync(id,fornecedorUpdateDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
