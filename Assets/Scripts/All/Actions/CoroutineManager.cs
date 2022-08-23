using System.Collections;

using UnityEngine;

public sealed class CoroutineManager : MonoBehaviour
{
    private static CoroutineManager m_Instance;
    private static CoroutineManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                var gameObject = new GameObject("Coroutine Manager");
                m_Instance = gameObject.AddComponent<CoroutineManager>();
                DontDestroyOnLoad(gameObject);
            }
            return m_Instance;
        }
    }

    public static Coroutine StartRoutine(IEnumerator enumerator)
    {
        return Instance.StartCoroutine(enumerator);
    }

    public static void StopRoutine(Coroutine coroutine)
    {
        Instance.StopCoroutine(coroutine);
    }
}
