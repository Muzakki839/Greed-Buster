using UnityEngine;
using DG.Tweening;

/// <summary>
/// Set start and end position for the object via context menu in (un-playing)editor, then call PlayForward() or PlayBackward() to animate the object.
/// </summary>
public class DOMove : MonoBehaviour
{
    public bool playOnAwake = true;

    public Vector3 startPos;
    public Vector3 endPos;

    [Header("")]
    public bool useLocalForward = true;
    public float forwardTime = 1f;

    public bool useLocalBackward = true;
    public float backwardTime = 0.5f;

    private void Awake()
    {
        transform.localPosition = startPos;
        if (playOnAwake)
        {
            PlayForward();
        }
    }

    [ContextMenu("PlayForward")]
    public void PlayForward()
    {
        if (transform.localPosition != endPos)
        {
            if (useLocalForward)
            {
                transform.DOLocalMove(endPos, forwardTime)
                    .SetEase(Ease.OutQuint)
                    .From(startPos);
            }
            else
            {
                transform.DOMove(endPos, forwardTime)
                    .SetEase(Ease.OutQuint)
                    .From(startPos);
            }
        }
    }

    [ContextMenu("PlayBackward")]
    public void PlayBackward()
    {
        if (transform.localPosition != startPos)
        {
            if (useLocalBackward)
            {
                transform.DOLocalMove(startPos, backwardTime)
                    .SetEase(Ease.InOutSine)
                    .From(endPos);
            }
            else
            {
                transform.DOMove(startPos, backwardTime)
                    .SetEase(Ease.InOutSine)
                    .From(endPos);
            }
        }
    }

    [ContextMenu("Set/SetStartPos")]
    public void SetStartPos()
    {
        startPos = transform.localPosition;
    }

    [ContextMenu("Set/SetEndPos")]
    public void SetEndPos()
    {
        endPos = transform.localPosition;
    }

    [ContextMenu("To/ToStartPos")]
    public void ToStartPos()
    {
        transform.localPosition = startPos;
    }

    [ContextMenu("To/ToEndPos")]
    public void ToEndPos()
    {
        transform.localPosition = endPos;
    }
}
