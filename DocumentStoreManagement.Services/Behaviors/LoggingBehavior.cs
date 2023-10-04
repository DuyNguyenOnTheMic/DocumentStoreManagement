﻿using MediatR;
using Microsoft.Extensions.Logging;

namespace DocumentStoreManagement.Services.Behaviors
{
    /// <summary>
    /// Log behavior of request
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Logging information when request is handled
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling {TRequestName}", typeof(TRequest).Name);

            var response = await next();

            _logger.LogInformation("Handled {TResponseName}", typeof(TResponse).Name);

            return response;
        }
    }
}