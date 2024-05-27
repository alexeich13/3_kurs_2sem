using UnityEngine;

public class ShowButtons : MonoBehaviour
{
    public GameObject button1; // Первая новая кнопка
    public GameObject button2; // Вторая новая кнопка
	public GameObject button3;
    public GameObject panel;
    public GameObject podskaz;
    public GameObject table_panel;
    public GameObject table;

    void Start()
    {
        // Изначально скрываем кнопки
        button1.SetActive(false);
        button2.SetActive(false);
		button3.SetActive(false);
        panel.SetActive(false);
        podskaz.SetActive(false);
        table_panel.SetActive(false);
        table.SetActive(false);
    }

    public void ShowNewButtons()
    {
        // Отображаем новые кнопки
        button1.SetActive(true);
        button2.SetActive(true);
        panel.SetActive(true);
        podskaz.SetActive(true);
        table_panel.SetActive(false);
    }
	public void ShowText()
    {
        // Отображаем новые кнопки
        button3.SetActive(true);
    }
    public void MoveToShooting()
    {
        button1.SetActive(false);
        button2.SetActive(false);
		button3.SetActive(false);
        panel.SetActive(false);
        podskaz.SetActive(false);
        table_panel.SetActive(true);
        table.SetActive(false);
    }
    public void OpenTable()
    {
        table.SetActive(true);
    }
    public void CloseTable()
    {
        table.SetActive(false);
    }
}
