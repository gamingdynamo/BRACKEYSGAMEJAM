using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Task",menuName = "Task System/New Task")]
public class Task : ScriptableObject
{
    public Task[] followUpTasks;
    public void AddTask() => TaskSGT.Instance.AddTask(this);
    public void ComleteTask() => TaskSGT.Instance.CompleteTask(this);
    public void RemoveTask() => TaskSGT.Instance.RemoveTask(this);
    public enum State { ACTIVE, COMPLETED, REMOVED }
}
