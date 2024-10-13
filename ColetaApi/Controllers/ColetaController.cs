using ColetaApi.Data.Contexts;
using ColetaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ColetaApi.Controllers
{
    public class ColetaController : Controller
    {
        private readonly DatabaseContext _dbContext;

        public ColetaController(DatabaseContext context)
        {
            _dbContext = context;
        }

        [HttpGet]
        public IActionResult Index(int page = 1, int pageSize = 5)
        {
            var totalCount = _dbContext.Coleta.Count();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var model = _dbContext.Coleta
                                .OrderByDescending(c => c.Dt_coleta)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList();

            ViewData["PageIndex"] = page;
            ViewData["PageSize"] = pageSize;
            ViewData["TotalPages"] = totalPages;
            ViewData["HasPrevious"] = (page > 1);
            ViewData["HasNext"] = (page < totalPages);

            return View(model);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ColetaModel coleta)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Coleta.Add(coleta);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(coleta);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var coleta = _dbContext.Coleta.Find(id);
            if (coleta == null)
            {
                return NotFound();
            }
            return View(coleta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, ColetaModel coleta)
        {
            if (id != coleta.Pk_id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var validarColeta = _dbContext.Coleta.Find(id);

                if (validarColeta == null)
                {
                    return NotFound();
                }

                validarColeta.Ds_coleta = coleta.Ds_coleta;
                validarColeta.Ds_tipocoleta = coleta.Ds_tipocoleta;
                validarColeta.Dt_coleta = coleta.Dt_coleta;

                _dbContext.Coleta.Update(validarColeta);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(coleta);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var coleta = _dbContext.Coleta.Find(id);
            if (coleta == null)
            {
                return NotFound();
            }
            return View(coleta);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int Pk_id)
        {
            var coleta = _dbContext.Coleta.Find(Pk_id);
            if (coleta == null)
            {
                return NotFound();
            }

            _dbContext.Coleta.Remove(coleta);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
