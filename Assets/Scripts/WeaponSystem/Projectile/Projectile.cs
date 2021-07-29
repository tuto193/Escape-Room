using UnityEngine;

public class Projectile : MonoBehaviour {
    // Signal when hitting a propper target
    public delegate void collidedEventHandler(GameObject target, Vector3 hit_location);
    public event collidedEventHandler collidedEvent;
    // If not hitting a target
    public delegate void missedEventHandler(Vector3 miss_location);
    public event missedEventHandler missedEvent;

    [Tooltip("How long the projectile will travel for")]
    private float lifetime = 1.0f;
    // [Tooltip("What kind of trajectory/motions the projectile has")]
    // private


}
