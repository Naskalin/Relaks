using App.DbConfigurations;
using App.DbEvents.Fts;
using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.DbEvents;

public static class EventSpreader
{
    public static void OnSavingChanges(object? sender, EventArgs eventArgs)
    {
        if (sender == null || sender is AppDbContext == false) return;
        var db = (AppDbContext) sender;

        foreach (var trackEntry in db.ChangeTracker.Entries())
        {
            switch (trackEntry.Entity)
            {
                case Entry entry:
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
                case EntryInfo eInfo:
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
            }
        }
    }
}