
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using StardropTools.Pool;
using StardropTools.Tween;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [Header("Main Components")]
    [SerializeField] TextMeshProUGUI    generatedText;
    [SerializeField] Slider             maxCharactersSlider;
    [SerializeField] TweenComponent     tweenGeneratedText;
    [NaughtyAttributes.Expandable][SerializeField] NameGeneratorSO nameGenerator;

    [Header("History")]
    [SerializeField] Pool       pool_HistoryItem;
    [SerializeField] Transform  historyParent;
    [SerializeField] Transform  favouriteParent;

    [Header("Generation")]
    [SerializeField] int maxHistoryItems = 30;
    [SerializeField] string generatedName;
    [SerializeField] string randomizedName;
    [SerializeField] List<HistoryItem> history;

    [SerializeField] bool canUseMaxCharacterLength;

    #region Events

    public static readonly EventHandler OnGenerate  = new EventHandler();
    public static readonly EventHandler OnRandomize = new EventHandler();

    public static readonly EventHandler<string> OnHistoryItemClicked        = new EventHandler<string>();
    public static readonly EventHandler<bool>   OnToggleRandomCharacters    = new EventHandler<bool>();
    #endregion // Events

    private void Awake()
    {
        OnGenerate.AddListener(GenerateNewName);
        OnRandomize.AddListener(RandomizeName);
        OnHistoryItemClicked.AddListener(HisotryItemClicked);
        OnToggleRandomCharacters.AddListener(SetRandomCharacterLength);

        GenerateNewName();
    }

    void SetGeneratedTextValue(string value)
    {
        value = Utilities.FirstLetterUppercase(value);
        generatedText.text = value;
        tweenGeneratedText.StartTween();
    }

    void GenerateNewName()
    {
        // create history item
        if (generatedText.text.Length > 0)
            SpawnHistoryItem(historyParent, generatedText.text);

        int charAmount = canUseMaxCharacterLength ? Random.Range((int)maxCharactersSlider.minValue, (int)maxCharactersSlider.value + 1) : (int)maxCharactersSlider.value;

        generatedName = nameGenerator.GenerateName(charAmount);
        SetGeneratedTextValue(generatedName);
    }

    void RandomizeName()
    {
        randomizedName = nameGenerator.RandomizeName(generatedName);
        SetGeneratedTextValue(randomizedName);
    }

    void SpawnHistoryItem(Transform parent, string content)
    {
        var item = pool_HistoryItem.Spawn<HistoryItem>(Vector3.zero, Quaternion.identity, parent);
        item.SetGeneratedName(content);
        history.Add(item);

        if (history.Count > maxHistoryItems)
        {
            var first = history.GetFirst();
            first.Despawn();
            history.Remove(first);
        }
    }

    void HisotryItemClicked(string historyName)
    {
        if (generatedName == historyName)
            return;

        generatedName = historyName;
        SetGeneratedTextValue(historyName);
    }

    void SetRandomCharacterLength(bool value) => canUseMaxCharacterLength = value;
}