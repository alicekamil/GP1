using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{ 
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning($"Found a second instance of {typeof(T)}!");
            Destroy(gameObject);
        }
        Instance = this as T;
    }
}   