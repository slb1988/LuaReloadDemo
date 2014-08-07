using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using LuaInterface;

public class PadBase : MonoBehaviour
{
    [SerializeField]
    TextAsset assertScript;
    public string luaModelName;
// 	[HideInInspector]
// 	public GameObjectDictionary allGameObject = new GameObjectDictionary();
    string modelinitStr;
    public virtual void LoadScript(LuaState l)
    {
        modelinitStr = string.Format("module(\"{0}\",package.seeall)", luaModelName);
        if (assertScript != null)
            l.DoString(modelinitStr + assertScript.text);
    }

    public virtual void InitScript()
    {
        if (string.IsNullOrEmpty(luaModelName) == false)
        {
            LuaFunction func = LuaBindingManager.Instance.getLuaState().GetFunction(luaModelName + ".OnInit");
            if (func != null)
                func.Call();
        }
    }

    public virtual void Awake()
    {
        if (string.IsNullOrEmpty(luaModelName) == false)
        {
            LuaFunction func = LuaBindingManager.Instance.getLuaState().GetFunction(luaModelName + ".OnAwake");
            if (func != null)
                func.Call();
        }
    }

    public virtual void Start()
    {
        if (string.IsNullOrEmpty(luaModelName) == false)
        {
            LuaFunction func = LuaBindingManager.Instance.getLuaState().GetFunction(luaModelName + ".OnStart");
            if (func != null)
                func.Call();
        }
    }

	public virtual void OnEnable()
	{
        if (string.IsNullOrEmpty(luaModelName) == false)
		{
            LuaFunction func = LuaBindingManager.Instance.getLuaState().GetFunction(luaModelName + ".OnEnable");
			if (func != null)
				func.Call();
		}
	}

    public virtual void OnDisable()
    {
        if (string.IsNullOrEmpty(luaModelName) == false)
        {
            LuaFunction func = LuaBindingManager.Instance.getLuaState().GetFunction(luaModelName + ".OnDisable");
            if (func != null)
                func.Call();
        }
    }

    // 显示或关闭面板，如果是显示会把面板在最前面显示（物体上必须有UIPanel）
    public virtual void ShowPad(bool isShow)
    {
        //var isChange = gameObject.activeSelf == isShow;
        gameObject.SetActive(isShow);

        if (isShow)
        {
            if (!string.IsNullOrEmpty(luaModelName))
            {
                LuaFunction func = LuaBindingManager.Instance.getLuaState().GetFunction(luaModelName + ".OnShow");
                if (func != null)
                    func.Call();
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(luaModelName))
            {
                LuaFunction func = LuaBindingManager.Instance.getLuaState().GetFunction(luaModelName + ".OnHide");
                if (func != null)
                    func.Call();
            }
        }
    }
    public virtual void ShowPad(bool isShow, int flag)
    {
        ShowPad(IsShow());
    }

    public virtual void TogglePad()
    {
        ShowPad(!gameObject.activeSelf);
    }

    public bool IsShow()
    {
        return gameObject.activeSelf;
    }
}
