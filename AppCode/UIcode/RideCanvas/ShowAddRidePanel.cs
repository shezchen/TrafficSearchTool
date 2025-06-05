using UnityEngine;

public class ShowAddRidePanel : MonoBehaviour
{
    
    [SerializeField] private GameObject addRidePanelPrefab;
    [SerializeField] private Transform addRidePanelTransform;
    private GameObject preCityPanel;
    
    public void ShowAddRide()
    {
        if(preCityPanel != null)
            Destroy(preCityPanel);
        if (addRidePanelPrefab != null)
        {
            preCityPanel = Instantiate(addRidePanelPrefab, addRidePanelTransform);
        }
        else
        {
            Debug.LogError("AddCityPanel prefab not found in Resources.");
        }
    }
}
