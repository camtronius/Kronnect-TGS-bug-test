using UnityEngine;
using System.Collections;

namespace TGS {

    public enum FaderStyle {
        FadeOut = 0,
        Blink = 1,
        Flash = 2,
        ColorTemp = 3
    }

    public class SurfaceFader : MonoBehaviour {

        public int cacheIndex;
        TerrainGridSystem grid;
        Material fadeMaterial;
        float startTime, duration;
        Color color, initialColor;
        FaderStyle style;
        int repetitions;

        public static void Animate(int cacheIndex, FaderStyle style, TerrainGridSystem grid, GameObject surface, Material fadeMaterial, Color color, float duration, int repetitions) {
            if (surface == null) return;
            surface.SetActive(true);
            SurfaceFader fader = surface.AddComponent<SurfaceFader>();
            fader.cacheIndex = cacheIndex;
            fader.grid = grid;
            fader.startTime = Time.time;
            fader.duration = duration + 0.0001f;
            fader.color = color;
            fader.fadeMaterial = fadeMaterial;
            fader.style = style;
            fader.initialColor = fadeMaterial.color;
            fader.repetitions = repetitions;
        }


        void Update() {
            float elapsed = Time.time - startTime;
            switch (style) {
                case FaderStyle.FadeOut:
                    UpdateFadeOut(elapsed);
                    break;
                case FaderStyle.Blink:
                    UpdateBlink(elapsed);
                    break;
                case FaderStyle.Flash:
                    UpdateFlash(elapsed);
                    break;
                case FaderStyle.ColorTemp:
                    UpdateColorTemp(elapsed);
                    break;
            }
        }

        void OnDestroy() {
            if (fadeMaterial != null && grid != null) {
                grid.materialPool.Return(fadeMaterial);
            }
        }

        #region Fade Out effect

        public void Finish(float fadeOutDuration) {
            repetitions = 0;

            if (fadeOutDuration <= 0) {
                startTime = float.MinValue;
                duration = 1f;
                Update();
            } else {
                style = FaderStyle.FadeOut;
                color = fadeMaterial.color;
                startTime = Time.time;
                duration = fadeOutDuration;
            }
        }

        void UpdateFadeOut(float elapsed) {
            float newAlpha = Mathf.Clamp01(1.0f - elapsed / duration);
            SetAlpha(newAlpha);
            if (elapsed >= duration) {
                if (--repetitions > 0) {
                    startTime = Time.time;
                    return;
                }
                SetAlpha(0);
                DestroyImmediate(gameObject);
            }
        }

        void SetAlpha(float newAlpha) {
            Color newColor = new Color(color.r, color.g, color.b, color.a * newAlpha);
            fadeMaterial.color = newColor;
        }

        #endregion

        #region Flash effect

        void UpdateFlash(float elapsed) {
            SetFlashColor(elapsed / duration);
            if (elapsed >= duration) {
                if (--repetitions > 0) {
                    startTime = Time.time;
                    return;
                }
                SetFlashColor(1f);
                DestroyImmediate(gameObject);
            }
        }


        void SetFlashColor(float t) {
            if (t < 0)
                t = 0;
            else if (t > 1)
                t = 1f;
            Color newColor = Color.Lerp(color, initialColor, t);
            fadeMaterial.color = newColor;
        }

        #endregion

        #region Blink effect

        void UpdateBlink(float elapsed) {
            SetBlinkColor(elapsed / duration);
            if (elapsed >= duration) {
                if (--repetitions > 0) {
                    startTime = Time.time;
                    return;
                }
                SetBlinkColor(0);
                DestroyImmediate(gameObject);
            }
        }

        void SetBlinkColor(float t) {
            Color newColor;
            if (t < 0.5f) {
                if (t < 0)
                    t = 0;
                newColor = Color.Lerp(initialColor, color, t * 2f);
            } else {
                if (t > 1)
                    t = 1;
                newColor = Color.Lerp(color, initialColor, (t - 0.5f) * 2f);
            }
            fadeMaterial.color = newColor;
        }

        #endregion

        #region Color Temp effect

        void UpdateColorTemp(float elapsed) {
            SetColorTemp(1);
            if (elapsed >= duration) {
                if (--repetitions > 0) {
                    startTime = Time.time;
                    return;
                }
                SetColorTemp(0);
                DestroyImmediate(gameObject);
            }
        }


        void SetColorTemp(float t) {
            fadeMaterial.color = t == 0 ? initialColor : color;
        }

        #endregion


    }

}