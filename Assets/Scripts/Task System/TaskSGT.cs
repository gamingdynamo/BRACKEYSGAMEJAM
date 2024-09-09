using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TaskSGT : GenericSingleton<TaskSGT>
{
    private List<Task> ActiveTasks = new List<Task>();
    private List<Task> CompletedTasks = new List<Task>();

    [SerializeField] private UnityEvent<Task> onTaskAdded;
    [SerializeField] private UnityEvent<Task> onTaskCompleted;
    [SerializeField] private UnityEvent<Task> onTaskRemoved;


    public  void AddTask(Task task)
    {
        if (!ActiveTasks.Contains(task))
        {
            ActiveTasks.Add(task);
            onTaskAdded?.Invoke(task);
        }
    }

    public  void RemoveTask(Task task) 
    {
        if (ActiveTasks.Contains(task))
        {
            ActiveTasks.Remove(task);
            onTaskRemoved?.Invoke(task);
        }
    }


    public  void CompleteTask(Task task)
    {
        if (ActiveTasks.Contains(task))
        {
            ActiveTasks.Remove(task);
            CompletedTasks.Add(task);
            onTaskCompleted?.Invoke(task);
        }
    }

    public  List<Task> GetActiveTasks() => ActiveTasks;
    public  List<Task> GetCompletedTasks() => CompletedTasks;
}

