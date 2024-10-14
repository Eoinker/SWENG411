using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForSecondsCommand : CharacterCommand
{
    private float delay;

    public WaitForSecondsCommand(float seconds)
    {
        this.delay = seconds;
    }

    public override void Execute(CharacterControl controller)
    {
        IEnumerator coroutine = controller.PauseExecutionForSeconds(delay);
        controller.StartCoroutine(coroutine);
    }
}
