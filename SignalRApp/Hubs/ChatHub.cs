using Microsoft.AspNetCore.SignalR;
using SignalRApp.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace SignalRApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, EncrypResponse message)
        {

            if (message != null)
            {
                try
                {

                    //decrypt
                    string decrypetedMessage = Decrypt.String(message);

                    // looging message
                    Log.Message(user, decrypetedMessage);

                    //push the notification
                    await Clients.All.SendAsync("ReceiveMessage", user, decrypetedMessage);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        public async Task SendMP3(string user, byte[] message)
        {

            if (message != null)
            {
                try
                {

                    //decrypt
                    string decrypetedMessage = "";// Decrypt.String(message);

                    // looging chunk
                    Log.Chunk(decrypetedMessage);

                    //push the notification
                    //await Clients.All.SendAsync("ReceiveMessage", user, decrypetedMessage);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }

        }


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public override string ToString()
        {
            return base.ToString();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
    public class EncrypResponse
    {
        public byte[] encrypted { get; set; }
        public byte[] key { get; set; }
        public byte[] IV { get; set; }
    }
}
