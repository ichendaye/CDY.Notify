using RestSharp;

namespace CDY.Notify
{
    /// <summary>
    /// 钉钉通知
    /// </summary>
    public class DingTalk
    {
        static RestClient client;

        /// <summary>
        /// 初始化钉钉通知
        /// </summary>
        /// <param name="webhook">webhook地址</param>
        public DingTalk(string webhook)
        {
            client = new RestClient(webhook);
        }

        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="isAtAll">是否@所有人</param>
        public void SendTextMsg(string content, bool isAtAll = false)
        {
            var body = new
            {
                msgtype = "text",
                text = new
                {
                    content
                },
                at=new
                {
                    isAtAll
                }
            };
            RestRequest request = new RestRequest();
            request.AddBody(body);
            _ = client.PostAsync(request);
        }
    }
}