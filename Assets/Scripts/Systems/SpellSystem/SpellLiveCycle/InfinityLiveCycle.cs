﻿using System;

namespace Systems.SpellSystem.SpellEffect.SpellLiveCycle
{
    [Serializable]
    public class InfinityLiveCycle : BaseLiveCycle
    {
        public InfinityLiveCycle() { }

        public override void LogicUpdate() { }

        public override void Start() { }

        public override void Reset() { }

        public override bool IsEnd() => false;
    }
}