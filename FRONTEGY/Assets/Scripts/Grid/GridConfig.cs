using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GridConfig
{
    [SerializeField] private int seed;
    [SerializeField] private Vector2Int size;
    [SerializeField] private List<Vector2Int> tileWeights;
    [SerializeField] private List<Vector2Int> cardWeights;
    [SerializeField] private int startCardCount;
    [SerializeField] private bool giveSameCards;
    [SerializeField] private CardBPs cardBPs;
    [SerializeField] private Sprite backSprite;

    public int getSeed() { return seed; }
    public void setSeed(int seed) { this.seed = seed; }
    public Vector2Int getSize() { return size; }
    public List<Vector2Int> getTileWeights() { if (tileWeights == null) Debug.LogError("InspectorException: set tileWeights"); if (tileWeights.Count == 0) Debug.LogError("InspectorException: empty tileWeights"); return tileWeights; }
    public List<Vector2Int> getCardWeights() { if (cardWeights == null) Debug.LogError("InspectorException: set cardWeights"); if (cardWeights.Count == 0) Debug.LogError("InspectorException: empty cardWeights"); return cardWeights; }
    public int getStartCardCount() { return startCardCount; }
    public bool getGiveSameCards() { return giveSameCards; }
    public CardBPs getBPs() { if (cardBPs == null) Debug.LogError("InspectorException: set roles"); cardBPs.verify(); return cardBPs; }
    public Sprite getBackSprite() { if (backSprite == null) Debug.LogError("InspectorException: set backSprite"); return backSprite; }
}
