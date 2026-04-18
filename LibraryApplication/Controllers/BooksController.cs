using Microsoft.AspNetCore.Mvc;
using LibraryApplication.Models;
namespace LibraryApplication.Controllers
{
    public class BooksController : Controller
    {
        // Verileri buradan ekliyoruz.
        private static readonly List<Book> _books = new List<Book>
        {
            new Book { Id = 1, Title = "Çalıkuşu", Author = "Reşat Nuri Güntekin", Price = 150m, Stock = 100 },
            new Book { Id = 2, Title = "Bir İdam Mahkumunun Son Günü", Author = "Victor Hugo", Price = 105.50m, Stock = 50 },
            new Book { Id = 3, Title = "Beyaz Leke", Author = "Aslı Arslan", Price = 347.40m, Stock = 300 }
        };
        private static int _nextId = 4;

        public IActionResult Index()
        {
            return View(_books);
        }
        public IActionResult Add()
        {
            return View(new Book());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            book.Id = _nextId++;
            _books.Add(book);
            TempData["Success"] = "Kitap başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Book book)
        {
            if (id != book.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View(book);
            }
            var existing = _books.FirstOrDefault(b => b.Id == id);
            if (existing == null) return NotFound();

            existing.Title = book.Title;
            existing.Author = book.Author;
            existing.Price = book.Price;
            existing.Stock = book.Stock;

            TempData["Success"] = "Kitap başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var book = _books.FirstOrDefault(b => b.Id == id);
            if (book == null) return NotFound();

            _books.Remove(book);
            TempData["Success"] = "Kitap başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}