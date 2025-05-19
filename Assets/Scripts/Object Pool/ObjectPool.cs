using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour, IObjectPool {
  public GameObject prefab;
  public int initialSize = 100;

  private Queue<GameObject> pool = new Queue<GameObject>();

  public bool IsReady { get; private set; } = false;

  public void Initialize() {
    for (int i = 0; i < initialSize; i++) {
      GameObject obj = Instantiate(prefab);
      obj.SetActive(false);
      obj.transform.SetParent(transform);
      pool.Enqueue(obj);
    }

    IsReady = true;
  }

  public GameObject Get() {
    if (pool.Count > 0) {
      GameObject obj = pool.Dequeue();
      obj.SetActive(true);
      return obj;
    }

    GameObject newObj = Instantiate(prefab);
    newObj.SetActive(true);
    newObj.transform.SetParent(transform);
    return newObj;
  }

  public void Return(GameObject obj) {
    obj.SetActive(false);
    pool.Enqueue(obj);
  }
}
