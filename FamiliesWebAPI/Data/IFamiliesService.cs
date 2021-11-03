using System.Collections.Generic;
using System.Threading.Tasks;
using FamiliesWebAPI.Models;

namespace FamiliesWebAPI.Data
{
    public interface IFamiliesService
    {
        
        Task<IList<Family>> GetFamiliesAsync();
        Task<Family> AddFamilyAsync(Family family);
        Task RemoveFamilyAsync(int id);
        Task<Family> UpdateFamilyAsync(Family family);

        Family GetFamilyById(int? id);
    }
}