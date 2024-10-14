using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForFramesCommand : CharacterCommand
{
    private int frames;

    public WaitForFramesCommand(int frames)
    {
        this.frames = frames;
    }

    public override void Execute(CharacterControl controller)
    {
        IEnumerator coroutine = controller.PauseExecutionForFrames(frames);
        controller.StartCoroutine(coroutine);
    }
}
