using System;
using System.Threading.Tasks;
using EasyNetQ;
using SuitSupply.Messages;

namespace SuitSupply.SericeBase
{
    public abstract class BaseHandler<T> where T : class
    {
        protected readonly IBus Bus;

        protected BaseHandler(IBus bus, string handlerName)
        {
            Bus = bus;
            Bus.Subscribe<T>(handlerName, async command => await OnHandle(command));
        }

        protected abstract Task OnHandle(T message);
    }
}