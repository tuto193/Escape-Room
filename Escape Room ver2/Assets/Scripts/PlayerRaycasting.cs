using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRaycasting : MonoBehaviour
{
    // how long we want our raycast to draw out 
    public float distanceToSee;
    // to store the object that I hit with my ray
    private RaycastHit objectThatIHit; 
    void Start()
    {
        
    }

    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * distanceToSee, Color.magenta);
        
        if(Physics.Raycast(this.transform.position, this.transform.forward, out objectThatIHit, distanceToSee))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                // if the object I collided with is a Puzzle 
                // (solve pictures by order maybe with enum (https://www.youtube.com/watch?v=isiOcFXubXY) --> next video maybe also for opening door)
                if (objectThatIHit.collider.gameObject.CompareTag("Puzzle"))
                {
                    // puzzle will be displayed  
                    // and also the input field 
                    Debug.Log("Please solve the " + objectThatIHit.collider.gameObject.name);
                }
            }
        }
        
    }
} 