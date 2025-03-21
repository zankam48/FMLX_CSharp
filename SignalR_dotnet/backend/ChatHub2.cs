using  Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

public record User(string Name, string Room);
public record Message(string User, string Text);

public class AdvancedChatHub : Hub
{
    private static ConcurrentDictionary
}
