using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 附加在newTrain或newAirplane预制体上的脚本
/// </summary>
public class NewRidePanel : MonoBehaviour
{
    [SerializeField]private GameObject StartCityDropdown;
    [SerializeField]private GameObject EndCityDropdown;
    
    [SerializeField] private GameObject StartTimeYear;
    [SerializeField] private GameObject StartTimeMonth;
    [SerializeField] private GameObject StartTimeDay;
    [SerializeField] private GameObject StartTimeHour;
    [SerializeField] private GameObject StartTimeMinute;
    
    [SerializeField] private GameObject EndTimeYear;
    [SerializeField] private GameObject EndTimeMonth;
    [SerializeField] private GameObject EndTimeDay;
    [SerializeField] private GameObject EndTimeHour;
    [SerializeField] private GameObject EndTimeMinute;
    
    [SerializeField] private GameObject RidePriceInput;

    [SerializeField] private GameObject ErrorPanel;
    
    [SerializeField] private bool isTrain; // 是否是火车时刻表
    [SerializeField] private Transform canvasTransform; // 用于实例化错误面板的父对象
    void Start()
    {
        // 初始化下拉菜单和输入框
        InitializeDropdowns();
    }

    private void InitializeDropdowns()
    {
        // 初始化起始城市下拉菜单
        var startCityDropdown = StartCityDropdown.GetComponent<TMP_Dropdown>();
        startCityDropdown.ClearOptions();
        startCityDropdown.AddOptions(DataManager.Instance.CityList);

        // 初始化结束城市下拉菜单
        var endCityDropdown = EndCityDropdown.GetComponent<TMP_Dropdown>();
        endCityDropdown.ClearOptions();
        endCityDropdown.AddOptions(DataManager.Instance.CityList);
    }
    
    public void Cancel_Clicked()
    {
        // 取消按钮点击事件，销毁当前面板
        Destroy(gameObject);
    }

    public void Confirm_Clicked()
    {
        string startCity, endCity;
        int startYear, startMonth, startDay, startHour, startMinute,endYear, endMonth, endDay, endHour, endMinute;
        // 确认按钮点击事件，获取输入数据并创建新的火车或航班
        try
        {
            startCity = StartCityDropdown.GetComponent<TMP_Dropdown>().options[StartCityDropdown.GetComponent<TMP_Dropdown>().value].text;
            endCity = EndCityDropdown.GetComponent<TMP_Dropdown>().options[EndCityDropdown.GetComponent<TMP_Dropdown>().value].text;

            startYear = int.Parse(StartTimeYear.GetComponent<TMP_InputField>().text);
            startMonth = int.Parse(StartTimeMonth.GetComponent<TMP_InputField>().text);
            startDay = int.Parse(StartTimeDay.GetComponent<TMP_InputField>().text);
            startHour = int.Parse(StartTimeHour.GetComponent<TMP_InputField>().text);
            startMinute = int.Parse(StartTimeMinute.GetComponent<TMP_InputField>().text);

            endYear = int.Parse(EndTimeYear.GetComponent<TMP_InputField>().text);
            endMonth = int.Parse(EndTimeMonth.GetComponent<TMP_InputField>().text);
            endDay = int.Parse(EndTimeDay.GetComponent<TMP_InputField>().text);
            endHour = int.Parse(EndTimeHour.GetComponent<TMP_InputField>().text);
            endMinute = int.Parse(EndTimeMinute.GetComponent<TMP_InputField>().text);
        }
        catch (Exception e)
        {
            ShowError("输入格式错误，请检查所有输入框");
            return;
        }
        

        int price;
        if (!int.TryParse(RidePriceInput.GetComponent<TMP_InputField>().text, out price))
        {
            ShowError("价格输入错误，请输入一个有效的整数");
            return;
        }

        var startTime = new System.DateTime(startYear, startMonth, startDay, startHour, startMinute, 0);
        var endTime = new System.DateTime(endYear, endMonth, endDay, endHour, endMinute, 0);
        if (startTime >= endTime)
        {
            ShowError("开始时间必须早于结束时间");
            return;
        }
        
        if(startCity == endCity)
        {
            ShowError("起始城市和结束城市不能相同");
            return;
        }

        // 创建新的火车或航班
        if (isTrain)
        {
            // 如果是火车时刻表
            DataManager.Instance.AddTrain(new Ride(Ride.RideType.Train, startCity, endCity,
                startTime, endTime, price));
        }
        else
        {
            // 如果是航班时刻表
            DataManager.Instance.AddAirplane(new Ride(Ride.RideType.Airplane, startCity, endCity,
                startTime, endTime, price));
        }
        Destroy(gameObject);
    }


    private void ShowError(string error)
    {
        // 显示错误信息
        var errorPanel = Instantiate(ErrorPanel, canvasTransform);
        errorPanel.GetComponent<ErrorPanel>().ShowError(error);
        
        errorPanel.transform.localScale = Vector3.one;
    }

}
