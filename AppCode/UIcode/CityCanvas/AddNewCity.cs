using TMPro;
using UnityEngine;

/// <summary>
/// 附加在添加新城市的面板上
/// </summary>
public class AddNewCity : MonoBehaviour
{
    [SerializeField]
    private GameObject Panel;
    
    [SerializeField]
    private GameObject ErrorPanel;
    
    public void Cancel_Clicked()
    {
        Destroy(Panel);
    }
    
    public void Confirm_Clicked()
    {
        string cityName = Panel.transform.Find("Input").GetComponent<TMPro.TMP_InputField>().text;
        if (string.IsNullOrEmpty(cityName))
        {
            ShowError("城市名称不能为空");
            return;
        }

        
        if (DataManager.Instance.CityExists(cityName))
        {
            ShowError("城市已存在");
            return;
        }

        DataManager.Instance.AddCity(cityName);
        Destroy(Panel);
    }

    private void ShowError(string error)
    {
        var errorpanel = Instantiate(ErrorPanel, Panel.transform);
        errorpanel.GetComponent<ErrorPanel>().ShowError(error);
    }
}
