﻿using System;

using UnityEngine;

namespace Systems.SpellSystem.SpellEffect
{
    [Serializable]
    public abstract class EffectApplyStrategy : ICloneable
    {
        [SerializeField] private EffectApplyStrategySO _applyStrategySO;
        public EffectApplyStrategySO ApplyStrategySO => _applyStrategySO;

        protected EffectApplyStrategy(EffectApplyStrategySO applyStrategySo)
        {
            _applyStrategySO = applyStrategySo;
        }

        // public abstract void Start();
        // public abstract void End();

        public abstract void OnApply();
        public abstract void OnCancel();
        
        
        public abstract bool CanApply();
        public abstract bool CanCancel();

        
        public abstract object Clone();
    }
}