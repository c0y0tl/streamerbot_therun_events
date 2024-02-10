# TheRun Events
Stremer.bot extensions for listening to events TheRun
## Setup
* Go to the “Servers/Clients” > “Websocket Clients” tab in Stremer.bot.
* Create a new web socket client.
  - `Name` - any name
  - `Endpoint` - `wss://fh76djw1t9.execute-api.eu-west-1.amazonaws.com/prod?username=YOUR_NICKNAME` Instead of `YOUR_NICKNAME` is your nickname on TheRun, the nickname is case sensitive.
  - [x] Auto Connect on Startup 
  - [x] Reconnect on Disconnect
  - Retry Interval - any value
  - [x] TLS 1.0 
  - [ ] TLS 1.1
  - [x] TLS 1.2
* Click the Import button in Stremer.bot
* Drag the downloaded [`TheRun`](https://github.com/c0y0tl/therun.events/releases) file into the Import String area, or copy its contents and paste manually
* The list of available variables is given in the comments to the action

