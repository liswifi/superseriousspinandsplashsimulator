using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public Vector3 spawnPoint;
    public GameObject playerCharacter;
    bool hasDied;
    public GameObject winGraphic;
    public TextMeshProUGUI timerText;
    public float timerValue;
    string timerTime = "";
    bool winGame;

    public IEnumerator RestartGame()
    {
        if (!hasDied)
        {
            hasDied = true;
            yield return new WaitForSeconds(5.0F);
            var player = Instantiate(playerCharacter, spawnPoint, Quaternion.identity);
            FindFirstObjectByType<CameraFollow>().playerRef = player;
        }
        hasDied = false;
        yield return null;
    }

    public void YouWin()
    {
        winGraphic.SetActive(true);
        winGame = true;
        Destroy(FindFirstObjectByType<InputManager>());
    }

    private void Update()
    {
        if (!winGame)
        {
            timerValue += Time.deltaTime;
            timerValue = Mathf.Ceil(timerValue);
            timerText.text = timerValue.ToString();
        }
    }
}
