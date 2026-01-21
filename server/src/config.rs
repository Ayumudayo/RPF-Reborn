use serde::Deserialize;
use std::net::SocketAddr;

#[derive(Deserialize)]
pub struct Config {
    pub web: Web,
    pub mongo: Mongo,
    /// FFLogs API 설정 (선택적)
    #[serde(default)]
    pub fflogs: Option<FFLogs>,
}

/// FFLogs API 설정
#[derive(Deserialize, Clone)]
pub struct FFLogs {
    /// OAuth2 Client ID
    pub client_id: String,
    /// OAuth2 Client Secret
    pub client_secret: String,
}

#[derive(Deserialize)]
pub struct Web {
    pub host: SocketAddr,
}

#[derive(Deserialize)]
pub struct Mongo {
    pub url: String,
}
