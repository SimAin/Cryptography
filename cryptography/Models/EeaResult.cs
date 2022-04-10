namespace cryptography.Models
{
    public class EeaResult
    {
        public int d;
        public int x;
        public int y;

        public EeaResult(int d1, int x1, int y1)
        {
            d = d1;
            x = x1;
            y = y1;
        }
    }
}