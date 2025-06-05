using System;
using UnityEngine;

public class Ride
{
    public enum RideType
    {
        Train,
        Airplane
    }
    public Ride(Ride.RideType rideType, string startCity, string endCity, DateTime startTime, DateTime endTime, int price)
    {
        Type= rideType;
        StartCity = startCity;
        EndCity = endCity;
        StartTime = startTime;
        EndTime = endTime;
        Price = price;
        
        ID = $"{rideType}_{startCity}_{endCity}_{startTime:yyyyMMddHHmmss}_{endTime:yyyyMMddHHmmss}";
    }

    public readonly RideType Type;

    public readonly string StartCity;
    public readonly string EndCity;

    public readonly DateTime StartTime;
    public readonly DateTime EndTime;

    public readonly int Price;

    public readonly string ID;
}
