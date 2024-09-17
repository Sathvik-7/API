using API.Models.DTO;
using DemoAPIProject.Models.Domain;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace API.Repository.Interface
{
    public interface IRegionRepository
    {
       Task<List<Region>> GetAllData();

        Task<Region?> GetById(Guid id);

        Task<int> CreateRegions(Region region);

        Task<int> UpdateRegions(Guid id, Region region);

        Task<int> DeleteRegions(Guid id);
    }
}
