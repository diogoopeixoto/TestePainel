namespace PainelDeTarefas.Models
{
    public class Funcionarios
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }

        // Um funcionário pode ter várias tarefas atribuídas
        public List<Tarefas>? TarefasAtribuidas { get; set; }
    }
}
