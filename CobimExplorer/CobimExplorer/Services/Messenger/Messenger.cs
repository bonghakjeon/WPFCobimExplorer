using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CobimExplorer.Services.Messenger
{
    public class Messenger
    {
        //    List<SubScription> _Subscription = new();

        //    public void Send<TMessage>(TMessage message)
        //    {
        //        foreach (var subscription in _Subscription)
        //            _Subscription.Action(message);
        //    }

        //    public void Subscribe<TMessage>(object subscriber, Action<object> action)
        //    {
        //        _Subscription.Add(new SubScription(subscriber, action));
        //    }

        //    public void UnSubscribe<TMessage>(object subscriber)
        //    {
        //        var subscription = _Subscription.FirstOrDefault(x => x.Subscriber == subscriber);
        //        if (subscription != null)
        //            _Subscription.Remove(subscription);
        //    }
        //}

        //public record SubScription(object Subscriber, Action<object> Action);
    }
}
