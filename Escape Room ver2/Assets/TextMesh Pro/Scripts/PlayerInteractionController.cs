using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*public class ObjectClicker : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit,100.0f))
        {
            // check if we have a hit 
            if (hit.transform != null)
            {
                PrintName(hit.transform.gameObject);
            }
        }
    }

    private void PrintName(GameObject go)
    {
        print(go.name);
    }
}
*/
*/public class PlayerInteractionController : MonoBehaviour
{
    [Header("Interaction settings")] 
    public float maxDistance = 5;
    public LayerMask interacableLayer;

    [Header("UI: adapt this to fit your game")]
    public Button interactButton;

    private Interactable currentInteractable;

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance, interacableLayer))
        {
            currentInteractable = hit.collider.GetComponent<Interactable>();
        }
        else currentInteractable = null;

        interactButton.interactable = currentInteractable != null; 
    }

    public void Interact()
    {
        if (currentInteractable)
        {
            currentInteractable.OnInteraction();
        }
            
    } 
    */
}

// wenn ich nah genug am Bild dran bin, kann ich auf die space taste drücken und 
// dann erscheint das Quiz 
// Use OnTriggerEnter


