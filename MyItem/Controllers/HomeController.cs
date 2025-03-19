using Microsoft.AspNetCore.Mvc;
using MyItem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http; // 添加这个引用用于Session
using MyItem.Models.ViewModels;
namespace MyItem.Controllers; // 修改命名空间与项目一致

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly BookDbContext _context;

    public HomeController(ILogger<HomeController> logger, BookDbContext context)
    {
        _logger = logger;
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

    public async Task<IActionResult> Make(int id)
    {
        try
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return Json(new { success = false, message = "用户未登录" });
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return Json(new { success = false, message = "暂无此书" });
            }

            if (book.Status != 0)
            {
                return Json(new { success = false, message = "已被借阅" });
            }

            book.Status = 1;
            book.UserId = userId;

            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "成功" });
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "出现问题：" + ex.Message });
        }
    }

    public async Task<IActionResult> UnMake(int id)
    {
        try
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (!userId.HasValue)
            {
                return Json(new { success = false, message = "未登录" });
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return Json(new { success = false, message = "暂无此书" });
            }

            if (book.Status != 1 || book.UserId != userId)
            {
                return Json(new { success = false, message = "已被借阅" });
            }

            book.Status = 0;
            book.UserId = null;

            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "成功" });
        }
        catch (Exception e)
        {
            return Json(new { success = false, message = e.Message });
        }
    }
}
