using HttpClientCognitive.Models;
using Refit;
using System.Threading.Tasks;

namespace HttpClientCognitive.Models
{
    public interface ICognitiveClient
    {
        [Post("/text/analytics/v2.0/languages")]
        Task<LanguageResponse> GetListLanguageAsync([Body(BodySerializationMethod.Serialized, buffered: false)]LanguageRequest request);
    }
}
