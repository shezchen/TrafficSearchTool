using UnityEngine;

/// <summary>
/// 附加在城市显示滚动视图
/// </summary>
public class CityDisPlayCode : MonoBehaviour
{
    [SerializeField] private GameObject CityDisplayPrefab;
    
    /// <summary>
    /// 滚动视图的content
    /// </summary>
    [SerializeField] private GameObject CityDisplayContent;

    public void Refresh()
    {
        // 清空Content
        foreach (Transform child in CityDisplayContent.transform)
        {
            Destroy(child.gameObject);
        }
        
        var cityList = DataManager.Instance.CityList;
        foreach (var city in cityList)
        {
            GameObject cityDisplayPrefab = Instantiate(CityDisplayPrefab, CityDisplayContent.transform);
            cityDisplayPrefab.transform.SetParent(CityDisplayContent.transform);
            
            cityDisplayPrefab.GetComponent<SetCityCode>().Set(city);
        }
    }
}
