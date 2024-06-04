using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MovieRecommendationSystem.Application.Interfaces;
using MovieRecommendationSystem.Domain.Common;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieRecommendationSystem.Application.Services
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly IMongoCollection<T> _dbContext;
        private readonly IConfiguration _config;

        public Repository(IOptions<T> config, IConfiguration configuration)
        {
            _config = configuration;
            var mongoClient = new MongoClient(_config.GetConnectionString("DefaultConnection"));
            var mongoDatabase = mongoClient.GetDatabase("MovieRecommendationSystem");
            _dbContext = mongoDatabase.GetCollection<T>(config.Value.ToString());
        }

        public async Task Create(T entity)
        {
            await _dbContext.InsertOneAsync(entity);
        }
        public async Task CreateAll(List<T> entities)
        {
            await _dbContext.InsertManyAsync(entities);
        }

        public async Task<T> Find(Expression<Func<T, bool>> expression)
        {
            var filter = Builders<T>.Filter.Where(expression);
            var result = await _dbContext.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbContext.Find(u => true).ToListAsync();
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> expression)
        {
            var filter = Builders<T>.Filter.Where(expression);
            var result = await _dbContext.Find(filter).ToListAsync();
            return result;
        }
        public async Task<List<T>> GetAllTake(Expression<Func<T, bool>> expression, int Take, int PageNum)
        {
            var startedData = (PageNum - 1) * Take;

            var filter = Builders<T>.Filter.Where(expression);
            return await _dbContext.AsQueryable()
                                    .Where(expression)
                                    .Skip(startedData)
                                    .Take(Take)
                                    .ToListAsync();
        }


        public async Task Update(T entity)
        {
            await _dbContext.ReplaceOneAsync(_ => _.Id == entity.Id, entity);
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            var filter = Builders<T>.Filter.Where(expression);
            var count = await _dbContext.CountDocumentsAsync(filter);
            return count > 0;
        }
        public async Task<int> Delete(Expression<Func<T, bool>> entity)
        {
            var filter = Builders<T>.Filter.Where(entity);
            var result = _dbContext.FindOneAndDelete(filter);
            return 1;
        }
    }
}
