using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    public Text hitInfoText;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // Найти экземпляр GameManager в сцене
    }

    public void OnResetButtonClick()
    {
        if (gameManager != null)
        {
            // Сбросить информацию о попаданиях и счетчик выстрелов
            gameManager.ResetHitInfo();
            // Очистить текстовый объект
            hitInfoText.text = "";
        }
    }
}
