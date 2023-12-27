using Modding;
using UnityEngine;

namespace LostFocus
{
    public sealed class LostFocusMod : Mod, ITogglableMod
    {
        GameObject? go;
        Behaviour behaviour = null!;
        public LostFocusMod() : base("LostFocus") { }
        public override string GetVersion() => "v0.1";
        public override void Initialize()
        {
            if (go == null)
            {
                go = new("LostFocusModObject", typeof(Behaviour));
                GameObject.DontDestroyOnLoad(go);
                behaviour = go.GetComponent<Behaviour>();
            }
            behaviour.Active = true;
        }

        public void Unload()
        {
            if (behaviour == null)
            {
                LogWarn("Unload called when behaviour was null.");
                return;
            }
            behaviour.Active = false;
        }

        private sealed class Behaviour : MonoBehaviour
        {
            public void OnApplicationFocus(bool hasFocus)
            {
                if (!Active)
                    return;
                if (hasFocus)
                {
                    StartCoroutine(GameManager.instance.PauseGameToggle());
                    AudioListener.pause = false;
                }
                else
                {
                    StartCoroutine(GameManager.instance.PauseGameToggle());
                    AudioListener.pause = true;
                }
            }
            public bool Active { get; set; }
        }
    }
}
