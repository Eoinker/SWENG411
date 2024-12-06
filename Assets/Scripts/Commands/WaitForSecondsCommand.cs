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

    public override void Execute(CharacterCommandManager ccm)
    {
        IEnumerator coroutine = ccm.controller.PauseExecutionForSeconds(delay);
        ccm.controller.StartCoroutine(coroutine);
    }
}
