using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TriggerBase))]
class ConnectLineHandleExampleScript : Editor
{
    void OnSceneGUI()
    {
        TriggerBase connectedObjects = target as TriggerBase;
        if (connectedObjects.Targets == null)
            return;

        Vector3 center = connectedObjects.transform.position;
        for (int i = 0; i < connectedObjects.Targets.Length; i++)
        {
            GameObject connectedObject = connectedObjects.Targets[i];
            if (connectedObject)
            {
                Handles.DrawLine(center, connectedObject.transform.position);
            }
            else
            {
                Handles.DrawLine(center, Vector3.zero);
            }
        }
    }
}
