using UnityEditor;
using UnityEngine;

namespace Traffic.Editor
{
    [InitializeOnLoad()]
    public class WaypointEditor
    {
        private static void DrawArrow(Vector3 point, Vector3 forward, float size)
        {
            forward = forward.normalized * size;
            Vector3 left = Quaternion.Euler(0, 45, 0) * forward;
            Vector3 right = Quaternion.Euler(0, -45, 0) * forward;

            Gizmos.DrawLine(point, point + left);
            Gizmos.DrawLine(point, point + right);
        }

        [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
        public static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
        {
            var transform = waypoint.transform;
            var pos = transform.position;
            if ((gizmoType & GizmoType.Selected) != 0)
            {
                Gizmos.color = Color.yellow;
            }
            else
            {
                Gizmos.color = Color.yellow * 0.5f;
            }

            Gizmos.DrawSphere(pos, 2f);

            // Gizmos.DrawLine(pos, pos + transform.forward * 10f);

            // Gizmos.color = Color.white;
            // Gizmos.DrawLine(pos + (waypoint.transform.right * 2.5f), pos + (transform.right * -2.5f));

            // if (waypoint.previousWaypoint != null)
            // {
            //     Gizmos.color = Color.red;
            //     var offset = transform.right * 2.5f;
            //     var offsetTo = waypoint.previousWaypoint.transform.right * 2.5f;
            //     
            //     Gizmos.DrawLine(pos + offset, waypoint.previousWaypoint.transform.position + offsetTo);
            // }

            if (waypoint.nextWaypoint != null)
            {
                Gizmos.color = Color.green;
                var offset = transform.right * -2.5f;
                var nextWaypointTransform = waypoint.nextWaypoint.transform;
                var offsetTo = nextWaypointTransform.right * -2.5f;

                var nextPos = nextWaypointTransform.position;
                Gizmos.DrawLine(pos, nextPos);


                var dir = (pos - nextPos).normalized;
                DrawArrow(nextPos, dir * 1.5f, 3f);
            }

            if (waypoint.branches != null)
            {
                if ((gizmoType & GizmoType.Selected) != 0)
                {
                    foreach (var branch in waypoint.branches)
                    {
                        Gizmos.color = Color.red * 2f;
                        Gizmos.DrawLine(waypoint.transform.position, branch.transform.position);
                    
                        var dir = (pos - branch.transform.position).normalized;
                        DrawArrow(branch.transform.position, dir * 1.5f, 3f);
                    } 
                }
                else
                {
                    foreach (var branch in waypoint.branches)
                    {
                        Gizmos.color = Color.blue * 0.6f;
                        Gizmos.DrawLine(waypoint.transform.position, branch.transform.position);
                    } 
                }
                
            }
        }
    }
}