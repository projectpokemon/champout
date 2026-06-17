using System.Text.Json.Serialization;

namespace champout;

// personal.json
public class Personal
{
    [JsonPropertyName("id")] public required string id { get; set; }

    [JsonPropertyName("no")] public required string no { get; set; }
    [JsonPropertyName("fo")] public required string fo { get; set; }

    [JsonPropertyName("ff")] public required string ff { get; set; }

    [JsonPropertyName("rr")] public required string rr { get; set; }

    [JsonPropertyName("ffge")] public required string ffge { get; set; }

    [JsonPropertyName("ms_name")] public required string ms_name { get; set; }
    [JsonPropertyName("ms_name_lbl")] public required string ms_name_lbl { get; set; }
    [JsonPropertyName("ms_form")] public required string ms_form { get; set; }
    [JsonPropertyName("ms_form_lbl")] public required string ms_form_lbl { get; set; }
    [JsonPropertyName("ms_form_mini")] public required string ms_form_mini { get; set; }
    [JsonPropertyName("ms_form_mini_lbl")] public required string ms_form_mini_lbl { get; set; }
    [JsonPropertyName("disp_form")] public required string disp_form { get; set; }
    [JsonPropertyName("poke_class")] public required string poke_class { get; set; }

    [JsonPropertyName("is_valid")] public required string is_valid { get; set; }

    [JsonPropertyName("reg_no")] public required string reg_no { get; set; }
    [JsonPropertyName("is_same")] public required string is_same { get; set; }
    [JsonPropertyName("cod")] public required string cod { get; set; }

    [JsonPropertyName("type1")] public required string type1 { get; set; }
    [JsonPropertyName("type2")] public required string type2 { get; set; }

    [JsonPropertyName("hp")] public required string HP { get; set; }
    [JsonPropertyName("atk")] public required string ATK { get; set; }
    [JsonPropertyName("def")] public required string DEF { get; set; }
    [JsonPropertyName("spatk")] public required string SpA { get; set; }
    [JsonPropertyName("spdef")] public required string SpD { get; set; }
    [JsonPropertyName("agi")] public required string SPE { get; set; }

    [JsonPropertyName("toku0")] public required string toku0 { get; set; }
    [JsonPropertyName("toku1")] public required string toku1 { get; set; }
    [JsonPropertyName("toku2")] public required string toku2 { get; set; }

    [JsonPropertyName("sex")] public required string sex { get; set; }
    [JsonPropertyName("weight")] public required string weight { get; set; }
    [JsonPropertyName("weight_ms")] public required string weight_ms { get; set; }
    [JsonPropertyName("weight_ms_lbl")] public required string weight_ms_lbl { get; set; }

    public ushort Species => ushort.Parse(no);
    public ushort Form => ushort.Parse(fo);
    public string StatLine => $"{HP}/{ATK}/{DEF}/{SpA}/{SpD}/{SPE}";

    public int Ability1 => int.Parse(toku0);
    public int Ability2 => int.Parse(toku1);
    public int AbilityH => int.Parse(toku2);

    public int Type1 => int.Parse(type1);
    public int Type2 => int.Parse(type2);
}
