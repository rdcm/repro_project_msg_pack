using MessagePack;

[MessagePackObject()]
public class JoinEvent
{
    [Key(0)]
    public int RoomId { get; set; }
    
    [Key(1)]
    public string Nickname { get; set; }
}
