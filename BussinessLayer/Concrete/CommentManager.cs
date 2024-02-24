using BussinessLayer.Abstract;
using DataAccsesLayer.Abstract;
using EntityLayer.Concrete;
using System.Linq.Expressions;

namespace BussinessLayer.Concrete
{
    public class CommentManager : ICommentService
    {
        private readonly ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        public void TDelete(Comment t)
        {
            throw new NotImplementedException();
        }

        public Comment TGetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Comment> TGetList()
        {
            throw new NotImplementedException();
        }

        public List<Comment> TGetDestinationByID(int id)
        {
            return _commentDal.GetListByFilter(x => x.DestinationID == id);
        }

        public void TInsert(Comment t)
        {
            _commentDal.Insert(t);
            
        }

        public void TUpdate(Comment t)
        {
            throw new NotImplementedException();
        }
    }
}
