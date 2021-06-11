namespace Pirita {
    public static class Utils {
        /// <summary>
        /// Moves "from" towards "to" by "amount" and returns the result
        /// </summary>
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

        /// <summary>
        /// Returns the value that equates to the position between two other values for a given percentage
        /// </summary>
        public static float Lerp(float from, float to, float amount) {
            return from + (to - from) * amount;
        }

        /// <summary>
        /// Returns the value wrapped, values over or under will be wrapped around
        /// </summary>
        public static float Wrap(float value, float min, float max) {
            if (value % 1 == 0) {
                while (value > max || value < min) {
                    if (value > max)
                        value += min - max - 1;
                    else if (value < min)
                        value += max - min + 1;
                }
                return value;
            } else {
                var vOld = value + 1;
                while (value != vOld) {
                    vOld = value;
                    if (value < min)
                        value = max - (min - value);
                    else if (value > max)
                        value = min + (value - max);
                }
                return value;
            }
        }

        /// <summary>
        /// Maintains a value between the specified range
        /// </summary>
        public static float Clamp(float value, float min, float max) {
            if (value < min) return min;
            else if (value > max) return max;
            else return value;
        }
    }
}
