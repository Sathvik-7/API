using API.Repository.Interface;
using DemoAPIProject.DataDbContext;
using DemoAPIProject.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;

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

        public async Task<List<Walk>> GetWalkAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAsc = true,int pageNumber=1,int pageSize=3)
        {
            #region Filtering
            var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            var propertyNames = typeof(Walk).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                .Select(p => p.Name)
                                .ToList();

            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                // Check if the filterOn is a valid property name
                if (propertyNames.Contains(filterOn, StringComparer.OrdinalIgnoreCase))
                {
                    // Get the property info
                    var property = typeof(Walk).GetProperty(filterOn, BindingFlags.Public | BindingFlags.Instance | 
                        BindingFlags.IgnoreCase);

                    // Create a parameter expression for the lambda
                    var parameter = Expression.Parameter(typeof(Walk), "x");

                    // Create the property access expression
                    var propertyAccess = Expression.Property(parameter, property);

                    // Create the contains method call expression
                    var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    var containsExpression = Expression.Call(propertyAccess, containsMethod, Expression.Constant(filterQuery));

                    // Create the lambda expression
                    var lambda = Expression.Lambda<Func<Walk, bool>>(containsExpression, parameter);

                    // Apply the filter
                    walks = walks.Where(lambda);
                }
            }

            #endregion

            #region Filtering based on Column name 
            //var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            //if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            //{
            //    if(filterOn.Equals("Name",StringComparison.OrdinalIgnoreCase))
            //    {
            //        walks = walks.Where(x => x.Name.Contains(filterQuery));
            //    }
            //}

            //return await walks.ToListAsync();
            #endregion

            #region Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                //if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                //    walks = isAsc ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);

                var parameter = Expression.Parameter(typeof(Walk), "x");
                var property = Expression.Property(parameter, sortBy);
                var lambda = Expression.Lambda(property, parameter);

                var orderByMethod = isAsc ? "OrderBy" : "OrderByDescending";
                var genericMethod = typeof(Queryable).GetMethods()
                    .First(m => m.Name == orderByMethod && m.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(Walk), property.Type);

                walks = (IQueryable<Walk>)genericMethod.Invoke(null, new object[] { walks, lambda });
            }
            #endregion

            //Pagination

            var skip = (pageNumber - 1) * pageSize;

            return await walks.Skip(skip).Take(pageSize).ToListAsync();

            //return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        }

        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            return await _dbContext.Walks.Include("Difficulty").Include("Region").Where(w => w.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walk)
        {
            var walksData = await _dbContext.Walks.Where(w => w.Id == id).FirstOrDefaultAsync();

            if (walksData == null)
                return null;

            walksData.Name = walk.Name;
            walksData.Description = walk.Description;
            walksData.LengthInKm = walk.LengthInKm;
            walksData.WalkImageUrl = walk.WalkImageUrl;

            await _dbContext.SaveChangesAsync();

            return walksData;
        }
    }
}
