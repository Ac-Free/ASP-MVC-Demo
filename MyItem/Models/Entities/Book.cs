using System;
using System.Collections.Generic;

namespace MyItem.Models.Entities;

public partial class Book
{
    public int Id { get; set; }

    public string? BookName { get; set; }

    public int? UserId { get; set; }

    public int? Status { get; set; }

    public User? User { get; set; }
}
