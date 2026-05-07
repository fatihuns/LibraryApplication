namespace LibraryApplication.Models
{
    public class BookListViewModel
    {
        public IEnumerable<Book> Kitaplar { get; set; } = [];

        public string? AramaMetni { get; set; }
        public int? SeciliKategoriId { get; set; }
        public string SiralamaAlani { get; set; } = "baslik";
        public string SiralamaDuzeni { get; set; } = "asc";

        public IEnumerable<Category> Kategoriler { get; set; } = [];
    }
}
