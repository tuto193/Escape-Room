using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collecting : MonoBehaviour
{
    private int _winningThreshold;
    private int _collected;

    private bool _wonTheGame;
    // Start is called before the first frame update
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
            Debug.Log("You won!");
            Debug.Log(string.Format("You took : {} seconds", Time.realtimeSinceStartup));
            _wonTheGame = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            Destroy(other.gameObject);
            _collected++;
        }
    }
}
