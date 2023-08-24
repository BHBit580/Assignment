using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FilterOptions : MonoBehaviour
{
    [SerializeField] private Transform entryContainer;
    [SerializeField] private Transform entryTemplate;
    [SerializeField] private TMP_Dropdown filterDropdown;
    [SerializeField] private GameObject fixedAsset;
    [SerializeField] private GameObject infoPopUp;

    [SerializeField] private float templateHeight = 20f;
    
    [Header("InfoPopUpDisplayText")] 
    [SerializeField] private TextMeshProUGUI popUpNameText;
    [SerializeField] private TextMeshProUGUI popUpPointsText;
    [SerializeField] private TextMeshProUGUI popUpAddressText;
    
    
    private ClientDataContainer clientDataContainer;
    
    private void Start()
    {
        clientDataContainer = GameManager.clientDataContainer;                            //Getting clientdatacontainer from game manager
        DisplayClients(clientDataContainer.clients);                            
        filterDropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }
    
    private void OnDropdownValueChanged(int value)
    {
        switch (value)
        {
            case 0: 
                DisplayClients(clientDataContainer.clients);
                break;
            case 1:
                DisplayClients(clientDataContainer.clients.FindAll(client => client.isManager));
                break;
            case 2: 
                DisplayClients(clientDataContainer.clients.FindAll(client => !client.isManager));
                break;
        }
    }
    
    
    private void DisplayClients(List<Client> clientsToDisplay)
    {
        DestroyPreviousDisplay();
        
        float yOffset = 0;
        
        foreach (var client in clientsToDisplay)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);                  //Text Instantation
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -yOffset);

        
            TextMeshProUGUI points = entryTransform.Find("Points").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI label = entryTransform.Find("Label").GetComponentInChildren<TextMeshProUGUI>();
            
            

            label.text = client.label;
            
            if (clientDataContainer.data.TryGetValue(client.id.ToString() , out ClientData value))            //Checking if value dictionary 
            {                                                                                                 // info valid
                ClientData clientData = clientDataContainer.data[client.id.ToString()];
                points.text = clientData.points.ToString();
            }
            else
            {
                points.text = "Not Given";
            }
            
           label.GetComponentInParent<Button>().onClick.AddListener(() => PopUpWindow(client.id));

            yOffset += templateHeight;                                                    //Adding distance between the rows 

        }
    }

    private void DestroyPreviousDisplay()
    {
        while (entryContainer.childCount > 0)
        {
            DestroyImmediate(entryContainer.GetChild(0).gameObject);
        }
    }
    
    
    public void PopUpWindow(int clientId)
    {
        if (clientDataContainer.data.TryGetValue(clientId.ToString(), out ClientData clientData))
        {
            popUpNameText.text = "Name: " + clientData.name;
            
            if (clientData.points != null)
            {
                popUpPointsText.text = "Points: " + clientData.points.ToString();
            }
            popUpAddressText.text = "Address: " + clientData.address;
        }
        else
        {
            popUpNameText.text = "Name: Not Given";
            popUpPointsText.text = "Points: Not Given";
            popUpAddressText.text = "Address: Not Given";
        }
        
        infoPopUp.SetActive(true);
        fixedAsset.SetActive(false);
    }

}
