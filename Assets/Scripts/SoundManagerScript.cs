using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class SoundManagerScript : NetworkBehaviour
{

    public static AudioClip playerShootSound, ennemyShootSound, teleportSound, playerDamageSound,
        ssethHereSound, ssethCircleSound, ssethLineSound;

    private static AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {

        playerShootSound = Resources.Load<AudioClip>("playerShoot");
        ennemyShootSound = Resources.Load<AudioClip>("ennemyShoot");
        teleportSound = Resources.Load<AudioClip>("Teleport");
        playerDamageSound = Resources.Load<AudioClip>("PlayerDamage");
        ssethHereSound = Resources.Load<AudioClip>("SsethHere");
        ssethLineSound = Resources.Load<AudioClip>("SsethLine");
        ssethCircleSound = Resources.Load<AudioClip>("SsethCircle");

        _audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void playSound(string clip)
    {
        switch (clip)
        {
            case "playerShoot":
                _audioSource.PlayOneShot(playerShootSound);
                break;
            case "ennemyShoot":
                _audioSource.PlayOneShot(ennemyShootSound);
                break;
            case "teleport":
                _audioSource.PlayOneShot(teleportSound);
                break;
            case "PlayerDamage":
                _audioSource.PlayOneShot(playerDamageSound);
                break;
            case "ssethHere":
                _audioSource.PlayOneShot(ssethHereSound);
                break;
            case "ssethCircle":
                _audioSource.PlayOneShot(ssethCircleSound);
                break;
            case "ssethLine":
                _audioSource.PlayOneShot(ssethHereSound);
                break;

        }
    }
}
