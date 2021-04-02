
using System;
using UnityEngine;

/*public class Interactable : MonoBehaviour
{
    // distance of how close our player needs to get to  the object
    public float radius = 3f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
        
    }
}
*/
public class Interactable : MonoBehaviour
{
    public void OnInteraction()
    {
        Debug.Log("Interaction!");
    }
}