// See https://aka.ms/new-console-template for more information


using FeatureHubSDK;
using IO.FeatureHub.SSE.Model;

FeatureLogging.DebugLogger += (sender, s) => Console.WriteLine("DEBUG: " + s + "\n"); 
FeatureLogging.TraceLogger += (sender, s) => Console.WriteLine("TRACE: " + s + "\n"); 
FeatureLogging.InfoLogger += (sender, s) => Console.WriteLine("INFO: " + s + "\n"); 
FeatureLogging.ErrorLogger += (sender, s) => Console.WriteLine("ERROR: " + s + "\n");

var config = new EdgeFeatureHubConfig("http://featurehub:8085", "2b999acc-31ea-4ca0-96bc-e1a07045d1fb/f5LoYQIH5ST3D9FHd92WbnRmIg3oiJBE70i2NDOZ");

// All values are captured on build and not automatically updated, but changes can be streamed afterwards
var fh = await config.NewContext()
    .UserKey("my-user-key3")
    .Country(StrategyAttributeCountryName.Denmark)
    .Build();
var timeSpecificGreeting = fh["TimeSpecificGreeting"];
var timeSpecificGreetingValue = timeSpecificGreeting.IsEnabled;
timeSpecificGreeting.FeatureUpdateHandler += (sender, feature) => timeSpecificGreetingValue = feature.IsEnabled;

await Task.Run(() =>
{
    // Thread.Sleep(5000);
    
    Console.WriteLine(timeSpecificGreetingValue);
    if (timeSpecificGreetingValue)
    {
        var greeting = "";
        if (DateTime.Now.Hour < 12)
        {
            greeting = "Good morning";
        }
        else
        {
            greeting = "Good day";
        }
        Console.WriteLine($"{greeting}, World!");
    }
    else
    {
        Console.WriteLine("Hello, World!");
    }
});

