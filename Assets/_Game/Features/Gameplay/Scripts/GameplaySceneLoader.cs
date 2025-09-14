using System.Collections;
using UnityEngine;

public class GameplaySceneLoader : MonoBehaviour {

  void Start() {
    // StartCoroutine(GameManager.Instance.poolManager.InitializeAllPools());
    GameManager.Instance.StartGame();
  }


}
