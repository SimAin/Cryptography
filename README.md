# Cryptography

A C# console application for exploring cryptographic algorithms, built as a learning exercise to better understand how they work. It currently implements a range of historic ciphers and supporting number-theory calculations, with the intention of growing towards more modern algorithms over time.

## ⚠️ Educational use only

These are **historic, broken-by-design ciphers** implemented for learning purposes. They provide **no real security** and must **never** be used to protect sensitive data. Use a vetted library (e.g. the modern primitives in `System.Security.Cryptography`) for anything real.

## Prerequisites

- [.NET SDK 10.0](https://dotnet.microsoft.com/download) or later

Check your installed SDK with:

```bash
dotnet --list-sdks
```

## Build and test

```bash
# Restore dependencies and build the solution
dotnet build

# Run the unit test suite (NUnit)
dotnet test
```

## Running the application

The app is an interactive console menu. Because it reads and writes files using paths relative to the working directory, run it from inside the `cryptography` project folder:

```bash
cd cryptography
dotnet run
```

### Encoding / decoding workflow

The app uses three text files in `cryptography/files/`:

| File | Purpose |
|------|---------|
| `input.txt` | The plaintext you want to encode |
| `output.txt` | Where encoded (ciphertext) output is written |
| `decoded.txt` | Where decoded (plaintext) output is written |

1. Put the message you want to encode in `files/input.txt`.
2. Run the app and select a cipher by number from the main menu.
3. Choose **1. Encode** — the ciphertext is written to `files/output.txt`.
4. To reverse it, select the cipher again and choose **2. Decode** — it reads `files/output.txt` and writes the recovered plaintext to `files/decoded.txt`.

## Implemented ciphers

Selected from the main menu:

| # | Cipher | Type |
|---|--------|------|
| 1 | Caesar | Substitution |
| 2 | Simple Substitution | Substitution |
| 3 | Playfair | Substitution |
| 4 | Vigenère | Substitution |
| 5 | Simple Transposition | Transposition |
| 6 | Rail Fence | Transposition |

## Calculations

Menu option **88** opens a calculations sub-menu covering the number theory behind these algorithms:

| # | Calculation |
|---|-------------|
| 1 | Euler's totient function — ϕ(n) |
| 2 | Greatest common divisor (GCD) |
| 3 | Extended Euclidean algorithm |
| 4 | Modulo calculator |

## Project structure

```
cryptography/                 # Main console application
  Program.cs                  # Entry point and main menu loop
  HistoricCiphers/            # Cipher implementations (derive from Cipher)
  Services/                   # Key generation, string ops, user interaction
  Models/                     # Cipher, CipherList, CipherType, EeaResult
  Calculations/               # GCD, Extended Euclid, totient, modulo
  files/                      # input.txt / output.txt / decoded.txt
cryptography.tests/           # NUnit test suite mirroring the source layout
```

## Roadmap

- Implement a custom composite (product) cipher.
- Implement a Feistel cipher.
- Continue towards modern algorithms.
- Expand unit test coverage and inline documentation.

## License

Released under the [MIT License](LICENSE).
