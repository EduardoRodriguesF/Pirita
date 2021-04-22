﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.Engine.Sound {
    public class SoundAttributes {
        public float Volume { get; set; }
        public float Pitch { get; set; }
        public float Pan { get; set; }

        public SoundAttributes(float volume, float pitch, float pan) {
            Volume = volume;
            Pitch = pitch;
            Pan = pan;
        }
    }
}
