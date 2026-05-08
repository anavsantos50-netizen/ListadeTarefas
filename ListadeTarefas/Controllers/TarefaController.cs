using ListadeTarefas.Data;
using ListadeTarefas.Models;
using Microsoft.AspNetCore.Mvc;
namespace ListadeTarefas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly PessoaContext _context;
        public TarefaController(PessoaContext context)
        {
            _context = context;

        }
        [HttpDelete("Deletar/{id}")]
        public IActionResult DeletarTarefa(int id)
        {
            var tarefa = _context.Tarefas.Find(id);
            if (tarefa == null)
                return NotFound("Tarefa não encontrada.");
            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();

            return NoContent();
        }
        [HttpPut("atualizar/{id}")]

        public IActionResult AtualizarTarefas(int id, Tarefa tarefa)
        {
            var TarefaDoBanco = _context.Tarefas.Find(id);

            if (TarefaDoBanco == null)
                return NotFound("Tarefa não encontrada");
            TarefaDoBanco.Descricao = tarefa.Descricao;
            TarefaDoBanco.Statuss = tarefa.Statuss;

            _context.SaveChanges();

            return Ok("Atualizado");

        }
        
    
        [HttpGet("{id}")]
        public IActionResult ConsultarPessoaId(int id)
        {
            var tarefaDoBanco = _context.Tarefas.Where(t => t.Id.Equals(id)).ToList();
            if (!tarefaDoBanco.Any())
                return NotFound("Não encontrada");
            return Ok(tarefaDoBanco);
        }
        [HttpPost("Cadastrar")]
        public IActionResult CriarTarefas(Tarefa tarefa)
        {
            var idPessoa = HttpContext.Session.GetString("email");
            if (idPessoa == null) return Unauthorized("não autorizado");

            var sessao = Request.Cookies["Idusado"];

            if (sessao != null)
            {

                tarefa.IdPessoas = int.Parse(sessao);

            }
            _context.Add(tarefa);
            _context.SaveChanges();
            return Created("Teste", tarefa);
        }
    }
}