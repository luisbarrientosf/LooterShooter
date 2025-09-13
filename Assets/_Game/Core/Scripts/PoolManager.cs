using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour {
  public PoolManager Instance;
  public List<ObjectPool> pools;

  void Awake() {
    if (Instance == null) {
      Instance = this;
    }
    else {
      Destroy(gameObject);
    }
    GameManager.Instance.poolManager = this;
  }

  public IEnumerator InitializeAllPools() {
    foreach (var pool in pools) {
      pool.Initialize();
    }
    yield return new WaitUntil(() => pools.All(p => p.IsReady));
    Debug.Log("All pools initialized.");
  }
}
