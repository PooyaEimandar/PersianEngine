using System;
using System.Diagnostics;
using System.Threading;

namespace ParallelThreading
{
    class Worker
    {
        Thread thread;
        Deque<Task> tasks;
        WorkStealingScheduler scheduler;

        public bool LookingForWork { get; private set; }
        public AutoResetEvent Gate { get; private set; }

        static Hashtable<Thread, Worker> workers = new Hashtable<Thread, Worker>(Environment.ProcessorCount);
        public static Worker CurrentWorker
        {
            get
            {
                var currentThread = Thread.CurrentThread;
                Worker worker;
                if (workers.TryGet(currentThread, out worker))
                    return worker;
                return null;
            }
        }

#if XBOX
        static int affinityIndex;
#endif

        public Worker(WorkStealingScheduler scheduler)
        {
            this.thread = new Thread(Work);
            this.thread.Name = "ParallelTasks Worker";
            this.thread.IsBackground = true;
            this.tasks = new Deque<Task>();
            this.scheduler = scheduler;
            this.Gate = new AutoResetEvent(false);

            workers.Add(thread, this);
        }

        public void Start()
        {
            thread.Start();
        }

        public void AddWork(Task task)
        {
            tasks.LocalPush(task);
        }

        private void Work()
        {
#if XBOX
            int i = Interlocked.Increment(ref affinityIndex) - 1;
            int affinity = Parallel.XboxProcessorAffinity[i % Parallel.XboxProcessorAffinity.Length];
            Thread.CurrentThread.SetProcessorAffinity((int)affinity);
#endif

            Task task = new Task();
            while (true)
            {
                if (tasks.LocalPop(ref task))
                {
                    task.DoWork();
                    try { task.DoWork(); }
                    catch (Exception e) { Debug.WriteLine(e); throw; }
                }
                else
                    FindWork();
            }
        }

        private void FindWork()
        {
            LookingForWork = true;
            Task task;
            bool foundWork = false;
            do
            {
                if (scheduler.TryGetTask(out task))
                {
                    foundWork = true;
                    break;
                }

                var replicable = WorkItem.Replicable;
                if (replicable.HasValue)
                {
                    replicable.Value.DoWork();
                    WorkItem.SetReplicableNull(replicable);
                }

                for (int i = 0; i < scheduler.Workers.Count; i++)
                {
                    var worker = scheduler.Workers[i];
                    if (worker == this)
                        continue;

                    if (worker.tasks.TrySteal(ref task))
                    {
                        foundWork = true;
                        break;
                    }
                }

                Gate.WaitOne();
            } while (!foundWork);

            LookingForWork = false;
            tasks.LocalPush(task);
        }
    }
}
