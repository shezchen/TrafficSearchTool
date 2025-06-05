using TMPro;
using UnityEngine;

public class SetCityCode : MonoBehaviour
{
    private TextMeshProUGUI cityName;
    public void Set(string cityName)
    {
        this.cityName = transform.Find("CityName").GetComponent<TextMeshProUGUI>();
        this.cityName.text = cityName;
    }
}
