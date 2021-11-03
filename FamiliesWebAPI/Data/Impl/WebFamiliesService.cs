using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamiliesWebAPI.Models;
using FamiliesWebAPI.Persistence;

namespace FamiliesWebAPI.Data.Impl
{
    public class WebFamiliesService : IFamiliesService
    {
        private IFileContext fileContext;
        private IList<Family> Families { get; }

        public WebFamiliesService(IFileContext fileContext)
        {
            this.fileContext = fileContext;
            Families = new List<Family>();
            int maxId;
            foreach (var f in fileContext.Families)
            {
                if (!Families.Any())
                {
                    f.Id = 1;
                }
                else
                {
                    maxId = Families.Max(f => f.Id);
                    f.Id = (++maxId);
                }

                Families.Add(f);
            }
        }

        public async Task<IList<Family>> GetFamiliesAsync()
        {
            return Families;
        }

        public async Task<Family> AddFamilyAsync(Family family)
        {
            Families.Add(family);
            fileContext.Families = Families;
            fileContext.SaveChanges();
            return family;
        }

        public async Task RemoveFamilyAsync(int id)
        {
            Family toRemove = Families.First(f => f.Id == id);
            Families.Remove(toRemove);
            fileContext.Families = Families;
            fileContext.SaveChanges();
        }

        public async Task<Family> UpdateFamilyAsync(Family family)
        {
            Family toUpdate = Families.First(f => f.Id == family.Id);
            toUpdate.StreetName = family.StreetName;
            toUpdate.HouseNumber = family.HouseNumber;
            toUpdate.Adults = family.Adults;
            toUpdate.Children = family.Children;
            toUpdate.Pets = family.Pets;
            fileContext.Families = Families;
            fileContext.SaveChanges();
            return toUpdate;
        }

        public Family GetFamilyById(int? id)
        {
            return Families.FirstOrDefault(f => f.Id == id);
        }
    }
}