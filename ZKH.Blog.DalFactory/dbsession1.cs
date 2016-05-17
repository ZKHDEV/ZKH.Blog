 
using ZKH.Blog.IDAL;

namespace ZKH.Blog.DalFactory
{
    public class DbSession : IDbSession
    {
	
        public IAdminInfoDal AdminInfoDal
        {
            get
            {
                return StaticDalFactory.GetAdminInfoDal();
            }
        }     
	
        public IAdminInfoExtDal AdminInfoExtDal
        {
            get
            {
                return StaticDalFactory.GetAdminInfoExtDal();
            }
        }     
	
        public IAdvertisingInfoDal AdvertisingInfoDal
        {
            get
            {
                return StaticDalFactory.GetAdvertisingInfoDal();
            }
        }     
	
        public IArticleInfoDal ArticleInfoDal
        {
            get
            {
                return StaticDalFactory.GetArticleInfoDal();
            }
        }     
	
        public IArticleTypeDal ArticleTypeDal
        {
            get
            {
                return StaticDalFactory.GetArticleTypeDal();
            }
        }     
	
        public ICommentInfoDal CommentInfoDal
        {
            get
            {
                return StaticDalFactory.GetCommentInfoDal();
            }
        }     
	
        public IMessageInfoDal MessageInfoDal
        {
            get
            {
                return StaticDalFactory.GetMessageInfoDal();
            }
        }     
	
        public IPhotoInfoDal PhotoInfoDal
        {
            get
            {
                return StaticDalFactory.GetPhotoInfoDal();
            }
        }     
	
        public IPhotoTypeDal PhotoTypeDal
        {
            get
            {
                return StaticDalFactory.GetPhotoTypeDal();
            }
        }     
	
        public IReplyInfoDal ReplyInfoDal
        {
            get
            {
                return StaticDalFactory.GetReplyInfoDal();
            }
        }     
	
        public IWebInfoDal WebInfoDal
        {
            get
            {
                return StaticDalFactory.GetWebInfoDal();
            }
        }     
	
     }
}        