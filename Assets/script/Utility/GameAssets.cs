using System.Collections.Generic;
using UnityEngine;
using Jy_Util;


public class GameAssets : MonoSingleton<GameAssets>
{
    public Color knobNormalColor;
    public Color knobHoverColor;
    public Color knobConnectedColor;
    [Header("Game Events")]
    public GameEvent OnCoinCollectedEvent;
}
