﻿using System.Collections.Generic;
using System.Web.Script.Serialization;
using WeChatEntities;
using WeChatHelper.Interface;
using WeChatHelper.Response;

namespace WeChatHelper.Request
{
    /// <summary>
    /// 菜单查询请求
    /// </summary>
    public class GetMenuRequest : RequestBase<GetMenuResponse>, IMpRequest<GetMenuResponse>
    {
        public string Method => "GET";

        /// <summary>
        /// 需要POST发送的数据
        /// </summary>
        public string SendData { get; set; }

        /// <summary>
        /// 调用接口凭证 
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// AppId信息
        /// </summary>
        public AppIdInfo AppIdInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 获取Api请求地址
        /// </summary>
        /// <returns></returns>
        public string GetReqUrl()
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}";
            var url = string.Format(urlFormat, AccessToken);
            return url;
        }

        public IDictionary<string, string> GetParameters()
        {
            return new Dictionary<string, string>();
        }

        public void Validate()
        {

        }

        public GetMenuResponse ParseHtmlToResponse(string body)
        {
            var response = new GetMenuResponse {Body = body};

            if (response.HasError())
            {
                response.ErrInfo = response.GetErrInfo();
            }
            else
            {
                var jsonSerializer = new JavaScriptSerializer();
                var mg = jsonSerializer.Deserialize<MenuGroup>(response.Body);
                response.Menu = mg;
            }
            return response;
        }
    }
}
