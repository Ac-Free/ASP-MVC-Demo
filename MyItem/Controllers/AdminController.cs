using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyItem.Data;
using MyItem.Models.ViewModels;
using System.Threading.Tasks;

namespace MyItem.Controllers
{
    public class AdminController : Controller
    {
        private readonly BookDbContext _context;

        public AdminController(BookDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            // 验证输入参数
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;
            if (pageSize > 50) pageSize = 50; // 设置最大页面大小，防止请求过大

            // 创建分页查询
            var booksQuery = _context.Books.AsQueryable();
            var paginatedBooks = await PaginatedList<Models.Entities.Book>.CreateAsync(
                booksQuery, page, pageSize);

            // 加载用户信息
            foreach (var book in paginatedBooks.Items)
            {
                if (book.Status == 1 && book.User == null)
                {
                    book.User = await _context.Users.FindAsync(book.UserId);
                }
            }

            return View(paginatedBooks);
        }

        //修改图书信息
        [HttpPost]
        public async Task<IActionResult> UpdateBook(int Id, string BookName)
        {
            var book = await _context.Books.FindAsync(Id);
            if (book == null)
            {
                return NotFound();
            }

            book.BookName = BookName;
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "图书信息已成功更新";
            return RedirectToAction(nameof(Index));
        }

        // 新增图书
        [HttpPost]
        public async Task<IActionResult> AddBook(string BookName)
        {
            var book = new Models.Entities.Book
            {
                BookName = BookName,
                Status = 0,
                UserId = null,
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // 删除图书
        [HttpPost]
        public async Task<IActionResult> DeleteBook(int Id)
        {
            var book = await _context.Books.FindAsync(Id);
            if (book == null)
            {
                return NotFound();
            }

            // 检查图书是否可以删除（未被借出）
            if (book.Status == 1)
            {
                TempData["ErrorMessage"] = "无法删除已借出的图书";
                return RedirectToAction(nameof(Index));
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "图书已成功删除";
            return RedirectToAction(nameof(Index));
        }
    }
}
