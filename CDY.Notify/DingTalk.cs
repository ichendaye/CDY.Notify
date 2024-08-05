using RestSharp;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace CDY.Notify
{
    /// <summary>
    /// 钉钉通知
    /// </summary>
    public class DingTalk
    {
        RestClient client = null;
        string AppName = string.Empty;
        string Secret = string.Empty;

        /// <summary>
        /// 初始化钉钉通知
        /// </summary>
        /// <param name="appName">应用名称</param>
        /// <param name="webhook">webhook地址</param>
        /// <param name="secret">密钥</param>
        public DingTalk(string appName, string webhook, string secret)
        {
            client = new RestClient(webhook);
            AppName = appName;
            Secret = secret;
        }

        /// <summary>
        /// 发送文本消息
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="isAtAll">是否@所有人</param>
        public async Task SendTextMsgAsync(string content, bool isAtAll = false)
        {
            if (client == null)
            {
                throw new Exception("钉钉通知未初始化");
            }

            var body = new
            {
                msgtype = "text",
                text = new
                {
                    content = $"【{AppName}】\r\n{content}" 
                },
                at = new { isAtAll }
            };

            RestRequest request = new RestRequest();
            request.AddBody(body);
            var timestamp = GenerateTimestamp();
            request.AddQueryParameter("timestamp", timestamp);
            request.AddQueryParameter("sign", GenerateSignature(timestamp));

            try
            {
                // 等待异步操作完成，并处理可能的异常
                var jg=await client.PostAsync(request);
            }
            catch (Exception ex)
            {
                // 处理发送消息时可能出现的异常，例如网络错误等
                Console.WriteLine($"发送消息失败: {ex.Message}");
                // 可以根据需要抛出异常或进行其他错误处理
                throw; // 或者用其他方式处理异常，如记录日志等
            }
        }

        /// <summary>
        /// 发送异常消息
        /// </summary>
        /// <param name="exception">异常消息</param>
        /// <param name="isAtAll">是否@所有人</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SendErrorMsgAsync(Exception exception, bool isAtAll = false)
        {
            if (client == null)
            {
                throw new Exception("钉钉通知未初始化");
            }
            var body = new
            {
                msgtype = "markdown",
                markdown = new
                {
                    title = $"【{AppName}】",
                    text = $"# {AppName}异常信息\r\n```\r\n{exception.Message}\r\n```\r\n# 堆栈信息\r\n```\r\n{exception.StackTrace}\r\n```"
                },
                at = new
                {
                    isAtAll
                }
            };
            RestRequest request = new RestRequest();
            request.AddBody(body);
            var timestamp = GenerateTimestamp();
            request.AddQueryParameter("timestamp", timestamp);
            request.AddQueryParameter("sign", GenerateSignature(timestamp));
            try
            {
                await client.PostAsync(request); // 等待异步操作完成，并处理可能的异常
            }
            catch (Exception ex)
            {
                // 记录或处理发送通知时发生的异常，例如日志记录等。
                Console.WriteLine($"发送钉钉通知失败: {ex.Message}");
                // 可以根据需要重新抛出异常或进行其他处理。
                throw; // 或者用其他方式处理异常，如记录日志等
            }
        }

        /// <summary>
        /// 生成时间戳
        /// </summary>
        /// <returns></returns>
        private long GenerateTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <returns></returns>
        private string GenerateSignature(long timestamp)
        {
            // 将时间戳和密钥组合成签名字符串
            string signString = $"{timestamp}\n{Secret}";

            // 使用 HMACSHA256 算法生成签名
            using (var hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(Secret)))
            {
                byte[] hashBytes = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(signString));

                // 将签名结果进行Base64编码
                string base64Signature = Convert.ToBase64String(hashBytes);

                // 对Base64编码后的签名进行URL编码
                string urlEncodedSignature = HttpUtility.UrlEncode(base64Signature);

                return urlEncodedSignature;
            }
        }
    }
}