using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RobotController : MonoBehaviour
{
    public RobotMovement robotMovement;
    public TMP_InputField commandInputField;
    public TextMeshProUGUI outputText;

    private Queue<RobotCommand> moveCommands = new Queue<RobotCommand>();
    private bool isExecuting = false;

    private List<string> outputLines = new List<string>();
    private const int maxOutputLines = 12; 

    void Update()
    {
        if (commandInputField.isFocused)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                bool shiftHeld = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
                if (shiftHeld)
                {
                    int caretPosition = commandInputField.caretPosition;
                    string currentText = commandInputField.text;
                    commandInputField.text = currentText.Insert(caretPosition, "\n");
                    commandInputField.caretPosition = caretPosition + 1;
                }
                else
                {
                    SubmitCommands();
                    UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
                }
            }
        }
    }

    void SubmitCommands()
    {
        string input = commandInputField.text;
        if (string.IsNullOrWhiteSpace(input))
            return;

        outputLines.Clear(); 
        UpdateOutputText();

        ParseCommands(input);
        commandInputField.text = "";

        if (!isExecuting)
            StartCoroutine(ExecuteCommands());
    }

    void ParseCommands(string input)
    {
        var lines = input.Split('\n');
        Stack<int> repeatStack = new Stack<int>();
        List<string> expandedLines = new List<string>();

        for (int i = 0; i < lines.Length; i++)
        {
            var line = lines[i].Trim();
            if (string.IsNullOrWhiteSpace(line)) continue;

            if (line.StartsWith("repeat"))
            {
                var parts = line.Split(' ');
                if (parts.Length == 2 && int.TryParse(parts[1], out int count))
                {
                    repeatStack.Push(count);
                    expandedLines.Add("REPEAT_BEGIN");
                }
                else
                {
                    AppendOutput("Comanda repeat invalida. Foloseste: repeat n");
                    return;
                }
            }
            else if (line == "end")
            {
                if (repeatStack.Count > 0)
                {
                    int count = repeatStack.Pop();
                    int start = expandedLines.LastIndexOf("REPEAT_BEGIN") + 1;
                    var block = expandedLines.GetRange(start, expandedLines.Count - start);

                    for (int j = 0; j < count - 1; j++)
                        expandedLines.AddRange(block);

                    expandedLines.RemoveAt(start - 1);
                }
                else
                {
                    expandedLines.Add("end");
                }
            }
            else
            {
                expandedLines.Add(line);
            }
        }

        Stack<bool> ifConditionStack = new Stack<bool>();
        bool currentCondition = true;

        foreach (var rawLine in expandedLines)
        {
            var line = rawLine.Trim();

            if (line.StartsWith("if position =="))
            {
                var parts = line.Substring("if position ==".Length).Trim().Split(' ');
                if (parts.Length == 3 &&
                    float.TryParse(parts[0], out float x) &&
                    float.TryParse(parts[1], out float y) &&
                    float.TryParse(parts[2], out float z))
                {
                    ifConditionStack.Push(currentCondition);
                    Vector3 target = new Vector3(x, y, z);
                    currentCondition = Vector3.Distance(robotMovement.transform.position, target) < 0.1f;
                }
                else
                {
                    AppendOutput("Comanda if invalida. Foloseste: if position == x y z");
                    return;
                }
            }
            else if (line == "end")
            {
                if (ifConditionStack.Count > 0)
                    currentCondition = ifConditionStack.Pop();
                else
                    AppendOutput("Comanda 'end' fara 'if' sau 'repeat'");
            }
            else if (!currentCondition)
            {
                continue;
            }
            else if (line.StartsWith("moveTo"))
            {
                var parts = line.Split(' ');
                if (parts.Length == 4 &&
                    float.TryParse(parts[1], out float x) &&
                    float.TryParse(parts[2], out float y) &&
                    float.TryParse(parts[3], out float z))
                {
                    float clampedY = Mathf.Max(0, y);
                    moveCommands.Enqueue(new RobotCommand_MoveTo(new Vector3(x, clampedY, z)));
                    AppendOutput($"Queued moveTo {x} {clampedY} {z}");
                }
                else
                {
                    AppendOutput("Coordonate invalide in moveTo");
                }
            }
            else if (line.StartsWith("wait"))
            {
                var parts = line.Split(' ');
                if (parts.Length == 2 && float.TryParse(parts[1], out float seconds))
                {
                    moveCommands.Enqueue(new RobotCommand_Wait(seconds));
                    AppendOutput($"Queued wait {seconds} secunde");
                }
                else
                {
                    AppendOutput("Comanda invalida. Foloseste: wait n");
                }
            }
            else if (line.StartsWith("say"))
            {
                string message = line.Substring(4);
                moveCommands.Enqueue(new RobotCommand_Say(message));
                AppendOutput($"Robotul va spune: {message}");
            }
            else
            {
                AppendOutput($"Comanda necunoscuta: {line}");
            }
        }
    }

    IEnumerator ExecuteCommands()
    {
        isExecuting = true;

        while (moveCommands.Count > 0)
        {
            RobotCommand command = moveCommands.Dequeue();
            yield return command.Execute(robotMovement, AppendOutput);
        }

        isExecuting = false;
    }

    void AppendOutput(string message)
    {
        if (outputText != null)
        {
            outputLines.Add(message);
            if (outputLines.Count > maxOutputLines)
                outputLines.RemoveAt(0); 

            UpdateOutputText();
        }
    }

    void UpdateOutputText()
    {
        outputText.text = string.Join("\n", outputLines);
    }
}

// COMMAND BASE CLASSES

public abstract class RobotCommand
{
    public abstract IEnumerator Execute(RobotMovement movement, System.Action<string> outputCallback);
}

public class RobotCommand_MoveTo : RobotCommand
{
    private Vector3 target;
    public RobotCommand_MoveTo(Vector3 target) => this.target = target;

    public override IEnumerator Execute(RobotMovement movement, System.Action<string> outputCallback)
    {
        Vector3 direction = (target - movement.transform.position).normalized;
        
        if (direction.magnitude > 0.1f)
        {
            Vector3 horizontalDirection = new Vector3(direction.x, 0, direction.z).normalized;
            if (horizontalDirection.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(horizontalDirection);
                movement.RotateTowards(targetRotation);
                
                while (movement.IsRotating())
                    yield return null;
            }
        }

        movement.MoveTo(target);
        while (movement.IsMoving())
            yield return null;

        outputCallback?.Invoke($"Ajuns la {target.x} {target.y} {target.z}");
    }
}

public class RobotCommand_Wait : RobotCommand
{
    private float duration;
    public RobotCommand_Wait(float duration) => this.duration = duration;

    public override IEnumerator Execute(RobotMovement movement, System.Action<string> outputCallback)
    {
        outputCallback?.Invoke($"Asteapta {duration} secunde...");
        yield return new WaitForSeconds(duration);
    }
}

public class RobotCommand_Say : RobotCommand
{
    private string message;
    public RobotCommand_Say(string message) => this.message = message;

    public override IEnumerator Execute(RobotMovement movement, System.Action<string> outputCallback)
    {
        outputCallback?.Invoke(message);
        yield return null;
    }
}
