using Microsoft.AspNetCore.Mvc;
using AutoglassAPI.Application.DTOs;
using AutoglassAPI.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutoglassAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarProdutos([FromQuery] FiltroProdutosDTO filtro)
        { 
            var produtoResult = await _produtoService.GetProdutosAsync(filtro, true);
            return Ok(produtoResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var produto = await _produtoService.GetByIdAsync(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoDTO>> Post([FromBody] ProdutoCreateDTO produtoCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdProduto = await _produtoService.AddAsync(produtoCreateDTO);
                return CreatedAtAction(nameof(GetById), new { id = createdProduto.Codigo }, createdProduto);
            }

            catch (System.Exception ex)
            {
                
                return BadRequest(ex.Message);
            }



        }

        


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateById(int id, [FromBody] ProdutoUpdateDTO produtoUpdateDTO)
        {   
            try
            {
                await _produtoService.UpdateByIdAsync(id, produtoUpdateDTO);
                return Ok();
            }
            catch(ArgumentException aex )
            {
                return BadRequest(aex.Message);
            }
            catch(FluentValidation.ValidationException vex )
            {
                    return BadRequest(vex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }           
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _produtoService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
            
        }
    }
}
