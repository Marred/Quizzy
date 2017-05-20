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

    public void NewGameAction()
    {
        StartCoroutine(MoveCanvasToScreen(0.001f, DifficultyMenuCanvas.GetComponent<RectTransform>()));
        StartCoroutine(MoveCanvasOutFromScreen(0.001f, CategoryMenuCanvas.GetComponent<RectTransform>()));
        StartCoroutine(SetButtonColorBlue(NewGameButton));
        StartCoroutine(SetButtonColorWhite(FirstDifficultyButton));
        StartCoroutine(SetButtonColorWhite(SecondDifficultyButton));
        StartCoroutine(SetButtonColorWhite(ThirdDifficultyButton));
    }

    public void DifficultyAction(int buttonId)
    {
        StartCoroutine(MoveCanvasToScreen(0.001f, CategoryMenuCanvas.GetComponent<RectTransform>()));
        switch (buttonId)
        {
            case 0:
                StartCoroutine(SetButtonColorBlue(FirstDifficultyButton));
                StartCoroutine(SetButtonColorWhite(SecondDifficultyButton));
                StartCoroutine(SetButtonColorWhite(ThirdDifficultyButton));
                break;
            case 1:
                StartCoroutine(SetButtonColorWhite(FirstDifficultyButton));
                StartCoroutine(SetButtonColorBlue(SecondDifficultyButton));
                StartCoroutine(SetButtonColorWhite(ThirdDifficultyButton));
                break;
            case 2:
                StartCoroutine(SetButtonColorWhite(FirstDifficultyButton));
                StartCoroutine(SetButtonColorWhite(SecondDifficultyButton));
                StartCoroutine(SetButtonColorBlue(ThirdDifficultyButton));
                break;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
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

    public IEnumerator SetButtonColorBlue(GameObject button)
    {
        button.GetComponent<Image>().color = Color.Lerp(Color.white, Color.blue, 1f);
        yield return null;
    }

    public IEnumerator SetButtonColorWhite(GameObject button)
    {
        button.GetComponent<Image>().color = Color.Lerp(Color.blue, Color.white, 1f);
        yield return null;
    }

}
