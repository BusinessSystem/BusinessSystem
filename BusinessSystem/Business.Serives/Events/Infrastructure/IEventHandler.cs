using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Serives.Events
{
    public interface IEventHandler<T>
    {
        void HandleEvent(T eventMessage);
    }
}
