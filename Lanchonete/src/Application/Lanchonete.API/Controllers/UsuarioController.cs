﻿using Lanchonete.Business.Filters;
using Lanchonete.Business.Ports.In;
using Lanchonete.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Lanchonete.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioUseCase usuarioUseCase;
        
        public UsuarioController(IUsuarioUseCase usuarioUseCase)
        {
            this.usuarioUseCase = usuarioUseCase;
        }
        [HttpGet()]
        [Produces("application/json")]
        public async Task<IActionResult> Buscar(string? nome = null, string? cpf = null)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioFiltro = new UsuarioFiltro()
            {
                CPF = cpf,
                Nome = nome
            };

            var usuario = await usuarioUseCase.Buscar(usuarioFiltro); 
            return Ok(usuario);
        }
        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Inserir([FromBody] Usuario usuario)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await usuarioUseCase.Inserir(usuario);

            return Ok(usuario);
        }
        [HttpDelete("{usuarioId}")]
        [Produces("application/json")]
        public async Task<IActionResult> Apagar([FromRoute] int usuarioId)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await usuarioUseCase.Apagar(usuarioId);

            return NoContent();
        }
        [HttpPatch]
        [Produces("application/json")]
        public async Task<IActionResult> Atualizar([FromBody] Usuario usuario)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await usuarioUseCase.Atualizar(usuario);

            return Ok(usuario);
        }
    }
}
