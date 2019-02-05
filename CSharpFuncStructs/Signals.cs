using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using System.Collections.Concurrent;

namespace CSharpFuncStructs
{
    class Signals : Hub
    {
        private static ConcurrentDictionary<Guid, string> onlineUsers =
            new ConcurrentDictionary<Guid, string>();

        public override Task OnConnected()
        {
            var connId = new Guid(Context.ConnectionId);
            var user = Context.User;

            var name = user.Identity.Name;
            if (onlineUsers.TryAdd(connId, name))
            {
                RegisterUserConnection(connId, name);
            }
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var connId = new Guid(Context.ConnectionId);
            string name;
            if (onlineUsers.TryRemove(connId, out name))
            {
                DeregisterUserConnection(connId, name);
            }
            return base.OnDisconnected(stopCalled);
        }

        private void RegisterUserConnection(Guid connId, string name) { }
        private void DeregisterUserConnection(Guid connId, string name) { }
    }
}
