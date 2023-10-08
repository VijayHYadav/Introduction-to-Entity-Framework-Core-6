using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFCoreMovies.Utilites
{
    public interface IChangeTrackerEventHandler
    {
        void SavedChangesFailHandler(object sender, SaveChangesFailedEventArgs args);
        void SavedChangesHandler(object sender, SavedChangesEventArgs args);
        void SavingChangesHandler(object sender, SavingChangesEventArgs args);
        void StateChangeHandler(object sender, EntityStateChangedEventArgs args);
        void TrackedHandler(object sender, EntityTrackedEventArgs args);
    }

    public class ChangeTrackerEventHandler :  IChangeTrackerEventHandler
    {
        private readonly ILogger<ChangeTrackerEventHandler> logger;
        public ChangeTrackerEventHandler(ILogger<ChangeTrackerEventHandler> logger) {
            this.logger = logger;
        }

        public void TrackedHandler(object sender, EntityTrackedEventArgs args)
        {
            var message = $"Entity: {args.Entry.Entity}, state: {args.Entry.State}";
            logger.LogInformation(message);
        }

        public void StateChangeHandler(object sender, EntityStateChangedEventArgs args)
        {
            var message = $"Entity: {args.Entry.Entity}, previous state: {args.OldState} - new State: {args.NewState}";
            logger.LogInformation(message);
        }

        // ! runs before saving
        public void SavingChangesHandler(object sender, SavingChangesEventArgs args) {
            var entities = ((ApplicationDbContext)sender).ChangeTracker.Entries();

            foreach (var entity in entities)
            {
                var message = $"Entity: {entity.Entity} it's going to be {entity.State}";
                logger.LogInformation(message);
            }
        }

        // ! runs after changes
        public void SavedChangesHandler(object sender, SavedChangesEventArgs args) {
            var message = $"We processed {args.EntitiesSavedCount} entities.";
            logger.LogInformation(message);
        }

       // ! runs on save changes operation fails
        public void SavedChangesFailHandler(object sender, SaveChangesFailedEventArgs args) {
            logger.LogError(args.Exception, "Error in SaveChanges");
        }
    }
}
