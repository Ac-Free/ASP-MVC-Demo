using System;
using System.Collections.Generic;

namespace MyItem.Models.Entities;

public partial class User
{
    public int Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string? Pwd { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreateTime { get; set; }
}
