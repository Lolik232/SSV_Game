using UnityEditor;

using UnityEngine;

[CustomEditor(typeof(MousePlace))]
public class MousePlaceEditor : Editor
{
    private MousePlace _mousePlace => target as MousePlace;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Place at mouse cursor") && !_mousePlace.IsTargeting)
        {
            _mousePlace.BeginTargeting();
            SceneView.duringSceneGui += OnDuringSceneGui;
        }
    }

    private void OnDuringSceneGui(SceneView sceneView)
    {
        Event currentGUIEvent = Event.current;

        Vector2 mousePos = currentGUIEvent.mousePosition;

        float pixelsPerPoint = EditorGUIUtility.pixelsPerPoint;
        mousePos.y = sceneView.camera.pixelHeight - mousePos.y * pixelsPerPoint;
        mousePos.x *= pixelsPerPoint;


        mousePos = sceneView.camera.ScreenToWorldPoint(mousePos);

        _mousePlace.UpdateTargeting(mousePos);


        switch (currentGUIEvent.type)
        {
            case EventType.MouseMove:
                HandleUtility.Repaint();
                break;

            case EventType.MouseDown:
                switch (currentGUIEvent.button)
                {
                    case 0:
                        _mousePlace.EndTargeting();
                        break;
                    case 1:
                        _mousePlace.Cancel();
                        break;
                }

                SceneView.duringSceneGui -= OnDuringSceneGui;
                currentGUIEvent.Use();
                break;
        }
    }
}