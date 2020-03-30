using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DartTracker.Interface.Mapper
{
    public interface IMapper<TIn, TOut>
    {
        Task<TOut> Map(TIn source);
    }
}
