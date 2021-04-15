using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{

    public int maximum;

    public int current;

    public Image mask;

    public TextMeshProUGUI numberOfSolvedRiddles;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
        numberOfSolvedRiddles.text = current.ToString() + "/6";
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)current/ (float) maximum;
        mask.fillAmount = fillAmount;
    }
}
