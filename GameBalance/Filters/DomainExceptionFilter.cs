using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Api.Application.Filters
{
    public class DomainExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<DomainExceptionFilter> _logger;

        public DomainExceptionFilter(ILogger<DomainExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Erro ao executar a requisição!");

            context.Result = new ObjectResult("Erro ao executar requisição!");
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
