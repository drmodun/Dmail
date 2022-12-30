using Dmail.Data.Entities;
using Dmail.Domain.Enums;

namespace Dmail.Domain.Repositories;
public abstract class BaseRepo
{
    protected readonly DmailContext DbContext;

    protected BaseRepo(DmailContext dbContext)
    {
        DbContext = dbContext;
    }

    protected ResponseType SaveChanges()
    {
        var hasChanges = DbContext.SaveChanges() > 0;
        if (hasChanges)
            return ResponseType.Success;

        return ResponseType.NotChanged;
    }
}
