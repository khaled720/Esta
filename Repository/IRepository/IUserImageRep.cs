using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface IUserImageRep
    {


        public Task<bool> AddImage(UserImage image);
        public Task<bool> AddImages(List<UserImage> images);


        public Task<bool> RemoveImage(int ImageId);

        public Task<List<UserImage>> GetUserNationalIdImages(string userId);
        public Task<List<UserImage>> GetUserPassportImages(string userId);

        public Task<List<UserImage>>  GetUserGraduationImages(string userId);

        public Task<List<UserImage>> GetUserDocsImages(string userId);






    }
}
