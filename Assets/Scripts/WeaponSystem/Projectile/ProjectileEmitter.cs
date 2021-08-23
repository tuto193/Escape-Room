using UnityEngine;
using System.Collections.Generic;

public class ProjectileEmitter : MonoBehaviour {
    [Tooltip("Damage every time it hits a target")]
    public float damagePerCollision = 5f;

    [Tooltip("Frequency of projectiles")]
    public float projectilesPerSecond = 1.0f;

    [Tooltip("How long they're active after being instanced")]
    public float projectileLifetime = 1.0f;
    
    private List<GameObject> _spawned_objects;

    // The WS this is currently "attached" to
    public WeaponSystem weaponSystem;

    public void Fire() {
        DoFire(transform.rotation, weaponSystem.ProjectileMotions, projectileLifetime);
    }

    private void DoFire(Quaternion direction, List<ProjectileMotion> motions, float lifetime) {
        return;
    }

    private void OnProjectileCollided(GameObject target, Vector3 hit_location) {
        weaponSystem.OnDamaged(target, damagePerCollision);
        foreach(ProjectileEvent pe in weaponSystem._ProjectileEvents) {
            pe.Trigger(hit_location, sp)
        }
    }

    private void OnProjectileMissed(Vector3 miss_location) {}
}
