using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comparer
{
    public bool CompareNumbers(float num1, float num2, ComparisonType compType)
    {
        // Comparison Type is as follows: >, >=, =, <=, <
        // Numbered 0 to 4, using ComparisonType enum
        // Any other number returns false
        switch (compType)
        {
            case ComparisonType.GT:
                return num1 > num2;
            case ComparisonType.GTEQ:
                return num1 >= num2; 
            case ComparisonType.EQ: 
                return num1 == num2;
            case ComparisonType.LTEQ:
                return num1 <= num2;
            case ComparisonType.LT:
                return num1 < num2;
            default:
                return false;
        }
    }
}

public enum ComparisonType {
    GT,
    GTEQ,
    EQ,
    LTEQ,
    LT
}