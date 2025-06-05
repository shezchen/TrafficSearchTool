using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
   [SerializeField] Image bottomBackground;
   [SerializeField] Image topBackground;
   
   [SerializeField] private Sprite CityBackground;
   [SerializeField] private Sprite RailwayBackground;
   [SerializeField] private Sprite FlightBackground;
   [SerializeField] private Sprite SearchBackground;
   [SerializeField] private Sprite AboutBackground;

   public readonly float transitionDuration = .5f; // 背景切换动画持续时间

   private void Start()
   {
      PageManager.onPageShowed += OnPageShowed;

      bottomBackground.sprite = AboutBackground;
      topBackground.sprite = AboutBackground;

      bottomBackground.color = new Color(1, 1, 1, 1);
      topBackground.color = new Color(1, 1, 1, 0);// Set initial transparency for the top background
   }

   private void OnPageShowed(PageManager.PageType pageType)
   {
      Sprite newBackground = null;

      switch (pageType)
      {
         case PageManager.PageType.City:
            newBackground = CityBackground;
            break;
         case PageManager.PageType.Railway:
            newBackground = RailwayBackground;
            break;
         case PageManager.PageType.Flight:
            newBackground = FlightBackground;
            break;
         case PageManager.PageType.Search:
            newBackground = SearchBackground;
            break;
         case PageManager.PageType.About:
            newBackground = AboutBackground;
            break;
      }

      if (bottomBackground.sprite != newBackground)
         StartCoroutine(TransitionBackground(newBackground));
   }

   private IEnumerator TransitionBackground(Sprite newBackground)
   {
      if (newBackground == null)
      {
         yield break;
      }
      
      // 停止之前可能的动画
      DOTween.Kill(topBackground);
      DOTween.Kill(bottomBackground);
      
      // 设置顶层背景为新背景
      topBackground.sprite = newBackground;
      topBackground.color = new Color(1, 1, 1, 0); // 完全透明
      
      // 淡入新背景
      topBackground.DOFade(1, transitionDuration);
      
      // 等待动画完成
      yield return new WaitForSeconds(transitionDuration);
      
      // 更新底层背景为新背景并重置透明度
      bottomBackground.sprite = newBackground;
      bottomBackground.color = new Color(1, 1, 1, 1);
      topBackground.color = new Color(1, 1, 1, 0);
   }


}
