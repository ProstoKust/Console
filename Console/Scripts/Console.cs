using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Console : MonoBehaviour
{
    public static Console instance;

    [Header("Console Elements")]
    [SerializeField] private TMP_Text consoleText;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text tooltip;
    [SerializeField] private Button sendButton;

    [Header("Console Colors")]
    [SerializeField] private Color linkColor;
    [SerializeField] private Color dateColor;
    [SerializeField] private Color dangerColor;

    [Header("System Messages")]
    [SerializeField] private string startMessage;

    [Header("Contents List")]
    [SerializeField] private GameObject listPanel;
    [SerializeField] private GameObject contentElement;

    private string request;

    private List<Command> Commands = new();
    private List<GameObject> listPanelCommands = new();

    private ConsoleInputSystem consoleInputSystem;
    
    void Start()
    {
        Console.instance.WriteConsole(startMessage);
    }

    private void OnDisable()
    {
        inputField.text = string.Empty;
        listPanel.SetActive(false);
        ClearConsole();
    }

    private void Awake()
    {
        instance = this;

        consoleInputSystem = new ConsoleInputSystem();
        consoleInputSystem.Console.Enable();

        FindCommands();
        AddListeners();
    }

    private void HelpAppend()
    { 
        inputField.text = tooltip.text;
        inputField.MoveTextEnd(false);
    }

    private void FindCommands()
    {
        var MonoBehaviours = FindObjectsOfType<MonoBehaviour>();

        foreach (var MonoBehaviour in MonoBehaviours)
        {
            var methods = MonoBehaviour.GetType()
                .GetMethods()
                .Where(method => Attribute.IsDefined(method, typeof(CommandAttribute)));
            foreach (var method in methods)
            {
                var attribute = (CommandAttribute)Attribute.GetCustomAttribute(method, typeof(CommandAttribute));
                Commands.Add(new Command(MonoBehaviour, method, attribute.ParamsTypes));
            }
        }
    }

    private void AddListeners()
    {
        consoleInputSystem.Console.AppendText.performed += e => HelpAppend();
        consoleInputSystem.Console.InputText.performed += e =>
        {
            sendButton?.onClick.Invoke();
            UnityEngine.EventSystems.
            EventSystem.current.SetSelectedGameObject(null);
        };
        sendButton.onClick.AddListener(Send);
        inputField.onValueChanged.AddListener(InputValueChanged);
        inputField.onSelect.AddListener(e =>
        {
            inputField.text = string.Empty;
            listPanel.SetActive(true);
            InputValueChanged(string.Empty);
        });
        inputField.onDeselect.AddListener(e =>
        { 
            listPanel.SetActive(false);
            tooltip.text = string.Empty;
        });
    }

    private object[] GetParameters(List<Type> Types, List<string> Strings)
    {
        object[] Parameters = new object[Types.Count];
        if (Types.Count != Strings.Count)
        {
            WriteConsole($"Invalid number of attributes. Specify {Types.Count} attributes", dangerColor);
            return null;
        }
        else
        {
            for (int i = 0; i < Types.Count; i++)
            {
                try
                {
                    if (Types[i] == typeof(string)) Parameters[i] = Strings[i];
                    else if (Strings[i] == "null") Parameters[i] = null;
                    else if (Types[i] == typeof(int)) Parameters[i] = int.Parse(Strings[i]);
                    else if (Types[i] == typeof(float)) Parameters[i] = float.Parse(Strings[i]);
                    else if (Types[i] == typeof(double)) Parameters[i] = double.Parse(Strings[i]);
                    else if (Types[i] == typeof(GameObject)) Parameters[i] = Resources.Load($"Prefabs/{Strings[i]}") as GameObject;
                    else if (Types[i] == typeof(Transform)) Parameters[i] = GameObject.Find(Strings[i]).transform;
                }
                catch
                {
                    WriteConsole("Invalid attribute value or type", dangerColor);
                    return null;
                }
            }
            return Parameters;
        }
    }

    private void Send() => Send(inputField.text);

    private void Send(string Text)
    {
        List<String> Strings = new();
        Strings.AddRange(Text.Split(' '));
        inputField.text = string.Empty;

        IEnumerable<Command> SendCommads = Commands.OfType<Command>().Where(i => i.Method.Name == Strings[0]);
        if (SendCommads.Count() == 0)
        {
            WriteConsole("Requested command was not found", dangerColor);
            return;
        }
        List<String> tempList = new();
        tempList.AddRange(Strings.Skip(1));
        object[] param = GetParameters(SendCommads.First().ParametersType, tempList);
        if (param == null)
        {
            return;
        }
        SendCommads.First().Method.Invoke(SendCommads.First().Target, param);
    }

    private void InputValueChanged(string value)
    {
        for (int i = 0; i < listPanelCommands.Count; i++)
        {
            Destroy(listPanelCommands[i]);
        }
        listPanelCommands.Clear();

        List<Command> SendCommads = new();
        SendCommads.AddRange(Commands.OfType<Command>().Where(i => i.Method.Name.StartsWith(value)));

        for (int i = 0; i < SendCommads.Count(); i++)
        {
            TMP_Text TempText = Instantiate(contentElement, listPanel.transform).GetComponent<TMP_Text>();
            listPanelCommands.Add(TempText.gameObject);
            TempText.text = SendCommads[i].Method.Name;
        }

        if(SendCommads.Count == 0 || string.IsNullOrEmpty(inputField.text))
        {
            tooltip.text = string.Empty;
            return;
        }
        tooltip.text = SendCommads.First().Method.Name;
    }

    public void WriteConsole(string Text)
    {
        consoleText.text += $"\n<color=#{ColorUtility.ToHtmlStringRGBA(dateColor)}>[{DateTime.Now.ToString("HH:mm:ss")}]</color> {Text}";
    }

    public void WriteConsole(string Text, Color TextColor)
    {
        consoleText.text += $"\n<color=#{ColorUtility.ToHtmlStringRGBA(dateColor)}>[{DateTime.Now.ToString("HH:mm:ss")}]</color> <color=#{ColorUtility.ToHtmlStringRGBA(TextColor)}>{Text}</color>";
    }

    public void WriteConsoleIgnore(string Text)
    {
        consoleText.text += $"\n{Text}";
    }

    public void ClearConsole()
    {
        consoleText.text = string.Empty;
    }

    public List<Command> GetCommands()
    {
        List<Command> temp = new();
        temp.AddRange(Commands.ToArray());
        return temp;
    }
}

public struct Command
{
    public Command(object _Target, MethodInfo _Method, List<Type> _ParametersType)
    {
        Target = _Target;
        Method = _Method;
        ParametersType = _ParametersType;
    }
    public object Target;
    public MethodInfo Method;
    public List<Type> ParametersType;
}