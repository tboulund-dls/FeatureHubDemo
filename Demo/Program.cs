// See https://aka.ms/new-console-template for more information

using FeatureHubSDK;

FeatureLogging.DebugLogger += (sender, s) => Console.WriteLine("DEBUG: " + s); 
FeatureLogging.TraceLogger += (sender, s) => Console.WriteLine("TRACE: " + s); 
FeatureLogging.InfoLogger += (sender, s) => Console.WriteLine("INFO: " + s); 
FeatureLogging.ErrorLogger += (sender, s) => Console.WriteLine("ERROR: " + s);

var config = new EdgeFeatureHubConfig("http://featurehub:8085", "94b24451-dff0-430a-9aab-cd161835e885/GhY4jU1vJ9xe7j6ZGBTDc881wLNhzdwUDFL2n0n5");
var fh = await config.NewContext().UserKey("my-user-key-1").Build();
var danishGreeting = fh["DanishGreeting"].IsEnabled;

await Task.Run(() =>
{
    if (danishGreeting)
    {
        Console.WriteLine("Hejsa Verden");
    }
    else
    {
        Console.WriteLine("Hello World");
    }
});

