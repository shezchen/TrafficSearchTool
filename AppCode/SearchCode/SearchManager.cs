using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 附加在搜索页面canvas
/// </summary>
public class SearchManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown StartCityDropdown; // 起始城市下拉框
    [SerializeField] private TMP_Dropdown EndCityDropdown; // 终点城市下拉框

    [SerializeField] private GameObject TrainImage;
    [SerializeField] private GameObject AirplaneImage;
    
    [SerializeField] private GameObject ErrorPanel; // 错误面板预制体
    [SerializeField] private Transform ErrorTransform; // 错误面板的父对象，用于实例化错误面板
    
    [SerializeField] private TextMeshProUGUI InfoText; // 用于显示搜索信息的文本
    
    private static List<List<Ride>> _searchRideListResult = new(); // 用于存储搜索结果
    
    /// <summary>
    /// 是否已经进行过一次搜索
    /// </summary>
    private bool _isSearched = false;
    
    public bool IsTrain { get; private set; } // 是否是火车搜索
    
    private void ShowError(string error)
    {
        var errorpanel = Instantiate(ErrorPanel, ErrorTransform);
        errorpanel.GetComponent<ErrorPanel>().ShowError(error);
    }
    
    public void RefreshSearchPage()
    {
        try
        {
            StartCityDropdown.ClearOptions();
            StartCityDropdown.AddOptions(DataManager.Instance.CityList);
            
            EndCityDropdown.ClearOptions();
            EndCityDropdown.AddOptions(DataManager.Instance.CityList);

            IsTrain = true; // 默认是火车搜索
            TrainImage.SetActive(true);
            AirplaneImage.SetActive(false);
            //ButtonText.text = "查询车次";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        
    }
    
    public void OnSwitchButtonClicked()
    {
        IsTrain = !IsTrain; // 切换搜索类型
        
        if (IsTrain)
        {
            TrainImage.SetActive(true);
            AirplaneImage.SetActive(false);
            //ButtonText.text = "查询车次";
        }
        else
        {
            TrainImage.SetActive(false);
            AirplaneImage.SetActive(true);
            //ButtonText.text = "查询航班";
        }
    }

    private void OnEnable()
    {
        RefreshSearchPage();
        _isSearched = false;
        
        _searchRideListResult.Clear(); // 清空搜索结果
        ClearRideListContent(); // 清空显示的搜索结果
    }

    /// <summary>
    /// 搜索两座城市之间的最早抵达路线
    /// </summary>
    public void StartSearch()
    {
        _isSearched = true;
        var searchRideList = IsTrain ? DataManager.Instance.TrainList : DataManager.Instance.AirplaneList;
        
        string startCity = StartCityDropdown.options[StartCityDropdown.value].text;
        string endCity = EndCityDropdown.options[EndCityDropdown.value].text;
        
        _searchRideListResult.Clear();
        
        if (startCity == endCity)
        {
            // 如果起始城市和终点城市相同，直接返回
            ShowError("起始城市和终点城市不能相同");
            return;
        }
        
        
        
        Search(new SearchState(startCity,new List<Ride>(),DateTime.Now), endCity);
        
        DisplaySearchResults();
    }

    private void Search(SearchState currentState, string endCity)
    {
        List<Ride> availableRides = new List<Ride>();

        if (currentState.TakeRides.Count == 0)
        {
            availableRides = IsTrain ? DataManager.Instance.TrainList : DataManager.Instance.AirplaneList;
            availableRides = availableRides.FindAll(ride =>
                ride.StartTime >= currentState.CurrentTime &&
                ride.StartCity == currentState.StartCityName);// 找到所有可用的班次
        }
        else
        {
            if(currentState.TakeRides[^1].EndCity == endCity)
            {
                // 如果当前状态的最后一班车的终点城市就是目标城市，记录结果
                _searchRideListResult.Add(new List<Ride>(currentState.TakeRides));
                return;
            }
    
            #region 超时检测
            //按照终点城市的班次抵达时间排序
            var endCityRideList = IsTrain ? DataManager.Instance.TrainList : DataManager.Instance.AirplaneList;
            endCityRideList = endCityRideList.FindAll(ride =>
                ride.EndCity == endCity &&
                ride.StartTime >= currentState.CurrentTime);
            endCityRideList.Sort((x,y)=> x.EndTime.CompareTo(y.EndTime));
            
            if(endCityRideList.Count == 0)
            {
                // 这种情况下不可能到达终点城市，直接返回
                return;
            }
            
            //获取终点城市的最后一班车的抵达时间
            var lastestTime = endCityRideList[^1].EndTime;
            
            if(currentState.CurrentTime > lastestTime)
            {
                // 如果当前时间已经超过终点城市的最后一班车的抵达时间，直接返回
                return;
            }
            #endregion
            
            availableRides = IsTrain ? DataManager.Instance.TrainList : DataManager.Instance.AirplaneList;
            availableRides = availableRides.FindAll(ride =>
                ride.StartTime >= currentState.CurrentTime &&
                ride.StartCity == currentState.TakeRides[^1].EndCity);// 找到所有可用的班次
        }
        
        foreach (var singleRide in availableRides)
        {
            var newState = currentState.TakeRide(singleRide);// 搭乘一个可用班次，分裂出新的状态
            Search(newState, endCity);
        }
    }

    
    [SerializeField] private TMP_Dropdown SearchTypeDropdown; // 搜索类型下拉框
    /// <summary>
    /// 将_searchRideListResult搜索结果显示在UI上
    /// </summary>
    public void DisplaySearchResults()
    {
        if(!_isSearched)
            return;
        
        if (_searchRideListResult.Count == 0)
        {
            ShowError("没有找到符合条件的行程");
            return;
        }
        
        List<List<Ride>> copy;
        switch (SearchTypeDropdown.value)
        {
            case 0:
                copy = new List<List<Ride>>(_searchRideListResult);
                copy.Sort((x, y) => x[^1].EndTime.CompareTo(y[^1].EndTime));
                DisplayRideList(copy[0]);
                break;
            case 1:
                copy = new List<List<Ride>>(_searchRideListResult);
                copy.Sort((x, y) => x.Sum(r=>r.Price).CompareTo(y.Sum(r=>r.Price)));
                DisplayRideList(copy[0]);
                break;
            case 2:
                copy = new List<List<Ride>>(_searchRideListResult);
                copy.Sort((x, y) => x.Count.CompareTo(y.Count));
                DisplayRideList(copy[0]);
                break;
        }
    }

    [SerializeField] private GameObject TrainRidePrefab;
    [SerializeField] private GameObject AirplaneRidePrefab;
    
    [SerializeField] private Transform RideListContent; // 用于显示搜索结果的滚动视图内容
    private void DisplayRideList(List<Ride> rideList)
    {
        ClearRideListContent();

        if (rideList == null || rideList.Count == 0)
        {
            ShowError("没有找到符合条件的行程");
            return;
        }

        // 根据搜索类型选择预制体
        GameObject ridePrefab = IsTrain ? TrainRidePrefab : AirplaneRidePrefab;

        foreach (var ride in rideList)
        {
            GameObject rideDisplay = Instantiate(ridePrefab, RideListContent);
            rideDisplay.GetComponent<SetPrefabCode>().Set(
                ride.StartCity, 
                ride.EndCity, 
                ride.StartTime.ToString(), 
                ride.EndTime.ToString(), 
                ride.Price, 
                ride.ID);
        }
        var totalPrice = rideList.Sum(r => r.Price);
        var totalTime = rideList[^1].EndTime - rideList[0].StartTime;
        var totalRides = rideList.Count;
        InfoText.text = $"总金额：{totalPrice}\n" +
                        $"行程时间：{totalTime.Hours+totalTime.Days*24}小时{totalTime.Minutes}分钟\n" +
                        $"搭乘次数：{totalRides}次";
    }

    public void ClearRideListContent()
    {
        // 清空之前的内容
        foreach (Transform child in RideListContent)
        {
            Destroy(child.gameObject);
        }
        
        InfoText.text = ""; // 清空搜索结果信息
    }
}