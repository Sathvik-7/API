using DemoAPIProject.Models.Domain;

namespace API.Repository.Interface
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetWalkAsync();

        Task<Walk?> GetWalkByIdAsync(Guid id);

        Task<int> AddWalkAsync(Walk walk);

        Task<int>  DeleteWalkAsync(Guid id);

        Task<Walk?> UpdateWalkAsync(Guid id,Walk walk);
    }
}
