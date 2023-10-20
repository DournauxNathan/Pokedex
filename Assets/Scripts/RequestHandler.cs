using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Threading.Tasks;

public class RequestHandler : MonoBehaviour
{
    private string mainURL = "https://pokeapi.co/api/v2/pokemon/";

    public async Task GetPokemonCount(Pokedex pokedex)
    {
        using (UnityWebRequest countRequest = UnityWebRequest.Get(mainURL))
        {
             var requestOperation = countRequest.SendWebRequest();

            while (!requestOperation.isDone)
            {
                await Task.Yield();
            }

            if (countRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + countRequest.error);
            }
            else
            {
                // Successfully received a response.
                string mainInfo = countRequest.downloadHandler.text;
                
                PokedexData _pokedex = JsonUtility.FromJson<PokedexData>(mainInfo);
                
                pokedex.count = 1017/*_pokedex.count*/;
            }
        }
    }

    public async Task GetPokemonDataAsync()
    {
        for (int i = 1; i <= Pokedex.Instance.count; i++)
        {
            string url = "https://pokeapi.co/api/v2/pokemon/";

            using (UnityWebRequest webRequest = UnityWebRequest.Get(url + i))
            {
                var requestOperation = webRequest.SendWebRequest();

                while (!requestOperation.isDone)
                {
                    await Task.Yield();
                }

                if (webRequest.result != UnityWebRequest.Result.Success)
                {

                    Debug.LogError("Error: " + webRequest.error + " at " + i);
                }
                else
                {
                    // Successfully received a response.
                    string response = webRequest.downloadHandler.text;

                    Pokemon _pokemon = JsonUtility.FromJson<Pokemon>(response);
                    _pokemon.GetData(response);

                    // Add the retrieved Pokemon to the Pokedex
                    Pokedex.Instance.AddPokemon(_pokemon);
                }
            }
        }
    }

    public void CallCoroutine(string method, MonoBehaviour mono)
    {
        StartCoroutine(method, mono);
    }

    public void CallCoroutine(string method)
    {
        StartCoroutine(method);
    }
}
