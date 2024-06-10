using System;
using System.Collections.Generic;

namespace Relationship.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public ICollection<Blog> Blogs { get; set; } = null!;

    public ICollection<BlogUser> BlogUsers { get; set; } = null!;
}
