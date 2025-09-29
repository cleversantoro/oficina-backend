using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oficina.Messaging;

public interface IClientMessagingService
{
    Task SendMessage(ClientMessage message);
    IEnumerable<ClientMessage> GetMessages(string clientId);
}