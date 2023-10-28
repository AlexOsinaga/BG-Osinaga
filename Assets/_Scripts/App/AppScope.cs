using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AppScope", menuName = "BG/AppScope")]
public class AppScope : ScriptableObject
{
    /*public List<GameObject> prefabsToInject = new List<GameObject>();

    public int PrefabsCount
    {
        get { return prefabsToInject.Count; }
    }

    public GameObject GetPrefab(int index)
    {
        if (index >= 0 && index < prefabsToInject.Count)
        {
            return prefabsToInject[index];
        }
        return null;
    }
    */
    public GameObject mainMenuPrefab;
}
