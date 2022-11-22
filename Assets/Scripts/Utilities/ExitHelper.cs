#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;


public class ExitHelper : MonoBehaviour
{
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}