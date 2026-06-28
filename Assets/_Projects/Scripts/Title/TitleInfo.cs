using UnityEngine;
using UnityEngine.UI;
using R3;
using Unity.Collections.LowLevel.Unsafe;

public class TitleInfo : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;

    public Toggle easyToggle;
    public Toggle normalToggle;
    public Toggle hardToggle;

    public Subject<DifficultyType> difficultyChangedSubject;

    public Observable<Unit> OnStartClicked;
    public Observable<Unit> OnExitClicked;
    public Observable<DifficultyType> OnDifficultyChanged;

    public CompositeDisposable disposables;

    public void Awake()
    {
        OnStartClicked = startButton.onClick.AsObservable();
        OnExitClicked = exitButton.onClick.AsObservable();
    }

    public void BindDifficultyToggle(Toggle toggle, DifficultyType difficultyType)
    {
        if (toggle.isOn)
        {
            difficultyChangedSubject.OnNext(difficultyType);
        }
    }

    public DifficultyType GetSelectedDifficulty()
    {
        if (easyToggle.isOn)   return DifficultyType.Easy;
        if (normalToggle.isOn) return DifficultyType.Normal;
        if (hardToggle.isOn)   return DifficultyType.Hard;
    }

    public void SetInteractable(bool interactable)
    {
        startButton.interactable = interactable;
        exitButton.interactable = interactable;

        easyToggle.interactable = interactable;
        normalToggle.interactable = interactable;
        hardToggle.interactable = interactable;
    }

    public void OnDestroy()
    {
        disposables.Dispose();

        difficultyChangedSubject.OnCompleted();
        difficultyChangedSubject.Dispose();
    }
}
