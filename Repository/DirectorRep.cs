﻿using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Repository
{
    public class DirectorRep : IDirectorRep
    {

        private readonly AppDbContext appContext;

        public DirectorRep(AppDbContext appContext)
        {
            this.appContext = appContext;
        }
        public async Task<bool> AddDirector(Director director)
        {
            try
            {
      await appContext.Directors.AddAsync(director);
            return true;
            }
            catch (Exception)
            {
                return false;
            }
      
        }

        public bool DeleteDirector(int id)
        {
            try
            {
      appContext.Directors.Remove(appContext.Directors.First(t => t.Id == id));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
       }

        public async Task<bool> EditDirector(Director director)
        {
            try
            {
   var dbDirector = await appContext.Directors.AsTracking().FirstAsync(y => y.Id == director.Id);
            dbDirector.NameEn = director.NameEn;
            dbDirector.NameAr = director.NameAr;
            dbDirector.JobEn=director.JobEn;
            dbDirector.JobAr = director.JobAr;  
            dbDirector.PhotoPath=director.PhotoPath;
                dbDirector.BioEn=director.BioEn;
                dbDirector.BioAr=director.BioAr;
                return true;
            }
            catch (Exception)
            {
                return false;

            }

        }

        public async Task<IEnumerable<Director>> GetAllDirectors()
        {
            try
            {
var directors= await appContext.Directors.ToListAsync();
                return directors;
            }
            catch (Exception ex)
            {

                return new List<Director>();
            }
           
        }

        public async Task<Director> GetDirector(int id)
        {
            try
            {
            return  await appContext.Directors.AsNoTracking().FirstAsync(y=>y.Id==id);
          
            }
            catch (Exception)
            {
                return new Director();
            }
        }
    }
}
