using UnityEngine;

public class CoinDrop : MonoBehaviour {
  public GameObject coinPrefab;
  public float dropChance = 0.5f;

  public void Die() {
    if (Random.value < dropChance && coinPrefab != null) {
      Instantiate(coinPrefab, transform.position, Quaternion.identity);
    }

    Destroy(gameObject);
  }
}