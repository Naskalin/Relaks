using App.DbConfigurations;
using App.Models;
using App.Utils;
using Microsoft.EntityFrameworkCore;

namespace App.DbEvents;

public static class EventSpreader
{
    public static void OnSavingChanges(object? sender, EventArgs eventArgs)
    {
        if (sender == null || sender is AppDbContext == false) return;
        var db = (AppDbContext) sender;
        // var hasChanges = false;
        // var trackStates = new List<EntityState>{EntityState.Added, EntityState.Modified, EntityState.Deleted};
        // var addRows = new List<string> {};

        foreach (var trackEntry in db.ChangeTracker.Entries())
        {
            switch (trackEntry.Entity)
            {
                case Entry entry:
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