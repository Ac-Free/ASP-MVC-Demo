using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyItem.Data;
using System.Linq;

namespace MyItem.Controllers
{
    public class ChartController : Controller
    {
        private readonly BookDbContext _context;
        public ChartController(BookDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // 查询 Status = 0 的书籍数量
            int statusZeroCount = _context.Books.Count(b => b.Status == 0);
            
            // 查询 Status = 1 的书籍数量
            int statusOneCount = _context.Books.Count(b => b.Status == 1);
            
            // 将数据传递给视图
            ViewBag.StatusZeroCount = statusZeroCount;
            ViewBag.StatusOneCount = statusOneCount;
            
            return View();
        }
    }
}

