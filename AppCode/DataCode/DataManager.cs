using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }



    [SerializeField] public GameObject CityDisplay;
    [SerializeField] public GameObject TrainDisplay;
    [SerializeField] public GameObject AirplaneDisplay;
    private void Start()
    {
        CityDisplay.GetComponent<CityDisPlayCode>().Refresh();
        TrainDisplay.GetComponent<RideDisplayCode>().Refresh();
        AirplaneDisplay.GetComponent<RideDisplayCode>().Refresh();
    }


    /// <summary>
    /// 城市列表
    /// </summary>
    public List<string> CityList { get; private set; } = new List<string>
    {
        "北京",
        "上海",
        "广州",
        "深圳",
        "杭州",
        "成都",
        "武汉",
        "西安",
        "重庆",
        "南京"
    };
    
    /// <summary>
    /// 只有在城市列表中读取城市文件才使用
    /// </summary>
    /// <param name="cityData">从文件中读取的城市列表</param>
    public void SetCityList(List<string> cityData)
    {
        CityList = cityData;
        CityDisplay.GetComponent<CityDisPlayCode>().Refresh();
    }


    public void AddCity(string cityName)
    {
        if (!CityList.Contains(cityName))
        {
            CityList.Add(cityName);
        }
        
        CityDisplay.GetComponent<CityDisPlayCode>().Refresh();
    }
    public void RemoveCity(string cityName)
    {
        if (CityList.Contains(cityName))
        {
            CityList.Remove(cityName);
        }
        foreach (var trainRide in TrainList.ToList())
        {
            if(trainRide.StartCity == cityName || trainRide.EndCity == cityName)
            {
                TrainList.Remove(trainRide);
            }
        }
        foreach (var airplaneRide in AirplaneList.ToList())
        {
            if (airplaneRide.StartCity == cityName || airplaneRide.EndCity == cityName)
            {
                AirplaneList.Remove(airplaneRide);
            }
        }
        
        CityDisplay.GetComponent<CityDisPlayCode>().Refresh();
        TrainDisplay.GetComponent<RideDisplayCode>().Refresh();
        AirplaneDisplay.GetComponent<RideDisplayCode>().Refresh();
    }
    public bool CityExists(string cityName)
    {
        return CityList.Contains(cityName);
    }
    public void ClearCities()
    {
        foreach (var city in CityList.ToList())
        {
            RemoveCity(city);
        }
    }
    
    
    
    public List<Ride> TrainList { get; private set; } = new List<Ride>
    {
        new Ride(Ride.RideType.Train, "上海", "北京", new DateTime(2025,5,30, 8, 0, 0), new DateTime(2025,5,30, 12, 0, 0), 100),
    };

    public void AddTrain(Ride ride)
    {
        TrainList.Add(ride);
        TrainDisplay.GetComponent<RideDisplayCode>().Refresh();
    }
    public void RemoveTrain(string rideID)
    {
        var rideToRemove = TrainList.FirstOrDefault(r => r.ID == rideID);
        if (rideToRemove != null)
        {
            TrainList.Remove(rideToRemove);
        }
        TrainDisplay.GetComponent<RideDisplayCode>().Refresh();
    }
    public void ClearTrains()
    {
        TrainList.Clear();
        TrainDisplay.GetComponent<RideDisplayCode>().Refresh();
    }
    
    public List<Ride> AirplaneList { get; private set; } = new List<Ride>
    {
        new Ride(Ride.RideType.Airplane, "广州", "深圳", new DateTime(2025,5,30, 9, 0, 0), new DateTime(2025,5,30, 11, 0, 0), 50),
    };
    
    public void AddAirplane(Ride ride)
    {
        AirplaneList.Add(ride);
        AirplaneDisplay.GetComponent<RideDisplayCode>().Refresh();
    }
    public void RemoveAirplane(string rideID)
    {
        var rideToRemove = AirplaneList.FirstOrDefault(r => r.ID == rideID);
        if (rideToRemove != null)
        {
            AirplaneList.Remove(rideToRemove);
        }
        AirplaneDisplay.GetComponent<RideDisplayCode>().Refresh();
    }
    public void ClearAirplanes()
    {
        AirplaneList.Clear();
        AirplaneDisplay.GetComponent<RideDisplayCode>().Refresh();
    }


    public List<Ride> GetData(bool isTrain)
    {
        if (isTrain)
        {
            return new List<Ride>(TrainList);
        }
        else
        {
            return new List<Ride>(AirplaneList);
        }
    }
    
    public void LoadData(List<Ride> rides, bool isTrain)
    {
        if (isTrain)
        {
            TrainList = rides;
            TrainDisplay.GetComponent<RideDisplayCode>().Refresh();
        }
        else
        {
            AirplaneList = rides;
            AirplaneDisplay.GetComponent<RideDisplayCode>().Refresh();
        }
    }

    public List<string> GetCityData()
    {
        return new List<string>(CityList);
    }
}
