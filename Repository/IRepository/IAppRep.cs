﻿using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface IUnitOfWork
    {


        ICoursesRep CoursesRep { get; }
        ILevelRep LevelRep { get; }
        IUserRep UserRep { get; }
        IDirectorRep DirectorRep { get; }
        IContentRep ContentRep { get; }
        IQuestionRep QuestionRep { get; }

        IContactRep ContactRep { get; }
        Task<bool> SaveChangesAsync();
    }
}
