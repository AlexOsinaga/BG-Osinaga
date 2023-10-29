//**Osinaga Yujra Gaabriel Alex**
/// <summary>
////to those cases where i don't see necesary to use persistan singleton(most cases in this game)
/// </summary>
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
