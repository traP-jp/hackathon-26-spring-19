using R3;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseInfo : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button titleButton;

    public Observable<Unit> OnPauseClicked { get; private set; }
    public Observable<Unit> OnResumeClicked { get; private set; }
    public Observable<Unit> OnTitleClicked { get; private set; }

    private void Awake()
    {
        OnPauseClicked = pauseButton.OnClickAsObservable();
        OnResumeClicked = resumeButton.OnClickAsObservable();
        OnTitleClicked = titleButton.OnClickAsObservable();
    }

    public void SetPauseButtonInteractable(bool interactable)
    {
        pauseButton.interactable = interactable;
    }

    public void SetMenuButtonsInteractable(bool interactable)
    {
        resumeButton.interactable = interactable;
        titleButton.interactable = interactable;
    }
}
