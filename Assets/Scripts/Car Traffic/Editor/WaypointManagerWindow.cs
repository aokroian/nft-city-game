using UnityEditor;
using UnityEngine;

namespace Car_Traffic.Editor
{
    public class WaypointManagerWindow : EditorWindow
    {
        [MenuItem("Tools/Waypoint Editor")]
        public static void Open()
        {
            GetWindow<WaypointManagerWindow>();
        }

        public Transform waypointRoot;

        private void OnGUI()
        {
            var obj = new SerializedObject(this);

            EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

            if (waypointRoot == null)
            {
                EditorGUILayout.HelpBox("Root transform must be selected. Please assign a root transform.",
                    MessageType.Warning);
            }
            else
            {
                EditorGUILayout.BeginVertical("box");
                DrawButtons();
                EditorGUILayout.EndVertical();
            }

            obj.ApplyModifiedProperties();
        }

        private void DrawButtons()
        {
            if (GUILayout.Button("Create Waypoint"))
            {
                CreateWaypoint();
            }

            if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<Waypoint>())
            {
                if (GUILayout.Button("Create Branch Waypoint"))
                {
                    CreateBranchWaypoint();
                }

                if (GUILayout.Button("Create Waypoint Before"))
                {
                    CreateWaypointBefore();
                }

                if (GUILayout.Button("Create Waypoint After"))
                {
                    CreateWaypointAfter();
                }

                if (GUILayout.Button("Remove Waypoint"))
                {
                    RemoveWaypoint();
                }
            }
        }

        private void CreateBranchWaypoint()
        {
            var waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
            waypointObject.transform.SetParent(waypointRoot, false);

            var waypoint = waypointObject.GetComponent<Waypoint>();

            var branchedFrom = Selection.activeGameObject.GetComponent<Waypoint>();
            branchedFrom.branches.Add(waypoint);

            waypoint.transform.position = branchedFrom.transform.position;
            waypoint.transform.forward = branchedFrom.transform.forward;

            Selection.activeGameObject = waypoint.gameObject;
        }

        private void CreateWaypointBefore()
        {
            var waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
            waypointObject.transform.SetParent(waypointRoot, false);

            var newWaypoint = waypointObject.GetComponent<Waypoint>();
            var selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

            waypointObject.transform.position =
                selectedWaypoint.transform.position;
            waypointObject.transform.forward = selectedWaypoint.transform.forward;
            if (selectedWaypoint.previousWaypoint != null)
            {
                newWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
                selectedWaypoint.previousWaypoint.nextWaypoint = newWaypoint;
            }

            newWaypoint.nextWaypoint = selectedWaypoint;
            selectedWaypoint.previousWaypoint = newWaypoint;

            newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

            Selection.activeGameObject = newWaypoint.gameObject;
        }

        private void CreateWaypointAfter()
        {
            var waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
            waypointObject.transform.SetParent(waypointRoot, false);

            var newWaypoint = waypointObject.GetComponent<Waypoint>();
            var selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();

            waypointObject.transform.position =
                selectedWaypoint.transform.position;
            waypointObject.transform.forward = selectedWaypoint.transform.forward;

            newWaypoint.previousWaypoint = selectedWaypoint;

            if (selectedWaypoint.nextWaypoint != null)
            {
                selectedWaypoint.nextWaypoint.previousWaypoint = newWaypoint;
                newWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
            }

            selectedWaypoint.nextWaypoint = newWaypoint;

            newWaypoint.transform.SetSiblingIndex(selectedWaypoint.transform.GetSiblingIndex());

            Selection.activeGameObject = newWaypoint.gameObject;
        }

        private void RemoveWaypoint()
        {
            var selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
            if (selectedWaypoint.nextWaypoint != null)
            {
                selectedWaypoint.nextWaypoint.previousWaypoint = selectedWaypoint.previousWaypoint;
            }

            if (selectedWaypoint.previousWaypoint != null)
            {
                selectedWaypoint.previousWaypoint.nextWaypoint = selectedWaypoint.nextWaypoint;
                Selection.activeGameObject = selectedWaypoint.previousWaypoint.gameObject;
            }

            DestroyImmediate(selectedWaypoint.gameObject);
        }

        private void CreateWaypoint()
        {
            var waypointObject = new GameObject("Waypoint " + waypointRoot.childCount, typeof(Waypoint));
            waypointObject.transform.SetParent(waypointRoot, false);

            var waypoint = waypointObject.GetComponent<Waypoint>();
            if (waypointRoot.childCount > 1)
            {
                waypoint.previousWaypoint = waypointRoot.GetChild(waypointRoot.childCount - 2).GetComponent<Waypoint>();
                waypoint.previousWaypoint.nextWaypoint = waypoint;
                // place new waypoint near previous one
                var previousWaypointTransform = waypoint.previousWaypoint.transform;
                var previousWaypointForward = previousWaypointTransform.forward;
                var thisTransform = waypoint.transform;
                thisTransform.position = previousWaypointTransform.position;
                // set same rotation
                thisTransform.forward = previousWaypointForward;
            }

            Selection.activeGameObject = waypoint.gameObject;
        }
    }
}