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

        // read RFID msg "mulai" or read button msg "btn_1""
        switch (gameState)
        {
            case GameState.TapCard:
                if (message.Trim() == "mulai")
                {
                    AnyButtonPressedEvent?.Invoke();
                }
                break;
            default:
                CheckButton(message);
                break;
        }
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
                    case ButtonScheme.None:
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
        AnyButtonPressedEvent?.Invoke();
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
        string message = state switch
        {
            GameState.TapCard => "sebelumMain_ON\n",
            GameState.Game => "mainGame_ON\n",
            GameState.Win => "menang_ON\n",
            GameState.Lose => "kalah_ON\n",
            GameState.InputName => "pilihNama_ON\n",
            GameState.Leaderboard => "highscore_ON\n",
            _ => "sebelumMain_ON\n",
        };
        serialController.SendSerialMessage(message);
    }

    public void SendCurrentGameState()
    {
        string message = gameState switch
        {
            GameState.TapCard => "sebelumMain_ON\n",
            GameState.Game => "mainGame_ON\n",
            GameState.Win => "menang_ON\n",
            GameState.Lose => "kalah_ON\n",
            GameState.InputName => "pilihNama_ON\n",
            GameState.Leaderboard => "highscore_ON\n",
            _ => "sebelumMain_ON\n",
        };
        serialController.SendSerialMessage(message);
    }

    public void SendGameState(int id)
    {
        string message = id switch
        {
            0 => "sebelumMain_ON\n",
            1 => "mainGame_ON\n",
            2 => "menang_ON\n",
            3 => "kalah_ON\n",
            4 => "pilihNama_ON\n",
            5 => "highscore_ON\n",
            _ => "sebelumMain_ON\n",
        };
        serialController.SendSerialMessage(message);
    }
}
