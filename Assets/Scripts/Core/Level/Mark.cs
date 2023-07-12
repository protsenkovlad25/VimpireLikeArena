using UnityEngine;
using DG.Tweening;

namespace VampireLike.Core.Levels
{
    public class Mark : MonoBehaviour
    {
        private void Awake()
        {
            SpriteRenderer sprite = GetComponent<SpriteRenderer>();

            Sequence s = DOTween.Sequence();

            s.Append(sprite.DOFade(.2f, .25f));
            s.Append(sprite.DOFade(1f, .25f));
            s.Append(sprite.DOFade(.2f, .25f));
            s.Append(sprite.DOFade(1f, .25f));
            s.Append(sprite.DOFade(1f, .25f));
            s.Append(sprite.DOFade(1f, .25f));

            s.AppendCallback(new TweenCallback(Destroy));
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
