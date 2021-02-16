using System.Collections;
using UnityEngine;

namespace GG.GlobalTaskRunner
{
    internal class TaskManager : MonoBehaviour
    {
        public static TaskManager singleton;

        public static TaskState CreateTask(IEnumerator coroutine)
        {
            if (singleton == null)
            {
                GameObject go = new GameObject("TaskManager");
                singleton = go.AddComponent<TaskManager>();
                go.hideFlags = HideFlags.HideAndDontSave;
            }
            return new TaskState(coroutine);
        }
    }
}