
using Microsoft.AspNetCore.Mvc;
using InventarioInteligente.Data;
using InventarioInteligente.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarioInteligente.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductosController(ApplicationDbContext context){ _context = context; }

        public async Task<IActionResult> Index(string buscar)
        {
            var productos = _context.Productos.AsQueryable();
            if(!string.IsNullOrEmpty(buscar))
                productos = productos.Where(p => p.Nombre.Contains(buscar));
            return View(await productos.ToListAsync());
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Producto producto)
        {
            if(ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Producto producto)
        {
            if(ModelState.IsValid)
            {
                _context.Update(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            _context.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
