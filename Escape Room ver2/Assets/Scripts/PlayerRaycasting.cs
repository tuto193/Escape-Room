using System;
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
    private string _currentRiddle;
    private bool _seenRiddle;
    void Start()
    {
        _collected = 0;
        _wonTheGame = false;
        _winningThreshold = GameObject.FindGameObjectsWithTag("Collectable").Length;
        _currentRiddle = "nr1"; // oder erst, nachdem das erste Rätsel angeklickt wurde?
        _seenRiddle = false;

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
                    if (objectThatIHit.collider.gameObject.GetComponent<KeyCards>().whatIsMyNumber.ToString() == _currentRiddle)
                    {
                        
                        // all riddles are hidden 
                        // if riddle is clicked 
                        // if riddle is next riddle (==currentState) (and previous riddle is solved -> change currentState) 
                        // turn / unhide 

                        // puzzle will be displayed  
                        // and also the input field 
                        Debug.Log("Please solve the " + objectThatIHit.collider.gameObject.name);
                        _currentRiddle = objectThatIHit.collider.gameObject.GetComponent<KeyCards>().whatIsMyNumber
                            .ToString();
                        _seenRiddle = true;
                    }
                    else
                    {
                        Debug.Log("Not possible yet. Please solve the riddle with " + _currentRiddle + " first. This is the riddle with " + objectThatIHit.collider.gameObject.GetComponent<KeyCards>().whatIsMyNumber);
                    }
                }
                
                // if the thing I collided with is an "answer"-object 
                if (objectThatIHit.collider.gameObject.CompareTag("Collectable"))
                {
                    if (objectThatIHit.collider.gameObject.GetComponent<KeyCards>().whatIsMyNumber.ToString() == _currentRiddle && _seenRiddle == true)
                    {
                        Debug.Log("Collected " + objectThatIHit.collider.gameObject.name);
                        Destroy(objectThatIHit.collider.gameObject);
                        int _currentState = (int) Char.GetNumericValue(_currentRiddle[2]);
                        _currentState++;
                        _currentRiddle = "nr" + _currentState.ToString();
                        //Debug.Log("_currentRiddle: " + _currentRiddle);
                        _collected++;
                        _seenRiddle = false;
                    }
                    else
                    {
                        Debug.Log("Cannot be collected yet. Please solve the riddle with " + _currentRiddle + " first or if already done so, look at the next riddle.");
                    }
                }
            }
        }
        
    }
} 