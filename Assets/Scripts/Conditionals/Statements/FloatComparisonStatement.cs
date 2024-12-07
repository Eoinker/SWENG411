using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatComparisonStatement : ConditionalStatement
{
    private ConditionalStatementValue CSV1, CSV2;
    private ComparisonType comparisonType;
    public FloatComparisonStatement(ConditionalStatementValue csv1, ConditionalStatementValue csv2, ComparisonType comp)
    {
        CSV1 = csv1;
        CSV2 = csv2;
        comparisonType = comp;
    }

    public override bool GetStatementResult(CharacterCommandManager ccm)
    {
        float val1 = CSV1.GetValue(ccm);
        float val2 = CSV2.GetValue(ccm);

        return new Comparer().CompareNumbers(val1, val2, comparisonType);
    }
}
