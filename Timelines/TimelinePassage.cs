using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.Timelines {
    public class TimelinePassage {
        public float Timestamp;
        public Action Action;

        public TimelinePassage(float time, Action action) {
            Timestamp = time;
            Action = action;
        }
    }
}
