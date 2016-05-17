 
using System.Configuration;
using System.Reflection;
using ZKH.Blog.DAL;
using ZKH.Blog.IDAL;

namespace ZKH.Blog.DalFactory
{
    public class StaticDalFactory
    {
        public readonly static string assemblyName = ConfigurationManager.AppSettings["DalAssemblyName"];

	
        public static IAdminInfoDal GetAdminInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".AdminInfoDal") as AdminInfoDal;
        }
	
        public static IAdminInfoExtDal GetAdminInfoExtDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".AdminInfoExtDal") as AdminInfoExtDal;
        }
	
        public static IAdvertisingInfoDal GetAdvertisingInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".AdvertisingInfoDal") as AdvertisingInfoDal;
        }
	
        public static IArticleInfoDal GetArticleInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".ArticleInfoDal") as ArticleInfoDal;
        }
	
        public static IArticleTypeDal GetArticleTypeDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".ArticleTypeDal") as ArticleTypeDal;
        }
	
        public static ICommentInfoDal GetCommentInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".CommentInfoDal") as CommentInfoDal;
        }
	
        public static IMessageInfoDal GetMessageInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".MessageInfoDal") as MessageInfoDal;
        }
	
        public static IPhotoInfoDal GetPhotoInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".PhotoInfoDal") as PhotoInfoDal;
        }
	
        public static IPhotoTypeDal GetPhotoTypeDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".PhotoTypeDal") as PhotoTypeDal;
        }
	
        public static IReplyInfoDal GetReplyInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".ReplyInfoDal") as ReplyInfoDal;
        }
	
        public static IWebInfoDal GetWebInfoDal()
        {
            return Assembly.Load(assemblyName).CreateInstance(assemblyName + ".WebInfoDal") as WebInfoDal;
        }
	
     }
}        