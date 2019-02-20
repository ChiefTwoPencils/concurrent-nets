using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RxPubSub
{
    public class RxPubSub<T> : IDisposable
    {
        private ISubject<T> subject;
        private List<IObserver<T>> observers = new List<IObserver<T>>();
        private List<IDisposable> observables = new List<IDisposable>();

        public IDisposable Subscribe(IObserver<T> observer)
        {
            observers.Add(observer);
            subject.Subscribe(observer);
            return new Subscription<T>(observer, observers);
        }

        public IDisposable AddPublisher(IObservable<T> observable) =>
            observable
                .SubscribeOn(TaskPoolScheduler.Default)
                .Subscribe(subject);

        public IObservable<T> AsObservable => subject.AsObservable();                

        public void Dispose()
        {
            observers.ForEach(o => o.OnCompleted());
            observers.Clear();
        }
    }

    class Subscription<T> : IDisposable
    {
        private IObserver<T> observer;
        private List<IObserver<T>> observers;

        public Subscription(IObserver<T> observer, List<IObserver<T>> observers)
        {
            this.observer = observer;
            this.observers = observers;
        }

        public void Dispose()
        {
            observer.OnCompleted();
            observers.Remove(observer);
        }
    }
}
