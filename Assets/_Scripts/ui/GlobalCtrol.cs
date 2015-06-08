using UnityEngine;
using System.Collections;

public enum GameState
{
    Playing,
    GameOver,
}
public class GlobalCtrol  {

	// Use this for initialization
    public static string ENEMY = "Enemy";
    public static string PLAYER = "Player";
    public static string ATTACK = "Attack";

    public static GameState gameState = GameState.Playing;
}
