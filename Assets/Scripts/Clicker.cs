using UnityEngine;
using TMPro;
using DG.Tweening;

public class Clicker : MonoBehaviour
{
    public TextMeshProUGUI clickBox;

    [Header("Animation settings")]
    public float duration = 0.3f;
    public Vector3 size = Vector3.one * 1.5f;
    public Ease ease = Ease.Linear;

    int clicks = 0;
    AudioSource audio;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        LoadScore();
    }

    private void OnMouseDown()
    {
        clickBox.transform
            .DOScale(Vector3.zero, duration / 2)
            .OnComplete(
            () =>
            {
                clickBox.text = $"{++clicks}";
                clickBox.transform.DOScale(Vector3.one, duration / 2);
            });


        audio.pitch = Random.Range(0.9f, 1.2f);
        audio.Play();

        transform
            .DOScale(Vector3.one, duration)
            .ChangeStartValue(size)
            .SetEase(ease);
    }
    private void OnApplicationQuit()
    {
        SaveScore();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveScore();
    }

    void SaveScore()
    {
        PlayerPrefs.SetInt("clicks", clicks);
        PlayerPrefs.Save();
    }

    void LoadScore()
    {
        if (PlayerPrefs.HasKey("clicks"))
        {
            clicks = PlayerPrefs.GetInt("Click");
            clickBox.text = clicks.ToString();
        }
    }
}
