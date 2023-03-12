using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface IContentRep
    {


        public Content GetContent(string type);

        public Task<bool> AddContent(Content content);
        public Task<bool> UpdateContent(Content content);
    }
}
