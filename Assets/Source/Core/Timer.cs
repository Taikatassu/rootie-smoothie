using System;

namespace RootieSmoothie.Core
{
    public class Timer
    {
        public Action OnDone = null;

        public float Duration { get; private set; }
        public float TimeSinceStarted => _lastTimeNow - _startTime;
        public float TimeToEnd => Duration - TimeSinceStarted;
        public bool IsDone => _lastTimeNow - _startTime >= Duration;

        private float _startTime;
        private float _lastTimeNow;

        public void Start(float duration, float timeNow)
        {
            Duration = duration;
            _startTime = timeNow;
        }

        public void Update(float timeNow)
        {
            _lastTimeNow = timeNow;

            BroadcastIfDone();
        }

        private void BroadcastIfDone()
        {
            if (IsDone)
                OnDone?.Invoke();
        }
    }
}
