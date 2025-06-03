using UnityEngine;
using DG.Tweening;

public class DOScale : MonoBehaviour
{
    public bool playOnAwake = true;

    [Header("General Scaling")]
    public bool useGeneralScale;
    public float scale = 0.03f;

    [Header("Manual Scaling")]
    public Vector3 normalScale;
    public Vector3 upScale;
    public Vector3 downScale;

    [Header("")]
    public float duration = 0.2f;

    private void Start()
    {
        if (useGeneralScale)
        {
            upScale = normalScale + new Vector3(scale, scale, scale);
            downScale = normalScale - new Vector3(scale, scale, scale);
        }
    }

    private void Awake()
    {
        if (playOnAwake)
        {
            transform.localScale = downScale;
            ScaleUp();
        }
    }

    [ContextMenu("ScaleUp")]
    public void ScaleUp()
    {
        transform.DOScale(upScale, duration);
    }

    [ContextMenu("ScaleDown")]
    public void ScaleDown()
    {
        transform.DOScale(downScale, duration);
    }

    [ContextMenu("ScaleNormal")]
    public void ScaleNormal()
    {
        transform.DOScale(normalScale, duration);
    }

    // ====================================================================

    [ContextMenu("Set/SetNormalScale")]
    public void SetNormalScale()
    {
        normalScale = transform.localScale;
    }

    [ContextMenu("Set/SetUpScale")]
    public void SetUpScale()
    {
        upScale = transform.localScale;
    }

    [ContextMenu("Set/SetDownScale")]
    public void Set()
    {
        downScale = transform.localScale;
    }

    // ====================================================================

    [ContextMenu("To/ToNormalScale")]
    public void ToNormalScale()
    {
        transform.localScale = normalScale;
    }

    [ContextMenu("To/ToUpScale")]
    public void ToUpScale()
    {
        transform.localScale = upScale;
    }

    [ContextMenu("To/ToDownScale")]
    public void ToDownScale()
    {
        transform.localScale = downScale;
    }
}
