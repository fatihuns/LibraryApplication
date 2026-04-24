using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using LibraryApplication.Models;

namespace LibraryApplication.Controllers
{
    public class BooksController : Controller
    {
        private static readonly List<Category> _kategoriler = new List<Category>
        {
            new Category { Id = 1, Name = "Roman" },
            new Category { Id = 2, Name = "Hikaye" },
            new Category { Id = 3, Name = "Şiir" },
            new Category { Id = 4, Name = "Deneme" },
            new Category { Id = 5, Name = "Bilim Kurgu" }
        };

        private static readonly List<Book> _kitaplar = new List<Book>
        {
            new Book { Id = 1, Baslik = "Çalıkuşu", Yazar = "Reşat Nuri Güntekin", Fiyat = 150m, Stok = 100, CategoryId = 1 },
            new Book { Id = 2, Baslik = "Bir İdam Mahkumunun Son Günü", Yazar = "Victor Hugo", Fiyat = 105.50m, Stok = 50, CategoryId = 2 },
            new Book { Id = 3, Baslik = "Beyaz Leke", Yazar = "Aslı Arslan", Fiyat = 347.40m, Stok = 300, CategoryId = 1 }
        };
        private static int _sonrakiId = 4;

        public IActionResult Index()
        {
            foreach (var kitap in _kitaplar)
            {
                kitap.Category = _kategoriler.FirstOrDefault(k => k.Id == kitap.CategoryId);
            }

            return View(_kitaplar);
        }

        public IActionResult Add()
        {
            ViewBag.Categories = new SelectList(_kategoriler, "Id", "Name");
            return View(new Book());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Book book)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_kategoriler, "Id", "Name", book.CategoryId);
                return View(book);
            }

            book.Id = _sonrakiId++;
            _kitaplar.Add(book);
            TempData["Success"] = "Kitap başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            var kitap = _kitaplar.FirstOrDefault(k => k.Id == id);
            if (kitap == null) return NotFound();

            ViewBag.Categories = new SelectList(_kategoriler, "Id", "Name", kitap.CategoryId);
            return View(kitap);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Book updatedBook)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_kategoriler, "Id", "Name", updatedBook.CategoryId);
                return View(updatedBook);
            }

            var mevcutKitap = _kitaplar.FirstOrDefault(k => k.Id == updatedBook.Id);
            if (mevcutKitap == null) return NotFound();

            mevcutKitap.Baslik = updatedBook.Baslik;
            mevcutKitap.Yazar = updatedBook.Yazar;
            mevcutKitap.Fiyat = updatedBook.Fiyat;
            mevcutKitap.Stok = updatedBook.Stok;
            mevcutKitap.CategoryId = updatedBook.CategoryId;

            TempData["Success"] = "Kitap başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            var kitap = _kitaplar.FirstOrDefault(k => k.Id == id);
            if (kitap == null) return NotFound();

            _kitaplar.Remove(kitap);
            TempData["Success"] = "Kitap başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
