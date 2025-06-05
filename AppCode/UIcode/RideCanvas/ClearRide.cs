using UnityEngine;

/// <summary>
/// 火车或航班页面的清空按钮
/// </summary>
public class ClearRide : MonoBehaviour
{
    [SerializeField] private bool isTrain; // 是否是火车时刻表

    public void On_Clicked()
    {
        if (isTrain)
        {
            // 清除所有火车时刻表
            DataManager.Instance.ClearTrains();
        }
        else
        {
            // 清除所有飞机时刻表
            DataManager.Instance.ClearAirplanes();
        }
    }
}
