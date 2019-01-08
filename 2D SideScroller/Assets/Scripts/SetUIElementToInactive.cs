using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUIElementToInactive : MonoBehaviour
{
    private bool currentUIElementState;
    // Start is called before the first frame update
    void Start()
    {
        currentUIElementState = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleUIElementActive()
    {
        currentUIElementState = !currentUIElementState;

        gameObject.SetActive(currentUIElementState);
    }
}
