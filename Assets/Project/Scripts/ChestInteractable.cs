using UnityEngine;
using DG.Tweening;

public class ChestInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] private Animator anim;

    private int _isOpenHash;
    private Tween _loopTween;
    private Tween _collectTween;



    void Start()
    {
        if (!anim) return;

        _isOpenHash = Animator.StringToHash("IsOpen");

        transform.DOScale(1.1f, 1.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
    }

    public void OnHoverIn()
    {
        Debug.Log("Interactor in!");
        anim?.SetBool(_isOpenHash, true);

        // TODO - Show UI
        Toast.Instance.ShowToast("[E] Interact");
    }

    public void OnInteract()
    {
        Debug.Log($"Interacted with {gameObject.name}");

        _collectTween = transform.DOScale(0, .5f).SetEase(Ease.InBack).OnComplete(() =>
        {
            Destroy(gameObject);
        });

    }

    void OnDestroy()
    {
        DOTween.Kill(this.gameObject);
    }

    public void OnHoverOff()
    {
        Debug.Log("Interactor out!");
        anim?.SetBool(_isOpenHash, false);

        // TODO - Hide UI
        Toast.Instance.HideToast();
    }
}