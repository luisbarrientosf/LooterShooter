using UnityEngine;

public static class YSortUtils {
  public static int GetBaseSortingOrder(Bounds bounds, float sortingScale) {
    return -(int)(bounds.min.y * sortingScale);
  }
  public static int GetBaseSortingOrderCenter(Bounds bounds, float sortingScale) {
    return -(int)(bounds.center.y * sortingScale);
  }
}
