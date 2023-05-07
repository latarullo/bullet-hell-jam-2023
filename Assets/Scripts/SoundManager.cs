using UnityEngine;

public class SoundManager : MonoBehaviour {

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    public static SoundManager Instance { get; private set; }
    private void Awake() {
        Instance = this;
        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);
    }

    [SerializeField] private AudioClip[] demonDie;
    [SerializeField] private AudioClip cannonShoot;
    [SerializeField] private AudioClip rifleShoot;

    private float volume;

    private void Start() {

    }

    public void PlayDemonDie(Transform transform) {
        this.PlaySound(demonDie, transform.position);
    }
    public void PlayCannonShoot(Transform transform) {
        this.PlaySound(cannonShoot, transform.position);
    }
    public void PlayRifleShoot(Transform transform) {
        this.PlaySound(rifleShoot, transform.position);
    }


    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f) {
        AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    public void ChangeVolume() {
        volume += .1f;
        if (volume > 1.1) {
            volume = 0;
        } else if (volume > 1) {
            volume = 1;
        }

        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume() {
        return volume;
    }
}
