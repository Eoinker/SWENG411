using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using MoonSharp.Interpreter;

public class CodeCompiler : MonoBehaviour
{
    private UIDocument doc;
    private Button compileButton;
    private Script luaScript;

    void Awake()
    {
        doc = GetComponent<UIDocument>();
        compileButton = doc.Q<Button>("Compile Button");
        compileButton.clicked += CompileCode;

        luaScript = new Script();
        RegisterLuaFunctions();
    }

    void CompileCode()
    {
        // Example code lines, replace with actual input from user
        string luaCode = @"
            function main()
                move_forward()
                if condition() then
                    move_left()
                end
                for i = 1, 3 do
                    move_right()
                end
            end
        ";

        luaScript.DoString(luaCode);
        DynValue function = luaScript.Globals.Get("main");
        luaScript.Call(function);
    }

    void RegisterLuaFunctions()
    {
        luaScript.Globals["move_forward"] = (System.Action)MoveForward;
        luaScript.Globals["move_left"] = (System.Action)MoveLeft;
        luaScript.Globals["move_right"] = (System.Action)MoveRight;
        luaScript.Globals["condition"] = (System.Func<bool>)Condition;
    }

    void MoveForward()
    {
        // Implement player movement forward
    }

    void MoveLeft()
    {
        // Implement player movement left
    }

    void MoveRight()
    {
        // Implement player movement right
    }

    bool Condition()
    {
        // Implement condition logic
        return true;
    }
}
