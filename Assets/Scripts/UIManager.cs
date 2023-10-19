using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;

public class UIManager : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI id;

    public RawImage artworkImage; // Reference to the RawImage where the artwork will be displayed
    public TextMeshProUGUI nameText;    
    public TextMeshProUGUI heightText;
    public TextMeshProUGUI weightText;    

    public void LoadBaseInfo(Pokemon data)
    {
        id.text = data.GetId().ToString("0000");
        nameText.text = data.GetName();
        weightText.text = data.GetWeight().ToString() + " Kg";
        heightText.text = data.GetHeight().ToString() + " m";
    }

    public void LoadImage(UnityWebRequest request)
    {
        Texture2D texture = DownloadHandlerTexture.GetContent(request);
        artworkImage.texture = texture;
    }
}
