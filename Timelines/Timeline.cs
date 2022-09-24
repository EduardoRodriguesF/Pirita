using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pirita.Timelines;

public class Timeline {
    public float CurrentTime;
    public List<TimelinePassage> _passages;

    public Timeline() {
        _passages = new List<TimelinePassage>();
    }

    public Timeline(List<TimelinePassage> passages) {
        _passages = passages;
    }

    public void Update(GameTime gameTime) {
        var previousTime = CurrentTime;
        CurrentTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

        foreach (var passage in _passages) {
            if (passage.Timestamp > previousTime && passage.Timestamp <= CurrentTime) {
                passage.Action();
            }
        }
    }

    public void AddPassage(TimelinePassage passage) {
        _passages.Add(passage);
        OrganizePassageList();
    }

    private void OrganizePassageList() {
        _passages.OrderBy(p => p.Timestamp);
    }
}
