using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : Singleton<PoolManager> {
  public List<ObjectPool> pools;

  void Start() {
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
