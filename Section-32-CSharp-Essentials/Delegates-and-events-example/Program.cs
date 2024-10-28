using Delegates_and_events_example;
using static System.Console;

namespace Delegates_and_events_example {
    class Program {
        public static async Task Main() {
            Console.WriteLine("delegates and events example");

            Player player = new Player();
            player.AchievementUnlocked += OnAchievementUnlocked; // subscribe

            await player.AddPoints(30);
            await player.AddPoints(40);
            await player.AddPoints(35);
            await player.AddPoints(1);
        }

        static void OnAchievementUnlocked(int points) {
            WriteLine($"Congratulations! Achievement unlocked for earning {points} points!");
        }
    }
}


