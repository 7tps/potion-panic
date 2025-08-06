using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public static ProgressBar instance;

    public Image progressBar;
    public bool isCustomer = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateProgress(float progress)
    {
        progressBar.fillAmount = Mathf.Clamp01(progress);
        if (isCustomer)
        {
            if (progressBar.fillAmount > .5)
            {
                progressBar.color = Color.green;
            }
            else if (progressBar.fillAmount > .2)
            {
                progressBar.color = Color.yellow;
            }
            else
            {
                progressBar.color = Color.red;
            }
        }
    }
}
