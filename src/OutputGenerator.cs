using PKHeX.Core;

namespace champout;

public static class OutputGenerator
{
    public static void Generate(Personal[] personalTable, WazaLearn[] wazaLearn, string outputDir)
    {
        var table = personalTable.ToDictionary(info => (info.Species, info.Form));
        var moves = wazaLearn.ToDictionary(info => (info.Species, info.Form));

        GeneratePersonalDump(outputDir, table, moves);
        GenerateMoveAvailability(outputDir, moves);
        GenerateSpeciesWithMove(outputDir, table, moves);
    }

    private static void GenerateSpeciesWithMove(string outputDir,
        Dictionary<(ushort Species, ushort Form), Personal> table,
        Dictionary<(ushort Species, ushort Form), WazaLearn> species)
    {
        // for each move, list the species that can learn it
        Dictionary<ushort, List<(ushort Species, ushort Form)>> moveToSpecies = new();
        foreach (var kvp in species)
        {
            var key = kvp.Key;
            var learnset = kvp.Value;
            foreach (var move in learnset.Moves)
            {
                if (!moveToSpecies.ContainsKey(move))
                    moveToSpecies[move] = [];
                moveToSpecies[move].Add(key);
            }
        }

        var file = Path.Combine(outputDir, "species_with_move.txt");
        using var writer = new StreamWriter(file);
        foreach (var kvp in moveToSpecies)
        {
            var move = kvp.Key;
            var speciesList = kvp.Value;
            

            writer.WriteLine($"Move: {GameInfo.Strings.Move[move]}");
            writer.WriteLine("Species that can learn this move:");
            foreach (var sf in speciesList)
            {
                var pi = table.GetValueOrDefault(sf);
                if (pi is null)
                    continue; // shouldn't happen

                if (pi.is_valid != "1")
                    continue;

                var name = GetName(sf);
                writer.WriteLine($"- {name}");
            }
            writer.WriteLine();
        }
    }

    private static void GenerateMoveAvailability(string outputDir,
        Dictionary<(ushort Species, ushort Form), WazaLearn> moves)
    {
        // get a list of moves
        var possible = new HashSet<ushort>();
        foreach (var kvp in moves)
        {
            var learnset = kvp.Value;
            foreach (var move in learnset.Moves)
                possible.Add(move);
        }
        var file = Path.Combine(outputDir, "move_availability.txt");
        using var writer = new StreamWriter(file);
        foreach (var move in possible.Order())
            writer.WriteLine($"{move:000}\t{GameInfo.Strings.Move[move]}");
    }

    private static void GeneratePersonalDump(string outputDir,
        Dictionary<(ushort Species, ushort Form), Personal> table,
        Dictionary<(ushort Species, ushort Form), WazaLearn> waza)
    {
        var file = Path.Combine(outputDir, "personal_dump.txt");
        using var writer = new StreamWriter(file);

        // Write the species, base stats, types, abilities, and move learnset for each entry in the personal table
        var ordered = table.OrderBy(z => z.Key.Species).ThenBy((z => z.Key.Form));
        foreach (var (key, pi) in ordered)
        {
            writer.WriteLine($"{key.Species:0000} - {GetName(key)}");
            writer.WriteLine($"{pi.StatLine}");

            writer.Write($"{GameInfo.Strings.Types[pi.Type1]}");
            if (pi.Type2 != 0)
                writer.Write($"/{GameInfo.Strings.Types[pi.Type2]}");
            writer.WriteLine();

            writer.WriteLine($"{GetAbility(pi.Ability1)}/{GetAbility(pi.Ability2)}/{GetAbility(pi.AbilityH)}");

            writer.WriteLine("Moves:");
            var moves = waza.GetValueOrDefault(key)?.Moves ?? [];
            foreach (var move in moves)
                writer.WriteLine($"- {GameInfo.Strings.Move[move]}");

            // space for next
            writer.WriteLine();
        }
    }

    private static string GetAbility(int index)
    {
        var arr = GameInfo.Strings.Ability;
        if (index >= arr.Count)
            return $"{index:000}";
        return arr[index];
    }

    private static string GetName((ushort Species, ushort Form) sf)
    {
        var speciesName = GameInfo.Strings.Species[sf.Species];
        if (sf.Form == 0)
            return speciesName;
        return $"{speciesName}-{GetForm(sf.Species, sf.Form)}";
    }

    private static string GetForm(ushort species, ushort form)
    {
        var forms = FormConverter.GetFormList(species, GameInfo.Strings.types, GameInfo.Strings.forms, ["M", "F", "-"], EntityContext.Gen9a);
        if (form >= forms.Length)
            return $"{form:000}";
        return forms[form];
    }
}