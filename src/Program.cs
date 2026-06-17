using System.Text.Json;
using champout;

Console.WriteLine("Hello, World!");

// locate directory of the executable
var exeDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
// navigate up to repo root, get the `masterdata` folder
string? root = exeDir;
while (!Directory.Exists(Path.Combine(root!, "masterdata")))
{
    root = Path.GetDirectoryName(root);
    if (root == null)
    {
        Console.WriteLine("Could not find masterdata directory.");
        return;
    }
}

if (root is null)
{
    Console.WriteLine("Could not find masterdata directory.");
    return;
}

string masterdataDir = Path.Combine(root, "masterdata");
Console.WriteLine($"Masterdata directory: {masterdataDir}");

// read personal.json
string personalJsonPath = Path.Combine(masterdataDir, "personal.json");
if (!File.Exists(personalJsonPath))
{
    Console.WriteLine("personal.json not found.");
    return;
}

string personalJson = File.ReadAllText(personalJsonPath);
var personalTable = JsonSerializer.Deserialize<Personal[]>(personalJson) ?? throw new Exception("Failed to deserialize personal.json");

// read waza_learn.json
string wazaLearnJsonPath = Path.Combine(masterdataDir, "waza_learn.json");
if (!File.Exists(wazaLearnJsonPath))
{
    Console.WriteLine("waza_learn.json not found.");
    return;
}
string wazaLearnJson = File.ReadAllText(wazaLearnJsonPath);
var wazaLearn = JsonSerializer.Deserialize<WazaLearn[]>(wazaLearnJson) ?? throw new Exception("Failed to deserialize waza_learn.json");

// Generate Outputs

// Create output folder in root dir if it doesn't exist, then write outputs to it
string outputDir = Path.Combine(root, "parse");
if (!Directory.Exists(outputDir))
    Directory.CreateDirectory(outputDir);

OutputGenerator.Generate(personalTable, wazaLearn, outputDir);
Console.WriteLine("Output generated successfully.");