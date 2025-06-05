using System;
using UnityEngine;

public class PageManager : MonoBehaviour
{
    [SerializeField] private GameObject CityPage;
    [SerializeField] private GameObject RailwayPage;
    [SerializeField] private GameObject FlightPage;
    [SerializeField] private GameObject SearchPage;
    [SerializeField] private GameObject AboutPage;

    public enum PageType
    {
        City,
        Railway,
        Flight,
        Search,
        About,
        None // 用于表示没有页面被激活
    }
    
    /// <summary>
    /// 当页面切换并展示时触发
    /// </summary>
    public static Action<PageType> onPageShowed;
    
    /// <summary>
    /// 当页面切换并隐藏时触发（通常是隐藏上一个页面类型）
    /// </summary>
    public static Action<PageType> onPageHided;
    
    
    /// <summary>
    /// 目前的页面类型，用于在函数中对比切换
    /// </summary>
    private PageType previousPage = PageType.None;

    #region PageShowMethods
    public void ShowCityPage()
    {
        if(!CheckPage(PageType.City))
            return; 
        
        HideAllPages();
        CityPage.SetActive(true);
        DataManager.Instance.CityDisplay.GetComponent<CityDisPlayCode>().Refresh();
        
        onPageHided?.Invoke(previousPage);
        onPageShowed?.Invoke(PageType.City);
        
        previousPage = PageType.City;
    }
    
    public void ShowRailwayPage()
    {
        if(!CheckPage(PageType.Railway))
            return; 
        
        HideAllPages();
        RailwayPage.SetActive(true);
        DataManager.Instance.TrainDisplay.GetComponent<RideDisplayCode>().Refresh();
        
        onPageHided?.Invoke(previousPage);
        onPageShowed?.Invoke(PageType.Railway);

        previousPage = PageType.Railway;
    }
    
    public void ShowFlightPage()
    {
        if(!CheckPage(PageType.Flight))
            return;
        
        HideAllPages();
        FlightPage.SetActive(true);
        DataManager.Instance.AirplaneDisplay.GetComponent<RideDisplayCode>().Refresh();
        
        onPageHided?.Invoke(previousPage);
        onPageShowed?.Invoke(PageType.Flight);

        previousPage = PageType.Flight;
    }
    
    public void ShowSearchPage()
    {
        if(!CheckPage(PageType.Search))
            return;
        
        HideAllPages();
        SearchPage.SetActive(true);
        
        onPageHided?.Invoke(previousPage);
        onPageShowed?.Invoke(PageType.Search);

        previousPage = PageType.Search;
    }
    
    public void ShowAboutPage()
    {
        if(!CheckPage(PageType.About))
            return;
        
        HideAllPages();
        AboutPage.SetActive(true);
        
        onPageHided?.Invoke(previousPage);
        onPageShowed?.Invoke(PageType.About);

        previousPage = PageType.About;
    }
    
    private void HideAllPages()
    {
        CityPage.SetActive(false);
        RailwayPage.SetActive(false);
        FlightPage.SetActive(false);
        SearchPage.SetActive(false);
        AboutPage.SetActive(false);
    }
    #endregion
    
    private void Awake()
    {
        HideAllPages(); // 确保在开始时所有页面都隐藏
        
        ShowAboutPage();// 默认显示关于页面
    }

    /// <summary>
    /// 检查现在的页面类型，判断是否需要切换
    /// </summary>
    /// <param name="nowType">用户点击切换的类型</param>
    /// <returns>true为有意义的切换</returns>
    private bool CheckPage(PageType nowType)
    {
        switch (nowType)
        {
            case PageType.City:
                return !CityPage.activeSelf;
            case PageType.Railway:
                return !RailwayPage.activeSelf;
            case PageType.Flight:
                return !FlightPage.activeSelf;
            case PageType.Search:
                return !SearchPage.activeSelf;
            case PageType.About:
                return !AboutPage.activeSelf;
        }
        return true;
    }
}
