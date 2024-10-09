using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterCommand
{
   protected CharacterControl controller;
   public abstract void Execute();
}
