//#define THIS_DEBUG

/// <summary>
/// Singleton v1.0 by Edgar Rodriguez, edgarj_rodriguezg@hotmail.com
/// based on Singleton v1.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Base class to provide a singleton status to an object.
/// 
/// According to Wikipedia:
///     "The singleton pattern is a design pattern that restricts the instantiation of a class to one object. 
///This is useful when exactly one object is needed to coordinate actions across the system."
/// All singletons have also an option that can be marked in the Inspector to keep them alive whenever the current scene changes in Unity. 
/// </summary>

using UnityEngine;

/// <summary>
/// Remember to call base.Awake() in each new Singleton
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance;

    /// <summary>
    /// The lock to prevent concurrency problems.
    /// </summary>
    private static object _lock = new object();

    [Header("Singleton properties")]

    /// <summary>
    /// If true, the singleton won't be destroyed when the scene changes
    /// </summary>
    [Tooltip("If true, the singleton won't be destroyed when the scene changes")]
    [SerializeField]
    protected bool isPersistent = true;

    /// <summary>
    /// Returns the instance of this singleton.
    /// </summary>
    public static T Instance
    {
        get
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (instance == null)
                    {
#if THIS_DEBUG
                        Debug.LogWarning("An instance of " + typeof(T) + " is needed in the scene, but there is none. Generated automatically.");
#endif
                        GameObject obj = new GameObject("Singleton_" + typeof(T));
                        instance = obj.AddComponent(typeof(T)) as T;
                    }
                }
                return instance;
            }
        }
    }

    public static bool Exists
    {
        get { return !Object.ReferenceEquals(instance, null); }
    }

    public bool IsCurrentSingleton()
    {
        if (instance == null)
            return false;

        return instance.gameObject.GetInstanceID() == this.gameObject.GetInstanceID();
    }

    protected virtual void Awake()
    {
        if (instance != null && !IsCurrentSingleton())
        {
#if THIS_DEBUG
                Debug.LogWarning("Warning: More than one instance of singleton " + typeof(T) + " existing.");
#endif
            Destroy(this.gameObject);
        }

        else if (instance == null)
        {
            instance = gameObject.GetComponent<T>(); // AddComponent(typeof(T)) as T;

            if (isPersistent)
                DontDestroyOnLoad(gameObject);
        }
    }

    public virtual void OnDestroy()
    {
        if (IsCurrentSingleton())
        {
            instance = null;
        }
    }

}