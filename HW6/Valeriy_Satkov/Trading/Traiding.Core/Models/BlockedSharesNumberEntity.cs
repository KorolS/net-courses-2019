﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traiding.Core.Models
{
    public class BlockedSharesNumberEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual SharesNumberEntity ClientSharesNumber { get; set; }
        public virtual OperationEntity Operation { get; set; }
        public virtual ShareEntity Share { get; set; }
        public string ShareTypeName { get; set; } // see ShareTypeEntity.Name (The name will be fixed here at the time of purchase)
        public decimal Cost { get; set; } // see ShareTypeEntity.Cost (The cost will be fixed here at the time of purchase)
        public int Number { get; set; } // Number of shares for deal
    }
}
