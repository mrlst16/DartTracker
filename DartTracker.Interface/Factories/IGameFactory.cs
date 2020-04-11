using DartTracker.Model.Games;
using System;
using System.Collections.Generic;
using System.Text;

namespace DartTracker.Interface.Factories
{
    public interface IGameFactory<TIn>
    {
        Game Create(TIn request); 
    }
}
