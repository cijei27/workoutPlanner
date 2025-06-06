using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkoutPlanner.Domain.Interfaces.ExternalServices
{
    public interface IKernelOpenAIRepository
    {
        Task<string> HandleVoiceCommandToText(Stream audioStream, CancellationToken cancellationToken = default);
        Task<string> NormalRequest(string prompt, CancellationToken cancellationToken = default);
    }
}