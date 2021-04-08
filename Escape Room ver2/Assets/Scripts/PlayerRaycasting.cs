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
    
    private int _winningThreshold;
    private int _collected;
    private bool _wonTheGame;
    void Start()
    {
        _collected = 0;
        _wonTheGame = false;
        _winningThreshold = GameObject.FindGameObjectsWithTag("Collectable").Length;
        
    }
    
    private void LateUpdate()
    {
        if (_collected >= _winningThreshold && !_wonTheGame)
        {
            Debug.Log("You solved all the riddles and now you are free. Yay!");
            //Debug.Log(string.Format("You took : {} seconds", Time.realtimeSinceStartup));
            _wonTheGame = true;
        }
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
                
                if (objectThatIHit.collider.gameObject.CompareTag("Collectable"))
                {
                    Debug.Log("Collect " + objectThatIHit.collider.gameObject.name);
                    Destroy(objectThatIHit.collider.gameObject);
                    _collected++;
                }
            }
        }
        
    }
} 