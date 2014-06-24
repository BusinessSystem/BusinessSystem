using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Business.Serives.Events
{
    public class SubscriptionService:ISubscriptionService
    {
        public IList<IEventHandler<T>> GetSubscriptions<T>()
        {
            IList<IEventHandler<T>> consumers = new List<IEventHandler<T>>();
            Type[] types = Assembly.Load("Business.Serives.Events").GetTypes();
            foreach (var type in types)
            {
                if (type.IsInterface)
                {
                    continue;
                }
                var implementInterfaces = type.GetInterfaces();
                foreach (var implementInterface in implementInterfaces)
                {
                    if (implementInterface.IsGenericType && implementInterface == typeof(IEventHandler<T>))
                    {
                        IEventHandler<T> temp = (IEventHandler<T>)Activator.CreateInstance(type);
                        consumers.Add(temp);
                    }
                }
            }
            return consumers;
        }
    }
}
