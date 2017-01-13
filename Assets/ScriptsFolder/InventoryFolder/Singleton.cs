using UnityEngine;
using System.Collections;

/// <summary>
/// class를 싱글톤으로 만든다.   
///   예) class MyClass : Singleton<MyClass>  { }
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static string singletonRoot = "SingletonRoot";

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                var type = typeof(T);
                var objects = FindObjectsOfType<T>();

                if (objects.Length > 0)
                {
                    _instance = objects[0];
                    if (objects.Length > 1)
                    {
                        Debug.LogWarning("There is more than one instance of Singleton of type \"" + type + "\". Keeping the first. Destroying the others.");
                        for (var i = 1; i < objects.Length; i++)
                            DestroyImmediate(objects[i].gameObject);
                    }
                    return _instance;
                }

                string gameObjectName = "ScriptObject";
                GameObject gameObject = GameObject.Find(gameObjectName);
                if (gameObject == null)
                {
                    gameObject = new GameObject();
                    gameObject.name = gameObjectName;
                } else
                {

                }
                /*
                var gameObject = new GameObject();
                gameObject.name = type.ToString();
                */
                _instance = gameObject.AddComponent<T>();
                DontDestroyOnLoad(gameObject);
            }
            return _instance;
        }
    }
}