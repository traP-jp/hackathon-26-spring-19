using UnityEngine;
using UnityEngine.UI;
using R3;

public class ResultInfo : MonoBehaviour
{
    [SerializeField]public Button retryButton;
    [SerializeField]public Button titleButton;
    [SerializeField]public Observable<Unit> OnRetryClicked;
    [SerializeField]public Observable<Unit> OnTitleClicked;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Awake()
    {
        OnRetryClicked = retryButton.OnClickAsObservable();
        OnTitleClicked = titleButton.OnClickAsObservable();
    }

    // Update is called once per frame
    public void SetInteractable(bool interactable)
    {
        retryButton.interactable = interactable;
        titleButton.interactable = interactable;
    }
}
