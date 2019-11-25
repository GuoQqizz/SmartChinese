using AbhsChinese.Domain.Entity;
using AbhsChinese.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Repository.IRepository
{
    public interface ISumStudentRepository : IRepositoryBase<Sum_Student>
    {
        int Add(Sum_Student entity);

        Sum_Student Get(int id);

        bool Update(Sum_Student entity);

        void AddCoins(int studentId, int coins);
    }
}
