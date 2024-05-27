using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public Text hitInfoText; // UI элемент для отображения информации
    private List<string> hitInfoList = new List<string>();
    private int hitCount = 0;

    public void OnHit(float damage)
    {
        hitCount++;
        string hitInfo = $"Попадание - {hitCount}, Очки - {damage}";
        hitInfoList.Add(hitInfo);
        UpdateHitInfo();
    }

    private void UpdateHitInfo()
    {
        hitInfoText.text = string.Join("\n", hitInfoList);
    }
    public void ResetHitInfo()
    {
        hitInfoList.Clear(); // Очистить список информации о попаданиях
        hitCount = 0; // Сбросить счетчик выстрелов
    }
}
