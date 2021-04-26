using System;
using System.Collections.Generic;
using System.Text;

namespace Pirita.Engine.Utils {
    public class Numbers {
        public static float Approach(float from, float to, float amount) {
			if (from < to) {
				from += amount;
				if (from > to) return to;
			} else {
				from -= amount;
				if (from < to) return to;
			}
			return from;
		}
    }
}
