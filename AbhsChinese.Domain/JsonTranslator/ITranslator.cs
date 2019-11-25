using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbhsChinese.Domain.JsonTranslator
{
    public interface ITranslator
    {
    }

    public interface ITranslator<T,T1>: ITranslator
    {
        T1 MakeTranslator(T entity);
    }
}

