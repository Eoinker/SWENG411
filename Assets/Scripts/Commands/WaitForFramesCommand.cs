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

    public override void Execute(CharacterCommandManager ccm)
    {
        IEnumerator coroutine = ccm.PauseExecutionForFrames(frames);
        ccm.StartCoroutine(coroutine);
    }
}
