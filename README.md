[README.md](https://github.com/user-attachments/files/30038245/README.md)
# The Momoi Tictactoe Date

A pink, Airi Momoi–themed tic-tac-toe game with 4 difficulty levels
(Easy, Medium, Hard, and a nearly-unbeatable "Impossible"), a local 2-player
Friend mode, background music, and a win sound effect.

Made by: **P I S H**

## Play it — two ways

### In your browser (easiest, works on anything)
Just download [`index.html`](./index.html) and open it in any browser —
Chrome, Firefox, Edge, Safari, even on your phone. No install required.

### As a Windows app
The [`windows-app`](./windows-app) folder has the full C# / WinForms +
WebView2 project — same exact game, wrapped as a native `.exe`.

To build it yourself:
1. Install the [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Open a terminal in the `windows-app` folder
3. Run:
   ```
   dotnet publish -c Release
   ```
4. Your standalone `.exe` will be at:
   ```
   windows-app/bin/Release/net8.0-windows/win-x64/publish/TheMomoiTicTacToe.exe
   ```

See [`windows-app/README.md`](./windows-app/README.md) for more detail.

## Features
- 4 difficulties, from "chill" all the way to "plays perfectly (with
  a 0.0000034% chance of blundering."
- Local Play-a-Friend mode
- Looping background music with a volume slider
- A little victory sound effect just for Airi
- Pink/gold theme throughout, built around Airi Momoi

## Tech
Single-file HTML/CSS/JS for the web version (image and audio embedded as
base64, so it's one file, no dependencies). The Windows app just hosts that
same HTML inside a WebView2 control via WinForms.
