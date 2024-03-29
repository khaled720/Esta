﻿using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Repository
{
    public class ContentRep : IContentRep
    {
        private readonly AppDbContext appContext;

        public ContentRep(AppDbContext appContext)
        {
            this.appContext = appContext;

        }
        public async Task<bool> AddContent(Content content)
        {
           await appContext.Contents.AddAsync(content);
            return true;
        }

        public async Task<Content> GetContent(string type)
        {
          return  await appContext.Contents.Where(y => y.Type == type).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateContent(Content content)
        {
            var dbContent = await appContext.Contents.Where(y=>y.Id==content.Id).FirstOrDefaultAsync();
            if (dbContent == null) return false;
            dbContent.DescriptionAr = content.DescriptionAr;
            dbContent.DescriptionEn=   content.DescriptionEn;


            return true;

        }
    }
}
