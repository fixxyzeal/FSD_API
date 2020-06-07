using Line.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLB
{
    public class LineMessageService : ILineMessageService
    {
        private readonly LineMessagingClient _lineMessagingClient;

        public LineMessageService()
        {
            _lineMessagingClient = new LineMessagingClient(Environment.GetEnvironmentVariable("LineChannelAccessToken"));
        }

        public async Task SendTextMessage(string to, string[] messages)
        {
            await _lineMessagingClient.PushMessageAsync(to, messages).ConfigureAwait(false);
        }

        public async Task<object> GetProfile(string userid)
        {
            return await _lineMessagingClient.GetUserProfileAsync(userid).ConfigureAwait(false);
        }
    }
}