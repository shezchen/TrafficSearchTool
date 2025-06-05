using System.Windows.Forms;
using UnityEngine;
using Newtonsoft.Json;

public class SaveHandler : MonoBehaviour
{
    
    [SerializeField] private bool isTrain; // 是否是火车时刻表
    
    public void OpenSaveDialog()
    {
#if UNITY_STANDALONE_WIN
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "JSON files (*.json)|*.json";
        saveFileDialog.FilterIndex = 1;
        saveFileDialog.RestoreDirectory = true;
        saveFileDialog.Title = "保存文件";
        saveFileDialog.FileName = isTrain ? "TrainData" : "AirlineData"; // 默认文件名

        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            string filePath = saveFileDialog.FileName;
            Debug.Log("选择的保存路径: " + filePath);
            SaveFile(filePath);
        }
#endif
    }
    
    private void SaveFile(string filePath)
    {
        // 获取数据
        var data = DataManager.Instance.GetData(isTrain);
        
        // 将数据序列化为JSON字符串
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        
        // 写入文件
        System.IO.File.WriteAllText(filePath, json);
        
        Debug.Log("数据已保存到: " + filePath);
    }
}
