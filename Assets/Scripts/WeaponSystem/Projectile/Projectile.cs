using UnityEngine;

public class Projectile : MonoBehaviour {
    public delegate void CollidedEvent(GameObject target, Vector3 hit_location);

}
