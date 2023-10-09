using Microsoft.EntityFrameworkCore;
using PainelDeTarefas.Models;

namespace PainelDeTarefas.Data
{
    public class PainelDeTarefasContext : DbContext
    {
        public PainelDeTarefasContext(DbContextOptions<PainelDeTarefasContext> options)
           : base(options)
        {
        }
             
        public DbSet<PainelDeTarefas.Models.Tarefas> Tarefa { get; set; } = default!;
        public DbSet<PainelDeTarefas.Models.Funcionarios> Funcionario { get; set; } = default!;

        
    }
}
