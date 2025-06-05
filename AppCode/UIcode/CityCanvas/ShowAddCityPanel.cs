using UnityEngine;

/// <summary>
/// “添加城市”按钮
/// </summary>
public class ShowAddCityPanel : MonoBehaviour
{
    [SerializeField] private GameObject addCityPanelPrefab;
    [SerializeField] private Transform addCityPanelTransform;
    
    /// <summary>
    /// 先前的城市面板
    /// </summary>
    private GameObject preCityPanel;
    
    public void ShowAddCity()
    {
        Destroy(preCityPanel);
        if (addCityPanelPrefab != null)
        {
            preCityPanel = Instantiate(addCityPanelPrefab, addCityPanelTransform);
        }
        else
        {
            Debug.LogError("AddCityPanel prefab not found in Resources.");
        }
    }
}
