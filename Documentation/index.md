# Global Task Runner Documentation

Welcome to the official documentation for the Global Task Runner for Unity.

## Introduction

Global Task Runner is a lightweight and easy-to-use utility for running and managing long-running processes (coroutines) in Unity without needing a `MonoBehaviour` instance. It provides a clean and simple API to start, stop, pause, and resume tasks, making it ideal for managing asynchronous operations in your game.

## Key Features

*   **No MonoBehaviour required:** Run coroutines from any C# class.
*   **Full task control:** Start, stop, pause, and resume tasks at any time.
*   **Completion events:** Get notified when a task finishes, and whether it was stopped manually.
*   **Easy to use:** A simple, intuitive API that gets you up and running in minutes.
*   **Lightweight:** No external dependencies and a minimal footprint.

## Getting Started

### Installation

To install the Global Task Runner, follow these steps:

1.  Open the Unity Package Manager by navigating to `Window > Package Manager`.
2.  Click the **+** button in the top-left corner and select **"Add package from git URL..."**.
3.  Enter the following URL: `https://github.com/Greener-Games/TaskRunner.git`

The package will be automatically downloaded and installed in your project.

### Basic Usage

Here’s a simple example of how to start a coroutine using the `TaskRunner`:

```csharp
using UnityEngine;
using GG.GlobalTaskRunner;
using System.Collections;

public class BasicExample : MonoBehaviour
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

In this example, `MyCoroutine` will start executing immediately. The `TaskManager` will be automatically created in the scene to handle the coroutine.

## Advanced Usage

For more control over your tasks, you can create a `TaskRunner` instance. This allows you to manage the task's lifecycle, add completion handlers, and more.

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

        // Start the task manually
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

## API Reference

For a detailed look at the classes and methods available, please see the [API Documentation](api/GG.GlobalTaskRunner.html).
