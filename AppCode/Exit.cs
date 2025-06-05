using UnityEngine;

public class Exit : MonoBehaviour
{
    /// <summary>
    /// 退出游戏
    /// </summary>
    public void OnClick()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
