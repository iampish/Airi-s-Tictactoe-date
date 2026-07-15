# The Momoi Tictactoe — Windows desktop build

This is the exact same game (same HTML/CSS/JS, same pink Airi theme, same
difficulty modes) wrapped in a native Windows app using WinForms + WebView2,
so you get a real double-clickable `.exe`.

## What you need (one-time, on a Windows PC)

1. **.NET 8 SDK** — https://dotnet.microsoft.com/download/dotnet/8.0
   (pick the SDK, not just the runtime)
2. That's it. The WebView2 *runtime* is already built into Windows 10/11 on
   basically every modern PC, so players won't need to install anything.

## Build it

Open a terminal (PowerShell or cmd) in this folder — the one with
`MomoiTicTacToe.csproj` — and run:

```
dotnet publish -c Release
```

That produces a single self-contained `.exe` at:

```
bin\Release\net8.0-windows\win-x64\publish\TheMomoiTicTacToe.exe
```

Copy that one file anywhere — it's fully standalone, no installer, no
loose files, no dependencies to drag along. Double-click it and the game
opens in its own window titled "the momoi tictactoe".

## Just want to test it without building an exe yet?

```
dotnet run
```

runs it straight from source in a dev window.

## Project layout

```
MomoiTicTacToe/
├── MomoiTicTacToe.csproj   ← project + publish settings
├── Program.cs              ← app entry point
├── MainForm.cs             ← the window; loads the game UI into WebView2
├── Assets/
│   └── game.html           ← the entire game (HTML/CSS/JS + Airi image, embedded as base64)
└── README.md
```

`game.html` is embedded directly into the compiled `.exe` as a resource
(not shipped as a separate file) At startup, the app quietly extracts it
to a temp file and point WebView2 at it. Everything you interact with —
difficulty pills, the friend mode toggle, the board, Airi's speech
Bubbles is the identical code from the browser version.

## Tweaking the game

Since the UI is just HTML/CSS/JS, you don't need to touch any C# to change
gameplay, colors, or text — just edit `Assets/game.html` and rebuild.
