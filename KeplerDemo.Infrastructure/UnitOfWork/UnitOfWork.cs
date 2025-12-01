using KeplerDemo.DataAccess.Context;

namespace KeplerDemo.Infrastructure;

public class UnitOfWork : BaseUnitOfWork<KeplerDemoDbContext>
{
    private readonly KeplerDemoDbContext _context;

    public UnitOfWork(KeplerDemoDbContext context) : base(context)
    {
        _context = context;
    }
}
