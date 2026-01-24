# Remote Party Finder

A tool to synchronize FFXIV Party Finder listings to a web interface, integrated with FFLogs for automatic parse data display.

## Key Features

- **Real-time Synchronization**: View in-game Party Finder listings on the web with minimal latency.
- **FFLogs Integration**: automatically fetches and displays Best Performance Average (Parse) for party members and leaders.
  - Supports Batch GraphQL queries for efficient data retrieval (solving N+1 query problems).
  - Caches parse data in MongoDB (24-hour expiration) to respect API rate limits.
- **Modern Web UI**: Clean, responsive interface with parse color coding (Grey to Gold/Pink).
- **Optimized Performance**: Rust-backed server using Warp and Tokio for high concurrency.

## Architecture

The project consists of two main components:

1.  **Client (Plugin)**: A C# Dalamud Plugin that collects Party Finder data from the game client and sends it to the server.
2.  **Server**: A Rust backend that receives data, communicates with FFLogs API, stores data in MongoDB, and serves the web interface.

### Project Structure

```
/
├── csharp/                 # Dalamud Plugin (C#)
│   ├── RemotePartyFinder/  # Main Plugin Logic
│   └── ...
├── server/                 # Backend Server (Rust)
│   ├── src/
│   │   ├── domain/         # Business Logic (Listing, Player, Stats)
│   │   ├── infra/          # Infrastructure (MongoDB, FFLogs API)
│   │   └── web/            # Web Handlers & Routes
│   ├── templates/          # HTML Templates (Askama)
│   └── assets/             # CSS/JS Assets
└── ...
```

## Setup & Usage

### Prerequisites

- **Rust** (Latest Stable)
- **MongoDB** (Running locally or accessible remotely)
- **FFXIV with Dalamud** (for the plugin)
- **FFLogs V2 API Client** (ClientId & ClientSecret)

### Server Setup

1.  Navigate to the `server/` directory.
2.  Copy `config.example.toml` to `config.toml`.
3.  Edit `config.toml` and fill in your details:
    ```toml
    [fflogs]
    client_id = "YOUR_CLIENT_ID"
    client_secret = "YOUR_CLIENT_SECRET"
    
    [mongo]
    connection_string = "mongodb://localhost:27017"
    ```
4.  Run the server:
    ```bash
    cargo run --release
    ```
    The server typically listens on `http://127.0.0.1:8000`.

### Plugin Setup

1.  Open `csharp/RemotePartyFinder.sln` in Visual Studio.
2.  Build the solution (Release mode recommended).
3.  Load the built plugin in FFXIV using Dalamud's dev plugins feature.
4.  The plugin will automatically start sending Party Finder data to the configured server endpoint.

## License

(No license specified yet)
