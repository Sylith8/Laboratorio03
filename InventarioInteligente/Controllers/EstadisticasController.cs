using Microsoft.AspNetCore.Mvc;
using InventarioInteligente.Data;
using System.Linq;

namespace InventarioInteligente.Controllers
{
    public class EstadisticasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstadisticasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            
            var productosPorPrecio = _context.Productos
                .OrderByDescending(p => p.Precio)
                .ToList();

            
            decimal precioPromedio = _context.Productos.Any()
                ? _context.Productos.Average(p => p.Precio)
                : 0;

            
            decimal valorTotalInventario = _context.Productos
                .Sum(p => p.Precio * p.Stock);

          
            var productosStockCritico = _context.Productos
                .Where(p => p.Stock < 5)
                .ToList();

            ViewBag.ProductosPorPrecio = productosPorPrecio;
            ViewBag.PrecioPromedio = precioPromedio;
            ViewBag.ValorTotalInventario = valorTotalInventario;
            ViewBag.ProductosStockCritico = productosStockCritico;

            return View();
        }
    }
}
