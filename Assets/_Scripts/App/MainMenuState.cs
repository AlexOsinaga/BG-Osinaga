using UnityEngine;
using Cysharp.Threading.Tasks;

public class MainMenuState : StaticInstance<MainMenuState> {

  private AppScope appScope;
  
  /*void Awake() {
    appScope = Resources.Load<AppScope>("AppScope");
  
    Debug.Log("AppScope: " + appScope); 
  }

    public void SpawnMainMenu(AppScope appScope) {
        Instantiate(appScope.mainMenuPrefab);
    }
*/
}
