using System.ComponentModel.DataAnnotations;

namespace PainelDeTarefas.Models
{
    public class Tarefas
    {
        //public int Id { get; set; }
        //public string Descricao { get; set; }
        //public Funcionarios Responsavel { get; set; }
        //public int ResponsavelId { get; set; }
        //[Display(Name = "Data de Início")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime HoraInicio { get; set; }
        //[Display(Name = "Data Final Estimada")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime HoraFinalEstimada { get; set; }
        //public TimeSpan TempoDecorrido { get; set; }
        //public StatusTarefa Status { get; set; }
        public int Id { get; set; }
        public string Descricao { get; set; }
        public Funcionarios Responsavel { get; set; }
        public int ResponsavelId { get; set; }
        [Display(Name = "Data de Início")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HoraInicio { get; set; }
        [Display(Name = "Data Final Estimada")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime HoraFinalEstimada { get; set; }
        public TimeSpan TempoDecorrido { get; set; }
        public StatusTarefa Status { get; set; }

        public TimeSpan TempoExecucao { get; set; }

        // Adicione esta propriedade para armazenar o tempo estimado em horas
        public TimeSpan TempoEstimadoEmHoras { get; set; }

        public bool TempoExcedido        
            
            {
                get { return TempoDecorrido > TempoEstimadoEmHoras; }
            }
        
    }



    public enum StatusTarefa
    {
        Pendente = 0,
        EmAndamento = 1,
        Concluida = 2,
        Pausada = 3,
    }
}
