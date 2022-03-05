using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientTrigger : MonoBehaviour
{
    public void OnSelect(string sceneName)
    {
        FindObjectOfType<CameraMoveManager>().GoToVR(sceneName);
    }
}
