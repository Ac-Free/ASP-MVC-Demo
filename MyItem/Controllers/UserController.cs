using Microsoft.AspNetCore.Mvc;
using MyItem.Data;
using Microsoft.EntityFrameworkCore;
using MyItem.Models.Entities;

namespace Book.Controllers;

public class UserController : Controller
{
    private readonly BookDbContext _context;

    private readonly ILogger<UserController> _logger;

    public UserController(BookDbContext context, ILogger<UserController> logger)
    {
        _logger = logger;
        _context = context;
    }

    // 用来展示登录页面
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // 用来展示注册页面
    [HttpGet]
    public IActionResult Register()
    {
        return View("register");
    }

    // 处理用户注册逻辑
    [HttpPost]
    public async Task<IActionResult> Register(User user)
    {
        if (ModelState.IsValid)
        {
            // 检查用户名是否已存在
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == user.UserName);
            
            if (existingUser != null)
            {
                ModelState.AddModelError("", "用户名已存在，请使用其他用户名");
                return View(user);
            }
            
            // 设置创建时间
            user.CreateTime = DateTime.Now;

            try
            {
                // 保存到数据库
                _context.Users.Add(user);
                await _context.SaveChangesAsync();


                // 重定向到登录页面
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "注册失败：" + ex.Message);
                return View(user);
            }
        }
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Login(User user)
    {
        if (ModelState.IsValid)
        {
            // 根据用户名查询
            var dbUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == user.UserName);

            // 判断用户是否存在
            if (dbUser == null)
            {
                ModelState.AddModelError("", "用户名不存在");
                _logger.LogWarning("用户名不存在");
                return View(user);
            }

            // 判断密码是否正确
            if (dbUser.Pwd != user.Pwd)
            {
                ModelState.AddModelError("", "密码不正确");

                return View(user);
            }

            // 登录成功，可以设置Session
            HttpContext.Session.SetInt32("UserId", dbUser.Id);
            HttpContext.Session.SetString("UserName", dbUser.UserName);

            // 重定向到首页
            return RedirectToAction("Index", "Home");
        }

        return View(user);
    }

    // 退出登录
    [HttpGet]
    public IActionResult Logout()
    {
        // 清除会话中的所有数据
        HttpContext.Session.Clear();

        // 可选：添加成功退出的提示信息
        TempData["Message"] = "您已成功退出登录";

        // 重定向到首页
        return RedirectToAction("Index", "Home");
    }
}
