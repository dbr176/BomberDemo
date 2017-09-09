using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GeneratedMap : MonoBehaviour
{
    public int width = 10;
    public int height = 15;

    public int startPosX = 5;
    public int startPosY = 5;

    public float tileWidth = 1.0f;
    public float tileHeight = 1.0f;

    public float impassableProb = 0.1f;


    public GameObject impassableTile;
    public GameObject emptyTile;
    public GameObject borderTile;
    public GameObject playerTile;

    public Joystick joystick;

    protected virtual void Start()
    {
        Assert.IsNotNull(impassableTile);
        Assert.IsNotNull(emptyTile);
        Assert.IsNotNull(borderTile);
        Assert.IsNotNull(playerTile);
        Assert.IsTrue(impassableProb >= 0.0f || impassableProb <= 1.0f);

        for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                GameObject tile = null;

                var isPlayerPos = x == startPosX && y == startPosY;
                var currentPos = new Vector2(x * tileWidth, y * tileHeight);

                if (x == 0
                    || x == width - 1
                    || y == 0
                    || y == height - 1)
                    tile = Instantiate(borderTile, transform);
                else
                    tile = Instantiate(
                            Random.value > impassableProb || isPlayerPos
                                ? emptyTile
                                : impassableTile, transform);
                tile.transform.localPosition = currentPos;
                if (isPlayerPos)
                {
                    var player = Instantiate(playerTile, transform);
                    player.transform.localPosition = (Vector3)currentPos + Vector3.back;
                    var inp = player.AddComponent<JoystickInput>();
                    inp.player = player.GetComponent<Player>();
                    inp.joystick = joystick;
                }
            }
    }
}
