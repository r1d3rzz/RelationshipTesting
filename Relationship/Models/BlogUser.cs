using System;
using System.Collections.Generic;

namespace Relationship.Models;

public partial class BlogUser
{
    public int BlogId { get; set; }
    public Blog Blog { get; set; } = null!;

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime Created_At { get; set; } = DateTime.Now;
}
