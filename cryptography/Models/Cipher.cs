namespace cryptography.Models
{
    public class Cipher
    {
        public string Name { get; set; }
        public CipherType Type { get; set; }

        public Cipher(string name, CipherType type)
        {
            Name = name;
            Type = type;
        }
        
        public virtual void run(){}
    }
}