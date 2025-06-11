using MongoDB.Driver;
using WorkoutPlanner.Domain.Entities.Exercise;
using WorkoutPlanner.Domain.Interfaces;
using WorkoutPlanner.Domain.Interfaces.ExternalServices;
using WorkoutPlanner.Infrastructure.MongoDb;

namespace WorkoutPlanner.Infrastructure.Database.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly IMongoCollection<ExerciseEntity> _col;
        private readonly IExerciseOpenAIRepository _openAIRepository;
        public ExerciseRepository(MongoService mongo, IExerciseOpenAIRepository openAIRepository)
        {
            _col = mongo.Exercises;
            _openAIRepository = openAIRepository ?? throw new ArgumentNullException(nameof(openAIRepository));
        }

        public async Task<ExerciseEntity> CreateExerciseAsync(ExerciseEntity exercise, CancellationToken cancellationToken)
        {
            await _col.InsertOneAsync(exercise, cancellationToken: cancellationToken);

            return exercise;
        }

        public async Task<ExerciseEntity> ReadExerciseByIdAsync(Guid id)
        {
            return await _col
                .Find(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<ExerciseEntity>> ReadAllExercisesync(int skip, int limit = 20)
        {
            return await _col
                .Find(_ => true)
                .Skip(skip)
                .Limit(limit)
                .ToListAsync();
        }

        public async Task<bool> ExistsExerciseAsync(Guid id)
        {
            return await _col
                .Find(e => e.Id == id)
                .AnyAsync();
        }
        public async Task DeleteExerciseAsync(Guid id)
        {
            await _col.DeleteOneAsync(e => e.Id == id);
        }
    }
}