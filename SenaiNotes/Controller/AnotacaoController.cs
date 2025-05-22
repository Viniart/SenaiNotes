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

            if (anotacao.ArquivoImagem != null)
            {
                #region Verificação Tipo Imagem
                var tiposPermitidos = new[] { "image/jpeg", "image/png", "image/gif", "image/webp" };

                if (!tiposPermitidos.Contains(anotacao.ArquivoImagem.ContentType))
                {
                    return BadRequest("Apenas arquivos de imagem são permitidos (JPEG, PNG, GIF, WEBP).");
                }
                #endregion

                var pastaDestino = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

                //if(!Directory.Exists(pastaDestino))
                //    Directory.CreateDirectory(pastaDestino);

                #region Limpando Nome Arquivo
                // Limpando nome removendo caracteres inválidos
                var nomeAnotacao = string.Concat(anotacao.TituloAnotacao.Where(c => !Path.GetInvalidFileNameChars().Contains(c)));

                // Pego a data atual
                var dataAtual = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                var extensao = Path.GetExtension(anotacao.ArquivoImagem.FileName);

                var novoNomeArquivo = $"{nomeAnotacao}_{dataAtual}{extensao}";
                #endregion

                // Novo Nome Arquivo - arquivo.FileName
                var caminhoCompleto = Path.Combine(pastaDestino, novoNomeArquivo);

                using (var stream = new FileStream(caminhoCompleto, FileMode.Create))
                {
                    anotacao.ArquivoImagem.CopyTo(stream);
                }

                anotacao.ImagemAnotacao = novoNomeArquivo;
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
