using Application.Common;
using FluentValidation;
using MediatR;

namespace Application.Behavior
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors)
                                                .Where(f => f != null)
                                                .ToList();

                if (failures.Count > 0)
                {
                    if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(ApiResult<>))
                    {
                        var errors = failures.Select(f => f.ErrorMessage).ToArray();
                        var responseType = typeof(TResponse).GenericTypeArguments[0];
                        var apiResult = Activator.CreateInstance(
                            typeof(ApiResult<>).MakeGenericType(responseType),
                            new object[] { false, null, errors }
                        );

                        return (TResponse)apiResult!;
                    }

                    throw new ValidationException(failures);
                }
            }

            return await next();
        }
    }
}
