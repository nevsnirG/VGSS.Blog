using System.Text.Json;

namespace VGSS.TestCommon;

public class ScenarioFixtureBase
{

    private static string GetStatesDirectory()
    {
        var projectDir = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;
        var statesDirectory = Path.Combine(projectDir, "States");
        return statesDirectory;
    }

    public void Persist(int priority)
    {
        var statesDirectory = GetStatesDirectory();
        Directory.CreateDirectory(statesDirectory);
        var fileName = Path.Combine(statesDirectory, $"{priority}.json");
        var stateAsJson = JsonSerializer.Serialize(this);
        File.WriteAllText(fileName, stateAsJson);
    }
}