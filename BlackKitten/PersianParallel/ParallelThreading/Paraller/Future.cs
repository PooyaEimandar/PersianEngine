using System;

namespace ParallelThreading
{
    /// <summary>
    /// A task struct which can return a result.
    /// </summary>
    /// <typeparam name="T">The type of result this future calculates.</typeparam>
    public struct Future<T>
    {
        private Task task;
        private FutureWork<T> work;
        private int id;

        /// <summary>
        /// Gets a value which indicates if this future has completed.
        /// </summary>
        public bool IsComplete
        {
            get { return task.IsComplete; }
        }

        /// <summary>
        /// Gets an array containing any exceptions thrown by this future.
        /// </summary>
        public Exception[] Exceptions
        {
            get { return task.Exceptions; }
        }

        internal Future(Task task, FutureWork<T> work)
        {
            this.task = task;
            this.work = work;
            this.id = work.ID;
        }

        /// <summary>
        /// Gets the result. Blocks the calling thread until the future has completed execution.
        /// This can only be called once!
        /// </summary>
        /// <returns></returns>
        public T GetResult()
        {
            if (work == null || work.ID != id)
                throw new InvalidOperationException("The result of a future can only be retrieved once.");

            task.Wait();
            var result = work.Result;
            work.ReturnToPool();
            work = null;

            return result;
        }
    }

    class FutureWork<T>
        : IWork
    {
        public int ID { get; private set; }
        public WorkOptions Options { get; set; }
        public Func<T> Function { get; set; }
        public T Result { get; set; }

        public void DoWork()
        {
            Result = Function();            
        }

        public static FutureWork<T> GetInstance()
        {
            return Pool<FutureWork<T>>.Instance.Get();
        }

        public void ReturnToPool()
        {
            if (ID < int.MaxValue)
            {
                ID++;
                Pool<FutureWork<T>>.Instance.Return(this);
            }
        }
    }
}
