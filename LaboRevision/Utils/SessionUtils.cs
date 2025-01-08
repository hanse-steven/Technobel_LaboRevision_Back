using Microsoft.AspNetCore.SignalR;

namespace LaboRevision.Utils;

public static class SessionUtils
{
    // public static Guid GetSession(HubCallerContext ctx)
    // {
    //     return Guid.Parse(ctx.GetHttpContext()!.Request.Query["session"]!);
    // }
    
    public static Guid GetSession(this HubCallerContext ctx)
    {
        return Guid.Parse(ctx.GetHttpContext()!.Request.Query["session"]!);
    }
}