using System.Collections.Generic;
using FamiliesWebAPI.Models;

namespace FamiliesWebAPI.Persistence
{
    public interface IFileContext
    {
        IList<Family> Families
        {
            get;
            set;
        }
        IList<Adult> Adults
        {
            get;
            set;
        }
        public void SaveChanges(); 
    }
}