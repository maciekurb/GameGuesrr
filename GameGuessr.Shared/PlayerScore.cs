namespace GameGuessr.Shared;

public class PlayerScore
{
    public string Name { get; set; }
    public byte[] Password { get; set; }
    public int Score { get; set; }
    public GameMode Mode { get; set; }
    public byte[] Sh { get; set; }
}
