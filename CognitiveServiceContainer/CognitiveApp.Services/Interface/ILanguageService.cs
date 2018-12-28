using CognitiveApp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CognitiveApp.Services
{
    public interface ILanguageService
    {
        Task<DetectedLanguage> GetSingleLanguageAsync(DocumentLanguageRequest request);
        Task<IEnumerable<DocumentLanguageResponse>> GetListLanguageAsync(List<DocumentLanguageRequest> request);
    }
}
