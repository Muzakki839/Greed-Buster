using UnityEngine;
using System.Collections;

public class SerialMessageHandler : Singleton<SerialMessageHandler>
{
    public SerialController serialController;

    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
    }

    //---------------------------------------------------------------------
    // Receive data
    //---------------------------------------------------------------------
    void Update()
    {
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

    //---------------------------------------------------------------------
    // Send data
    //---------------------------------------------------------------------
    public void SendLedMessage(int id, bool isOn)
    {
        if (isOn)
            serialController.SendSerialMessage("l" + id + "_on\n");
        else
            serialController.SendSerialMessage("l" + id + "_off\n");
    }
}
