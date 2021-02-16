using System.Collections;

namespace GG.GlobalTaskRunner
{
    public class TaskState
    {
        public bool running;

        public bool paused;

        public delegate void FinishedHandler(bool manual);
        public event FinishedHandler Finished;

        IEnumerator coroutine;
        bool stopped;

        public TaskState(IEnumerator c)
        {
            coroutine = c;
        }

        public void Pause()
        {
            paused = true;
        }

        public void Unpause()
        {
            paused = false;
        }

        public void Start()
        {
            running = true;
            TaskManager.singleton.StartCoroutine(CallWrapper());
        }

        public void Stop()
        {
            stopped = true;
            running = false;
        }

        IEnumerator CallWrapper()
        {
            yield return null;
            IEnumerator e = coroutine;
            while (running)
            {
                if (paused)
                    yield return null;
                else
                {
                    if (e != null && e.MoveNext())
                    {
                        yield return e.Current;
                    }
                    else
                    {
                        running = false;
                    }
                }
            }

            FinishedHandler handler = Finished;
            handler?.Invoke(stopped);
        }
    }
}