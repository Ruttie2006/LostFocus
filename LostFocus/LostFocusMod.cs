using Modding;
using UnityEngine;

namespace LostFocus
{
    public sealed class LostFocusMod : Mod, ITogglableMod
    {
        GameObject? go;
        public LostFocusMod() : base("LostFocus") { }
        public override string GetVersion() => "1.0";
        public override void Initialize()
        {
            if (go == null)
            {
                go = new("LostFocusModObject", typeof(Behaviour));
                GameObject.DontDestroyOnLoad(go);
            }
        }

        public void Unload()
        {
            if (go != null)
            {
                GameObject.Destroy(go);
                go = null;
            }
        }

        private sealed class Behaviour : MonoBehaviour
        {
            public void OnApplicationFocus(bool hasFocus)
            {
                if (hasFocus)
                {
                    if (GameManager.instance.IsGamePaused())
                        StartCoroutine(GameManager.instance.PauseGameToggle());
                    AudioListener.pause = false;
                }
                else
                {
                    if (!GameManager.instance.IsGamePaused())
                        StartCoroutine(GameManager.instance.PauseGameToggle());
                    AudioListener.pause = true;
                }
            }
        }
    }
}
