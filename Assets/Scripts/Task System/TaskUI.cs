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
        if (textMesh == null || task == null)
            return;

        List<Task> active = TaskSGT.Instance.GetActiveTasks();
        List<Task> completed = TaskSGT.Instance.GetCompletedTasks();

        string result = string.Empty;

        foreach(Task t in active)
            result += $"{t.Description}\n\n";

        foreach (Task t in completed)
            result += $"<s>{t.Description}</s>\n\n";

        textMesh.text = result;
    }
}
