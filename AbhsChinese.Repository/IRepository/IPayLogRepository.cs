﻿using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface IPayLogRepository : IRepositoryBase<Yw_PayLog>
    {
        void Add(Yw_PayLog log);
    }
}
