using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers
{
    public class TransactionsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = await _context.Transactions.ToListAsync();
            return View(transactions);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            if (ModelState.IsValid) //form doğrulama geçerli mi?
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction); //hata varsa formu tekrar gönder
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id==null || _context.Transactions==null)
            {
                return NotFound(); //hata döndürür
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction==null)
            {
                return NotFound(); //eğer böyle bir harcama yoksa hata döner
            }

            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Transaction transaction)
        {
            if (id!=transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction); 
                    await _context.SaveChangesAsync(); 
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!_context.Transactions.Any(e => e.Id == transaction.Id))
                    {
                        return NotFound(); // Güncellenmeye çalışılan harcama artık yoksa hata ver
                    }
                    else
                    {
                        throw; // Başka bir hata varsa fırlat
                    }
                }

            }
            return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound(); 
            }

            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(m => m.Id == id);

            if (transaction == null)
            {
                return NotFound(); // Eğer böyle bir harcama yoksa hata ver
            }

            return View(transaction); // Silme onay sayfasına harcama bilgilerini gönder
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Transactions' is null.");
            }

            var transaction = await _context.Transactions.FindAsync(id); 
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction); 
                await _context.SaveChangesAsync(); 
            }

            return RedirectToAction(nameof(Index)); // Harcama listesini yeniden yükle
        }



        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
