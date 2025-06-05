using System.ComponentModel.Design;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ButtonVision : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private Image buttonImage; // 按钮的Image组件
    
    [SerializeField] private Sprite closeImage; 
    [SerializeField] private Sprite openImage;
    
    [SerializeField] private float transitionDuration = 0.5f; // 过渡动画持续时间
    
    [Tooltip("当前页面类型")]
    [SerializeField] private PageManager.PageType thisPageType;
    
    [SerializeField] private GameObject animatorPrefab; // 动画预制体

    void Start()
    {
        buttonImage.sprite = closeImage;
        
        PageManager.onPageShowed += OnPage_Showed;
        PageManager.onPageHided += OnPage_Hided;
    }

    private void OnPage_Showed(PageManager.PageType pageType)
    {
        if(pageType == thisPageType)
            UIMotion_Active();
    }
    
    private void OnPage_Hided(PageManager.PageType pageType)
    {
        if(pageType == thisPageType)
            UIMotion_Inactive();
    }

    /// <summary>
    /// 按钮UI的激活动画
    /// </summary>
    private void UIMotion_Active()
    {
        SpriteChange(closeImage, openImage);
    }

    /// <summary>
    /// 按钮UI的取消激活动画
    /// </summary>
    private void UIMotion_Inactive()
    {
        buttonImage.sprite = closeImage;
    }


    private void SpriteChange(Sprite pre, Sprite next)
    {
        // 创建过渡效果序列
        Sequence sequence = DOTween.Sequence();
        
        // 第一步：淡出当前图像
        sequence.Append(buttonImage.DOFade(0, transitionDuration / 2));
        
        // 第二步：切换图像并淡入
        sequence.AppendCallback(() =>
        {
            buttonImage.sprite = next;
        });
        
        sequence.Append(buttonImage.DOFade(1, transitionDuration / 2));
        
        // 播放序列
        sequence.Play();
    }
    
    private GameObject prefab;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        prefab = Instantiate(animatorPrefab, transform);
        prefab.GetComponent<SpriteRenderer>().sortingOrder = -10; 
        animatorPrefab.GetComponent<Animator>().SetTrigger("PopUp");
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        if(prefab != null)
            Destroy(prefab);
    }
}
