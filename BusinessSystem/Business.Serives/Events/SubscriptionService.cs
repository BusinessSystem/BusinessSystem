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
        public IList<IConsumer<T>> GetSubscriptions<T>()
        {
            IList<IConsumer<T>> consumers = new List<IConsumer<T>>();
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
                    if (implementInterface.IsGenericType && implementInterface == typeof (IConsumer<T>))
                    {
                        IConsumer<T> temp = (IConsumer<T>) Activator.CreateInstance(type);
                        consumers.Add(temp);
                    }
                }
            }
            return consumers;
        }
    }
}
