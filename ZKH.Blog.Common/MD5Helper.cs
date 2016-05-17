using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace ZKH.Blog.Common
{
    public class MD5Helper
    {
        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="str">加密字符串</param>
        /// <param name="code">加密位数</param>
        /// <returns></returns>
       public static string Get32bitMD5(String str, int code)
        {
           if(code == 16)
           {
               return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);
           }
           else
           {
               return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
           }
        }

       public static string Get32bitMD5(String str)
       {
           return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower();
       }
    }
}