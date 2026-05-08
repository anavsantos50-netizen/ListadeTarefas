namespace ListadeTarefas.Models
{
    public class Tarefa
    {
        public int Id { get; set; } 
        public int IdPessoas { get; set; }
        public string Descricao { get; set; }
        public int Statuss { get; set; }
    }
}
