using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WorkPlanning.Infrastructure
{
    public static class HttpRequestExtensions
    {
        
        public static async Task<(T, CommandResult)> ValidateBody<T, TValidator>(this HttpRequest request)
            where T : class
            where TValidator : AbstractValidator<T>, new()
        {
            var subject = await DeserializeBodyPayload<T>(request);

            var validator = new TValidator();
            var validation = await validator.ValidateAsync(subject);
            
            var result = validation.IsValid 
                ? CommandResult.Success() 
                : CommandResult.Failure(validation.Errors.First().ErrorMessage);

            return (subject, result);
        }

		public static async Task<T> DeserializeBodyPayload<T>(this HttpRequest request) where T : class
        {
            using var reader = new StreamReader(request.Body);
            return JsonConvert.DeserializeObject<T>(await reader.ReadToEndAsync());
        }
    }
}
