namespace WhackAStoodent.Runtime.Helper
{
    public class APersistantSingletonManagerScript<T> : ASingletonManagerScript<T> where T : ASingletonManagerScript<T>
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            if(Instance == this)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

    }
}