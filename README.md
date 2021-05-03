# DiscordMikrotikBot
A simple bot that will update channel's specified with Mikrotik's changelog and notify when new versions are published.

Written and compiled in .NET Core 3.1


# Why?
Well I am a member in the Mikrotik Networking Discord and we're always talking about the next RouterOS version since they ALWAYS have issues to fix. This bot is a way for the dev group and stable group (and even testing/longterm) to be notified when a new version is released the moment it is released so everyone can stay up to date with their systems.
-> https://discord.gg/572PywgGTU

# Setup
It's really simple if you're on windows.

Download the release binary found in the [Releases](https://github.com/Crash0v3r1de/DiscordMikrotikBot/releases) section of this project.

**Windows**
------------------------
1.) Extract contents to a location

2.) Run the program like normal (it's a console so can be ran as you wish)

3.) Enter the details required upon first start

4.) Minimize or close VM and let the bot announce releases

**LInux/Mac**
------------------------
1.) Extract contents to a location

2.) Give the file permission to execute by running good ole chmod 777 <file_name>
  
3.) Rnn the console app like a normal executable in CLI/Console by running ./<file_name>

4.) Enter the details required upon first start

5.) Minimize or close VM and let the bot announce releases

# To-Do
- [x] - Enhance the simple webhook message text to include the URL to the correct changelog
- [ ] - Add option to send fancy markdown message with UI block or whatever it's called on Discord
- [ ] - FInalize the loop break code to adjust/change settings when the program is running/idle
- [ ] - Add timeout/loop delay setting for application

## Libraries Used
Newtonsoft JSON.NET - https://www.newtonsoft.com/json | For the saving and reading of the programs settings
