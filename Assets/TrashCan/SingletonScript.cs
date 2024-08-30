using UnityEngine;

// 하나의 인스턴스만 존재하도록 보장하는 싱글톤 스크립트
public class SingletonScript : MonoBehaviour
{
    static SingletonScript _instance;

    public static SingletonScript instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SingletonScript>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("SingletonPrefab");
                    _instance = singletonObject.AddComponent<SingletonScript>();
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Your other methods or functionality here
}