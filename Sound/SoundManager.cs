using Microsoft.Xna.Framework.Audio;
using Pirita.Scenes;
using System;
using System.Collections.Generic;

namespace Pirita.Sound;

public class SoundManager {
    private Dictionary<Type, SoundBankItem> _soundBank = new Dictionary<Type, SoundBankItem>();

    public void OnNotify(Event e) {
        if (_soundBank.ContainsKey(e.GetType())) {
            var sound = _soundBank[e.GetType()];
            sound.Sound.Play(sound.Attributes.Volume, sound.Attributes.Pitch, sound.Attributes.Pan);
        }
    }

    public void RegisterSound(Event e, SoundEffect sound) {
        RegisterSound(e, sound, 0.5f, 0f, 0f);
    }

    public void RegisterSound(Event e, SoundEffect sound, float volume, float pitch, float pan) {
        _soundBank.Add(e.GetType(), new SoundBankItem(sound, new SoundAttributes(volume, pitch, pan)));
    }
}
