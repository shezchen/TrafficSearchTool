using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SearchState
{
    /// <summary>
    /// 起始城市名称
    /// </summary>
    public readonly string StartCityName;
    
    /// <summary>
    /// 当前已经搭乘的载具班次
    /// </summary>
    public readonly List<Ride> TakeRides = new List<Ride>();

    /// <summary>
    ///  现在处于的时间
    /// </summary>
    public readonly DateTime CurrentTime = DateTime.Now;
    
    public SearchState(string startCity,List<Ride> takeRides, DateTime currentTime)
    {
        StartCityName = startCity ?? throw new ArgumentNullException(nameof(startCity), "Start city cannot be null");
        TakeRides = takeRides ?? new List<Ride>();
        CurrentTime = currentTime;
    }

    /// <summary>
    /// 搭乘一个新的载具班次，返回新SearchState
    /// </summary>
    /// <param name="ride">搭乘班次</param>
    /// <returns>新构造的对象</returns>
    public SearchState TakeRide(Ride ride)
    {
        if(ride == null)
            return this;
        
        if(ride.StartTime < CurrentTime)
        {
            Debug.LogError($"Cannot take ride {ride.ID} because it starts before the current time {CurrentTime}");
            return this;
        }
        
        SearchState newState = new SearchState(StartCityName,TakeRides.Append(ride).ToList(), ride.EndTime);
        
        return newState;
    }
}
