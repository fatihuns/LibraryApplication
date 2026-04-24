using System.ComponentModel.DataAnnotations;

namespace LibraryApplication.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık gerekli.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Başlık 2 ile 150 karakter arasında olmalı.")]
        [Display(Name = "Başlık")]
        public string Baslik { get; set; }

        [Required(ErrorMessage = "Yazar gerekli.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Yazar adı 2 ile 100 karakter arasında olmalı.")]
        [Display(Name = "Yazar")]
        public string Yazar { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalı.")]
        [Display(Name = "Fiyat")]
        public decimal Fiyat { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stok 0 veya daha büyük olmalı.")]
        [Display(Name = "Stok")]
        public int Stok { get; set; }

        [Required(ErrorMessage = "Kategori seçimi gerekli.")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
