using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class UIManager : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI id;

    public RawImage artworkImage; // Reference to the RawImage where the artwork will be displayed
    public TextMeshProUGUI nameText;    
    public TextMeshProUGUI heightText;
    public TextMeshProUGUI weightText;

    public List<TypeSlot> slots;
    public List<Sprite> typeIcons;

    public void DisplayPokemon(Pokemon data)
    {
        id.text = data.GetId().ToString("0000");
        nameText.text = data.GetName();
        weightText.text = data.GetWeight().ToString() + " Kg";
        heightText.text = data.GetHeight().ToString() + " m";

        if (data.TypeCount() > 1)
        {
            slots[1].slot.SetActive(true);
        }
        else
        {
            slots[1].slot.SetActive(false);
        }

        for (int i = 0; i < data.TypeCount(); i++)
        {
            string typeName = data.GetTypeAt(i);
            // Check if the target sprite name exists in the list
            bool containsSprite = typeIcons.Exists(sprite => sprite.name == typeName);

            if (containsSprite)
            {
                // Check if the target sprite name exists in the list
                Sprite foundSprite = typeIcons.Find(sprite => sprite.name == typeName);

                slots[i].icon.sprite = foundSprite;
                slots[i].text.text = typeName;
            }
            else
            {
                Debug.LogError("Sprite with name " + data.GetTypeAt(i) + " does not exist in the list.");
            }
        }

        _ = LoadImageFromURL(data.GetImageURL());
        Debug.Log(data.GetImageURL());
    }

    public async Task LoadImageFromURL(string artworkUrl)
    {
        using (UnityWebRequest textureRequest = UnityWebRequestTexture.GetTexture(artworkUrl))
        {
            var requestOperation = textureRequest.SendWebRequest();

            while (!requestOperation.isDone)
            {
                await Task.Yield();
            }

            if (textureRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error loading Pokemon artwork: " + textureRequest.error);
            }
            else
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(textureRequest);

                // Display the texture in a RawImage
                artworkImage.texture = texture;
            }
        }
    }
}

[System.Serializable]
public class TypeSlot
{
    public GameObject slot;
    public Image icon;
    public TextMeshProUGUI text;
}

public class TypeIcon
{
    public Sprite Icon;
}
