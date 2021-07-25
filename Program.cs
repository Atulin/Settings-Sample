using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace SettingsSample
{
    class Program
    {
        private const string SettingsFile = "./settings.json";

        static async Task Main(string[] args)
        {
            // Create the settings file if it doesn't exist
            if (!File.Exists(SettingsFile)) File.Create(SettingsFile);
            // Read text from the file
            var text = await File.ReadAllTextAsync(SettingsFile);
            // Check if the file contains text
            var settings = string.IsNullOrWhiteSpace(text) 
                ? new Settings() // if it doesn't, use a fresh instance of the `Settings` class
                : JsonSerializer.Deserialize<Settings>(text) ?? new Settings(); // if it does, deserialize it to a class or use a fresh instance if deserializing fails
            
            // Write the current settings to console
            Console.WriteLine(settings);

            // Change some settings
            settings.Name = "Henry Stickmin";
            settings.Number = 420;
            
            // Use those settings
            Console.WriteLine($"Our {settings.Name}'s age is {settings.Number} years.");
            
            // Write the settings to the console again
            Console.WriteLine(settings);
            
            // Write the changed settings to the file
            await File.WriteAllTextAsync(SettingsFile, JsonSerializer.Serialize(settings));
        }
    }

    public class Settings
    {
        // Both properties have default values
        public int Number { get; set; } = 69;
        public string Name { get; set; } = "Bobby Tables";

        // Overriding the `ToString()` method with a JSON serialized display for console writes to be prettier
        public override string ToString() => JsonSerializer.Serialize(this);
    }
}