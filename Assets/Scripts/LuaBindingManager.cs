using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LuaInterface;
using System;

public class LuaBindingManager : MonoBehaviour
{
    // lua vm接口
    LuaState m_l;

    #region 全局数据区

    public List<PadBase> luaPanel = new List<PadBase>();

    #endregion

    private static LuaBindingManager _instacne;
    public static LuaBindingManager Instance
    {
        get
        {
            if (_instacne == null)
            {
                var go = GameObject.Find("UIRoot");
                if (go != null)
                    _instacne = go.GetComponent<LuaBindingManager>();
            }

            return _instacne;
        }
    }

    public LuaState getLuaState()
    {
        return m_l;
    }

    void Start()
    {
        set_state(load_script());
    }

    public void reload()
    {
        set_state(load_script());
    }
    
	LuaState load_script()
	{
        LuaState newL = new LuaState();
        newL.DoString("require 'global'");

        foreach(var pad in luaPanel)
        {
            pad.LoadScript(newL);
        }

        return newL;
    }

    public void set_state(LuaState l)
    {
        if (l == null) return;
        LuaState ol = m_l;
        m_l = l;
        if (ol != null)
        {
            ol.Dispose();
        }

        LuaFunction func = m_l.GetFunction("oninit");
        func.Call();

        foreach (var pad in luaPanel)
        {
            pad.InitScript();
        }
    }

    void Destory()
    {
        m_l.Dispose();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            reload();
    }

    public static void RegisterCtoLuaFunction(string luaFuncName, object target, string csharpFuncName)
    {
        LuaBindingManager.Instance.getLuaState().RegisterFunction(luaFuncName,
            target,
            target.GetType().GetMethod(csharpFuncName));
    }

    public static void CallLuaFunction(string funcName, object args)
    {
        LuaFunction func = LuaBindingManager.Instance.getLuaState().GetFunction(funcName);
        if (func == null)
        {
            Debug.LogWarning(string.Format("{0} does not exist. {1}", funcName, args));
            return;
        }
        func.Call(args);
    }

    public static object GetComponentFromLua(GameObject parentObj, string typename, string targetname)
    {
        Type t = Type.GetType(typename);
        if (t == null)
            return null;
        Component[] ps = parentObj.GetComponentsInChildren(t);
        foreach (var p in ps)
        {
            if (p.name == targetname)
                return p.gameObject.GetComponent(t);
        }
        return null;
    }

}
