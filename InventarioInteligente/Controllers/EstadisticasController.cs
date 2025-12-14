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
            // 1️⃣ Reporte de precios (más caro a más barato)
            var productosPorPrecio = _context.Productos
                .OrderByDescending(p => p.Precio)
                .ToList();

            // 2️⃣ Análisis de costos (precio promedio)
            decimal precioPromedio = _context.Productos.Any()
                ? _context.Productos.Average(p => p.Precio)
                : 0;

            // 3️⃣ Valoración del inventario (precio * stock)
            decimal valorTotalInventario = _context.Productos
                .Sum(p => p.Precio * p.Stock);

            // 4️⃣ Alerta de stock crítico (< 5)
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
