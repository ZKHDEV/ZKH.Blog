 

namespace ZKH.Blog.IDAL
{
    public interface IDbSession
    {
	     
    IAdminInfoDal AdminInfoDal { get; }
	     
    IAdminInfoExtDal AdminInfoExtDal { get; }
	     
    IAdvertisingInfoDal AdvertisingInfoDal { get; }
	     
    IArticleInfoDal ArticleInfoDal { get; }
	     
    IArticleTypeDal ArticleTypeDal { get; }
	     
    ICommentInfoDal CommentInfoDal { get; }
	     
    IMessageInfoDal MessageInfoDal { get; }
	     
    IPhotoInfoDal PhotoInfoDal { get; }
	     
    IPhotoTypeDal PhotoTypeDal { get; }
	     
    IReplyInfoDal ReplyInfoDal { get; }
	     
    IWebInfoDal WebInfoDal { get; }
	
     }
}