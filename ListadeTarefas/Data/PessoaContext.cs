using listadeTarefas.Models;
using Microsoft.EntityFrameworkCore;

namespace listadeTarefas.Data
{
    public class PessoaContext : DbContext

    {
        public PessoaContext(DbContextOptions<PessoaContext> options) : base(options) { }

        public DbSet<Pessoa> Pessoas {get; set;}
        public DbSet<Tarefa> Tarefas {get; set;}
        
    }
}
