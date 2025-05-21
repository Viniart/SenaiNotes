using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SenaiNotes.DTO;
using SenaiNotes.Interfaces;
using SenaiNotes.Models;
using SenaiNotes.Validators;
using Swashbuckle.AspNetCore.Annotations;

namespace SenaiNotes.Controller
{
    [Route("api/anotacao")]
    [ApiController]
    public class AnotacaoController : ControllerBase
    {
        private readonly IAnotacaoRepository _repository;
        public AnotacaoController(IAnotacaoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            var dados = _repository.ListarTodosInclude();

            return Ok(dados);
        }

        [HttpPost]
        public IActionResult Cadastrar(CadastroAnotacaoDto anotacao)
        {
            var validacao = new AnotacaoDtoValidator().Validate(anotacao);

            if (!validacao.IsValid)
            {
                var erros = validacao.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(validacao.Errors);
            }

            _repository.CadastrarAnotacao(anotacao);

            return Created();
        }

        [HttpPost("{id}")]
        [SwaggerOperation(
            Summary = "Arquiva uma anotação",
            Description = "Este endpoint arquiva uma anotação existente com base no ID fornecido.",
            OperationId = "ArquivarAnotacao"
        )]
        public IActionResult ArquivarAnotacao(int id)
        {
            var anotacao = _repository.ArquivarAnotacao(id);

            if (anotacao == null) return NotFound();

            return Ok(anotacao);
        }

        [HttpGet("{id}")]
        public IActionResult ListarPorId(int id)
        {
            var anotacao = _repository.ObterPorId(id);

            if (anotacao == null) return NotFound();

            return Ok(anotacao);
        }

        [HttpGet("usuario/{idUsuario}")]
        public IActionResult ListarPorUsuario(int idUsuario)
        {
            var anotacao = _repository.BuscarPorUsuario(idUsuario);

            if (anotacao == null) return NotFound();

            return Ok(anotacao);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Anotacao anotacaoNova)
        {
            var anotacaoAtualizada = _repository.Atualizar(id, anotacaoNova);

            if (anotacaoAtualizada == null) return NotFound();

            return Ok(anotacaoAtualizada);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var anotacaoDeletada = _repository.Deletar(id);

            if (anotacaoDeletada == null) return NotFound();

            return Ok(anotacaoDeletada);
        }
    }
}
