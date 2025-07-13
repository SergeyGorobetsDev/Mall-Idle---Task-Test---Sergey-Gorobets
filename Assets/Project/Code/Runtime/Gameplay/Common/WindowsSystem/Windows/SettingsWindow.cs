using Assets.Project.Code.Runtime.Architecture.Engine;
using Assets.Project.Code.Runtime.Gameplay.Common.AudioSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Project.Code.Runtime.Gameplay.Common.WindowsSystem.Windows
{
    public sealed class SettingsWindow : Window
    {
        [SerializeField]
        private Slider musicVolumeSlider;
        [SerializeField]
        private Slider uiVolumeSlider;
        [SerializeField]
        private Slider sfxVolumeSlider;

        public override void Hide()
        {
            base.Hide();

            EngineSystem.Instance.AudioPlayer.Play("close", MixerTarget.UI);
        }

        protected override void BindDocumentData()
        {
            base.BindDocumentData();
            musicVolumeSlider.value = EngineSystem.Instance.AudioPlayer.GetVolumeMusic();
            uiVolumeSlider.value = EngineSystem.Instance.AudioPlayer.GetVolumeUI();
            sfxVolumeSlider.value = EngineSystem.Instance.AudioPlayer.GetVolumeSFX();
        }

        protected override void RegisterCallbacks()
        {
            base.RegisterCallbacks();
            musicVolumeSlider.onValueChanged.AddListener(value =>
                EngineSystem.Instance.AudioPlayer.SetVolumeMusic(value));
            uiVolumeSlider.onValueChanged.AddListener(value =>
                EngineSystem.Instance.AudioPlayer.SetVolumeUI(value));
            sfxVolumeSlider.onValueChanged.AddListener(value =>
                EngineSystem.Instance.AudioPlayer.SetVolumeSFX(value));
        }

        protected override void UnRegisterCallbacks()
        {
            base.UnRegisterCallbacks();

            musicVolumeSlider.onValueChanged.RemoveAllListeners();
            uiVolumeSlider.onValueChanged.RemoveAllListeners();
            sfxVolumeSlider.onValueChanged.RemoveAllListeners();
        }
    }
}