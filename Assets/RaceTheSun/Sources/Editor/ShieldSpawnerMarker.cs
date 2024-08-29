using Assets.RaceTheSun.Sources.Gameplay.WorldGenerator.Spawners;
using UnityEditor;
using UnityEngine;

namespace Assets.RaceTheSun.Sources.Editor
{
    [CustomEditor(typeof(ShieldSpawner))]
    public class ShieldSpawnerMarker : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(ShieldSpawner spawnMarker, GizmoType gizmo)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(spawnMarker.transform.position, 2f);
        }
    }
}
