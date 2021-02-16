using System.Collections;

namespace GG.GlobalTaskRunner
{
    public class TaskRunner
    {
        TaskState task;

        public bool Running => task.running;
        public bool Paused => task.paused;

        public delegate void FinishedHandler(bool manual);
        public event FinishedHandler TaskFinishedHandler;

        public static TaskRunner Create(IEnumerator c)
        {
            TaskRunner task1 = new TaskRunner(c);
            return task1;
        }
        
        public TaskRunner(IEnumerator c, bool autoStart = true)
        {
            task = TaskManager.CreateTask(c);
            task.Finished += Finished;
            if (autoStart)
            {
                Start();
            }
        }

        public TaskRunner(IEnumerator c, FinishedHandler handler, bool autoStart = true) : this(c, autoStart)
        {
            TaskFinishedHandler += handler;
        }

        public void Start()
        {
            task.Start();
        }

        public void Stop()
        {
            task.Stop();
        }

        public void Pause()
        {
            task.Pause();
        }

        public void Unpause()
        {
            task.Unpause();
        }

        void Finished(bool manual)
        {
            FinishedHandler handler = TaskFinishedHandler;
            handler?.Invoke(manual);
        }
    }
}