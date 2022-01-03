using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIStackManager
{
    static Stack<StackableUI> UIStack = new Stack<StackableUI>();

    public static void ClearUIStack()
    {
        SceneManager.sceneLoaded += (x, y) => UIStack = new Stack<StackableUI>();
    }

    public static void AddUIToStack(StackableUI item)
    {
        UIStack.Push(item);
    }

    public static bool IsUIStackEmpty()
    {
        return UIStack.Count <= 0 ? true : false;
    }

    public static StackableUI GetTopUI()
    {
        if(UIStack.Count <= 0)
        {
            return null;
        }
        return UIStack.Peek();
    }

    public static void Clear()
    {
        while(!IsUIStackEmpty())
        {
            RemoveUIOnTop();
        }
    }

    public static bool RemoveUIOnTop()
    {
        if (!IsUIStackEmpty())
        {
            StackableUI obj = UIStack.Pop();
            DOTween.Kill(obj);
            obj.transform.DOScale(0, 0.3f).OnComplete(() => obj.gameObject.SetActive(false));
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void RemoveUIOnTopWithNoTime()
    {
        if (!IsUIStackEmpty())
        {
            StackableUI obj = UIStack.Pop();
            DOTween.Kill(obj);
            obj.gameObject.SetActive(false);
        }
    }

}
