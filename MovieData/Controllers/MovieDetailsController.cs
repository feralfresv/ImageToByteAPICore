using System.IO;
using System.Threading.Tasks;
using MovieData.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MovieData.Controllers
{
    public class MovieDetailsController : Controller
    {
        private readonly DatingAppContext _context;

        public MovieDetailsController(DatingAppContext context)
        {
            _context = context;
        }

        public ActionResult ViewPhoto(int id)
        {
            var photo = _context.MovieDetails.Find(id).Image;

            return File(photo, "image/jpeg"); // you'll need to specify the content type based on your picture
        }

        // GET: MovieDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.MovieDetails.ToListAsync());
        }

        // GET: MovieDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieDetails = await _context.MovieDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieDetails == null)
            {
                return NotFound();
            }

            return View(movieDetails);
        }

        // GET: MovieDetails/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: MovieDetails/
        [HttpPost]
        public async Task<IActionResult> Index(MovieDetails movies, List<IFormFile> Image)
        {
            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using var stream = new MemoryStream();
                    await item.CopyToAsync(stream);
                    movies.Image = stream.ToArray();
                }
            }
            _context.MovieDetails.Add(movies);
            _context.SaveChanges();
            return View();
        }

        // GET: MovieDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieDetails = await _context.MovieDetails.FindAsync(id);
            if (movieDetails == null)
            {
                return NotFound();
            }
            return View(movieDetails);
        }

        // POST: MovieDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MovieDetails movieDetails, List<IFormFile> Image)
        {
            if (id != movieDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var item in Image)
                    {
                        if (item.Length > 0)
                        {
                            using var stream = new MemoryStream();
                            await item.CopyToAsync(stream);
                            movieDetails.Image = stream.ToArray();
                        }
                    }
                    _context.Update(movieDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieDetailsExists(movieDetails.Id))
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
            return View(movieDetails);
        }

        // GET: MovieDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieDetails = await _context.MovieDetails
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieDetails == null)
            {
                return NotFound();
            }

            return View(movieDetails);
        }

        // POST: MovieDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieDetails = await _context.MovieDetails.FindAsync(id);
            _context.MovieDetails.Remove(movieDetails);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("List", "AddMovie2", new { @id = id });
        }

        private bool MovieDetailsExists(int id)
        {
            return _context.MovieDetails.Any(e => e.Id == id);
        }
    }
}
