using listadeTarefas.Data;
using listadeTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace listadeTarefas.Controllers
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
        public IActionResult ConsultarTarefa(int id)
        {
            var pessoaLogada = HttpContext.Session.GetString("IdLogado");
            if (pessoaLogada == null)
            {
                return Unauthorized("Faça login antes!");
            }
            var idPessoaLogada = Request.Cookies["IdLogado"];
            if (idPessoaLogada != null)
            {
                var listadeTarefas = from u in _context.Pessoas
                                join t in _context.Tarefas
                                on u.Id equals t.IdPessoas
                                where u.Id == int.Parse(idPessoaLogada)
                                select new
                                {
                                    Usuarios = u.Nome, u.Email,
                                    Tarefas = t.Statuss,
                                    t.Descricao

                                };
                return Ok(listadeTarefas.ToList());
            }
            return BadRequest();
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