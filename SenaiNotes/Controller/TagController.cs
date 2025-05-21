using Azure;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.Validators;

namespace SenaiNotes.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _repository;
        public TagController(ITagRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            var dados = _repository.ListarTodos();

            return Ok(dados);
        }

        [HttpPost]
        public IActionResult Cadastrar(Tag tag)
        {
            var validacao = new TagValidator().Validate(tag);

            if (!validacao.IsValid)
            {
                var erros = validacao.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(validacao.Errors);
            }

            _repository.Cadastrar(tag);

            return Created();
        }

        [HttpGet("{id}")]
        public IActionResult ListarPorId(int id)
        {
            var tag = _repository.ObterPorId(id);

            if (tag == null) return NotFound();

            return Ok(tag);
        }

        [HttpGet("usuario/{idUsuario}")]
        public IActionResult ListarPorUsuario(int idUsuario)
        {
            var tag = _repository.BuscarPorUsuario(idUsuario);

            if (tag == null) return NotFound();

            return Ok(tag);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tag tagNova)
        {
            var tagAtualizada = _repository.Atualizar(id, tagNova);

            if (tagAtualizada == null) return NotFound();

            return Ok(tagAtualizada);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tagDeletada = _repository.Deletar(id);

            if (tagDeletada == null) return NotFound();

            return Ok(tagDeletada);
        }
    }
}
