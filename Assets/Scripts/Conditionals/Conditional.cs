using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Conditional
{
    public abstract bool GetValue(CharacterCommandManager ccm);


    protected bool CompareNumbers(float num1, float num2, int comparisonType)
    {
        // Comparison Type is as follows: >, >=, =, <=, <
        // Numbered 0 to 4
        switch (comparisonType)
        {
            case 0:
                return num1 > num2;
            case 1:
                return num1 >= num2;
            case 2: 
                return num1 == num2;
            case 3:
                return num1 <= num2;
            case 4:
                return num1 > num2;
            default:
                return false;
        }
    }
}
