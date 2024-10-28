using System;

namespace Delegates_and_events_example {
    internal class Player {
        // properties
        public int Points { get; private set; }

        // our delgate function signature
        public delegate void AchievementUnlockedhandler(int points);

        // our event that will be invoked, and subscribed outside of the function
        public event AchievementUnlockedhandler? AchievementUnlocked;

        public async Task AddPoints(int points) {
            Points += points; 
            Console.WriteLine($"Player earned {points} points.  Total Points: {Points}");
            await Task.Delay( 1000 );

            if(Points >= 100) {
                AchievementUnlocked?.Invoke(Points); // call to our event
            }
        }

    }
}
