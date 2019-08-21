﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Traiding.Core.Models;

namespace Traiding.Core.Repositories
{
    public interface IOperationTableRepository
    {
        bool Contains(OperationEntity entity);
        void Add(OperationEntity entity);
        void SaveChanges();
        bool ContainsById(int entityId);
        OperationEntity Get(int entityId);
        IEnumerable<OperationEntity> GetByClient(int clientId);
        void FillCustomerColumns(int entityId, int blockedMoneyId); // DateTime DebitDate, ClientEntity Customer, decimal Total 
        void FillSellerColumns(int entityId, int blockedSharesNumberId); // ClientEntity Seller, ShareEntity Share, string ShareTypeName, decimal Cost, int Number
        void SetChargeDate(int entityId, DateTime chargeDate);
        void Remove(int entityId);
    }
}
