using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBooleanStatement : ConditionalStatement
{
    private bool value;

    public SimpleBooleanStatement(bool val)
    {
        this.value = val;
    }

    public override bool GetStatementResult(CharacterCommandManager ccm)
    {
        // ccm not needed 
        return value;
    }
}
