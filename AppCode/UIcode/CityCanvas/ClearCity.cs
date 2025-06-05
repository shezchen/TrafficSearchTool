using UnityEngine;

public class ClearCity : MonoBehaviour
{
    public void OnClick()
    {
        // 清除所有城市
        DataManager.Instance.ClearCities();
    }
}
