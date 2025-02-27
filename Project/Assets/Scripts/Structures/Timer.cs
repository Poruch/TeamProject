namespace MyTypes
{
    public class Timer
    {
        public Timer(float time)
        {
            Time = time;
            DeltaTime = 0;
            isTime = false;
            isStoped = false;
        }

        public float Time;
        public float DeltaTime;
        private bool isTime;
        private bool isStoped;

        public bool IsTime
        {
            get
            {
                if (isTime)
                {
                    isTime = false;
                    DeltaTime = 0;
                    return true;
                }
                else
                    return false;
            }
        }

        public bool IsStoped { get => isStoped; set => isStoped = value; }


        /// <summary>
        /// usable in Update func
        /// </summary>
        public void Tick()
        {
            if (DeltaTime >= Time)
            {
                isTime = true;
            }
            if(!IsStoped)
                DeltaTime += UnityEngine.Time.deltaTime;
        }
        public void Reset()
        {
            DeltaTime = 0;
            isTime = false;
        }
        public float Сompleted_at()
        {
            return DeltaTime / Time;
        }
    }
}
