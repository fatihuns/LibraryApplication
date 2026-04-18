using System.ComponentModel.DataAnnotations;

namespace LibraryApplication.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık gerekli.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Başlık 2 ile 150 karakter arasında olmalı.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Yazar gerekli.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Yazar adı 2 ile 100 karakter arasında olmalı.")]
        public string Author { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalı.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stok 0 veya daha büyük olmalı.")]
        public int Stock { get; set; }
    }
}
