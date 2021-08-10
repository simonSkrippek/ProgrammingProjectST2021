
namespace WhackAStoodent.Helper
{
    public abstract class ASingletonManagerScript<T> : AManagerScript<T> where T : AManagerScript<T>
    {
        protected virtual void Awake()
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
