﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using WeChatEntities;
using WeChatHelper.Interface;
using WeChatHelper.Response;

namespace WeChatHelper.Request
{
   public class GetMaterialListRequest:RequestBase<GetMaterialListResponse>,IMpRequest<GetMaterialListResponse>
   {
        public string Method => "POST";

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
        public AppIdInfo AppIdInfo { get; set; }

        /// <summary>
        /// 获取Api请求地址
        /// </summary>
        /// <returns></returns>
        public string GetReqUrl()
        {
            const string urlFormat = "https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}";
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

        public GetMaterialListResponse ParseHtmlToResponse(string body)
        {
            var response = new GetMaterialListResponse { Body = body };

            if (response.HasError())
            {
                response.ErrInfo = response.GetErrInfo();
            }
            else
            {
                var jsonSerializer = new JavaScriptSerializer();
                var mg = jsonSerializer.Deserialize<CreativeManagement>(body);
                response.CreativeManagement = mg;

            }
            return response;
        }
    }

}
