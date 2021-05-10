using System;
using System.Collections.Generic;
using System.Threading;

namespace ParallelThreading
{
    class WorkItem
    {
        private static SpinLock replicableLock;
        private static Task? replicable;
        internal static Task? Replicable
        {
            get
            {
                try
                {
                    replicableLock.Enter();
                    return replicable;
                }
                finally
                {
                    replicableLock.Exit();
                }
            }
            set
            {
                try
                {
                    replicableLock.Enter();
                    replicable = value;
                }
                finally
                {
                    replicableLock.Exit();
                }
            }
        }

        internal static void SetReplicableNull(Task? task)
        {
            try
            {
                replicableLock.Enter();
                if (!task.HasValue || (replicable.HasValue && replicable.Value.ID == task.Value.ID && replicable.Value.Item == task.Value.Item))
                    replicable = null;
            }
            finally
            {
                replicableLock.Exit();
            }
        }

        private static List<WorkItem> awaitingCallbacks;
        internal static List<WorkItem> AwaitingCallbacks
        {
            get
            {
                if (awaitingCallbacks == null)
                {
                    var instance = new List<WorkItem>();
                    Interlocked.CompareExchange(ref awaitingCallbacks, instance, null);
                }

                return awaitingCallbacks;
            }
        }

        List<Exception> exceptionBuffer;
        Hashtable<int, Exception[]> exceptions;
        ManualResetEvent resetEvent;
        IWork work;
        volatile int runCount;
        volatile int executing;
        List<Task> children;
        volatile int waitCount;
        SpinLock executionLock;

        static Pool<WorkItem> idleWorkItems = new Pool<WorkItem>();
        static Hashtable<Thread, Stack<Task>> runningTasks = new Hashtable<Thread, Stack<Task>>(Environment.ProcessorCount);

        public int RunCount
        {
            get { return runCount; }
        }

        public Hashtable<int, Exception[]> Exceptions
        {
            get { return exceptions; }
        }

        public IWork Work
        {
            get { return work; }
        }

        public Action Callback { get; set; }

        public static Task? CurrentTask
        {
            get
            {
                Stack<Task> tasks;
                if (runningTasks.TryGet(Thread.CurrentThread, out tasks))
                {
                    if (tasks.Count > 0)
                        return tasks.Peek();
                }
                return null;
            }
        }

        public WorkItem()
        {
            resetEvent = new ManualResetEvent(true);
            exceptions = new Hashtable<int, Exception[]>(1);
            children = new List<Task>();
        }

        public Task PrepareStart(IWork work)
        {
            this.work = work;
            resetEvent.Reset();
            children.Clear();
            exceptionBuffer = null;

            var task = new Task(this);
            var currentTask = WorkItem.CurrentTask;
            if (currentTask.HasValue && currentTask.Value.Item == this)
                throw new Exception("whadafak?");
            if (!work.Options.DetachFromParent && currentTask.HasValue)
                currentTask.Value.Item.AddChild(task);

            return task;
        }

        public bool DoWork(int expectedID)
        {
            try
            {
                executionLock.Enter();
                if (expectedID < runCount)
                    return true;
                if (executing == work.Options.MaximumThreads)
                    return false;
                executing++;
            }
            finally
            {
                executionLock.Exit();
            }

            // associate the current task with this thread, so that Task.CurrentTask gives the correct result
            Stack<Task> tasks = null;
            if (!runningTasks.TryGet(Thread.CurrentThread, out tasks))
            {
                tasks = new Stack<Task>();
                runningTasks.Add(Thread.CurrentThread, tasks);
            }
            tasks.Push(new Task(this));

            // execute the task
            try { work.DoWork(); }
            catch (Exception e) 
            {
                if (exceptionBuffer == null)
                {
                    var newExceptions = new List<Exception>();
                    Interlocked.CompareExchange(ref exceptionBuffer, newExceptions, null);
                }

                lock (exceptionBuffer)
                    exceptionBuffer.Add(e);
            }

            if (tasks != null)
                tasks.Pop();

            try
            {
                executionLock.Enter();
                executing--;
                if (executing == 0)
                {
                    if (exceptionBuffer != null)
                        exceptions.Add(runCount, exceptionBuffer.ToArray());

                    // wait for all children to complete
                    foreach (var child in children)
                        child.Wait();

                    runCount++;

                    // open the reset event, so tasks waiting on this once can continue
                    resetEvent.Set();

                    // wait for waiting tasks to all exit
                    while (waitCount > 0) ;

                    if (Callback == null)
                    {
                        Requeue();
                    }
                    else
                    {
                        // if we have a callback, then queue for execution
                        lock (AwaitingCallbacks)
                            AwaitingCallbacks.Add(this);
                    }

                    return true;
                }
                return false;
            }
            finally
            {
                executionLock.Exit();
            }

        }

        public void Requeue()
        {
            // requeue the WorkItem for reuse, but only if the runCount hasnt reached the maximum value
            // dont requeue if an exception has been caught, to stop potential memory leaks.
            if (runCount < int.MaxValue && exceptionBuffer == null)
                idleWorkItems.Return(this);
        }

        public void Wait(int id)
        {
            WaitOrExecute(id);

            Exception[] e;
            if (exceptions.TryGet(id, out e))
                throw new TaskException(e);
        }

        private void WaitOrExecute(int id)
        {
            if (runCount != id)
                return;

            if (DoWork(id))
                return;

            try
            {
                Interlocked.Increment(ref waitCount);
                int i = 0;
                while (runCount == id)
                {
                    if (i > 1000)
                        resetEvent.WaitOne();
                    else
                        Thread.Sleep(0);
                    i++;
                }
            }
            finally
            {
                Interlocked.Decrement(ref waitCount);
            }
        }

        public void AddChild(Task item)
        {
            children.Add(item);
        }

        public static WorkItem Get()
        {
            return idleWorkItems.Get();
        }
    }
}