using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {
  public GameObject prefab;
  public int initialSize = 100;

  private Queue<GameObject> pool = new Queue<GameObject>();

  void Awake() {
    for (int i = 0; i < initialSize; i++) {
      GameObject obj = Instantiate(prefab);
      obj.SetActive(false);
      obj.transform.SetParent(transform);
      pool.Enqueue(obj);
    }
  }

  public GameObject Get() {
    if (pool.Count > 0) {
      GameObject obj = pool.Dequeue();
      obj.SetActive(true);
      return obj;
    }

    // Expand pool if needed
    GameObject newObj = Instantiate(prefab);
    newObj.SetActive(true);
    newObj.transform.SetParent(transform);
    return newObj;
  }

  public void Return(GameObject obj) {
    obj.SetActive(false);
    pool.Enqueue(obj);
    Debug.Log("Object returned to pool.");
  }
}