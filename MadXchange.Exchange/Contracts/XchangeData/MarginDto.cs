﻿using System;
using System.Runtime.Serialization;

namespace MadXchange.Exchange.Contracts
{
    [DataContract]
    public class MarginDto : HttpMessageDto
    {
        public Guid AccountId { get; set; }

        [DataMember]
        public string XchangeAccountId { get; }

        [DataMember]
        public string Currency { get; set; }

        [DataMember]
        public decimal? WalletBalance { get; set; }

        [DataMember]
        public decimal? MarginBalance { get; set; }

        [DataMember]
        public decimal? AvailableMargin { get; set; }

        [DataMember]
        public decimal? OrderMargin { get; set; }
    }
}