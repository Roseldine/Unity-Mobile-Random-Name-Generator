
using UnityEngine;
using StardropTools.Pool;
using StardropTools.Tween;

public class HistoryItem : MonoBehaviour, IPoolable
{
    [SerializeField] string generatedName;
    [SerializeField] TMPro.TextMeshProUGUI textMesh;
    [SerializeField] UnityEngine.UI.Button button;
    [SerializeField] TweenComponent tweenComponent;
    [SerializeField] Transform selfTransform;

    private void Awake()
    {
        selfTransform = transform;
        button.onClick.AddListener(HistoryItemClicked);
    }

    void HistoryItemClicked() => GameManager.OnHistoryItemClicked?.Invoke(generatedName);

    public void SetGeneratedName(string newName)
    {
        generatedName = newName;
        textMesh.text = generatedName;
    }

    public void SetAsFirstSibling() => selfTransform.SetAsFirstSibling();

    #region Poolable

    PoolItem poolItem;

    public void SetPoolItem(PoolItem poolItem) => this.poolItem = poolItem;

    public void Despawn() => poolItem.Despawn();

    public void OnSpawn()
    {
        SetAsFirstSibling();

        if (tweenComponent != null)
            tweenComponent.StartTween();
    }

    public void OnDespawn()
    {
        if (tweenComponent != null)
            tweenComponent.StopTween();
    }

    #endregion // poolable

}
