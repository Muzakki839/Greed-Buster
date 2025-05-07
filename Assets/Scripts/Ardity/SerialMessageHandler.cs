using UnityEngine;
using System.Collections;

public class SerialMessageHandler : Singleton<SerialMessageHandler>
{
    public SerialController serialController;

    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }

    void Update()
    {
        //---------------------------------------------------------------------
        // Send data
        //---------------------------------------------------------------------

        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     Debug.Log("Sending A");
        //     serialController.SendSerialMessage("A");
        // }

        // if (Input.GetKeyDown(KeyCode.Z))
        // {
        //     Debug.Log("Sending Z");
        //     serialController.SendSerialMessage("Z");
        // }

        // if (MoleSpawner.Instance.IsSpawnPointOccupied(0))
        // {
        //     Debug.Log("Sending l0_on");
        //     serialController.SendSerialMessage("l0_on\n");
        // } else
        // {
        //     Debug.Log("Sending l0_off");
        //     serialController.SendSerialMessage("l0_off\n");
        // }


        //---------------------------------------------------------------------
        // Receive data
        //---------------------------------------------------------------------

        string message = serialController.ReadSerialMessage();

        if (message == null)
            return;

        // Check if the message is plain data or a connect/disconnect event.
        if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_CONNECTED))
            Debug.Log("Connection established");
        else if (ReferenceEquals(message, SerialController.SERIAL_DEVICE_DISCONNECTED))
            Debug.Log("Connection attempt failed or disconnection detected");
        else
            Debug.Log("Message arrived: " + message);
    }
}
