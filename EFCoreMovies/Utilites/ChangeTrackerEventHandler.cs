using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EFCoreMovies.Utilites
{
    public interface IChangeTrackerEventHandler
    {
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
    }
}
