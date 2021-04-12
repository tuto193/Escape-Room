using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayerRaycasting : MonoBehaviour
{
    // how long we want our raycast to draw out 
    public float distanceToSee;
    // to store the object that I hit with my ray
    private RaycastHit _objectThatIHit;
    public Image endTextBackground;
    public TextMeshProUGUI endText;
    public Image keyTextBackground;
    public Image startTextBackground;
    public RawImage IntroVideo;
    private int _winningThreshold;
    private int _collected;
    private bool _wonTheGame;
    private string _currentRiddle;
    private bool _seenRiddle;
    private GameObject[] _riddles;
    private GameObject _key;
    public ProgressBar progressBar;
    public VideoPlayer VideoPlayer;

    private bool _shownIntro;
    //public TextMeshProUGUI inputField;
    public TMP_InputField inputF;
    void Start()
    {
        //_progressBar.current = _collected;
        _shownIntro = false;
        progressBar.gameObject.SetActive(false);
        _collected = 0;
        _wonTheGame = false;
        print(inputF.text);
        inputF.gameObject.SetActive(false);
        endTextBackground.gameObject.SetActive(false);
        endText.gameObject.SetActive(false);
        keyTextBackground.gameObject.SetActive(false);
        _key = GameObject.FindGameObjectWithTag("Key");
        _key.SetActive(false);
        _riddles = GameObject.FindGameObjectsWithTag("Puzzle");
        _winningThreshold = GameObject.FindGameObjectsWithTag("Collectable").Length;
        //_currentRiddle = "nr1"; // oder erst, nachdem das erste Rätsel angeklickt wurde?
        for (int i = 0; i < _winningThreshold; i++)
        {
            if (i == 0)
            {
                _currentRiddle = _riddles[i].GetComponent<Collider>().gameObject.GetComponent<KeyCards>().whatIsMyNumber.ToString();
            }
            else
            {
               _riddles[i].SetActive(false); 
            }
        }
        _seenRiddle = false;

    }
    
    private void LateUpdate()
    {
        

        if (_collected >= _winningThreshold && !_wonTheGame)
        {
            _key.SetActive(true);
            keyTextBackground.gameObject.SetActive(true);

            Debug.Log("You solved all the riddles and suddenly you hear a strange noise behind you. What is it?");
            //Debug.Log(string.Format("You took : {} seconds", Time.realtimeSinceStartup));
            _wonTheGame = true;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            if (_shownIntro == false)
            {
                Destroy(IntroVideo.gameObject);
                Destroy(VideoPlayer.gameObject);
                _shownIntro = true;
            }
            else if (_wonTheGame)
            {
                keyTextBackground.gameObject.SetActive(false);
            }
        }
        if (Input.GetKey(KeyCode.Space) && _shownIntro)
        {
            startTextBackground.gameObject.SetActive(false);
            progressBar.gameObject.SetActive(true);
        }
        
        
        Debug.DrawRay(this.transform.position, this.transform.forward * distanceToSee, Color.magenta);
        
        if(Physics.Raycast(this.transform.position, this.transform.forward, out _objectThatIHit, distanceToSee))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                // if the object I collided with is a Puzzle 
                // (solve pictures by order maybe with enum (https://www.youtube.com/watch?v=isiOcFXubXY) --> next video maybe also for opening door)
                if (_objectThatIHit.collider.gameObject.CompareTag("Puzzle"))
                {
                    if (_objectThatIHit.collider.gameObject.GetComponent<KeyCards>().whatIsMyNumber.ToString() == _currentRiddle)
                    {

                        // puzzle will be displayed  
                        // and also the input field 
                        Debug.Log("Please solve the " + _objectThatIHit.collider.gameObject.name);
                        _currentRiddle = _objectThatIHit.collider.gameObject.GetComponent<KeyCards>().whatIsMyNumber
                            .ToString();
                        _seenRiddle = true;
                        inputF.gameObject.SetActive(true);
                        inputF.ActivateInputField();
                    }
                    else
                    {
                        Debug.Log("Not possible yet. Please solve the riddle with " + _currentRiddle + " first. This is the riddle with " + _objectThatIHit.collider.gameObject.GetComponent<KeyCards>().whatIsMyNumber);
                    }
                }
                
                // if the thing I collided with is an "answer"-object 
                else if (_objectThatIHit.collider.gameObject.CompareTag("Collectable"))
                {
                    if (_objectThatIHit.collider.gameObject.GetComponent<KeyCards>().whatIsMyNumber.ToString() == _currentRiddle)
                    {
                        if (_seenRiddle == true)
                        {
                            Debug.Log("Collected " + _objectThatIHit.collider.gameObject.name);
                            Destroy(_objectThatIHit.collider.gameObject);
                            int currentState = (int) Char.GetNumericValue(_currentRiddle[2]);
                            if (currentState < _winningThreshold)
                            {
                                _riddles[currentState].SetActive(true);
                            }
                            currentState++;
                            _currentRiddle = "nr" + currentState.ToString();
                            _collected++;
                            _seenRiddle = false;
                            progressBar.current = _collected;
                        }
                        else
                        {
                            Debug.Log("Cannot be collected yet. Please look at the next riddle first.");
                        }
                    }
                    else
                    {
                        Debug.Log("Cannot be collected yet. Please solve the riddle with " + _currentRiddle + " first.");
                    }
                }
                else if (_objectThatIHit.collider.gameObject.CompareTag("Key"))
                {
                    Debug.Log("hit key");
                    _key.SetActive(false);
                    endText.gameObject.SetActive(true);
                    endTextBackground.gameObject.SetActive(true);
                }
            }
        }
        
    }

    public void ReadInput()
    {
        print(inputF.text);
        if (inputF.text == null)
        {
            Debug.Log("No input text.");
        }
        else if (_objectThatIHit.collider.gameObject.GetComponent<KeyCards>().Text == null)
        {
            Debug.Log("No object or no text of object.");
        }
        else if (inputF.text == "skip" ||
                 inputF.text == "Skip" || _objectThatIHit.collider.gameObject.GetComponent<KeyCards>().Text == inputF.text)
        {
            inputF.DeactivateInputField(true);
            inputF.gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("The answer is not correct. Try again!");
        }
        
    }
} 