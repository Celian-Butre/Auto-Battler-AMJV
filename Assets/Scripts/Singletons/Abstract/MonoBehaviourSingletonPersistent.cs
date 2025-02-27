using UnityEngine;

public class MonoBehaviourSingletonPersistent<T> : MonoBehaviour
    where T : Component
{
    public static T Instance { get; private set; }
	
    public virtual void Awake ()
    {
        if (Instance == null) {
            Instance = this as T;
            Instance.name = typeof(T).Name;
            DontDestroyOnLoad (this);
        } else {
            Destroy (gameObject);
        }
    }
}
