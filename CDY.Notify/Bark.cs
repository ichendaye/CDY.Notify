﻿using RestSharp;
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
        RestClient Client = null;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="key">密钥</param>
        public Bark(string key)
        {
            Client = new RestClient("https://api.day.app/" + key);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <returns></returns>
        public async Task SendAsync(string title, string content)
        {
            if (Client == null)
            {
                throw new Exception("Bark未初始化");
            }
            var request = new RestRequest($"/{title}/{content}");
            request.Method = Method.Get;
            await Client.ExecuteAsync(request);
        }
        /// <summary>
        /// 发送消息，带图标
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="icon">图标地址</param>
        /// <returns></returns>
        public async Task SendAsync(string title, string content, string icon)
        {
            if (Client == null)
            {
                throw new Exception("Bark未初始化");
            }
            var request = new RestRequest($"/{title}/{content}");
            request.AddQueryParameter("icon", icon);
            request.Method = Method.Get;
            await Client.ExecuteAsync(request);
        }
    }
}