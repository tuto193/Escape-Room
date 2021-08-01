using UnityEngine;
using System.Collections.Generic;

public class Projectile : MonoBehaviour {
    // Signal when hitting a propper target
    public delegate void collidedEventHandler(GameObject target, Vector3 hit_location);
    public event collidedEventHandler collidedEvent;
    // If not hitting a target
    public delegate void missedEventHandler(Vector3 miss_location);
    public event missedEventHandler missedEvent;

    [Tooltip("How long the projectile will travel for")]
    private float lifetime = 1.0f;
    [Tooltip("What kind of trajectory/motions the projectile has")]
    private List<IProjectileMotion> _motions;

    private Vector3 _direction;

    private bool _is_setup;

    public void Setup(Vector3 position, Vector3 direction, List<IProjectileMotion> motions, float lifetime) {

    }

}
