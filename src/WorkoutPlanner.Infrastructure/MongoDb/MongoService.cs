using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WorkoutPlanner.Domain.Entities.Exercise;
using WorkoutPlanner.Domain.Entities.Workout;
using WorkoutPlanner.Infrastructure.MongoDb.Settings;

namespace WorkoutPlanner.Infrastructure.MongoDb
{
    public class MongoService
    {
        private readonly IMongoDatabase _database;

        public MongoService(IOptions<MongoDbSettings> options)
        {
            if (options is not null)
            {
                var settings = options.Value;
                Client = new MongoClient(settings.ConnectionString);
                _database = Client.GetDatabase(settings.DatabaseName);

            }

        }
        public MongoClient Client { get; }

        public IMongoCollection<ExerciseEntity> Exercises
            => _database.GetCollection<ExerciseEntity>("Exercises");

        public IMongoCollection<WorkoutPlanEntity> WorkoutPlans

            => _database.GetCollection<WorkoutPlanEntity>("WorkoutPlans");
    }

}