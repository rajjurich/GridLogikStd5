using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;


namespace GridLogikViewer.Areas.ABTScreen
{
    public class MessageList
    {
        public string Msg_Text { get; set; }
        public string Msg_Type { get; set; }
    }
    public static class MessageRepository
    {
        static string url = WebConfigurationManager.AppSettings["APIUrl"];
        public static MessageList GetMessage(string MessageId, string ErrorParam)
        {
            MessageList objMsg = new MessageList();
            try
            {
                string Jsonstr = "";
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    Jsonstr = client.DownloadString(url + "exception/get/" + MessageId + "/" + "null");
                    dynamic dynamicMsg = JValue.Parse(Jsonstr);
                    
                    objMsg.Msg_Text = dynamicMsg.Data.d;
                    objMsg.Msg_Type = dynamicMsg.Data.e;
                    return objMsg;

                }
            }
            catch (Exception)
            {
                return objMsg;
            }
        }


    }
}