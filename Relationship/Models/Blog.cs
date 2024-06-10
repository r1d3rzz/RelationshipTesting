using System;
using System.Collections.Generic;

namespace Relationship.Models;

public partial class Blog
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public User User { get; set; } = null!;

    public int? CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public string? Title { get; set; }

    public string? Body { get; set; }

    public ICollection<BlogUser> BlogUsers { get; set; } = null!;
}
