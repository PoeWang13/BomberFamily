// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("uSeZFrhdgstkrSRqubXsiEkiuu1gBoUCxF2SUtbFJFXa7R16Ng1bkSXENBkqtyNwd/71U6eHku+WuLNOFr/NI/pw8sXagxjuxWeJuMQV9CFNLFv9a8XbJTkjzSjH4bE71p0l4y0vPd8G6+NQIdXQGFmQJvmFGUy4oxGSsaOelZq5FdsVZJ6SkpKWk5DhkHoyUlwf2FQ+3wWcHUsShmeKb3IEpbu+kJz+6OatW+UQOY/OUShbMxRRlAYaMc/kUBx9djA8qgB/ZvHRfFgN2oVrhSkmNvkmeWHEzsilSm0/DvNyu4Vbt/gVS8AGCQiHVTeJEZKck6MRkpmREZKSkzKUVgD0ZzjQgHjMoUHZFozQmFjDbTya/GkhNMnh6pyv81iQhpGQkpOS");
        private static int[] order = new int[] { 8,7,3,11,4,6,8,12,11,10,11,11,12,13,14 };
        private static int key = 147;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
