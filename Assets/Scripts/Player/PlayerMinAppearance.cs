using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMinAppearance : MonoBehaviour 
{
    [SerializeField]
    private SpriteRenderer playerSpriteRenderer;

    [SerializeField]
    private Sprite playerOriginalSprite;

    [SerializeField]
    private Sprite playerBlockSprite;

    [SerializeField]
    private Sprite playerFloorSprite;

    [SerializeField]
    private Sprite playerBurstSprite;

    [SerializeField]
    private Sprite playerProjectileSprite;

    public void ChangeMinAppearance(MinsType mintype)
    {
        switch(mintype)
        {
            case MinsType.Block:
                playerSpriteRenderer.sprite = playerBlockSprite;
                break;

            case MinsType.Floor:
                playerSpriteRenderer.sprite = playerFloorSprite;
                break;

            case MinsType.Burst:
                playerSpriteRenderer.sprite = playerBurstSprite;
                break;

            case MinsType.Projectile:
                playerSpriteRenderer.sprite = playerProjectileSprite;
                break;

            default:
                playerSpriteRenderer.sprite = playerOriginalSprite;
                break;
        }
    }
}
