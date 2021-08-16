using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ICardBP
{  // Short for Card Blueprint
    void cast(Card card, Tile tile, CastType type);
}
