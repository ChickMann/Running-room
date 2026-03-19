<div align="center">

# 🏃‍♂️ RUNNING ROOM

### *Run! Your boss and deadlines are chasing you!*

[![Unity](https://img.shields.io/badge/Unity-2022.3+-black?style=for-the-badge&logo=unity)](https://unity.com/)
[![Platform](https://img.shields.io/badge/Platform-Android-green?style=for-the-badge&logo=android)](https://developer.android.com/)
[![Firebase](https://img.shields.io/badge/Firebase-Realtime_DB-orange?style=for-the-badge&logo=firebase)](https://firebase.google.com/)
[![AdMob](https://img.shields.io/badge/Google-AdMob-blue?style=for-the-badge&logo=google)](https://admob.google.com/)
[![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)](LICENSE)

</div>

---

## 🎮 About

**Running Room** is a mobile **Endless Runner** game with a unique and hilarious twist. You play as an office worker being chased by your **BOSS** and the nightmare called **DEADLINE** — there is no finish line, only survival and cold hard cash!

> 💼 *Run through magical doors, collect money, dodge your pursuer, and survive as long as you can!*

---

## 🕹️ Gameplay

### Core Mechanic

The player controls a character **moving left or right** through an endless office hallway. **Colorful portal doors** appear on both sides of the screen — step through a door on one side and you'll teleport out of the matching-colored door on the other side!

```
[ 🚪 Red Door  ]  ←——— Player ———→  [ 🚪 Red Door  ]
[ 🚪 Blue Door ]  ←——— Player ———→  [ 🚪 Blue Door ]
```

### Game Flow

```
Launch → Tutorial (first time) → Press START → Intro Animation
    → Character starts running → Dodge / Collect items → Caught!
        → Results screen → Enter name → Save to Scoreboard
```

---

## ⚡ Features

### 🚪 Portal Door System
- Two rows of left/right doors with matching colors
- Each door **automatically opens** as the character approaches (Raycast-based)
- Door colors **shuffle randomly** on a periodic cycle (driven by the Timer Slider)
- Collect the **Star ⭐** to trigger continuous rapid color-shuffling — total chaos!

### 👿 Smart Enemy Chase AI
- The **Boss/Enemy** relentlessly pursues the player via Raycast pathfinding
- Dynamic navigation: detects the player's position and adjusts direction in real time
- The enemy can pick up **SpeedUp** items to temporarily **accelerate and close the gap!**
- On catching the player: death animation triggers, smoke particle effect bursts out

### 💰 Item System

| Item | Icon | Effect | Value |
|------|------|--------|-------|
| **Money** | 💵 | Adds to score | +10 pts |
| **Speed Up** | ⚡ | Increases player speed | +5 speed |
| **Star** | ⭐ | Triggers continuous door shuffling | Special |
| **Trap Banana** | 🍌 | Dangerous trap | Avoid it! |

> Items spawn randomly relative to door positions, with a maximum of **15 items** on screen at once.

### 🏆 Global Leaderboard (Firebase)
- Uses **Firebase Realtime Database** to store results from all players worldwide
- Ranked by **highest score**, displaying **name + score + survival time**
- Enter your name and save your score immediately after each run
- Data is identified by the device's unique **Device ID**

### ⏱️ Timer & Slider System
- **Count-up timer** displays survival time in MM:SS format
- **Countdown Slider** — when it hits zero, the door colors reset!
- Survival time is saved to the scoreboard at the end of each run

### 🔊 Audio & Settings
- Background music plays once the game starts
- Dedicated sound effects for: enemy collision, money pickup, speed boost, button clicks
- Independently toggle **Music** and **Sound Effects** on/off
- Settings persist across sessions via **PlayerPrefs**

### 📱 Google AdMob Integration
- **Banner Ad**: displayed when the game is paused or over
- **Interstitial Ad**: full-screen ad shown between runs
- **Rewarded Ad**: watch an ad to earn in-game rewards
- **App Open Ad**: displayed when the app is resumed from the background

---

## 🗂️ Project Architecture

```
Assets/
├── AssetsGame/
│   ├── Scripts/
│   │   ├── GameManager.cs          # Singleton orchestrating the entire game
│   │   ├── PlayerContronller.cs    # Player movement & collision handling
│   │   ├── EnemyMovement.cs        # Enemy AI (Raycast chase)
│   │   ├── DoorScript.cs           # Portal door system management
│   │   ├── GateContronller.cs      # Per-door open/close logic
│   │   ├── GatePos.cs              # Multi-resolution responsive positioning
│   │   ├── SpawnManager.cs         # Random item spawning
│   │   ├── Items.cs                # Item type definitions (enum)
│   │   ├── Timer.cs                # Survival timer & countdown slider
│   │   ├── DatabaseManager.cs      # Firebase CRUD & scoreboard loading
│   │   ├── ScoreBoardManager.cs    # Scoreboard UI management
│   │   ├── SettingScript.cs        # Audio & settings persistence
│   │   ├── IntroScript.cs          # Tutorial & intro cutscene
│   │   ├── AdmodAdsScript.cs       # Google AdMob integration
│   │   └── Singleton.cs            # Generic Singleton base class
│   ├── Prefabs/                    # Player, Enemy, NPC, Items, Gate...
│   ├── Scenes/                     # Game scenes
│   ├── Sound/                      # Music & sound effects
│   └── Materials/                  # Materials & door colors
```

---

## 🛠️ Tech Stack

| Technology | Purpose |
|------------|---------|
| **Unity 2022.3+** | Game Engine |
| **C#** | Programming Language |
| **Firebase Realtime Database** | Score storage & sync |
| **Google AdMob** | Monetization & ads |
| **TextMeshPro** | High-quality text rendering |
| **Unity Timeline** | Intro & cutscene animation |
| **Rigidbody Physics** | Character movement |
| **Raycast** | Collision detection & AI pathfinding |
| **PlayerPrefs** | Local settings persistence |
| **Object Pooling** | Optimized item spawning performance |

---

## 🚀 Getting Started

### Requirements
- Unity **2022.3** or later
- Android Build Support module installed
- A **Firebase** account (free tier works)
- A **Google AdMob** account (optional)

### Steps

1. **Clone the repository**
   ```bash
   git clone https://github.com/ChickMann/Running-room.git
   cd Running-room
   ```

2. **Open in Unity Hub**
   - Click `Open > Add project from disk`
   - Point to the cloned folder

3. **Configure Firebase**
   - Create a project on the [Firebase Console](https://console.firebase.google.com/)
   - Download `google-services.json` and replace the one in `Assets/`
   - Enable **Realtime Database** and set appropriate rules:
   ```json
   {
     "rules": {
       ".read": true,
       ".write": true
     }
   }
   ```

4. **Build & Run**
   - Go to `File > Build Settings > Android`
   - Connect an Android device or use an emulator
   - Click **Build and Run**

---

## 📸 Screenshots

> *Gameplay screenshots will be added here*

---

## 🎯 How to Play

1. **Launch** — The tutorial appears on the very first run
2. **Press START** — Camera zooms in, character begins moving
3. **Tap the screen** — Character switches direction (left ↔ right)
4. **Enter a door** — Teleport to the matching-color door on the opposite side
5. **Collect items** — Grab money 💵, speed boosts ⚡, and stars ⭐
6. **Avoid the enemy** — Don't let the boss catch you!
7. **Game Over** — Enter your name and post your score to the leaderboard 🏆

---

## 👨‍💻 Author

**ChickMann**
- GitHub: [@ChickMann](https://github.com/ChickMann)

---

## 📝 License

This project is distributed under the **MIT License**. See [LICENSE](LICENSE) for more details.

---

<div align="center">

*Made with ❤️ and way too much caffeine ☕*

**⭐ If you enjoy the project, please consider leaving a star! ⭐**

</div>
