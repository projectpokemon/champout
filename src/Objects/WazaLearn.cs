using System.Text.Json.Serialization;

namespace champout;

// waza_learn.json
public class WazaLearn
{
    [JsonPropertyName("id")]
    public required string Tag { get; set; }

    [JsonPropertyName("waza")]
    public required string MovesList { get; set; }

    public ushort Species => ushort.Parse(Tag[..4]);
    public ushort Form => ushort.Parse(Tag[4..]);

    public ushort[] Moves => Array.ConvertAll(MovesList.Split(','), ushort.Parse);
}
