namespace DiceServiceApplication.Domain.Models;

[Serializable]
public class HistoryEntry
{
    public HistoryEntry(string playerId, int rollResult)
    {
        PlayerId = playerId;
        RollResult = rollResult;
    }

    public string PlayerId { get; init; }

    public int RollResult { get; init; }
}