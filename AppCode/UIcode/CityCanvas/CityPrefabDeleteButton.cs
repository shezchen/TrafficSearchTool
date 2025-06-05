using TMPro;
using UnityEngine;

/// <summary>
/// 城市小预制体的删除按钮
/// </summary>
public class CityPrefabDeleteButton : MonoBehaviour
{
    [SerializeField]private GameObject CityName;
    public void OnClick()
    {
        DataManager.Instance.RemoveCity(CityName.GetComponent<TextMeshProUGUI>().text);
    }
}
