/// <summary>
/// Scribble.rs Pad managers namespace
/// </summary>
namespace ScribblersPad.Managers
{
    public abstract class ASingletonManagerScript<T> : AManagerScript<T> where T : AManagerScript<T>
    {
        protected virtual void Awake()
        {
            if ((Instance != null) && (Instance != this))
            {
                Destroy(gameObject);
            }
        }
    }
}
