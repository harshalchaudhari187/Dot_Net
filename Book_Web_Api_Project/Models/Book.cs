using System;
using System.Collections.Generic;

namespace BookWebAPI.Models;

public partial class Book
{
    public int Id { get; set; }

    public string BookName { get; set; } = null!;

    public string BookAuthor { get; set; } = null!;

    public decimal BookPrice { get; set; }
}
