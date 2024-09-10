using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class TaskSGT : GenericSingleton<TaskSGT>
{
    private Dictionary<Task, Task.State> tasks = new Dictionary<Task, Task.State>();

    [SerializeField] private UnityEvent<Task> onTaskAdded;
    [SerializeField] private UnityEvent<Task> onTaskCompleted;
    [SerializeField] private UnityEvent<Task> onTaskRemoved;

    public Dictionary<Task, Task.State> GetTasks() => tasks;


    public void AddListenerOnTaskAdded(UnityAction<Task> action) => onTaskAdded.AddListener(action);
    public void AddListenerOnTaskCompleted(UnityAction<Task> action) => onTaskCompleted.AddListener(action);
    public void AddListenerOnTaskRemoved(UnityAction<Task> action) => onTaskRemoved.AddListener(action);

    public void RemoveListenerOnTaskAdded(UnityAction<Task> action) => onTaskAdded.RemoveListener(action);
    public void RemoveListenerOnTaskCompleted(UnityAction<Task> action) => onTaskCompleted.RemoveListener(action);
    public void RemoveListenerOnTaskRemoved(UnityAction<Task> action) => onTaskRemoved.RemoveListener(action);

    private void Start() => onTaskCompleted.AddListener(OnAllTasksCompleted);

    public  void AddTask(Task task)
    {
        if (tasks.ContainsKey(task))
            return;

        tasks[task] = Task.State.ACTIVE;
        onTaskAdded?.Invoke(task);
    }

    public void CompleteTask(Task task)
    {
        if (!tasks.ContainsKey(task) || tasks[task] != Task.State.ACTIVE)
            return;

        tasks[task] = Task.State.COMPLETED;
        onTaskCompleted?.Invoke(task);
    }
    public  void RemoveTask(Task task) 
    {
        if (!tasks.ContainsKey(task) || tasks[task] != Task.State.COMPLETED)
            return;

        tasks[task] = Task.State.REMOVED;
        onTaskRemoved?.Invoke(task);  
    }

    public List<Task> GetTaskByState(Task.State state)
    {
        List<Task> result = new List<Task>();

        foreach(var task in tasks)
        {
            if (task.Value != state)
                continue;
            result.Add(task.Key);
        }
        return result;
    }

    private void OnAllTasksCompleted(Task t)
    {
        if (GetTaskByState(Task.State.ACTIVE).Count > 0)
            return;

        List<Task> tasksToRemove = new List<Task>();
        List<Task> tasksToAdd = new List<Task>();

        foreach (KeyValuePair<Task, Task.State> task in tasks)
        {
            if(task.Value != Task.State.COMPLETED)
                continue;

            foreach (Task newTask in task.Key.followUpTasks)
                tasksToAdd.Add(newTask);

            tasksToRemove.Add(task.Key);
        }

        foreach(var task in tasksToRemove)
            RemoveTask(task);

        foreach (var task in tasksToAdd)
            AddTask(task);

    }
}

