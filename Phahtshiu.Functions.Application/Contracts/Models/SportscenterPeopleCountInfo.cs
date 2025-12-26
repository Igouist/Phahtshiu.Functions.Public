using System.Text.Json.Serialization;

namespace Phahtshiu.Functions.Application.Contracts.Models;

public class SportscenterLocationPeopleCountInfo
{
    [JsonPropertyName("LID")]
    public string Lid { get; set; }

    [JsonPropertyName("lidName")]
    public string LidName { get; set; }

    [JsonPropertyName("swPeopleNum")]
    public string SwimmingPeopleCount { get; set; }

    [JsonPropertyName("swMaxPeopleNum")]
    public string SwimmingMaxPeopleCount { get; set; }

    [JsonPropertyName("gymPeopleNum")]
    public string GymPeopleCount { get; set; }

    [JsonPropertyName("gymMaxPeopleNum")]
    public string GymMaxPeopleCount { get; set; }
}

public class SportscenterPeopleCountInfo
{
    [JsonPropertyName("locationPeopleNums")]
    public List<SportscenterLocationPeopleCountInfo> LocationPeopleCount { get; set; }
}
