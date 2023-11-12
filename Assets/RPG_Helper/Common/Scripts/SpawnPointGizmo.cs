using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointGizmo : MonoBehaviour
{
    public Color color;
    public float radius;
    void OnDrawGizmos() {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
