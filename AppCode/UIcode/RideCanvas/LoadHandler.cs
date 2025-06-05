using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// 附加：车次文件载入按钮
/// </summary>
public class LoadHandler : MonoBehaviour
{
    [SerializeField] private bool isTrain; // 是否是火车时刻表
    
    /// <summary>
    /// 先前选择的文件路径
    /// </summary>
    private string selectedFilePath;
    
    /// <summary>
    /// 已经在另一个线程中选择了文件
    /// </summary>
    private bool fileSelected = false;
    
    public void ShowOpenDialog()
    {
#if UNITY_STANDALONE_WIN
        // 创建新线程来打开文件选择对话框
        Thread thread = new Thread(OpenFileDialogThread);
        thread.SetApartmentState(ApartmentState.STA); // 必须设置STA模式
        thread.Start();
#endif
    }
    
    public void OpenFileDialogThread()
    {
#if UNITY_STANDALONE_WIN
        OpenFileDialog openDialog = new OpenFileDialog();
        openDialog.Filter = "JSON文件 (*.json)|*.json";
        openDialog.Title = "选择文件";
        // 显示对话框
        if (openDialog.ShowDialog() == DialogResult.OK)
        {
            selectedFilePath = openDialog.FileName;
            fileSelected = true;
        }
        else
        {
            Debug.Log("用户取消了文件选择");
        }
        
        openDialog.Dispose();
#endif
    }
    
    private void Update()
    {
        // 检查是否有文件被选择
        if (fileSelected)
        {
            fileSelected = false; // 重置状态
            
            if (!string.IsNullOrEmpty(selectedFilePath))
            {
                Debug.Log("选择的文件路径: " + selectedFilePath);
                ReadFile(selectedFilePath);
            }
            else
            {
                Debug.LogWarning("没有选择有效的文件路径");
            }
        }
    }

    private void ReadFile(string filePath)
    {
        try
        {
            // 读取文件内容
            string json = System.IO.File.ReadAllText(filePath);
            Debug.Log("读取的JSON内容: " + json);
            
            // 反序列化为数据对象
            var data = JsonConvert.DeserializeObject<List<Ride>>(json);
            
            // 将数据添加到DataManager
            DataManager.Instance.LoadData(data, isTrain);
            
            Debug.Log("数据已成功加载");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("加载文件时出错: " + ex.Message);
        }
    }
}
