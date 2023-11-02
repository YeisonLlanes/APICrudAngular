using ApiCrudAngular.Models;
using ApiCrudAngular.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ApiCrudAngular.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamento _dpto;

        public DepartamentoController(IDepartamento departamento)
        {
            _dpto = departamento;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<Departamento>> ObtenerTodos()
        {
            var dpto = await _dpto.GetDepartamentos();
            return Ok(dpto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Departamento>> ObtenerDepartamento(int id)
        {
            var dpto = await _dpto.GetDepartamento(id);
            
            if(dpto.idDepartamento == 0 || dpto.descripcion == null)
            {
                return NotFound();
            }
            return Ok(dpto);
        }

        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> CrearDpto([FromBody] Departamento departamento)
        {
            var dpto = await _dpto.CreateDpto(departamento);
            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> ActualizarDpto(int id, [FromBody] Departamento departamento)
        {
            if (id != departamento.idDepartamento)
            {
                return BadRequest();
            }

            var _departamento = await _dpto.UpdateDpto(id, departamento);
            
            if(_departamento.idDepartamento == 0 || _departamento.descripcion == null)
            {
                return NotFound();
            }

            return NoContent();

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> EliminarDpto(int id)
        {

            var _departamento = await _dpto.DeleteDpto(id);

            if (!_departamento)
            {
                return NotFound();
            }

            return NoContent();

        }

    }
}
