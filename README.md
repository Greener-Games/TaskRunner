# Global Task Runner

[![openupm](https://img.shields.io/npm/v/com.greener-games.global-task-runner?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.greener-games.global-task-runner/)

A lightweight and easy-to-use Task/Coroutine runner for Unity.

This utility allows you to run and manage long-running processes (coroutines) without needing a `MonoBehaviour` instance. It provides a simple API to start, stop, pause, and resume tasks.

## Installation

You can install this package via the Unity Package Manager.

1.  Open the Package Manager in `Window > Package Manager`.
2.  Click the `+` button in the top-left corner and select "Add package from git URL...".
3.  Enter the following URL: `https://github.com/Greener-Games/TaskRunner.git`

The package will be installed in your project.

## Usage

Here is a basic example of how to use the `TaskRunner`:

```csharp
using UnityEngine;
using GG.GlobalTaskRunner;
using System.Collections;

public class Example : MonoBehaviour
{
    void Start()
    {
        // Create and start a new task
        TaskRunner.Create(MyCoroutine());
    }

    IEnumerator MyCoroutine()
    {
        Debug.Log("Task started!");
        yield return new WaitForSeconds(5);
        Debug.Log("Task finished after 5 seconds.");
    }
}
```

You can also create a `TaskRunner` instance to have more control over the task:

```csharp
using UnityEngine;
using GG.GlobalTaskRunner;
using System.Collections;

public class AdvancedExample : MonoBehaviour
{
    private TaskRunner myTask;

    void Start()
    {
        // Create a task without starting it immediately
        myTask = new TaskRunner(MyCoroutine(), autoStart: false);

        // Add a handler for when the task finishes
        myTask.TaskFinishedHandler += OnTaskFinished;

        // Start the task
        myTask.Start();
    }

    void OnTaskFinished(bool manuallyStopped)
    {
        if (manuallyStopped)
        {
            Debug.Log("Task was stopped manually.");
        }
        else
        {
            Debug.Log("Task completed successfully.");
        }
    }

    IEnumerator MyCoroutine()
    {
        Debug.Log("Task started!");
        for (int i = 0; i < 5; i++)
        {
            Debug.Log("Working... " + i);
            yield return new WaitForSeconds(1);
        }
    }

    void OnDestroy()
    {
        // It's good practice to stop the task if the object is destroyed
        if (myTask != null && myTask.Running)
        {
            myTask.Stop();
        }
    }
}
```

## Documentation

For more detailed information and API reference, please see the [full documentation](https://greener-games.github.io/TaskRunner/).

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.
