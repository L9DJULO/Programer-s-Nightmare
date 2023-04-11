using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class INPUTFORWARD : MonoBehaviour
{
    public GameObject detectInput;
    public GameObject CtrlSettings;

    private void Start()
    {
        detectInput.SetActive(true);
        CtrlSettings.SetActive(false);
    }
    private void Update()
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                INPUTS.forward = kcode;
                detectInput.SetActive(false);
                CtrlSettings.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}
