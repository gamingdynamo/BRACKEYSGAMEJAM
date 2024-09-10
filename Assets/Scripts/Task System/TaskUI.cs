using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[RequireComponent(typeof(TextMeshProUGUI))]
public class TaskUI : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
  
    private void Awake() => textMesh = GetComponent<TextMeshProUGUI>();
    public void UpdateText(Task task)
    {
        if (textMesh == null)
            return;

        string result = string.Empty;
        List<Task> active = TaskSGT.Instance.GetTaskByState(Task.State.ACTIVE);
        List<Task> completed = TaskSGT.Instance.GetTaskByState(Task.State.COMPLETED);

        result += "Active:\n";

        foreach (Task t in active)
            result += $"{t.name}\n";

        if (completed.Count == 0)
        {
            textMesh.text = result;
            return;
        }
        

        result += "Completed:\n";

        foreach (Task t in completed)
            result += $"<s>{t.name}</s>\n";

        textMesh.text = result;
    }
}
