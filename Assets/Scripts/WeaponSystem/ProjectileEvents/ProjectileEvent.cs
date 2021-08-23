using UnityEngine;

public class ProjectileEvent : MonoBehaviour {
    [Tooltip("Does the event happen also after not hitting a target?")]
    public bool triggersOnMisses = false;

    public void Trigger(Vector3 spawn_location, GameObject parent, WeaponSystem ws, bool missed) {
        if (missed && !triggersOnMisses) {
            return;
        }
        _DoTrigger(spawn_location, parent, ws, missed);
    }
    
    private void _DoTrigger(Vector3 spawn_location, GameObject parent, WeaponSystem ws, bool missed) {
        return;
    }

}
