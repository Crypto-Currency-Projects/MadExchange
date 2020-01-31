﻿using MadXchange.Exchange.Domain.Models;
using MadXchange.Exchange.Dto;
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
    public interface IPositionRequestService
    {
    }

    public class PositionRequestService : IPositionRequestService
    {

        private ILogger _logger;
        private IExchangeDescriptorService _descriptorService;
        private IRestRequestService _restRequestService;
        //private ISignatureProvider _signatureProvider;
        public PositionRequestService(IExchangeDescriptorService exchangeDescriptorService, IRestRequestService restRequestService, ILogger<PositionRequestService> logger)
        {
            _descriptorService = exchangeDescriptorService;
            _restRequestService = restRequestService;
            _logger = logger;
        }

        public async Task<IEnumerable<Position>> GetPositionAsync(Guid accountId, Exchanges exchange, string symbol, CancellationToken token = default)
        {
            var descriptor = _descriptorService.GetExchangeDescriptor(exchange);
            var route = descriptor.RouteGetPosition;
            var url = $"{descriptor.BaseUrl}/{route.Url}";
            var parameter = string.Empty;
            if (symbol != string.Empty)
            {
                parameter.AddQueryParam(route.Parameter[0], symbol);
            }
            var res = await _restRequestService.SendGetAsync(accountId, url, parameter, token).ConfigureAwait(false);            
            return res.result.FromJson<IEnumerable<Position>>();
        }
        /// <summary>
        /// Get Leverage
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="exchange"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public async Task<IEnumerable<LeverageDto>> GetLeverageAsync(Guid accountId, Exchanges exchange, string symbol, CancellationToken token = default)
        {
            var descriptor = _descriptorService.GetExchangeDescriptor(exchange);
            var route = descriptor.RouteGetLeverage;
            var url = $"{descriptor.BaseUrl}/{route.Url}";
            var parameter = string.Empty;
            if (symbol != string.Empty)
            {
                parameter.AddQueryParam(route.Parameter[0], symbol);
            }
            var res = await _restRequestService.SendGetAsync(accountId, url, parameter, token).ConfigureAwait(false);                        
            return res.result.FromJson<IEnumerable<LeverageDto>>();
        }

        public async Task<IEnumerable<LeverageDto>> PostLeverageAsync(Guid accountId, Exchanges exchange, string symbol, decimal leverage, CancellationToken token = default)
        {
            var descriptor = _descriptorService.GetExchangeDescriptor(exchange);
            var route = descriptor.RoutePostLeverage;
            var url = $"{descriptor.BaseUrl}/{route.Url}";
            var parameter = string.Empty;            
            parameter.AddQueryParam(route.Parameter[0], symbol);                            
            parameter.AddQueryParam(route.Parameter[1], leverage.ToString());
            var res = await _restRequestService.SendGetAsync(accountId, url, parameter, token).ConfigureAwait(false);
            return res.result.FromJson<IEnumerable<LeverageDto>>();
        }

    }

    
}