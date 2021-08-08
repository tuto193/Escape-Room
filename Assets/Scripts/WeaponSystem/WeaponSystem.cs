using UnityEngine;
using System.Collections.Generic;

public class WeaponSystem : MonoBehaviour {
    public delegate void DamagedEvent(GameObject target, float amount);
    public event DamagedEvent OnDamaged;

    private ProjectileEmitter _ProjectileEmitter;

    public List<ProjectileMotion> ProjectileMotions;

    private List<ProjectileEvent> _ProjectileEvents;


    public void AddMotion(ProjectileMotion new_mot, bool allow_duplicates = false) {
        if(!allow_duplicates) {
            foreach(var pm in ProjectileMotions) {
                if(new_mot.GetType() == pm.GetType()) {
                    return;
                }
            }
        }
        ProjectileMotions.Add(new_mot);
    }


    public void AddProjectileEvent(ProjectileEvent new_eve, bool allow_duplicates = false) {
        if(!allow_duplicates) {
            foreach(var e in _ProjectileEvents) {
                if(new_eve.GetType() == e.GetType()) {
                    return;
                }
            }
        }
        _ProjectileEvents.Add(new_eve);
    }


    public void SetEmitter(ProjectileEmitter e) {
        this._ProjectileEmitter = e;
    }


    // public void ClearEmitters() {
    // }



}
