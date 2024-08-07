using System.Net.Http;
using System.Text.Json;

namespace AssemblyAI.Core;

#nullable enable

public partial class RawClient
{
    private partial async Task OnResponseErrorAsync(HttpResponseMessage response){
        var responseContentString = await response.Content.ReadAsStringAsync()
            .ConfigureAwait(false);
        if (string.IsNullOrEmpty(responseContentString))
        {
            throw new HttpOperationException(
                $"Error with status code {response.StatusCode}", 
                response.StatusCode, 
                responseContentString
            ); 
        }

        try
        {
            var error = JsonUtils.Deserialize<AssemblyAI.Error>(responseContentString);
            throw new HttpOperationException(
                error.Error_, 
                response.StatusCode, 
                responseContentString
            );
        }
        catch (JsonException)
        {
        }
        
        // use response content as error message if error object cannot be deserialized
        throw new HttpOperationException(
            responseContentString, 
            response.StatusCode, 
            responseContentString
        );
    }
}