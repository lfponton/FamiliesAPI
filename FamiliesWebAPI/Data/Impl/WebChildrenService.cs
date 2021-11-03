using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamiliesWebAPI.Models;

namespace FamiliesWebAPI.Data.Impl
{
    public class WebChildrenService : IChildrenService
    {
        public IList<Child> Children { get; private set; }
        private readonly IFamiliesService familiesService;

        public WebChildrenService(IFamiliesService familiesService)
        {
            this.familiesService = familiesService;
            GetChildrenAsync();
        }
        
        public async Task<IList<Child>> GetChildrenAsync()
        {
            Children = new List<Child>();
            foreach (var family in await familiesService.GetFamiliesAsync())
            {
                foreach (var child in family.Children)
                {
                    Children.Add(child);
                }
            }
            return Children;
        }

        public async Task<Child> AddChildAsync(int familyId, Child child)
        {
            Children.Add(child);
            Family family = familiesService.GetFamilyById(familyId);
            family.Children.Add(child);
            await familiesService.UpdateFamilyAsync(family);
            return child;
        }

        public async Task RemoveChildAsync(int familyId, int id)
        {
            Child toRemove = Children.First(c => c.Id == id);
            Children.Remove(toRemove);
            Family family = familiesService.GetFamilyById(familyId);
            family.Children.Remove(toRemove);
            await familiesService.UpdateFamilyAsync(family);
        }

        public async Task<Child> UpdateChildAsync(int familyId, Child child)
        {
            Child toUpdate = Children.First(c => c.Id == child.Id);
            toUpdate.Interests = child.Interests;
            toUpdate.Pets = child.Pets;
            toUpdate.Age = child.Age;
            toUpdate.Height = child.Height;
            toUpdate.Sex = child.Sex;
            toUpdate.Weight = child.Weight;
            toUpdate.EyeColor = child.EyeColor;
            toUpdate.HairColor = child.HairColor;
            toUpdate.FirstName = child.FirstName;
            toUpdate.LastName = child.LastName;
            familiesService.UpdateFamilyAsync(familiesService.GetFamilyById(familyId));
            return toUpdate;
        }
        
        public IList<Child> GetFamilyChildren(int? familyId)
        {
            IList<Child> children = new List<Child>();
            foreach (var c in familiesService.GetFamilyById(familyId).Children)
            {
                children.Add(c);
            }
            return children;
        }
        
        /*
        public Child GetChild(int? id)
        {
            return Children.FirstOrDefault(c => c.Id == id);
        }

        private Family FindFamily(Child child)
        {
            Family familyToUpdate = null;
            foreach (var family in familiesService.GetFamilies())
            {
                if (family.Children.Any(c => c.Id == child.Id))
                {
                    familyToUpdate = family;
                    break;
                }
            }
            return familyToUpdate;
        }
        */
    }
}