﻿using MadXchange.Exchange.Domain.Models;
using MadXchange.MadClient.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadXchange.MadClient.Infrastructure
{
    public class ClientStore : IClientStore
    {
        private readonly Guid _accountId;
        private readonly IDictionary<string, IPosition> _position;
        private readonly IDictionary<string, IDictionary<string, IOrder>> _openOrders;
        private readonly bool _isSocket;

        public ClientStore(IDictionary<string, IPosition> position, IDictionary<string, IDictionary<string, IOrder>> orders)
        {
            _position = position;
            _openOrders = orders;
        }

   

        public async Task<IPosition> GetPositionAsync(string currency)
        {
            return _position[currency];
        }

        public async Task<List<IOrder>> GetOrdersAsync(string symbol)
        {
            return _openOrders[symbol].Values.ToList();
        }

        public Task<IOrder> GetOrder(string symbol)
        {
            throw new NotImplementedException();
        }
    }
}