using MongoDB.Driver;
using WorkoutPlanner.Domain.Entities.Workout;
using WorkoutPlanner.Domain.Interfaces;
using WorkoutPlanner.Infrastructure.MongoDb;

namespace WorkoutPlanner.Infrastructure.Database.Repositories
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly IMongoCollection<WorkoutPlanEntity> _col;

        public WorkoutRepository(MongoService mongo)
        {
            _col = mongo.WorkoutPlans;
        }

        public async Task<WorkoutPlanEntity> AddPlanAsync(WorkoutPlanEntity plan)
        {
            await _col.InsertOneAsync(plan);

            return plan;
        }

        public async Task<WorkoutPlanEntity> ReadPlanByIdAsync(Guid planId)
        {
            return await _col
                .Find(p => p.Id == planId)
                .FirstOrDefaultAsync();
        }

        public async Task<List<WorkoutPlanEntity>> ReadPlansByUserIdAsync(Guid userId)
        {
            return await _col
                .Find(p => p.UserId == userId)
                .ToListAsync();
        }
    }
}