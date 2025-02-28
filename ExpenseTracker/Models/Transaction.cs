using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required] // Boş bırakılamaz
        [StringLength(100)] // Maksimum 100 karakter
        public string Title { get; set; }

        [Required]
        public decimal Amount { get; set; } //gelir-gider miktarı

        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        [Required]
        public bool IsIncome { get; set; } //gelir mi gider mi?

        [Required]
        public string Category { get; set; } //(yiycek,ulaşım vs)

        
    }
}
