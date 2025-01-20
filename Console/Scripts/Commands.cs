using System.Collections.Generic;
using UnityEngine;

public class Commands : MonoBehaviour
{
    [Command]
    public void help()
    {
        Console.instance.WriteConsole("Help");
        List<Command> commands = Console.instance.GetCommands();
        foreach(Command command in commands)
        {
            string temp = "<color=#FFFFFF>" + command.Method.Name + "(</color>";

            for (int i = 0; i < command.ParametersType.Count; i++)
            {
                temp += $"<color=#FFC500>{command.ParametersType[i].Name}</color>";
                if(i!=command.ParametersType.Count-1) temp += "<color=#FFFFFF>, </color>";
            }
            temp += "<color=#FFFFFF>)</color>";
            Console.instance.WriteConsoleIgnore(temp);
        }
    }

    [Command]
    public void clear()
    {
        Console.instance.ClearConsole();
    }
}