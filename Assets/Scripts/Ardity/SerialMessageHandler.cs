using UnityEngine;
using System.Collections;

public class SerialMessageHandler : Singleton<SerialMessageHandler>
{
    public SerialController serialController;

    private PunchSystem punchSystem;

    void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();
        punchSystem = FindFirstObjectByType<PunchSystem>();
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
        CheckButton(message);
    }

    private void CheckButton(string message)
    {
        if (message.StartsWith("btn_"))
        {
            string[] parts = message.Split('_');
            if (parts.Length == 2 && int.TryParse(parts[1], out int id))
            {
                // Call the punch system to punch the corresponding hole
                punchSystem.PunchHoleID(id - 1);
            }
            else
            {
                Debug.LogWarning("Invalid button message format: " + message);
            }
        }
    }

    //---------------------------------------------------------------------
    // Send data
    //---------------------------------------------------------------------
    public void SendLedMessage(int id, bool isOn)
    {
        int ledID = id + 1;
        string message = "led_" + ledID + (isOn ? " ON\n" : " OFF\n");

        serialController.SendSerialMessage(message);
    }
}
