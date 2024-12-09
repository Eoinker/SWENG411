using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCommandManager : MonoBehaviour
{
    public CharacterControl controller;
    private Queue<CharacterCommand> commands;
    private Stack<CharacterCommand> runtimeStack;
    private bool isExecuting;

    void Awake()
    {
        controller = GetComponent<CharacterControl>();
        commands = new Queue<CharacterCommand>();
        runtimeStack = new Stack<CharacterCommand>();
    }

    void FixedUpdate()
    {
        if (isExecuting == true && (runtimeStack.Count + commands.Count) > 0)
        {
            if (runtimeStack.Count > 0)
            {
                CharacterCommand command = runtimeStack.Pop();
                command.Execute(this);
            } else if (commands.Count > 0)
            {
                CharacterCommand command = commands.Dequeue();
                command.Execute(this);
            }
        }
    }

    public void SetCommandQueue(Queue<CharacterCommand> comQ)
    {
        commands = comQ;
    }
    
    public void PushToRuntimeStack(CharacterCommand command)
    {
        runtimeStack.Push(command);
    }

    #region Execution

    public bool IsExecuting()
    {
        return isExecuting;
    }

    public void BeginExecution()
    {
        isExecuting = true;
    }

    public void StopExecuting()
    {
        isExecuting = false;
    }

    private void ResumeExecution()
    {
        isExecuting = true;
    }

    private void PauseExecution()
    {
        isExecuting = false;
    }   

    // Pause Execution and then resume after a delay amount in seconds
    public IEnumerator PauseExecutionForSeconds(float delay)
    {
        PauseExecution();
        yield return new WaitForSeconds(delay);
        ResumeExecution();
    }

    // Pause Execution and then resume after a delay amount in frames
    public IEnumerator PauseExecutionForFrames(int frames)
    {
        PauseExecution();
        yield return frames;
        ResumeExecution();
    }

    #endregion
}
