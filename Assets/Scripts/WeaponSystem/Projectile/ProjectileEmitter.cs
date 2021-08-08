using UnityEngine;

public class ProjectileEmitter : MonoBehaviour {
    [Tooltip("Damage every time it hits a target")]
    public float damagePerCollision = 5;

    [Tooltip("Frequency of projectiles")]
    public float projectilesPerSecond = 1.0;

    [Tooltip("How long they're active after being instanced")]
    public float projectileLifetime = 1.0;

    // The WS this is currently "attached" to
    public WeaponSystem weaponSystem;

    public void Fire() {
        DoFire(transform.rotation, weaponSystem.ProjectileMotions, projectileLifetime);
    }

    private void DoFire(Vector3 direction, List<ProjectileMotion> motions, float lifetime) {}

    private void OnProjectileCollided(GameObject target, Vector3 hit_location) {}

    private void OnProjectileMissed(Vector3 miss_location) {}
}
