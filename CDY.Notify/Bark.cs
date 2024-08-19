using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
   ____ _                    _                  
  / ___| |__   ___ _ __   __| | __ _ _   _  ___ 
 | |   | '_ \ / _ \ '_ \ / _` |/ _` | | | |/ _ \
 | |___| | | |  __/ | | | (_| | (_| | |_| |  __/
  \____|_| |_|\___|_| |_|\__,_|\__,_|\__, |\___|
                                     |___/      
*/
namespace CDY.Notify
{
    /// <summary>
    /// Bark 推送
    /// </summary>
    public class Bark
    {
        List<RestClient> Clients = new List<RestClient>();
        /// <summary>
        /// 初始化
        /// </summary>
        /// <remarks>
        /// 可同时推送多台设备
        /// </remarks>
        /// <param name="deviceTokens">设备token</param>
        public Bark(params string[] deviceTokens)
        {
            foreach (var item in deviceTokens)
            {
                Clients.Add(new RestClient("https://api.day.app/" + item));
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="isArchive">是否自动保存消息</param>
        /// <returns></returns>
        public void Send(string title, string content, bool isArchive = false)
        {
            if (Clients.Count == 0)
            {
                throw new Exception("Bark未初始化");
            }
            var request = new RestRequest($"/{title}/{content}");
            if (isArchive)
            {
                request.AddQueryParameter("archive", "1");
            }
            request.Method = Method.Get;
            Execute(request);
        }
        /// <summary>
        /// 发送消息，带图标
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="icon">图标地址</param>
        /// <param name="isArchive">是否自动保存消息</param>
        /// <returns></returns>
        public void Send(string title, string content, string icon, bool isArchive = false)
        {
            if (Clients.Count == 0)
            {
                throw new Exception("Bark未初始化");
            }
            var request = new RestRequest($"/{title}/{content}");
            request.AddQueryParameter("icon", icon);
            if (isArchive)
            {
                request.AddQueryParameter("archive", "1");
            }
            request.Method = Method.Get;
            Execute(request);
        }
        void Execute(RestRequest request)
        {
            foreach (var client in Clients)
            {
                _ = client.ExecuteAsync(request);
            }
        }
    }
}
