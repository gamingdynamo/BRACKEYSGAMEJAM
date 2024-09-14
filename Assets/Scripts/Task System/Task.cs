using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;
using System.Xml.Schema;



#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Task",menuName = "Task System/New Task")]
public class Task : ScriptableObject
{
    public Task[] followUpTasks;

    public Vector3 destination;

    public void AddTask() => TaskSGT.Instance.AddTask(this);
    public void ComleteTask() => TaskSGT.Instance.CompleteTask(this);
    public void RemoveTask() => TaskSGT.Instance.RemoveTask(this);
    public enum State { ACTIVE, COMPLETED, REMOVED }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Task))]
public class TaskEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var self = (Task)target;

        if (GUILayout.Button("Set Camera Position As Destination"))
        {
            SceneView scene = SceneView.lastActiveSceneView;
            if (!scene)
                return;

            Camera sceneCam = scene.camera;

            if (!sceneCam)
                return;

            self.destination = sceneCam.transform.position;
            AssetDatabase.SaveAssets();
        }
    }
}
#endif