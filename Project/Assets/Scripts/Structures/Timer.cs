namespace MyTypes
{
    public struct Timer
    {
        public Timer(float time)
        {
            Time = time;
            DeltaTime = 0;
            istime = false;
        }

        public float Time;
        public float DeltaTime;
        private bool istime;
        public bool isTime
        {
            get
            {
                if (istime)
                {
                    istime = false;
                    DeltaTime = 0;
                    return true;
                }
                else
                    return false;
            }
        }
        /// <summary>
        /// usable in Update func
        /// </summary>
        public void Tick()
        {
            if (DeltaTime >= Time)
            {
                istime = true;
            }
            DeltaTime += UnityEngine.Time.deltaTime;
        }
        public void Reset()
        {
            DeltaTime = 0;
            istime = false;
        }
        public float Сompleted_at()
        {
            return DeltaTime / Time;
        }
    }
}
