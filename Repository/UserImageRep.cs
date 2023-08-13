using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Repository
{
    public class UserImageRep : IUserImageRep
    {
        private readonly AppDbContext dbContext;

        public UserImageRep( AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public Task<bool> AddImage(UserImage image)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddImages(List<UserImage> images)
        {
            try
            {
                await dbContext.AddRangeAsync(images);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        



        }

        public async Task<List<UserImage>> GetUserDocsImages(string userId)
        {
            var Images = new List<UserImage>();
            Images = await dbContext.UserImages.Where(x => x.UserId == userId).ToListAsync();

            return Images;
        }

        public async Task<List<UserImage>> GetUserGraduationImages(string userId)
        {
            var Images = new List<UserImage>();
            Images = await dbContext.UserImages.Where(x => x.UserId == userId && x.TypeId == 3).ToListAsync();

            return Images;
        }

        public async Task<List<UserImage>> GetUserNationalIdImages(string userId)
        {
            var Images = new List<UserImage>();
         Images=await dbContext.UserImages.Where(x => x.UserId == userId&& x.TypeId==1).ToListAsync();

            return Images;  
        }

        public async Task<List<UserImage>> GetUserPassportImages(string userId)
        {
            var Images = new List<UserImage>();
            Images = await dbContext.UserImages.Where(x => x.UserId == userId && x.TypeId ==2).ToListAsync();

            return Images;
        }

        public Task<bool> RemoveImage(int ImageId)
        {
            throw new NotImplementedException();
        }
    }
}
