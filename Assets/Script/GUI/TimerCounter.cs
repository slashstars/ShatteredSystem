using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerCounter : MonoBehaviour
{
    private RectTransform slider;
    private float sliderMax;
    private float sliderStep;
    private Image background;
    private Gradient g;
    private LevelManager manager;

    // Use this for initialization
    void Start()
    {
        manager = GameObject.Find(Names.Game).GetComponentInChildren<LevelManager>();

        background = GetComponent<Image>();
        slider = GetComponent<RectTransform>();
        sliderMax = slider.rect.width;
        sliderStep = sliderMax / manager.totalTime;

        g = new Gradient();
        var gck = new GradientColorKey[2];
        gck[0].color = Color.red;
        gck[0].time = 0.0f;

        gck[1].color = Color.green;
        gck[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        var gak = new GradientAlphaKey[0];

        g.SetKeys(gck, gak);
    }

    // Update is called once per frame
    void Update()
    {
        background.color = g.Evaluate(manager.currentTime / manager.totalTime);
        slider.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, manager.currentTime * sliderStep);
    }
}
