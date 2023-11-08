using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MinimapController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private float blinkTime;
    [SerializeField] private float blinkCounter = 0;
    [SerializeField] private Color colorToDraw = Color.black;
    private Vector3Int[] positions;
    private Vector3Int previusLocation;

    [SerializeField] private GameObject map;
    [SerializeField] private Camera mapCamera;
    private void Update()
    {
        Vector3 playerTile = player.transform.position;
        Vector3Int location = tilemap.WorldToCell(playerTile);
        CheckLastLocation(location);
        
        previusLocation = location;

        CheckBlinkTime();

        //tilemap.SetColor(location, Color.white);
        SetCameraToTile(location);

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!map.activeInHierarchy)
            {
                mapCamera.orthographicSize = 100;
            }
            else
            {
                mapCamera.orthographicSize = 30;
            }
            map.SetActive(!map.activeInHierarchy);
        }
    }

    private void SetCameraToTile(Vector3Int location)
    {
        Vector3 camPosition = tilemap.CellToWorld(location);
        Vector3 camToCenter = new Vector3(camPosition.x + 8, camPosition.y + 8, -1);

        transform.position = camToCenter;
    }

    private void CheckBlinkTime()
    {
        blinkCounter += Time.deltaTime;
        if (blinkCounter >= blinkTime)
        {
            blinkCounter = 0;
            Color tileColor = tilemap.GetColor(previusLocation);
            colorToDraw = tileColor == Color.white ? colorToDraw = Color.black : colorToDraw = Color.white;
            tilemap.SetColor(previusLocation, colorToDraw);
        }
    }

    private void CheckLastLocation(Vector3Int location)
    {
        if (previusLocation == location || previusLocation == Vector3Int.zero) return;
        tilemap.SetColor(previusLocation, Color.white);
    }
}
