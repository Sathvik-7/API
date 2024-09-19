using API.Repository.Interface;
using DemoAPIProject.DataDbContext;
using DemoAPIProject.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Repository.Implementation
{
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly WalksDbContext _dbContext;

        public SQLWalkRepository(WalksDbContext _dbContext) 
        {
            this._dbContext = _dbContext;
        }

        public async Task<int> AddWalkAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteWalkAsync(Guid id)
        {
            await _dbContext.Walks.Where(w => w.Id == id).FirstOrDefaultAsync();
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Walk>> GetWalkAsync()
        {
            return await _dbContext.Walks.ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            return await _dbContext.Walks.Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int?> UpdateWalkAsync(Guid id, Walk walk)
        {
            var walksData = await _dbContext.Walks.Where(w => w.Id == id).FirstOrDefaultAsync();

            if (walksData == null)
                return 0;

            walksData.Name = walk.Name;
            walksData.Description = walk.Description;
            walksData.LengthInKm = walk.LengthInKm;
            walksData.WalkImageUrl = walk.WalkImageUrl;

            await _dbContext.SaveChangesAsync();
        }
    }
}
