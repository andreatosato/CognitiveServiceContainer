using CognitiveApp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CognitiveApp.Services
{
    public interface ISentimentService
    {
        Task<double> GetSingleSentimentAsync(DocumentSentimentRequest request);
        Task<List<DocumentSentimentResponse>> GetListSentimentAsync(List<DocumentSentimentRequest> request);
    }
}
