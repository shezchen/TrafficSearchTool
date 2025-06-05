using TMPro;
using UnityEngine;

public class SetPrefabCode : MonoBehaviour
{
    private TextMeshProUGUI StartCity;
    private TextMeshProUGUI EndCity;
    private TextMeshProUGUI StartTime;
    private TextMeshProUGUI EndTime;
    private TextMeshProUGUI Price;
    
    /// <summary>
    /// 此处包含了本车次的ID
    /// </summary>
    public string ID { get; private set; }

    public void Set(string StartCity, string EndCity, string StartTime, string EndTime,int Price,string id)
    {
        this.StartCity = transform.Find("StartCity").GetComponent<TextMeshProUGUI>();
        this.EndCity = transform.Find("EndCity").GetComponent<TextMeshProUGUI>();
        this.StartTime = transform.Find("StartTime").GetComponent<TextMeshProUGUI>();
        this.EndTime = transform.Find("EndTime").GetComponent<TextMeshProUGUI>();
        this.Price = transform.Find("Price").GetComponent<TextMeshProUGUI>();

        this.StartCity.text = StartCity;
        this.EndCity.text = EndCity;
        this.StartTime.text = StartTime;
        this.EndTime.text = EndTime;
        
        this.Price.text = "票价"+Price+"元";
        
        this.ID = id;
    }
}
