﻿using Convey.CQRS.Commands;
using MadXchange.Exchange.Domain.Types;
using MadXchange.Exchange.Types;
using System;

namespace MadXchange.Exchange.Messages.Commands.OrderService
{
    public class SetLeverage : ICommand
    {
        public Guid Id { get; }
        public Xchange Exchange { get; }
        public Guid AccountId { get; }
        public string Symbol { get; }
        public decimal Leverage { get; }
        public DateTime TimeStamp { get; } = DateTime.UtcNow;

        public SetLeverage(Guid id, Xchange exchange, Guid accountID, string symbol, decimal leverage)
        {
            Id = id == default ? Guid.NewGuid() : id;
            Exchange = exchange;
            AccountId = accountID;
            Symbol = symbol;
            Leverage = leverage;
        }

      
    }
}