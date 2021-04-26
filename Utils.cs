namespace Pirita {
    public static class Utils {
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
