using System.ComponentModel.DataAnnotations;

namespace LibraryApplication.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı gerekli.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Kategori adı 2 ile 50 karakter arasında olmalı.")]
        [Display(Name = "Kategori")]
        public string Name { get; set; }
    }
}
