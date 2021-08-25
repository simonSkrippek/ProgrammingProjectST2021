
namespace WhackAStoodent.Helper
{
    public abstract class ASingletonManagerScript<T> : AManagerScript<T> where T : AManagerScript<T>
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
