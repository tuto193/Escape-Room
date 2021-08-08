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
    private List<ProjectileMotion> _motions;

    private Vector3 _direction;

    private bool _is_setup = false;

    public void Setup(Vector3 position, Vector3 direction, List<ProjectileMotion> motions, float lifetime) {
        this.transform.position = position;
        this._direction = direction;
        this.lifetime = lifetime;

        if(!_is_setup) {
            foreach(ProjectileMotion pm in motions) {
                pm._projectile = this;
                this._motions.Add(pm);
            }
            _is_setup = true;
        }
        PostSetup();
    }

    public void PostSetup() {}

    public void Impact() {}

    public void Miss() {}

    public Vector3 UpdateMovement(float delta) {
        Vector3 mov_vec = new Vector3(0, 0, 0);
        if (this._motions.Count == 0) {
            return mov_vec;
        }
        foreach (ProjectileMotion pm in this._motions) {
            mov_vec += pm.UpdateMovement(this._direction, delta);
        }
        return mov_vec;
    }
}
