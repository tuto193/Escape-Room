using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseKeyForButton : MonoBehaviour
{
    public KeyCode keyName;
    
    void Update() {
        if(Input.GetKeyDown(keyName))
        {
            GetComponent<Button>().onClick.Invoke();
        }

    }
}

