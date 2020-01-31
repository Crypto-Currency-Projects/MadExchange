﻿using MadXchange.Exchange.Domain.Models;
using MadXchange.Exchange.Interfaces;
using MadXchange.Exchange.Services.RequestExecution;
using Microsoft.Extensions.Logging;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MadXchange.Exchange.Services.HttpRequests
{
    public interface IMarginRequestService
    {
        Task<IEnumerable<Margin>> GetMarginAsync(Guid accountId, Exchanges exchange, string currency, CancellationToken token);
    }

    public class MarginRequestService : IMarginRequestService
    {

        private ILogger _logger;
        private IExchangeDescriptorService _descriptorService;
        private IRestRequestService _restRequestService;
        public MarginRequestService(IExchangeDescriptorService exchangeDescriptorService, IRestRequestService restRequestService, ILogger<MarginRequestService> logger)
        {
            _descriptorService = exchangeDescriptorService;
            _restRequestService = restRequestService;
            _logger = logger;
        }

        public async Task<IEnumerable<Margin>> GetMarginAsync(Guid accountId, Exchanges exchange, string currency, CancellationToken token = default) 
        {
            var descriptor = _descriptorService.GetExchangeDescriptor(exchange);
            var route = descriptor.RouteGetEquity;
            var url = $"{descriptor.BaseUrl}/{route.Url}";
            var parameter = string.Empty;
            if (currency != string.Empty)
            {
                parameter.AddQueryParam(route.Parameter[0], currency);
            }
            var res = await _restRequestService.SendGetAsync(accountId, url, parameter, token).ConfigureAwait(false);
            return res.result.FromJson<IEnumerable<Margin>>();
        }
    }

    
}