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
    [SerializeField]
    public GameObject OptionsMenuCanvas;
    [SerializeField]
    public GameObject RankCanvas;

    public void NewGameAction()
    {
        StartCoroutine(MoveCanvasToScreen(0.001f, DifficultyMenuCanvas.GetComponent<RectTransform>()));
        StartCoroutine(MoveCanvasOutFromScreen(0.001f, RankCanvas.GetComponent<RectTransform>()));
        StartCoroutine(MoveCanvasOutFromScreen(0.001f, CategoryMenuCanvas.GetComponent<RectTransform>()));
        StartCoroutine(MoveCanvasOutFromScreen(0.001f, ModeMenuCanvas.GetComponent<RectTransform>()));
        StartCoroutine(MoveCanvasOutFromScreen(0.001f, OptionsMenuCanvas.GetComponent<RectTransform>()));
        ClearSelection(3);
        SetButtonActive(NewGameButton);
    }
    public void OptionsAction()
    {
        StartCoroutine(MoveCanvasToScreen(0.001f, OptionsMenuCanvas.GetComponent<RectTransform>()));
        StartCoroutine(MoveCanvasOutFromScreen(0.001f, RankCanvas.GetComponent<RectTransform>()));
        StartCoroutine(MoveCanvasOutFromScreen(0.001f, CategoryMenuCanvas.GetComponent<RectTransform>()));
        StartCoroutine(MoveCanvasOutFromScreen(0.001f, DifficultyMenuCanvas.GetComponent<RectTransform>()));
        StartCoroutine(MoveCanvasOutFromScreen(0.001f, ModeMenuCanvas.GetComponent<RectTransform>()));
        ClearSelection(3);
        SetButtonActive(OptionsButton);
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
    public void Start()
    {
        if (PlayerPrefs.GetInt("stats") == 0) setDefaults();

        setupRanking();
    }

    void setupRanking()
    {
        foreach (string prefix in new string[] { "Standard", "Time", "Perfect" } ){
            for( int i = 1;i <= 3;i++) {
                Transform myPlace = RankCanvas.transform.Find(prefix + "Rank/Place" + i).transform;

                myPlace.Find("Name").GetComponent<Text>().text = PlayerPrefs.GetString(prefix + "_name_" + i);
                myPlace.Find("Points").GetComponent<Text>().text = PlayerPrefs.GetInt(prefix + "_points_" + i) + " pkt";
            }
        }
    }

    void setDefaults()
    {
        PlayerPrefs.SetInt("stats", 1);
        PlayerPrefs.SetString("Standard_name_1", "Daniel");
        PlayerPrefs.SetInt("Standard_points_1", 5);
        PlayerPrefs.SetString("Standard_name_2", "Mariusz");
        PlayerPrefs.SetInt("Standard_points_2", 4);
        PlayerPrefs.SetString("Standard_name_3", "Kajetan");
        PlayerPrefs.SetInt("Standard_points_3", 4);

        PlayerPrefs.SetString("Time_name_1", "Bartek M.");
        PlayerPrefs.SetInt("Time_points_1", 5);
        PlayerPrefs.SetString("Time_name_2", "Gabrysia M.");
        PlayerPrefs.SetInt("Time_points_2", 3);
        PlayerPrefs.SetString("Time_name_3", "Maciek");
        PlayerPrefs.SetInt("Time_points_3", 3);

        PlayerPrefs.SetString("Perfect_name_1", "Anna");
        PlayerPrefs.SetInt("Perfect_points_1", 5);
        PlayerPrefs.SetString("Perfect_name_2", "Jacek");
        PlayerPrefs.SetInt("Perfect_points_2", 4);
        PlayerPrefs.SetString("Perfect_name_3", "Marzena");
        PlayerPrefs.SetInt("Perfect_points_3", 3);
    }
    public IEnumerator MoveCanvasToScreen(float t, RectTransform canvas)
    {
        while (canvas.localPosition.y > 50)
        {
            canvas.localPosition += Vector3.down * Time.deltaTime / t * 2;
            yield return null;
        }
        canvas.localPosition = new Vector3(canvas.localPosition.x, 47, canvas.localPosition.z);
    }

    public IEnumerator MoveCanvasOutFromScreen(float t, RectTransform canvas)
    {
        while (canvas.localPosition.y < 10 && canvas.localPosition.y > -500)
        {
            canvas.localPosition += Vector3.down * Time.deltaTime / t * 2;
            yield return null;
        }
        if (canvas.localPosition.y < 400) canvas.localPosition = new Vector3(canvas.localPosition.x, 800, canvas.localPosition.z);
    }

    public void SetButtonActive(GameObject button)
    {
        button.GetComponent<Image>().fillCenter = true;
        button.GetComponent<Image>().color = Color.grey;
    }

    public void SetButtonUnactive(GameObject button)
    {
        button.GetComponent<Image>().fillCenter = false;
        button.GetComponent<Image>().color = Color.white;
    }

}
