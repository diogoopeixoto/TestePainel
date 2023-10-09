using Microsoft.AspNetCore.Mvc;
using PainelDeTarefas.Models;

using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using PainelDeTarefas.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PainelDeTarefas.Controllers
{
    public class TarefasController : Controller
    {
        private readonly PainelDeTarefasContext _context;

        public TarefasController(PainelDeTarefasContext context)
        {
            _context = context;
        }

        #region CRUD
        // GET: Tarefa
        public async Task<IActionResult> Index()
        {
            return _context.Tarefa != null ?
                        View(await _context.Tarefa.ToListAsync()) :
                        Problem("Entity set 'PainelDeTarefasContext.Tarefa'  is null.");
        }

        // GET: Tarefa/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tarefa == null)
            {
                return NotFound();
            }

            var tarefa = await _context.Tarefa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        // GET: Tarefa/Create
        public IActionResult Create()
        {
            TarefasModel model = new TarefasModel();
            model.ListaTarefas = _context.Tarefa.ToList();
            model.ListaFuncionarios = _context.Funcionario.ToList(); // Carrega a lista de funcionários
            return View(model);
        }


        // POST: Tarefa/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TarefasModel model) // Use TarefasModel em vez de Tarefas
        {
            if (!ModelState.IsValid)
            {
                // Atribua o Funcionário responsável à Tarefa com base no ResponsavelId
                model.Responsavel = _context.Funcionario.FirstOrDefault(f => f.Id == model.ResponsavelId);

                _context.Add(model); // Adicione o modelo completo, não apenas a tarefa.
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult FuncionariosETarefas()
        {
            var funcionariosComTarefas = _context.Funcionario
                .Include(f => f.TarefasAtribuidas)
                .ToList();

            return View(funcionariosComTarefas);
        }

        public IActionResult MinhaPagina()
        {
            DateTime hoje = DateTime.Today;
            DayOfWeek primeiroDia = DayOfWeek.Sunday; // Defina o primeiro dia da semana conforme necessário
            int diferenca = (int)hoje.DayOfWeek - (int)primeiroDia;
            DateTime primeiroDiaDaSemana = hoje.AddDays(-diferenca);

            // Agora você pode usar primeiroDiaDaSemana para filtrar as tarefas, por exemplo.

            return View();
        }

        [HttpPost]
        public ActionResult AtualizarTempoExecucao(int tarefaId)
        {
            // Recupere a tarefa do banco de dados usando o tarefaId
            var tarefa = _context.Tarefa.Find(tarefaId);

            if (tarefa != null)
            {
                // Recupere o valor atual do cronômetro (tempo de execução)
                TimeSpan tempoExecucao = RecuperarTempoExecucaoAtual(tarefaId);

                // Atualize o TempoExecucao com o novo valor
                tarefa.TempoExecucao = tempoExecucao;

                // Salve as alterações no banco de dados
                _context.SaveChanges();

                // Retorne uma resposta adequada, por exemplo, um JSON com uma mensagem de sucesso
                return Json(new { success = true, message = "Tempo de execução atualizado com sucesso" });
            }

            // Se a tarefa não for encontrada, retorne uma resposta de erro
            return Json(new { success = false, message = "Tarefa não encontrada" });
        }

        // Função para recuperar o valor atual do cronômetro (tempo de execução)
        private TimeSpan RecuperarTempoExecucaoAtual(int tarefaId)
        {
            // Implemente a lógica para recuperar o tempo de execução atual
            // Pode envolver consultas ao banco de dados ou outra fonte de dados

            // Exemplo simples: retorna um valor fixo (00:05:30) para fins de demonstração
            return TimeSpan.Parse("00:05:30");
        }



        // GET: Tarefa/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Tarefa == null)
            {
                return NotFound();
            }

            var cotacao = await _context.Tarefa.FindAsync(id);
            if (cotacao == null)
            {
                return NotFound();
            }
            return View(cotacao);
        }

        // POST: Tarefa/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,HoraInicio,HoraFinalEstimada,Status,TempoDecorrido")] Tarefas tarefa)
        {
            if (id != tarefa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarefa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CotacaoExists(tarefa.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }

        // GET: Tarefa/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tarefa == null)
            {
                return NotFound();
            }

            var cotacao = await _context.Tarefa
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cotacao == null)
            {
                return NotFound();
            }

            return View(cotacao);
        }

        // POST: Cotacao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tarefa == null)
            {
                return Problem("Entity set 'PainelDeTarefasContext.Tarefa'  is null.");
            }
            var cotacao = await _context.Tarefa.FindAsync(id);
            if (cotacao != null)
            {
                _context.Tarefa.Remove(cotacao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CotacaoExists(int id)
        {
            return (_context.Tarefa?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }

    #endregion CRUD
    //// Ação para iniciar uma tarefa
    //public async Task<IActionResult> IniciarTarefa(int id)
    //    {
    //        var tarefa = await _context.Tarefa.FindAsync(id);
    //        if (tarefa == null)
    //        {
    //            return NotFound();
    //        }

    //        if (tarefa.Status != StatusTarefa.Pendente)
    //        {
    //            // Tarefa não pode ser iniciada se não estiver pendente
    //            return BadRequest("A tarefa não pode ser iniciada.");
    //        }

    //        tarefa.Status = StatusTarefa.EmAndamento;
    //        tarefa.HoraInicio = DateTime.Now;

    //        _context.Update(tarefa);
    //        await _context.SaveChangesAsync();

    //        return RedirectToAction(nameof(Index)); // Redirecionar de volta para a página principal
    //    }

    //    // Ação para pausar uma tarefa
    //    public async Task<IActionResult> PausarTarefa(int id)
    //    {
    //        var tarefa = await _context.Tarefa.FindAsync(id);
    //        if (tarefa == null)
    //        {
    //            return NotFound();
    //        }

    //        if (tarefa.Status != StatusTarefa.EmAndamento)
    //        {
    //            // Tarefa não pode ser pausada se não estiver em andamento
    //            return BadRequest("A tarefa não pode ser pausada.");
    //        }

    //        TimeSpan tempoDecorrido = DateTime.Now - tarefa.HoraInicio;
    //        tarefa.Status = StatusTarefa.Pausada;
    //        tarefa.TempoDecorrido += tempoDecorrido;

    //        _context.Update(tarefa);
    //        await _context.SaveChangesAsync();

    //        return RedirectToAction(nameof(Index)); // Redirecionar de volta para a página principal
    //    }

    //    // Ação para concluir uma tarefa
    //    public async Task<IActionResult> ConcluirTarefa(int id)
    //    {
    //        var tarefa = await _context.Tarefa.FindAsync(id);
    //        if (tarefa == null)
    //        {
    //            return NotFound();
    //        }

    //        if (tarefa.Status != StatusTarefa.EmAndamento && tarefa.Status != StatusTarefa.Pausada)
    //        {
    //            // Tarefa não pode ser concluída se não estiver em andamento ou pausada
    //            return BadRequest("A tarefa não pode ser concluída.");
    //        }

    //        TimeSpan tempoDecorrido = DateTime.Now - tarefa.HoraInicio;
    //        tarefa.TempoDecorrido += tempoDecorrido;
    //        tarefa.Status = StatusTarefa.Concluida;

    //        _context.Update(tarefa);
    //        await _context.SaveChangesAsync();

    //        return RedirectToAction(nameof(Index)); // Redirecionar de volta para a página principal
    //    }

        // Outras ações e métodos aqui, como ação para exibir a lista de tarefas
    }







