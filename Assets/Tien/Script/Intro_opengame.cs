using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro_Opengame_UI : MonoBehaviour
{
    [Header("Nền đổi màu")]
    public Image image;
    public Color fromColor = Color.white;
    public Color toColor   = Color.blue;
    public float fadeDuration  = 2f;
    public float holdTime = 2f;
    public bool loop = false; 
    [Header("Ảnh fade in")]
    public Image fadeInImage;        
    public float fadeInDelay = 0f;
    public float fadeOutDuration = 4f;

    void Start()
    {
        if (image == null) image = GetComponent<Image>();
        if (image == null)
        {
            Debug.LogWarning("Chưa gán Image!");
            return;
        }

        // đặt màu ban đầu
        image.color = fromColor;

        // chạy trình tự: trắng -> xanh (2s), chờ 2s, xanh -> trắng (2s)
        StartCoroutine(ColorSequence());

        if (fadeInImage != null)
        {
            // đặt alpha = 0 trước khi fade
            var c = fadeInImage.color;
            c.a = 0f;
            fadeInImage.color = c;
            fadeInImage.enabled = true;
            StartCoroutine(FadeInThenOut());
        }

        StartCoroutine(LoadScene());
    }

    System.Collections.IEnumerator ColorSequence()
    {
        do
        {
            yield return StartCoroutine(FadeImageColor(image, fromColor, toColor, fadeDuration));

            // 2) giữ 2s
            if (holdTime > 0f) yield return new WaitForSeconds(holdTime);

            yield return StartCoroutine(FadeImageColor(image, toColor, fromColor, fadeDuration));

            // (tùy chọn) giữ 2s trước vòng mới
            if (loop && holdTime > 0f) yield return new WaitForSeconds(holdTime);

        } while (loop);
    }

    System.Collections.IEnumerator FadeImageColor(Image img, Color a, Color b, float duration)
    {
        if (duration <= 0f) { img.color = b; yield break; }

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;           // 0 -> 1 trong 'duration' giây
            float s = Mathf.SmoothStep(0f, 1f, t);    // mượt hơn Linear
            img.color = Color.Lerp(a, b, s);
            yield return null;
        }
        img.color = b; // chốt màu cuối
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }

    System.Collections.IEnumerator FadeInThenOut()
    {
        if (fadeInDelay > 0f) yield return new WaitForSeconds(fadeInDelay);

        // --- Fade In ---
        float t = 0f;
        Color startIn = fadeInImage.color; // alpha = 0
        Color endIn = startIn; endIn.a = 1f;

        while (t < 1f)
        {
            t += Time.deltaTime / Mathf.Max(0.0001f, fadeDuration);
            float s = Mathf.SmoothStep(0f, 1f, t);
            fadeInImage.color = Color.Lerp(startIn, endIn, s);
            yield return null;
        }
        fadeInImage.color = endIn; // alpha = 1

        // --- Fade Out (4s) ---
        t = 0f;
        Color startOut = fadeInImage.color; // alpha = 1
        Color endOut = startOut; endOut.a = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / Mathf.Max(0.0001f, fadeOutDuration);
            float s = Mathf.SmoothStep(0f, 1f, t);
            fadeInImage.color = Color.Lerp(startOut, endOut, s);
            yield return null;
        }
        fadeInImage.color = endOut; // alpha = 0
    }
    
    System.Collections.IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(7f);

        int count = SceneManager.sceneCountInBuildSettings;
        if (1 < 0 || 1 >= count)
        {
            Debug.LogError($"Scene index {1} không hợp lệ. Hãy kiểm tra Build Settings.");
            yield break;
        }

        SceneManager.LoadScene(1); 
    }
}
