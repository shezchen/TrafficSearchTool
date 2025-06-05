using UnityEngine;

/// <summary>
/// 附加在铁路或飞机时刻表预制体的删除按钮
/// </summary>
public class RidePrefabDeleteButton : MonoBehaviour
{
    [SerializeField] private bool isTrain; // 是否是铁路时刻表

    public void On_Clicked()
    {
        if (isTrain)
        {
            // 删除火车时刻表
            string rideID = transform.parent.GetComponent<SetPrefabCode>().ID;
            DataManager.Instance.RemoveTrain(rideID);
        }
        else
        {
            // 删除飞机时刻表
            string rideID = transform.parent.GetComponent<SetPrefabCode>().ID;
            DataManager.Instance.RemoveAirplane(rideID);
        }
    }
}
