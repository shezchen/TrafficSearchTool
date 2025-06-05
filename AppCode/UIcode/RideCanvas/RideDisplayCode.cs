using UnityEngine;

/// <summary>
/// 附加在火车或飞机的滚动视图上
/// </summary>
public class RideDisplayCode : MonoBehaviour
{
    [SerializeField] private GameObject RideDisplayPrefab;
    
    /// <summary>
    /// 滚动视图的content
    /// </summary>
    [SerializeField] private GameObject RideDisplayContent;
    [SerializeField] private bool isTrain = true; // 是否为火车，默认为true

    public void Refresh()
    {
        // 清空Content
        foreach (Transform child in RideDisplayContent.transform)
        {
            Destroy(child.gameObject);
        }
        
        var rideList = isTrain ? DataManager.Instance.TrainList : DataManager.Instance.AirplaneList;
        foreach (var ride in rideList)
        {
            GameObject rideDisplayPrefab = Instantiate(RideDisplayPrefab,RideDisplayContent.transform);
            rideDisplayPrefab.transform.SetParent(RideDisplayContent.transform);
            
            rideDisplayPrefab.GetComponent<SetPrefabCode>().Set
            (ride.StartCity,ride.EndCity,ride.StartTime.ToString(),ride.EndTime.ToString(),ride.Price,ride.ID);
        }
    }
}
