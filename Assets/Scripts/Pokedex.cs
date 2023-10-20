using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Threading.Tasks;

[CustomEditor(typeof(Pokedex))]
public class PokedexEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Pokedex pokedex = (Pokedex)target;

        GUILayout.Space(10);

        if (GUILayout.Button("Load"))
        {
            //pokedex.LoadAllPokemonDataAsync();
        }
    }
}

public class Pokedex : MonoBehaviour
{
    public static Pokedex Instance;

    [SerializeField] private UIManager UIManager;
    [SerializeField] private RequestHandler requestHandler;
    public int count = 12;
    public PokedexData data;
    public List<Pokemon> pokemonList = new List<Pokemon>();

    private void Awake()
    {
        Instance = this;

    }

    async void Start()
    {
        // Call the GetRequest method in the RequestHandler
        await requestHandler.GetPokemonCount(this);

        await LoadAllPokemonDataAsync();
    }


    async Task LoadAllPokemonDataAsync()
    {
        List<Task> loadTasks = new List<Task>();

        loadTasks.Add(requestHandler.GetPokemonDataAsync());

        await Task.WhenAll(loadTasks);
    }

    public void AddPokemon(Pokemon pokemon)
    {
        pokemonList.Add(pokemon);

        // If the list is now complete, you can perform actions here.
        if (pokemonList.Count == 20)
        {
            AllPokemonLoaded();
        }
    }

    private void AllPokemonLoaded()
    {
        // All Pokemon data is now loaded. You can perform actions here.
        Pokemon _pokemon = pokemonList[0];

        _pokemon.DisplayBaseInfo();
        _pokemon.DisplayTypes();
        _pokemon.DisplayStats();

        // Get the URL of the official artwork.
        string artworkUrl = _pokemon.GetImageURL();
        
        UIManager.LoadBaseInfo(_pokemon);

        _ = UIManager.LoadImageFromURL(artworkUrl);
    }
}

public class PokedexData
{
    public int count;
}
