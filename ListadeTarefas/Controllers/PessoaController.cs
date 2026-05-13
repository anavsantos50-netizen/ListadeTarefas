using listadeTarefas.Data;
using listadeTarefas.Models;
using Microsoft.AspNetCore.Mvc;
namespace listadeTarefas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
       private readonly PessoaContext _context;
        public PessoaController(PessoaContext context)
        {
            _context = context;
        
        }
      
            
            [HttpDelete("Deletar/{id}")]
        public IActionResult DeletarPessoa(int id)
        {
            var pessoa = _context.Pessoas.Find(id);
            if (pessoa == null)
                return NotFound("Pessoa não encontrada.");
            _context.Pessoas.Remove(pessoa);
            _context.SaveChanges();

            return NoContent();
        }
        [HttpPut("Atualizar/{id}")]

        public IActionResult AtualizarPessoa(int id, Pessoa pessoa)
        {
            var pessoaDoBanco = _context.Pessoas.Find(id);

            if (pessoaDoBanco == null)
                return NotFound("Pessoa não encontrada");
            pessoaDoBanco.Nome = pessoa.Nome;
            pessoaDoBanco.Email = pessoa.Email;
            pessoaDoBanco.Senha = pessoa.Senha;
            _context.SaveChanges();

            return Ok("Atualizado");
        }

        [HttpPost("Cadastrar")]
        public IActionResult CadastrarPessoa (Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();
            return Created("", pessoa);
        }

        [HttpPost("Login")]
        public IActionResult Login (Pessoa dadosLogin)
        {
            var loginU = _context.Pessoas.Where(u => u.Email.Equals(dadosLogin.Email) && u.Senha.Equals(dadosLogin.Senha)).ToList();

            if (loginU.Count == 0)
                return Unauthorized("Email ou senha incorreta.");

            HttpContext.Session.SetString("email", dadosLogin.Email);
            Response.Cookies.Append("Idusado", loginU[0].Id.ToString(),
             new CookieOptions
             {
                 Expires = DateTime.Now.AddMinutes(38),
                 Secure = true,
                 HttpOnly = true
             
             });
            return Ok("Login realizado com sucesso.");
        }

        [HttpGet("{id}")]
        public IActionResult ConsultarPessoaId(int id)
        {
            var pessoaDoBanco = _context.Pessoas.Find(id);

            if (pessoaDoBanco == null)
                return NotFound("Não encontrada");
            return Ok("Vou consultar uma pessoa.");
        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            Response.Cookies.Delete("IdLogado");
            Response.Cookies.Delete(".AspNetCore.Session");
            return Ok("Logout realizado com sucesso");

        }

    }
}
