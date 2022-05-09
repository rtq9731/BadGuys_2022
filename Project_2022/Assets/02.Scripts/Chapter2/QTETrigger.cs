using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTETrigger : MonoBehaviour
{
    QTEManager QTEManager;

    // Start is called before the first frame update
    void Start()
    {
        QTEManager = FindObjectOfType<QTEManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            QTEManager.GenerateQTEEvent();
        }    
    }
}
