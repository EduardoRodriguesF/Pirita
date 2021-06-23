using Microsoft.Xna.Framework;

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

        public static Vector2 Approach(Vector2 from, Vector2 to, float amount) {
            return Approach(from, to, new Vector2(amount));
        }

        public static Vector2 Approach(Vector2 from, Vector2 to, Vector2 amount) {
            return new Vector2(
                Approach(from.X, to.X, amount.X),
                Approach(from.Y, to.Y, amount.Y)
                );
        }

        /// <summary>
        /// Returns the value that equates to the position between two other values for a given percentage
        /// </summary>
        public static float Lerp(float from, float to, float amount) {
            return from + (to - from) * amount;
        }

        public static Vector2 Lerp(Vector2 from, Vector2 to, float amount) {
            return Lerp(from, to, new Vector2(amount));
        }

        public static Vector2 Lerp(Vector2 from, Vector2 to, Vector2 amount) {
            return new Vector2(
                Lerp(from.X, to.X, amount.X),
                Lerp(from.Y, to.Y, amount.Y)
                );
        }

        /// <summary>
        /// Returns a percentage of a value between min and max parameters
        /// </summary>
        public static float InvLerp(float min, float max, float value) {
            return (value - min) / (max - min);
        }

        public static Vector2 InvLerp(Vector2 from, Vector2 to, float amount) {
            return InvLerp(from, to, new Vector2(amount));
        }

        public static Vector2 InvLerp(Vector2 from, Vector2 to, Vector2 amount) {
            return new Vector2(
                InvLerp(from.X, to.X, amount.X),
                InvLerp(from.Y, to.Y, amount.Y)
                );
        }

        /// <summary>
        /// Returns the remapped value from iMin-iMax range to oMin-oMax range
        /// </summary>
        /// <param name="iMin">Input min</param>
        /// <param name="iMax">Input max</param>
        /// <param name="oMin">Output min</param>
        /// <param name="oMax">Output max</param>
        /// <param name="value">Value between iMin and iMax</param>
        public static float Remap(float iMin, float iMax, float oMin, float oMax, float value) {
            return Lerp(oMin, oMax, InvLerp(iMin, iMax, value));
        }

        public static Vector2 Remap(Vector2 iMin, Vector2 iMax, Vector2 oMin, Vector2 oMax, Vector2 value) {
            return new Vector2(
                Remap(iMin.X, iMax.X, oMin.X, oMax.X, value.X),
                Remap(iMin.Y, iMax.Y, oMin.Y, oMax.Y, value.Y)
                );
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
