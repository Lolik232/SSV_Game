using System;
using System.Data;
using UnityEngine;

namespace Systems.SpellSystem.SpellEffect.SpellLiveCycle
{
    [Serializable]
    public class TimeLiveCycle : BaseLiveCycle
    {
        /*  TODO: отдельный объект таймера
         */
        private float _time          = 0f;
        private float _timeFromStart = 0f;
        private bool  _isStarted     = false;

        public TimeLiveCycle()
        {
        }

        public TimeLiveCycle(float time)
        {
            SetTime(time);
        }

        public void SetTime(float time)
        {
            if (time < 0)
            {
                throw new ArgumentException("time must be positive");
            }

            _time = time;
        }

        public override void LogicUpdate()
        {
            if (!_isStarted || IsEnd()) return;

            _timeFromStart += Time.deltaTime;
        }

        public override void Start()
        {
            _isStarted = true;
        }

        public override void Reset()
        {
            _isStarted     = false;
            _timeFromStart = 0;
        }

        public override bool IsEnd()
        {
            return _timeFromStart >= _time;
        }
    }
}