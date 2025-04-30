using UnityEngine;

public class PlayerInventory : MonoBehaviour {
  public int coinCount = 0;

  public void AddCoin() {
    coinCount++;
    Debug.Log("Coin collected! Total: " + coinCount);
  }

  public int GetCoinCount() {
    return coinCount;
  }
}