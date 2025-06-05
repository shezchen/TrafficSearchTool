using TMPro;
using UnityEngine;

/// <summary>
/// 附加在错误显示面板
/// </summary>
public class ErrorPanel : MonoBehaviour
{
    
    /// <summary>
    /// 显示错误信息
    /// </summary>
    /// <param name="message">显示错误信息字符串</param>
    public void ShowError(string message)
    {
        // Assuming there's a Text component to display the error message
        var errorText = transform.Find("ErrorText").GetComponent<TextMeshProUGUI>();
        if (errorText != null)
        {
            errorText.text = message;
        }
        else
        {
            Debug.LogError("ErrorText component not found in ErrorPanel.");
        }
    }
    
    public void Cancel_clicked()
    {
        Destroy(gameObject);
    }
}
