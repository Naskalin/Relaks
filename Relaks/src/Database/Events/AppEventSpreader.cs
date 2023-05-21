using Relaks.Models;
using Microsoft.EntityFrameworkCore;

namespace Relaks.Database.Events;

public static class AppEventSpreader
{
    public static void OnSavingChanges(object? sender, EventArgs eventArgs)
    {
        if (sender == null || sender is AppDbContext == false) return;
        var db = (AppDbContext) sender;

        foreach (var trackEntry in db.ChangeTracker.Entries())
        {
            switch (trackEntry.Entity)
            {
                case BaseEntry entry:
                    switch (trackEntry.State)
                    {
                        case EntityState.Added:
                            EntryEvents.Create(db, entry);
                            break;
                        case EntityState.Modified:
                            EntryEvents.Update(db, entry);
                            break;
                        case EntityState.Deleted:
                            EntryEvents.Delete(db, entry);
                            break;
                    }
                    break;
                case BaseEntryInfo eInfo:
                    switch (trackEntry.State)
                    {
                        case EntityState.Added:
                            EntryInfoEvents.Create(db, eInfo);
                            break;
                        case EntityState.Modified:
                            EntryInfoEvents.Update(db, eInfo);
                            break;
                        case EntityState.Deleted:
                            EntryInfoEvents.Delete(db, eInfo);
                            break;
                    }
                    break;
                case BaseFile baseFile:
                    switch (trackEntry.State)
                    {
                        case EntityState.Added:
                            FileEvents.Create(db, baseFile);
                            break;
                        case EntityState.Modified:
                            FileEvents.Update(db, baseFile);
                            break;
                        case EntityState.Deleted:
                            FileEvents.Delete(db, baseFile);
                            break;
                    }
                    break;
            }
        }
    }
}