using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    [SerializeField]
    public GameObject NewGameButton;
    [SerializeField]
    public GameObject OptionsButton;
    [SerializeField]
    public GameObject ExitButton;

    [SerializeField]
    public GameObject FirstDifficultyButton;
    [SerializeField]
    public GameObject SecondDifficultyButton;
    [SerializeField]
    public GameObject ThirdDifficultyButton;

    [SerializeField]
    public GameObject FirstCategoryButton;
    [SerializeField]
    public GameObject SecondCategoryButton;
    [SerializeField]
    public GameObject ThirdCategoryButton;


    [SerializeField]
    public GameObject DifficultyMenuCanvas;
    [SerializeField]
    public GameObject CategoryMenuCanvas;
    [SerializeField]
    public GameObject ModeMenuCanvas;

    public void NewGameAction()
    {
        StartCoroutine(MoveCanvasToScreen(0.001f, DifficultyMenuCanvas.GetComponent<RectTransform>()));
        StartCoroutine(MoveCanvasOutFromScreen(0.001f, CategoryMenuCanvas.GetComponent<RectTransform>()));
        StartCoroutine(MoveCanvasOutFromScreen(0.001f, ModeMenuCanvas.GetComponent<RectTransform>()));
        ClearSelection(3);
        SetButtonActive(NewGameButton);
    }

    public void DifficultyAction(GameObject button)
    {
        StartCoroutine(MoveCanvasToScreen(0.001f, CategoryMenuCanvas.GetComponent<RectTransform>()));
        StartCoroutine(MoveCanvasOutFromScreen(0.001f, ModeMenuCanvas.GetComponent<RectTransform>()));
        ClearSelection(2);
        SetButtonActive(button);
    }

    public void CategoryAction(GameObject button)
    {
        StartCoroutine(MoveCanvasToScreen(0.001f, ModeMenuCanvas.GetComponent<RectTransform>()));
        ClearSelection(1);
        SetButtonActive(button);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    //1: czyści CategoryCanvas, 2: czyści DifficultyCanvas, 3: czyści PrimaryCanvas
    public void ClearSelection(byte selectionSize)
    {
        switch (selectionSize)
        {
            case 1:
                SetButtonUnactive(FirstCategoryButton);
                SetButtonUnactive(SecondCategoryButton);
                SetButtonUnactive(ThirdCategoryButton);
                break;
            case 2:
                SetButtonUnactive(FirstDifficultyButton);
                SetButtonUnactive(SecondDifficultyButton);
                SetButtonUnactive(ThirdDifficultyButton);
                goto case 1;
            case 3:
                SetButtonUnactive(NewGameButton);
                SetButtonUnactive(OptionsButton);
                goto case 2;
        }
    }

    public IEnumerator MoveCanvasToScreen(float t, RectTransform canvas)
    {
        while (canvas.localPosition.y > 0)
        {
            canvas.localPosition += Vector3.down * Time.deltaTime / t;
            yield return null;
        }
    }

    public IEnumerator MoveCanvasOutFromScreen(float t, RectTransform canvas)
    {
        while (canvas.localPosition.y < 10 && canvas.localPosition.y > -500)
        {
            canvas.localPosition += Vector3.down * Time.deltaTime / t;
            yield return null;
        }
        if (canvas.localPosition.y < 400) canvas.localPosition += Vector3.up * 1200;
    }

    public void SetButtonActive(GameObject button)
    {
        button.GetComponent<Image>().color = Color.blue;
    }

    public void SetButtonUnactive(GameObject button)
    {
        button.GetComponent<Image>().color = Color.white;
    }

}
