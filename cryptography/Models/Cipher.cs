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
        
        public virtual void run(string inputFile = "files/input.txt", 
                                string encodedFile = "files/output.txt",
                                string decodedFile = "files/decoded.txt") { }
    }
}