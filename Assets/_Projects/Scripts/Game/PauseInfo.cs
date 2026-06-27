using UnityEngine;

public class PauseInfo : MonoBehaviour
{
    [SerializeField]public Button pauseButton;
    [SerializeField]public Button resumeButton;
    [SerializeField]public Button titleButton;

    public Observable<Unit> OnPauseClicked
    public Observable<Unit> OnResumeClicked
    public Observable<Unit> OnTitleClicked

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        OnPauseClicked = pauseButton.OnClickAsObservable();
        OnResumeClicked = resumeButton.OnClickAsObservable();
        OnTitleClicked = titleButton.OnClickAsObservable();
    }

    // Update is called once per frame
    void SetPauseButtonInteractable(bool interactable)
    {
        pauseButton.interactable = interactable;
    }

    void SetMenuButtonsInteractable(bool interactable)
    {
        resumeButton.interactable = interactable;
        titleButton.interactable = interactable;
    }

}
