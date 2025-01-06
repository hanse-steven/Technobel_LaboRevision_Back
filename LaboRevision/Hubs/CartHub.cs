using LaboRevision.DTO;
using Microsoft.AspNetCore.SignalR;

namespace LaboRevision.Hubs;

public class CartHub : Hub
{
    public async Task CartUpdate(IEnumerable<CartItemDTO> items)
    {
        await Clients.All.SendAsync("CartUpdated", items);
    }
}