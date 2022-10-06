using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MousePlace))]
public class MousePlaceEditor : Editor
{
    private MousePlace m_mousePlace => target as MousePlace;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Place at mouse cursor") && !m_mousePlace.IsTargeting)
        {
            m_mousePlace.BeginTargeting();
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

        m_mousePlace.UpdateTargeting(mousePos);
 

        switch (currentGUIEvent.type)
        {
            case EventType.MouseMove:
                HandleUtility.Repaint();
                break;

            case EventType.MouseDown:
                switch (currentGUIEvent.button)
                {
                    case 0:
                        m_mousePlace.EndTargeting();
                        break;
                    case 1:
                        m_mousePlace.Cancel();
                        break;
                }

                SceneView.duringSceneGui -= OnDuringSceneGui;
                currentGUIEvent.Use();
                break;
        }
    }
}