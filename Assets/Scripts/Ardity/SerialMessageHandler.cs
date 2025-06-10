using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public partial class SerialMessageHandler : Singleton<SerialMessageHandler>
{
    public SerialController serialController;
    public GameState gameState;
    public ButtonScheme buttonScheme;
    [Header("Optional")]
    [SerializeField] private UnityEvent AnyButtonPressedEvent;

    private PunchSystem punchSystem;

    private void Start()
    {
        serialController = GameObject.Find("SerialController").GetComponent<SerialController>();

        if (ButtonScheme.Punch == buttonScheme)
        {
            punchSystem = FindFirstObjectByType<PunchSystem>();
        }

        SendGameState(gameState);
    }

    //---------------------------------------------------------------------
    // Receive data
    //---------------------------------------------------------------------
    public void Update()
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
                // button action  based on the scheme
                switch (buttonScheme)
                {
                    case ButtonScheme.Punch:
                        punchSystem?.PunchHoleID(id - 1);
                        break;
                    case ButtonScheme.InputName:
                        if (KeySimulator.Instance != null)
                        {
                            SimulatePressKey(id);
                        }
                        break;
                    case ButtonScheme.AnyButton:
                        AnyButtonPressedEvent?.Invoke();
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Debug.LogWarning("Invalid button message format: " + message);
            }
        }
    }

    public void InvokeAnyButtonEvent()
    {
        if (buttonScheme == ButtonScheme.AnyButton)
        {
            AnyButtonPressedEvent?.Invoke();
        }
    }

    /// <summary>
    /// Simulate key press based on the push button ID
    /// </summary>
    /// <param name="id"></param>
    private void SimulatePressKey(int id)
    {
        Key key = id switch
        {
            2 => Key.UpArrow,
            4 => Key.LeftArrow,
            6 => Key.RightArrow,
            8 => Key.DownArrow,
            5 => Key.Enter,
            _ => Key.None
        };

        if (key != Key.None)
        {
            StartCoroutine(KeySimulator.Instance.SimulateKeyPressCoroutine(key));
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

    public void SendGameState(GameState state)
    {
        string message = gameState switch
        {
            GameState.TapCard => "sebelumMain\n",
            GameState.Game => "mainGame\n",
            GameState.Win => "menang\n",
            GameState.Lose => "kalah\n",
            GameState.InputName => "highscore\n",
            GameState.Leaderboard => "pilihNama\n",
            _ => "sebelumMain\n",
        };
        serialController.SendSerialMessage(message);
    }
}
