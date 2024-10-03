using DemoAPIProject.Models.Domain;

namespace API.Repository.Interface
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetWalkAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null , bool isAsc = true,int pageNumber = 1,int pageSize = 3);

        Task<Walk?> GetWalkByIdAsync(Guid id);

        Task<int> AddWalkAsync(Walk walk);

        Task<int>  DeleteWalkAsync(Guid id);

        Task<Walk?> UpdateWalkAsync(Guid id,Walk walk);
    }
}
