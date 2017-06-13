using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using   UnityEngine.EventSystems;
public class NFCScanning : MonoBehaviour
{
    public UnityEvent OnScanned;

  
        // Use this for initialization
    void Start()
    {
        AndroidNFCReader.enableBackgroundScan();
        AndroidNFCReader.ScanNFC(gameObject.name, "OnFinishScan");

    }

    void OnFinishScan(string result)
    {

        // Cancelled
        if (result == AndroidNFCReader.CANCELLED)
        {

            // Error
        }
        else if (result == AndroidNFCReader.ERROR)
        {


            // No hardware
        }
        else if (result == AndroidNFCReader.NO_HARDWARE)
        {
        }
        //EnteredRoom();

        // Scanned.text = ("game will start in"+result);
        if (result == "poop")
        {
            OnScanned.Invoke();
        }
        //qrString =  (result);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnScanned.Invoke();
        }
    }
}
