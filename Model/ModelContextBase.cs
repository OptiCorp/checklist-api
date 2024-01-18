
using Microsoft.EntityFrameworkCore;
using Model.Entity;

namespace Model.Context;

public class ModelContextBase : DbContext
{
    public ModelContextBase(DbContextOptions<ModelContextBase> options) : base(options)
    {

    }

    public DbSet<TodoItem> TodoItems { get; set; } = null!;
}
