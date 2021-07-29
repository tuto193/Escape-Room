using UnityEngine;
using System.Collections.Generic;

public class WeaponLocation : MonoBehaviour {
    void OnDrawGizmos() {
        DrawArrow.ForGizmo(new Vector3(0, 0, 0), new Vector3(0, 100, 0), 4);
        DrawArrow.ForGizmo(new Vector3(-15, -85, 0), new Vector3(15, -115, 0), 4);
        DrawArrow.ForGizmo(new Vector3(-15, -115, 0), new Vector3(15, -85, 0), 4);
    }
}
