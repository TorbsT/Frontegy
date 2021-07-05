using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ICardBP : IVerifiable
{  // Short for Card Blueprint
    void cast(Card card, Tile tile, CastType type);
}
