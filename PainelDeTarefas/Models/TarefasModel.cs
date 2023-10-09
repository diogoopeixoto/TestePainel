namespace PainelDeTarefas.Models
{
    public class TarefasModel: Tarefas
    {
        public List<Tarefas> ListaTarefas { get; set; }
        public List<Funcionarios> ListaFuncionarios { get; set; } // Adicione esta propriedade
    }
}
