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

        public IActionResult Index(string? aramaMetni, int? seciliKategoriId, string siralamaAlani = "baslik", string siralamaDuzeni = "asc")
        {
            foreach (var kitap in _kitaplar)
            {
                kitap.Category = _kategoriler.FirstOrDefault(k => k.Id == kitap.CategoryId);
            }

            IEnumerable<Book> sonuc = _kitaplar;

            if (!string.IsNullOrWhiteSpace(aramaMetni))
            {
                sonuc = sonuc.Where(k =>
                    k.Baslik.Contains(aramaMetni, StringComparison.OrdinalIgnoreCase) ||
                    k.Yazar.Contains(aramaMetni, StringComparison.OrdinalIgnoreCase));
            }

            if (seciliKategoriId.HasValue)
            {
                sonuc = sonuc.Where(k => k.CategoryId == seciliKategoriId.Value);
            }

            sonuc = (siralamaAlani, siralamaDuzeni) switch
            {
                ("baslik",  "asc")  => sonuc.OrderBy(k => k.Baslik),
                ("baslik",  "desc") => sonuc.OrderByDescending(k => k.Baslik),
                ("yazar",   "asc")  => sonuc.OrderBy(k => k.Yazar),
                ("yazar",   "desc") => sonuc.OrderByDescending(k => k.Yazar),
                ("fiyat",   "asc")  => sonuc.OrderBy(k => k.Fiyat),
                ("fiyat",   "desc") => sonuc.OrderByDescending(k => k.Fiyat),
                ("stok",    "asc")  => sonuc.OrderBy(k => k.Stok),
                ("stok",    "desc") => sonuc.OrderByDescending(k => k.Stok),
                _                   => sonuc.OrderBy(k => k.Baslik)
            };

            var model = new BookListViewModel
            {
                Kitaplar        = sonuc,
                AramaMetni      = aramaMetni,
                SeciliKategoriId = seciliKategoriId,
                SiralamaAlani   = siralamaAlani,
                SiralamaDuzeni  = siralamaDuzeni,
                Kategoriler     = _kategoriler
            };

            return View(model);
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
