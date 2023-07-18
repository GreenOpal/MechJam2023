using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour
    where T : SingletonMonoBehaviour<T>
{
    private static T _instance;

    public static bool HasInstance => _instance != null;
    public static T Instance => _instance != null ? _instance : _instance = FindObjectOfType<T>();


    protected virtual void Awake()
    {
        if (_instance == null || _instance == (T)this)
        {
            _instance = (T)this;
        }
        else
        {
            Debug.LogWarning($"Instance of type <{this.GetType()}> already exists: {_instance}");
        }
    }
    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
        else
        {
            Debug.LogWarning($"Instance of type <{this.GetType()}> has changed: {_instance} != {this}");
        }

    }
}
