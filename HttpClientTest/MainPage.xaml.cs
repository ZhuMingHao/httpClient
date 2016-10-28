using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace HttpClientTest
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {  //https://api.weibo.com/oauth2/authorize?client_id=YOUR_CLIENT_ID&response_type=code&redirect_uri=YOUR_REGISTERED_REDIRECT_URI

        static string Key = "4017369158";
        static string Secret = "fb016a546a460d60e7fe8d6bb0929fff";
        static string redirect = "http://www.baidu.com";
        string code;
        public MainPage()
        {   
            this.InitializeComponent();
            string url = string.Format("https://api.weibo.com/oauth2/authorize?client_id={0}&response_type=code&redirect_uri={1}",Key,"http://www.baidu.com");
            MyWebView.Source =new Uri(url);
        }

        private  void WebView_LoadCompleted(object sender, NavigationEventArgs e)
        {
            string temp = e.Uri.ToString();
            Debug.Write(temp);
            string[] strArr =temp.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);
            code = strArr.Last<string>();
            if(code != null)
            {
                HttpManager manager = new HttpManager();
                string two = string.Format("https://api.weibo.com/oauth2/access_token?client_id={0}&client_secret={1}&grant_type=authorization_code&redirect_uri={2}&code={3}", Key, Secret, redirect, code
   );
               //manager.SendGetMethod("http://pic33.nipic.com/20130916/3420027_192919547000_2.jpg", (response) =>
               // {
               //     Debug.Write(response);
               // });
               
            }
           
        }
       
    }
}
