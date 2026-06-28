using R3;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Pauselnfo : MonoBehaviour
{
    [SerializeField]
    private Button pauseButton;

    [SerializeField]
    private Button resumeButton;

    [SerializeField]
    private Button titleButton;

    public Observable<Unit> OnPauseClicked { get; private set; }
    public Observable<Unit> OnResumeClicked { get; private set; }
    public Observable<Unit> OnTitleClicked { get; private set; }

    private void Awake()
    {
        if (pauseButton != null)
        {
            OnPauseClicked = pauseButton.OnClickAsObservable();
        }

        if (resumeButton != null)
        {
            OnResumeClicked = resumeButton.OnClickAsObservable();
        }

        if (titleButton != null)
        {
            OnTitleClicked = titleButton.OnClickAsObservable();
        }
    }

    public void SetPauseButtonInteractable(bool interactable)
    {
        if (pauseButton != null)
        {
            pauseButton.interactable = interactable;
        }
    }

    public void SetMenuButtonsInteractable(bool interactable)
    {
        if (resumeButton != null)
        {
            resumeButton.interactable = interactable;
        }

        if (titleButton != null)
        {
            titleButton.interactable = interactable;
        }
    }
}