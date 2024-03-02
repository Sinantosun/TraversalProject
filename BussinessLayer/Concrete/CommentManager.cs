using BussinessLayer.AbstractValidator;
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
            _commentDal.Delete(t);
        }

        public Comment TGetById(int id)
        {
            return _commentDal.GetById(id);
        }

        public List<Comment> TGetList()
        {
            return _commentDal.GetList();
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
            _commentDal.Update(t);
        }

        public List<Comment> TgetAllCommentWithDestination()
        {
            return _commentDal.getAllCommentWithDestination();
        }
    }
}
