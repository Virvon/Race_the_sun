using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator;
using UnityEditor;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Editor
{
    [CustomEditor(typeof(JumpBoostSpawner))]
    public class JumpBoostSpawnerMarker : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(JumpBoostSpawner spawnMarker, GizmoType gizmo)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(spawnMarker.transform.position, 2f);
        }
    }
}
